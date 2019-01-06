using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public static class AIHandle
{
    private static bool moveLock;
    private static int moveLockNum;

    public static IEnumerator StartAI(Monster obj)
    {
        // use AI to determine which skill to use
        int skillId = AILoader.data[obj.aiId].AIProcess(obj, Battle.instance.charList); // use AI
        // find the skill
        Skill output = SkillLoader.data[obj.skillList[skillId]];

        List<CharacterB> aims = new List<CharacterB>();
        if (output.isAoe)
            foreach (CharacterB charB in Battle.instance.charList)
                if (obj.isEnemy != charB.isEnemy && !charB.dead)
                    aims.Add(charB);
                else
                    continue;
        else
            aims.Add(CommonHandle.SelectAim((obj.isEnemy && output.toSelf) || (!obj.isEnemy && !output.toSelf)));

        // Action Hint
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, output.name);
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, output.cnName);
        // Acting
        yield return Acting(output, obj, aims);
        // Close Action Hint
        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
        // Clear Loding Info
        obj.actionLoading = 0.0f;
    }

    private static IEnumerator Acting(Skill skill, CharacterB subjectC, List<CharacterB> objectC)
    {
        moveLock = false;
        moveLockNum = skill.moveOrders.Count;

        Battle.instance.StartCoroutine(AnimationOrder(skill.animationOrders, subjectC));
        Battle.instance.StartCoroutine(MoveOrder(skill.moveOrders, subjectC, objectC[0]));
        foreach(CharacterB charB in objectC)
        {
            Battle.instance.StartCoroutine(FlyObjectOrder(skill.flyObjectOrders, subjectC, charB));
            Battle.instance.StartCoroutine(DamageOrder(skill.damageOrders, subjectC, charB));
            Battle.instance.StartCoroutine(BuffOrder(skill.buffOrders, subjectC, charB));
        }

        while (CommonHandle.CheckLock() || moveLockNum > 0)
        {
            yield return null;
        }

        if (subjectC.isEnemy) // x = 1 face right & x = -1 face left
            subjectC.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        else
            subjectC.battleObj.transform.localScale = new Vector3(1, 1, 1);
    }

    private static IEnumerator AnimationOrder(List<string> animationOrders, CharacterB obj)
    {
        foreach(string animation in animationOrders)
        {
            yield return CommonHandle.ChangeAnimation(obj, animation);

            if (animation.Contains("run"))
            {
                moveLock = true;
                while (moveLock)
                    yield return null;
            }
        }

        yield return CommonHandle.ChangeAnimation(obj, "idle");
    }

    private static IEnumerator MoveOrder(List<Vector2> moveOrders, CharacterB subjectC, CharacterB objectC)
    {
        foreach (Vector2 vector in moveOrders)
        {
            while (!moveLock)
                yield return null;

            Vector2 destiny = new Vector2(vector.x, vector.y);
            if (destiny == new Vector2(-1, -1)) // go to the aim position
                if (subjectC.isEnemy)
                    destiny = (Vector2)objectC.battleObj.transform.position + new Vector2(1.5f, 0);
                else
                    destiny = (Vector2)objectC.battleObj.transform.position + new Vector2(-1.5f, 0);
            else if (destiny == new Vector2(-2, -2)) // return back
                destiny = subjectC.returnPos;

            if (destiny.x > subjectC.battleObj.transform.position.x)
                subjectC.battleObj.transform.localScale = new Vector3(1, 1, 1);
            else
                subjectC.battleObj.transform.localScale = new Vector3(-1, 1, 1);

            yield return CommonHandle.MoveTo(subjectC.battleObj, destiny, 0.5f);
            moveLock = false;
            moveLockNum--;
        }
    }

    private static IEnumerator FlyObjectOrder(Dictionary<int, float> flyObjectOrders, CharacterB subjectC, CharacterB objectC)
    {
        float controlTime = Time.time;

        foreach (KeyValuePair<int, float> kvp in flyObjectOrders)
        {
            float castTime = kvp.Value * CommonHandle.secPerFrame;

            while (Time.time - controlTime < castTime)
            {
                if (moveLock)
                    castTime += Time.deltaTime;

                yield return null;
            }

            FlyingObjectCast(kvp.Key % 10000, subjectC, objectC);

            controlTime = Time.time;
        }
    }

    private static IEnumerator DamageOrder(Dictionary<DamageEle, float> damageOrders, CharacterB subjectC, CharacterB objectC)
    {
        float controlTime = Time.time;

        foreach (KeyValuePair<DamageEle, float> kvp in damageOrders)
        {
            float castTime = kvp.Value * CommonHandle.secPerFrame;

            while (Time.time - controlTime < castTime)
            {
                if (moveLock)
                    castTime += Time.deltaTime;

                yield return null;
            }

            Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(kvp.Key, subjectC, objectC));

            controlTime = Time.time;
        }
    }

    private static IEnumerator BuffOrder(Dictionary<int, float> buffOrders, CharacterB subjectC, CharacterB objectC)
    {
        float controlTime = Time.time;

        foreach (KeyValuePair<int, float> kvp in buffOrders)
        {
            float castTime = kvp.Value * CommonHandle.secPerFrame;

            while (Time.time - controlTime < castTime)
            {
                if (moveLock)
                    castTime += Time.deltaTime;

                yield return null;
            }

            Battle.instance.StartCoroutine(CommonHandle.AddBuff(BuffLoader.data[kvp.Key % 10000], subjectC, objectC));

            controlTime = Time.time;
        }
    }

    private static void FlyingObjectCast(int objId, CharacterB subjectC, CharacterB objectC)
    {
        FlyingObject obj = FlyingObjectLoader.data[objId];

        string path = obj.path;
        Vector2 start = obj.start;
        Vector2 end = obj.end;
        float speed = obj.moveSpeed;
        float castDelayTime = 0.0f;
        float duration = obj.duration;
        DamageEle damageEle = new DamageEle(obj.damageEle);
        float damageDelayPercent = 1.0f;
        List<Buff> buffList = new List<Buff>();
        foreach (int i in obj.addBuffID)
            buffList.Add(new Buff(BuffLoader.data[i]));

        if (start == new Vector2(-1, -1)) // from caster
            if (subjectC.isEnemy)
                start = (Vector2)subjectC.battleObj.transform.Find("Center").position + new Vector2(-1.0f, 0);
            else
                start = (Vector2)subjectC.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);

        if (start == new Vector2(-2, -2)) // directly on aim
            start = (Vector2)objectC.battleObj.transform.Find("Center").position;

        if (end == new Vector2(-1, -1)) // to aim
            end = (Vector2)objectC.battleObj.transform.Find("Center").position;

        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast(path, start, end, speed, castDelayTime, duration, subjectC, objectC, damageEle, damageDelayPercent, buffList));
    }
}