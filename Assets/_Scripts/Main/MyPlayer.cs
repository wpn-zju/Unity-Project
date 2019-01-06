using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    public static MyPlayer instance;
    public Rigidbody2D rb;
    public StateManager stateManager;
    public bool cameraLock = false;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        stateManager = new StateManager(gameObject);
        cameraLock = false;
    }

    public void Run(Vector2 input)
    {
        rb.velocity = input;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.x != 0)
            transform.localScale = rb.velocity.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

        stateManager.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            SingletonObject<FightMedia>.GetInst().cantMove = true;
            string[] temp = collision.name.Split('-');
            Main.instance.StartCoroutine(Main.instance.EnterScene(temp[0], Convert.ToInt32(temp[1])));
        }
        else if(collision.tag == "SceneTrigger")
        {
            List<int> triggerList = new List<int>();
            triggerList.Add(Convert.ToInt32(collision.name));
            PlayerData.GetInst().conditions[Convert.ToInt32(collision.name)] = true;
            TriggerManager.GetInst().EnableTriggers(triggerList);
        }
        else if (collision.tag == "Enemy")
        {
            StartBattle(collision.transform.parent.GetComponent<Patrol>().id, collision.transform.parent.gameObject.name);
        }
    }

    public void StartBattle(int monsterId, string monsterName)
    {
        SingletonObject<FightMedia>.GetInst().cantMove = true;
        SingletonObject<FightMedia>.GetInst().Update();

        List<CharacterB> tempCharList = new List<CharacterB>();
        tempCharList.Add(PlayerData.GetInst().charB);

        // 竞技场指挥官的守护
        if (Main.instance.sceneManager.sceneName == "Arena")
            PlayerData.GetInst().charB.buffList.Add(new Buff(WarriorSkillHandle.playerArenaTough));

        // 作弊码队友
        if (PlayerData.GetInst().conditions[1100])
        {
            Monster veall = new Monster((Monster)CharLoader.dataTotal[200]);
            veall.isEnemy = false;
            tempCharList.Add(veall);
        }

        // 维尔加入战斗
        if (PlayerData.GetInst().conditions[1034] && !PlayerData.GetInst().conditions[1041])
        {
            Monster realVeall = new Monster((Monster)CharLoader.dataTotal[200]);
            realVeall.isEnemy = false;
            tempCharList.Add(realVeall);
        }

        int randomNum1 = UnityEngine.Random.Range(0, 10000) % 8 + 1; // the number of selected buff
        Monster monster1 = new Monster((Monster)CharLoader.dataTotal[monsterId]);
        Buff battleBuff = new Buff(BuffLoader.data[randomNum1]);
        monster1.buffList.Add(battleBuff);
        tempCharList.Add(monster1);

        // TEMP CODE
        int randomNum2 = UnityEngine.Random.Range(0, 10000) % 6 + 1; // the number of monsters
        if (randomNum2 == 1) randomNum2 = 3;
        else if (randomNum2 == 2 || randomNum2 == 3) randomNum2 = 2;
        else randomNum2 = 1;

        if (goblinNormalList.Contains(monsterId) || goblinEliteList.Contains(monsterId))
        {
            for (int i = 1; i < randomNum2; ++i)
            {
                int randomNum3 = UnityEngine.Random.Range(0, 10000) % goblinNormalList.Count;
                int randomNum4 = UnityEngine.Random.Range(0, 10000) % 8 + 1;
                Monster monsterX = new Monster((Monster)CharLoader.dataTotal[goblinNormalList[randomNum3]]);
                Buff battleBuffX = new Buff(BuffLoader.data[randomNum4]);
                monsterX.buffList.Add(battleBuffX);
                tempCharList.Add(monsterX);
            }
        }
        else if (ghostNormalList.Contains(monsterId) || ghostEliteList.Contains(monsterId))
        {
            for (int i = 1; i < randomNum2; ++i)
            {
                int randomNum3 = UnityEngine.Random.Range(0, 10000) % ghostNormalList.Count;
                int randomNum4 = UnityEngine.Random.Range(0, 10000) % 8 + 1;
                Monster monsterX = new Monster((Monster)CharLoader.dataTotal[ghostNormalList[randomNum3]]);
                Buff battleBuffX = new Buff(BuffLoader.data[randomNum4]);
                monsterX.buffList.Add(battleBuffX);
                tempCharList.Add(monsterX);
            }
        }
        else if (zombieNormalList.Contains(monsterId) || zombieEliteList.Contains(monsterId))
        {
            for (int i = 1; i < randomNum2; ++i)
            {
                int randomNum3 = UnityEngine.Random.Range(0, 10000) % zombieNormalList.Count;
                int randomNum4 = UnityEngine.Random.Range(0, 10000) % 8 + 1;
                Monster monsterX = new Monster((Monster)CharLoader.dataTotal[zombieNormalList[randomNum3]]);
                Buff battleBuffX = new Buff(BuffLoader.data[randomNum4]);
                monsterX.buffList.Add(battleBuffX);
                tempCharList.Add(monsterX);
            }
        }
        else if (skeletonNormalList.Contains(monsterId) || skeletonEliteList.Contains(monsterId))
        {
            for (int i = 1; i < randomNum2; ++i)
            {
                int randomNum3 = UnityEngine.Random.Range(0, 10000) % skeletonNormalList.Count;
                int randomNum4 = UnityEngine.Random.Range(0, 10000) % 8 + 1;
                Monster monsterX = new Monster((Monster)CharLoader.dataTotal[skeletonNormalList[randomNum3]]);
                Buff battleBuffX = new Buff(BuffLoader.data[randomNum4]);
                monsterX.buffList.Add(battleBuffX);
                tempCharList.Add(monsterX);
            }
        }
        else if (lizardNormalList.Contains(monsterId) || lizardEliteList.Contains(monsterId))
        {
            for (int i = 1; i < randomNum2; ++i)
            {
                int randomNum3 = UnityEngine.Random.Range(0, 10000) % lizardNormalList.Count;
                int randomNum4 = UnityEngine.Random.Range(0, 10000) % 8 + 1;
                Monster monsterX = new Monster((Monster)CharLoader.dataTotal[lizardNormalList[randomNum3]]);
                Buff battleBuffX = new Buff(BuffLoader.data[randomNum4]);
                monsterX.buffList.Add(battleBuffX);
                tempCharList.Add(monsterX);
            }
        }
        else if (mouseNormalList.Contains(monsterId))
        {
            for (int i = 1; i < randomNum2; ++i)
            {
                int randomNum3 = UnityEngine.Random.Range(0, 10000) % mouseNormalList.Count;
                int randomNum4 = UnityEngine.Random.Range(0, 10000) % 8 + 1;
                Monster monsterX = new Monster((Monster)CharLoader.dataTotal[mouseNormalList[randomNum3]]);
                Buff battleBuffX = new Buff(BuffLoader.data[randomNum4]);
                monsterX.buffList.Add(battleBuffX);
                tempCharList.Add(monsterX);
            }
        }

        Main.instance.sceneManager.monsterId = monsterId;
        Main.instance.sceneManager.monsterName = monsterName;
        Main.instance.StartCoroutine(Main.instance.EnterBattle(tempCharList));
    }

    private List<int> goblinNormalList = new List<int> { 4, 5, 6, 7, 8, 10 };
    private List<int> goblinEliteList = new List<int> { 12, 13, 14 };
    private List<int> ghostNormalList = new List<int> { 1, 2 };
    private List<int> ghostEliteList = new List<int> { 3 };
    private List<int> zombieNormalList = new List<int> { 27 };
    private List<int> zombieEliteList = new List<int> { 28 };
    private List<int> skeletonNormalList = new List<int> { 15, 16, 17, 18 };
    private List<int> skeletonEliteList = new List<int> { 19, 20 };
    private List<int> lizardNormalList = new List<int> { 21, 22 };
    private List<int> lizardEliteList = new List<int> { 23 };
    private List<int> mouseNormalList = new List<int> { 24, 25, 26 };
}
