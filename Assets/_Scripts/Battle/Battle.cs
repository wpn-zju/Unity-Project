using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Spine;
using Spine.Unity;

public class Battle : MonoBehaviour
{
    public static Battle instance;

    [HideInInspector]
    public GameObject thunderBall = null;
    [HideInInspector]
    public GameObject bladeStorm = null;
    [HideInInspector]
    public bool autoFight = false;
    [HideInInspector]
    public List<CharacterB> charList = new List<CharacterB>();
    [HideInInspector]
    public bool inBattle = false;

    private bool acting = false;
    private int control; // Fight Control Var
    private int roundNum = 0;
    private Dictionary<CharacterB, int> readyDic = new Dictionary<CharacterB, int>();
    private List<Vector2> posList = new List<Vector2>()
    {
        new Vector2(10,2.5f),
        new Vector2(10,0.5f),
        new Vector2(10,-1.5f),
        new Vector2(-10,2.5f),
        new Vector2(-10,0.5f),
        new Vector2(-10,-1.5f),
    };

    private void Awake()
    {
        instance = this;
    }

    public void BattleInit(List<CharacterB> charList)
    {
        bool hasBoss = false;

        this.charList = charList;

        SingletonObject<HpMedia>.GetInst().Open();
        SingletonObject<MpMedia>.GetInst().Open();
        SingletonObject<SpeedMedia>.GetInst().Open();
        SingletonObject<BuffMedia>.GetInst().Open();
        SingletonObject<FlywordMedia>.GetInst().Open();

        int playerCount = 0;
        int enemyCount = 0;

        // set players and monsters in scene
        foreach (CharacterB c in this.charList)
        {
            if (c.charType == CharacterType.Boss)
                hasBoss = true;

            // Pumpkin Knight
            if (c.name == "Pumpkin Knight")
            {
                int count = 0;

                for (int i = 1036; i <= 1040; ++i)
                    if (PlayerData.GetInst().conditions[i])
                        count++;

                if (count != 0)
                {
                    Buff bossSick = new Buff(BossAIHandle.pkSick);
                    bossSick.level = count;
                    c.buffList.Add(bossSick);
                }
            }

            Vector2 temp = Vector2.zero;
            if (c.isEnemy)
            {
                switch (enemyCount)
                {
                    case 0:
                        temp = posList[1];
                        break;
                    case 1:
                        temp = posList[0];
                        break;
                    case 2:
                        temp = posList[2];
                        break;
                }
                GenerateChar(c, temp);
                enemyCount++;
            }
            else
            {
                switch (playerCount)
                {
                    case 0:
                        temp = posList[4];
                        break;
                    case 1:
                        temp = posList[3];
                        break;
                    case 2:
                        temp = posList[5];
                        break;
                }
                GenerateChar(c, temp);
                playerCount++;
            }

            CommonHandle.SyncBuffAttributes(c);
        }

        // archer branch 3 - 2
        if (PlayerData.GetInst().branchData[12] == 2)
            PlayerData.GetInst().charB.actionLoading = 100.0f;
        // warrior branch 3 - 1
        if (PlayerData.GetInst().branchData[2] == 1)
            StartCoroutine(CommonHandle.AddBuff(new Buff(WarriorSkillHandle.warriorArmorUp), PlayerData.GetInst().charB, PlayerData.GetInst().charB));

        CommonHandle.RefreshSpine();
        CommonHandle.PlayerSetIdle();

        autoFight = false;
        inBattle = true;
        acting = false;
        control = 0;
        roundNum = 0;

        // Battle Audio
        if (hasBoss)
            AudioManager.GetInst().PlayBGM(104);
        else
            AudioManager.GetInst().PlayBGM(103);

        StartCoroutine(BattleControl());
        StartCoroutine(CheckReadyQueue());
    }

    public int BattleEnd()
    {
        bool noEnemy = true;
        bool noPlayer = true;

        foreach (CharacterB p in charList)
            if (p.isEnemy && !p.dead)
                noEnemy = false;
            else if (!p.isEnemy && !p.dead)
                noPlayer = false;

        if (noEnemy) // Player wins
            control = 1;
        else if (noPlayer) // Player loses
            control = 2;
        else // Continue
            control = 0;
        return control;
    }

    private IEnumerator BattleControl()
    {
        while (control == 0)
            yield return null;

        inBattle = false; // stop update

        while (CommonHandle.CheckLock())
        {
            yield return null;
        }

        StopAllCoroutines(); // stop all battle coroutines

        readyDic.Clear();

        Time.timeScale = 1.0f;

        if (control == 1) // you win
            SingletonObject<BattleCompleteMedia>.GetInst().SetWorL(true);
        else if (control == 2) // you lose
            SingletonObject<BattleCompleteMedia>.GetInst().SetWorL(false);
        SingletonObject<BattleCompleteMedia>.GetInst().Open();

        PlayerData.GetInst().charB.BattleEndClean();
    }

    private IEnumerator CheckReadyQueue()
    {
        while (inBattle)
        {
            acting = false;

            while (readyDic.Count == 0)
                yield return new WaitForFixedUpdate();

            acting = true;

            readyDic = readyDic.OrderBy((i) => (i.Value)).ToDictionary(k => k.Key, e => e.Value);

            foreach (KeyValuePair<CharacterB, int> v in readyDic)
            {
                if (control != 0 || v.Key.dead)
                    continue;
                else
                {
                    if (GameSettings.language == Language.English)
                        SingletonObject<BattleUIMedia>.GetInst().SetTurnInfo(v.Key.name, v.Key.isEnemy);
                    else
                        SingletonObject<BattleUIMedia>.GetInst().SetTurnInfo(v.Key.cnName, v.Key.isEnemy);
                    yield return NewAction(v.Key);
                    SingletonObject<BattleUIMedia>.GetInst().CloseTurnInfo();
                }
            }

            readyDic.Clear();
        }
    }

    public IEnumerator NewAction(CharacterB obj)
    {
        // calculate buff here
        CommonHandle.CalculateBuff(obj);

        if (!obj.dizzy)
            if (obj.charType == CharacterType.Boss || obj.charType == CharacterType.Teammate) // Boss or Veall
                yield return BossAIHandle.StartBossAI((Monster)obj);
            else if (obj.charType != CharacterType.Player) // enemy's turn
                yield return AIHandle.StartAI((Monster)obj);
            else // player's turn
            {
                SingletonObject<BattleUIMedia>.GetInst().SetRoundText(++roundNum);
                SingletonObject<BattleUIMedia>.GetInst().autoFightButton.gameObject.SetActive(false);

                // check thunder ball
                if (obj.GetBuff("Thunder Ball") != null)
                    yield return Battle.instance.StartCoroutine(MagicSkillHandle.ThunderBall());

                // check Blade Storm
                if (Battle.instance.bladeStorm != null)
                {
                    GameObject.Destroy(Battle.instance.bladeStorm);
                    Battle.instance.bladeStorm = null;
                }

                if (!autoFight)
                    yield return SingletonObject<SelectMedia>.GetInst().StartPlayerAction(); // to player making decision
                else
                    yield return CommonHandle.Acting(1, 0);

                if (roundNum == 100 && control == 0)
                    control = 2; // more than 100 rounds, lost.

                SingletonObject<BattleUIMedia>.GetInst().autoFightButton.gameObject.SetActive(true);
            }
        else
            obj.actionLoading = 0.0f;

        obj.BuffDestory();
    }

    private void FixedUpdate()
    {
        if(inBattle)
        {
            if (!acting)
                foreach (CharacterB v in charList)
                {
                    v.actionLoading += v.attriRuntime.tSpd / 5.0f; // 2 seconds for a 5 speed character to prepare an action.
                    v.actionLoading = Mathf.Min(v.actionLoading, 100.0f);

                    if (v.actionLoading == 100.0f && !v.dead)
                        readyDic.Add(v, v.absRank);
                }

            foreach (CharacterB v in charList)
            {
                if (v.attriRuntime.tHp > 0)
                {
                    v.UpdateHp();
                    v.UpdateHpPosition();
                    v.UpdateMp();
                    v.UpdateMpPosition();
                    v.UpdateSpeed();
                    v.UpdateSpeedPosition();
                    v.UpdateBuffPosition();
                }
                else if(!CommonHandle.CheckLock())
                {
                    if (!v.dead)
                    {
                        if (v.isEnemy)
                        {
                            v.dead = true;

                            SkeletonAnimation temp = v.battleObj.GetComponentInChildren<SkeletonAnimation>();
                            temp.state.SetAnimation(0, "death", false);
                            Battle.instance.StartCoroutine(CommonHandle.DeathDestroy(v, CommonHandle.GetAnimationDuration(temp, "death")));

                            BattleEnd();
                        }
                        else
                        {
                            v.dead = true;

                            SkeletonAnimation temp = v.battleObj.GetComponentInChildren<SkeletonAnimation>();
                            temp.state.SetAnimation(0, "jian_death", false);
                            Battle.instance.StartCoroutine(CommonHandle.DeathDestroy(v, CommonHandle.GetAnimationDuration(temp, "jian_death")));

                            BattleEnd();
                        }
                    }
                }
            }
        }
    }

    private void GenerateChar(CharacterB charB, Vector2 pos)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>(charB.objPath), pos, Quaternion.Euler(Vector3.zero));

        charB.battleObj = temp;
        charB.absRank = ReturnPos2AbsRank(pos);
        charB.returnPos = pos;
        charB.dizzy = false;
        charB.dead = false;
        charB.actionLoading = 0.0f;
        charB.UpdateBuff();

        // if enemy, change the facing direction to another side
        if (charB.isEnemy)
            temp.transform.localScale = new Vector3(-1, 1, 1);
        else
            temp.transform.localScale = new Vector3(1, 1, 1);

        if (charB.name == "Veall")
        {
            SpineController.SetVeallSpine(temp);
            CommonHandle.PlayerSetIdle(temp);
        }
    }

    public CharacterB Obj2Char(GameObject obj)
    {
        foreach (CharacterB charB in charList)
            if (obj == charB.battleObj)
                return charB;
        return null;
    }

    private int ReturnPos2AbsRank(Vector2 returnPos)
    {
        if (returnPos.x == 10)
        {
            if (returnPos.y == 2.5f)
                return 5;
            else if (returnPos.y == 0.5f)
                return 4;
            else if (returnPos.y == -1.5f)
                return 6;
        }
        else if (returnPos.x == -10)
        {
            if (returnPos.y == 2.5f)
                return 2;
            else if (returnPos.y == 0.5f)
                return 1;
            else if (returnPos.y == -1.5f)
                return 3;
        }

        return 0;
    }
}