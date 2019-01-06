using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine;
using Spine.Unity;

public class Lock
{
    public bool locked;

    public Lock()
    {
        locked = true;
    }
}

public static class CommonHandle
{
    public static float secPerFrame = 1.0f / 60;

    public static List<Lock> animationLocks = new List<Lock>(); // animation Lock
    public static List<Lock> castLocks = new List<Lock>(); // cast flying object Lock
    public static List<Lock> damageLocks = new List<Lock>(); // damage Lock

    public static int pageId;
    public static int skillId;

    public static void ResetLock()
    {
        animationLocks.Clear();
        castLocks.Clear();
        damageLocks.Clear();
    }

    public static bool CheckLock()
    {
        foreach (Lock v in animationLocks)
            if (v.locked)
                return true;
        foreach (Lock v in castLocks)
            if (v.locked)
                return true;
        foreach (Lock v in damageLocks)
            if (v.locked)
                return true;
        return false;
    }

    public static CharacterB SelectAim(bool aimIsEnemy)
    {
        int randomNum1 = UnityEngine.Random.Range(0, 10000) % 100 + 1;
        if (randomNum1 > 50)
            return CommonHandle.LeastHpAim(aimIsEnemy);
        else
            return CommonHandle.RandomAim(aimIsEnemy);
    }

    public static CharacterB LeastHpAim(bool aimIsEnemy)
    {
        CharacterB aimObj = null;
        float temp = 1.0f;
        foreach (CharacterB charB in Battle.instance.charList)
        {
            if (!charB.dead && charB.isEnemy == aimIsEnemy)
                if (charB.attriRuntime.tHp / charB.attriItems.tHp < temp)
                {
                    temp = charB.attriRuntime.tHp / charB.attriItems.tHp;
                    aimObj = charB;
                }
        }

        if (temp == 1.0f)
            return RandomAim(aimIsEnemy);
        else
            return aimObj;
    }

    public static CharacterB RandomAim(bool aimIsEnemy)
    {
        int randomNum1 = UnityEngine.Random.Range(0, 10000) % Battle.instance.charList.Count;
        while (Battle.instance.charList[randomNum1].isEnemy != aimIsEnemy || Battle.instance.charList[randomNum1].dead)
            randomNum1 = UnityEngine.Random.Range(0, 10000) % Battle.instance.charList.Count;
        return Battle.instance.charList[randomNum1];
    }

    public static IEnumerator Acting(int page, int skill)
    {
        CharacterB player = PlayerData.GetInst().charB;

        pageId = page;
        skillId = skill;

        // warrior branch 5 - 1
        if (PlayerData.GetInst().branchData[4] == 1)
            Battle.instance.StartCoroutine(AddBuff(new Buff(WarriorSkillHandle.warrior100Battle), player, player));

        switch (pageId)
        {
            case 1:
                if (GameSettings.language == Language.English)
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, "Attack");
                else
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, "普通攻击");
                yield return Battle.instance.StartCoroutine(AttackHandle.Acting());
                break;
            case 2:
                if (GameSettings.language == Language.English)
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, SkillInfoLoader.data[3 * ((pageId - 2) * 5 + skillId) + 1].name);
                else
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, SkillInfoLoader.data[3 * ((pageId - 2) * 5 + skillId) + 1].cnName);
                yield return Battle.instance.StartCoroutine(WarriorSkillHandle.Acting(skillId));
                break;
            case 3:
                if (GameSettings.language == Language.English)
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, SkillInfoLoader.data[3 * ((pageId - 2) * 5 + skillId) + 1].name);
                else
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, SkillInfoLoader.data[3 * ((pageId - 2) * 5 + skillId) + 1].cnName);
                yield return Battle.instance.StartCoroutine(MagicSkillHandle.Acting(skillId));
                break;
            case 4:
                if (GameSettings.language == Language.English)
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, SkillInfoLoader.data[3 * ((pageId - 2) * 5 + skillId) + 1].name);
                else
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, SkillInfoLoader.data[3 * ((pageId - 2) * 5 + skillId) + 1].cnName);
                yield return Battle.instance.StartCoroutine(ArcherSkillHandle.Acting(skillId));
                break;
            case 5:
                if (GameSettings.language == Language.English)
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, RuneHandle.runeSkillNameDic[skillId]);
                else
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, RuneHandle.runeSkillNameDicCN[skillId]);
                yield return Battle.instance.StartCoroutine(RuneHandle.Acting(skillId));
                break;
            case 6:
                if (GameSettings.language == Language.English)
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, "Attack");
                else
                    SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(PlayerData.GetInst().nickName, "普通攻击");
                yield return Battle.instance.StartCoroutine(AttackHandle.Acting());
                break;
        }

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    public static IEnumerator FlyingObjectCast(string objPath, Vector2 start, Vector2 end, float speed = 0.0f, float castDelayTime = 0.0f, float duration = -1.0f, CharacterB subjectC = null, CharacterB objectC = null, DamageEle damageEle = null, float damageDelayPercent = 1.00f, List<Buff> buffs = null)
    {
        yield return new WaitForSeconds(castDelayTime);

        if (damageEle != null || buffs != null)
        {
            float damageDelayTime = Vector2.Distance(end, start) / (speed * 50.0f) * damageDelayPercent;
            if (damageEle != null)
                Battle.instance.StartCoroutine(CalculateDamage(damageEle, subjectC, objectC, damageDelayTime));
            if (buffs != null)
                foreach (Buff buff in buffs)
                    Battle.instance.StartCoroutine(AddBuff(buff, subjectC, objectC, damageDelayTime));
        }

        GameObject resource = Resources.Load<GameObject>(objPath);
        GameObject flyingObj = GameObject.Instantiate(resource);
        flyingObj.transform.position = start;

        if(objPath.Contains("daleidianqiu"))
        {
            if(Battle.instance.thunderBall!=null)
                GameObject.Destroy(Battle.instance.thunderBall);

            Battle.instance.thunderBall = flyingObj;
        }

        if (objPath.Contains("BladeStorm"))
            Battle.instance.bladeStorm = flyingObj;

        if (start.x > end.x)
            flyingObj.transform.localScale = new Vector3(1, 1, 1);
        else
            flyingObj.transform.localScale = new Vector3(-1, 1, 1);

        // -1.0f means destroy when arrive its end, -2.0f means do not destroy automatically
        if (duration != -1.0f && duration != -2.0f)
            Battle.instance.StartCoroutine(DelayedDestroy(flyingObj, duration));

        Lock castLock = new Lock();
        castLocks.Add(castLock);
        yield return Battle.instance.StartCoroutine(MoveTo(flyingObj, end, speed));
        castLock.locked = false;

        if (duration == -1.0f)
            GameObject.Destroy(flyingObj);
    }

    public static IEnumerator ThunderSkrikeCast(Vector2 start, Vector2 end, float castDelayTime = 0.0f, float duration = 1.0f)
    {
        yield return new WaitForSeconds(castDelayTime);

        GameObject strikeResource = Resources.Load<GameObject>("MyResource/FlyingObject/shandianlian");
        GameObject strike = GameObject.Instantiate(strikeResource);

        Vector3[] vectors = new Vector3[2];
        vectors[0] = new Vector3(start.x, start.y, 0.0f);
        vectors[1] = new Vector3(end.x, end.y, 0.0f);
        strike.GetComponent<LineRenderer>().SetPositions(vectors);

        yield return new WaitForSeconds(duration);
        GameObject.Destroy(strike);
    }

    public static IEnumerator MoveTo(GameObject obj, Vector2 destiny, float speed)
    {
        while ((Vector2)obj.transform.position != destiny)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, destiny, speed);
            yield return new WaitForFixedUpdate(); // fixed moving speed
        }
    }

    public static IEnumerator CalculateDamage(DamageEle damageEle, CharacterB subjectC, CharacterB objectC, float delayTime = 0.0f)
    {
        CharacterB player = PlayerData.GetInst().charB;

        Lock damageLock = new Lock();
        damageLocks.Add(damageLock);
        yield return new WaitForSeconds(delayTime);
        damageLock.locked = false;

        float damage = 0.0f;
        damage += damageEle.atkBonus * subjectC.attriRuntime.tAtk;
        damage *= (1 + subjectC.attriRuntime.pDmg / 100.0f);
        damage = LuckyCal(damage, subjectC.attriRuntime.lucky);

        if (Crit(subjectC.attriRuntime.crit))
            damage *= subjectC.attriRuntime.cdmg / 100.0f;

        damage *= (1 - Attributes.DefCurve(objectC.attriRuntime.tDef * (1 - subjectC.attriRuntime.armorPntr / 100.0f)) / 100.0f);
        damage *= (1 - objectC.attriRuntime.rpDmgde / 100.0f);

        if (PlayerData.GetInst().branchData[9] == 2 && ((pageId == 3 && (skillId == 0 || skillId == 4)) || (pageId == 1 && GetPlayerWeaponType() == WeaponType.magic)))
        {
            if(subjectC == player)
            {
                objectC.attriRuntime.tHp -= damage * 0.7f;

                Buff curse = new Buff(MagicSkillHandle.curse);
                curse.buffData[1] = ((int)(-damage * 0.5f)).ToString();
                curse.describe = "Cursed, receive " + curse.buffData[1] + " damage per round.";
                curse.cnDescribe = "被诅咒，每回合受到 " + curse.buffData[1] + " 点伤害";
                Battle.instance.StartCoroutine(AddBuff(curse, subjectC, objectC));
            }
        }
        else
            objectC.attriRuntime.tHp -= damage;

        // Boss 4 Special Skill
        if (subjectC.name.Contains("Conjurer"))
        {
            if (subjectC.GetBuff("Blood Accumulation") == null)
            {
                Buff temp = new Buff(BossAIHandle.bloodAcc);
                temp.buffData[0] = ((int)(damage * 0.5f)).ToString();
                temp.describe = "Blood is Accumulating. Current : " + temp.buffData[0] + ".";
                temp.cnDescribe = "鲜血正在积攒，当前值 : " + temp.buffData[0] + ".";
                Battle.instance.StartCoroutine(AddBuff(temp, subjectC, subjectC));
            }
            else
            {
                subjectC.GetBuff("Blood Accumulation").buffData[0] = (Convert.ToInt32(subjectC.GetBuff("Blood Accumulation").buffData[0]) + (int)(damage * 0.5f)).ToString();
                subjectC.GetBuff("Blood Accumulation").describe = "Blood is Accumulating. Current : " + subjectC.GetBuff("Blood Accumulation").buffData[0] + ".";
                subjectC.GetBuff("Blood Accumulation").cnDescribe = "鲜血正在积攒，当前值 : " + subjectC.GetBuff("Blood Accumulation").buffData[0] + ".";
            }

            subjectC.UpdateBuff();
        }

        // warrior branch 4 - 2
        if (PlayerData.GetInst().branchData[3] == 2)
            if (objectC == player)
            {
                float recoverMp = damage * 0.35f;
                player.attriRuntime.tMp += recoverMp;
                SingletonObject<FlywordMedia>.GetInst().AddFlyWord(player.battleObj.transform.Find("Center").position, (int)recoverMp, FlyWordType.magic);
            }

        // warrior branch 4 - 1
        if (PlayerData.GetInst().branchData[3] == 1)
            if (objectC == player)
            {
                int randomNum = UnityEngine.Random.Range(0, 10000) % 100 + 1;
                if (randomNum <= 30)
                {
                    Buff numb = new Buff(WarriorSkillHandle.warriorNumb);
                    Battle.instance.StartCoroutine(AddBuff(numb, objectC, subjectC));
                }
            }

        // warrior branch 2 - 1
        if (PlayerData.GetInst().branchData[1] == 1)
            if (objectC == player)
            {
                int randomNum = UnityEngine.Random.Range(0, 10000) % 100 + 1;
                if (randomNum <= 20)
                {
                    Buff harden = WarriorSkillHandle.warriorHarden;
                    Battle.instance.StartCoroutine(AddBuff(harden, objectC, objectC));
                }
            }

        // warrior fury
        if (ItemManager.GetInst().equippedData[0] != null && ItemManager.GetInst().equippedData[0].weaponType == WeaponType.melee)
            if (objectC == player)
                Battle.instance.StartCoroutine(AddBuff(new Buff(WarriorSkillHandle.warriorFury), objectC, objectC));

        FlyWordType fwt = FlyWordType.normal;
        if (damageEle.dmgType == DamageType.Physical)
            fwt = FlyWordType.critical;
        else if (damageEle.dmgType == DamageType.Magical)
            fwt = FlyWordType.magic;
        SingletonObject<FlywordMedia>.GetInst().AddFlyWord(objectC.battleObj.transform.Find("Center").position, (int)damage, fwt);

        // attack heal
        if (subjectC.attriRuntime.lifeSteal > 0)
        {
            float atkH = damage * subjectC.attriRuntime.lifeSteal / 100.0f;
            subjectC.attriRuntime.tHp = Mathf.Min(subjectC.attriItems.tHp, subjectC.attriRuntime.tHp + atkH);
            SingletonObject<FlywordMedia>.GetInst().AddFlyWord(subjectC.battleObj.transform.Find("Center").position, (int)atkH, FlyWordType.heal);
        }

        // reflect damage
        if (objectC.attriRuntime.reflect > 0)
        {
            float reflectDmg = damage * objectC.attriRuntime.reflect / 100.0f;
            subjectC.attriRuntime.tHp -= reflectDmg;
            // avoid death causing by damage reflect
            if (subjectC.attriRuntime.tHp == 0)
                subjectC.attriRuntime.tHp = 1;
            SingletonObject<FlywordMedia>.GetInst().AddFlyWord(subjectC.battleObj.transform.Find("Center").position, (int)reflectDmg, FlyWordType.normal);
        }

        if (objectC == player)
        {
            // warrior skill5
            if (player.GetBuff("Warrior Immortal") != null)
                player.attriRuntime.tHp = Mathf.Max(1, player.attriRuntime.tHp);
            // warrior branch 5 - 2
            if (PlayerData.GetInst().branchData[4] == 2)
                if (player.attriRuntime.tHp / player.attriItems.tHp < 0.99f)
                {
                    Buff damageUp = new Buff(WarriorSkillHandle.warriorImpasse);
                    int damageUpNum = (int)((1.0f - (player.attriRuntime.tHp / player.attriItems.tHp)) / 2.0f * 100.0f);
                    damageUp.describe = "Increase Damage by " + damageUpNum.ToString() + "%.";
                    damageUp.cnDescribe = "伤害增加 " + damageUpNum.ToString() + "%";
                    damageUp.buffData[1] = damageUpNum.ToString();
                    Battle.instance.StartCoroutine(AddBuff(damageUp, player, player));
                }
        }

        objectC.UpdateHp();
    }

    public static IEnumerator DeathDestroy(CharacterB obj, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        obj.DeathCloseUI();
        obj.battleObj.SetActive(false);
        obj.BattleEndClean();
    }

    public static IEnumerator AddBuff(Buff buff, CharacterB subjectC, CharacterB objectC, float delayTime = 0.0f)
    {
        if (delayTime != 0.0f)
            yield return new WaitForSeconds(delayTime);

        bool control = false;

        Buff newBuff = new Buff();

        if (buff.toself) // to itself
        {
            // check
            foreach (Buff bf in subjectC.buffList)
            {
                if (bf.name == buff.name)
                {
                    // Hit
                    control = true;
                    newBuff = bf;
                    break;
                }
            }

            // load
            if (!control)
                newBuff = new Buff(buff);

            // link
            newBuff.subjectC = subjectC;
            newBuff.objectC = subjectC;

            if (!control)
                subjectC.buffList.Add(newBuff);
            else
            {
                if (newBuff.addable)
                    newBuff.level = newBuff.level + buff.level <= newBuff.maxLevel ? newBuff.level + buff.level : newBuff.maxLevel;

                newBuff.describe = buff.describe;
                newBuff.buffData = new List<string>(buff.buffData);
                newBuff.lastRounds = buff.totalRounds;
            }
        }
        else // to enemies
        {
            // check
            foreach (Buff bf in objectC.buffList)
            {
                if (bf.name == buff.name)
                {
                    // Hit
                    control = true;
                    newBuff = bf;
                    break;
                }
            }

            // load
            if (!control)
                newBuff = new Buff(buff);

            // link
            newBuff.subjectC = subjectC;
            newBuff.objectC = objectC;

            if (!control)
                objectC.buffList.Add(newBuff);
            else
            {
                if (newBuff.addable)
                    newBuff.level = newBuff.level + buff.level <= newBuff.maxLevel ? newBuff.level + buff.level : newBuff.maxLevel;

                newBuff.describe = buff.describe;
                newBuff.buffData = new List<string>(buff.buffData);
                newBuff.lastRounds = buff.totalRounds;
            }
        }

        // Update Buff Here
        newBuff.objectC.UpdateBuff();

        SyncBuffAttributes(objectC);
    }

    public static void SyncBuffAttributes(CharacterB obj)
    {
        obj.SyncRuntimeAttributes();

        foreach (Buff buff in obj.buffList)
            if (buff.type == BuffType.shuxing)
                for (int i = 0; i < buff.buffData.Count; i += 2)
                    switch (buff.buffData[i])
                    {
                        case "pAtk":
                            obj.attriRuntime.pAtk += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "pDef":
                            obj.attriRuntime.pDef += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "pSpd":
                            obj.attriRuntime.pSpd += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "lucky":
                            obj.attriRuntime.lucky += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "crit":
                            obj.attriRuntime.crit += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "cdmg":
                            obj.attriRuntime.cdmg += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "igDef":
                            obj.attriRuntime.armorPntr += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "pDmg":
                            obj.attriRuntime.pDmg += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "rpDmg":
                            obj.attriRuntime.rpDmgde += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "mpUsede":
                            obj.attriRuntime.mpUsede += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "reflect":
                            obj.attriRuntime.reflect += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                        case "atkHeal":
                            obj.attriRuntime.lifeSteal += (float)Convert.ToDouble(buff.buffData[i + 1]) * buff.level;
                            break;
                    }

        obj.attriRuntime.Calculate(true);
    }

    public static void CalculateBuff(CharacterB obj)
    {
        obj.dizzy = false;

        foreach (Buff buff in obj.buffList)
        {
            if (buff.timeOrTimes)
            {
                if (buff.type == BuffType.liushi)
                {
                    switch (buff.buffData[0])
                    {
                        case "Hp":
                            {
                                obj.attriRuntime.tHp += (float)Convert.ToDouble(buff.buffData[1]);

                                // avoid death causing by buffs
                                if (obj.attriRuntime.tHp == 0)
                                    obj.attriRuntime.tHp = 1;

                                FlyWordType fwt = (float)Convert.ToDouble(buff.buffData[1]) > 0 ? FlyWordType.heal : FlyWordType.normal;
                                SingletonObject<FlywordMedia>.GetInst().AddFlyWord(obj.battleObj.transform.Find("Center").position, Mathf.Abs((int)Convert.ToDouble(buff.buffData[1])), fwt);
                            }
                            break;
                    }
                }
                else if (buff.type == BuffType.yunxuan)
                {
                    float temp1 = (float)Convert.ToDouble(buff.buffData[1]);
                    float temp2 = UnityEngine.Random.Range(0, 10000) % 100 + 1;

                    if (temp2 <= temp1)
                        obj.dizzy = true;
                }

                if (!buff.infinity)
                    buff.lastRounds--;
            }
        }

        obj.UpdateBuff();
    }

    public static IEnumerator ChangeAnimation(CharacterB obj, string animation)
    {
        if (!animation.Contains("run") && !animation.Contains("idle"))
        {
            obj.battleObj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, animation, false);
            Lock animationLock = new Lock();
            animationLocks.Add(animationLock);
            yield return new WaitForSeconds(GetAnimationDuration(obj.battleObj.GetComponentInChildren<SkeletonAnimation>(), animation));
            animationLock.locked = false;
        }
        else
            obj.battleObj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, animation, true);
    }

    public static float GetAnimationDuration(SkeletonAnimation skeletonAnimation, string animationName)
    {
        foreach (Spine.Animation p in skeletonAnimation.skeleton.data.Animations)
            if (p.Name == animationName)
                return p.Duration;
        return 0;
    }

    public static void ChangeWeapon(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.melee:
                SpineController.SetDefaultSpine(PlayerData.GetInst().charB.battleObj, WeaponType.melee);
                break;
            case WeaponType.magic:
                SpineController.SetDefaultSpine(PlayerData.GetInst().charB.battleObj, WeaponType.magic);
                break;
            case WeaponType.range:
                SpineController.SetDefaultSpine(PlayerData.GetInst().charB.battleObj, WeaponType.range);
                break;
        }
    }

    public static void RefreshSpine()
    {
        if (PlayerData.GetInst().charB.battleObj != null)
            SpineController.RefreshSpine(PlayerData.GetInst().charB.battleObj);
        else if (MyPlayer.instance != null)
            SpineController.RefreshSpine(MyPlayer.instance.gameObject);
    }

    public static void PlayerSetIdle(GameObject player = null)
    {
        if (player == null)
            player = PlayerData.GetInst().charB.battleObj;

        if (ItemManager.GetInst().equippedData[0] == null || ItemManager.GetInst().equippedData[0].weaponType == WeaponType.melee)
            player.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "jian_idle", true);
        else if (ItemManager.GetInst().equippedData[0].weaponType == WeaponType.magic)
            player.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "fazhang_idle", true);
        else if (ItemManager.GetInst().equippedData[0].weaponType == WeaponType.range)
            player.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "gongjian_idle", true);
    }

    private static bool Crit(float crit)
    {
        int randomNum1 = UnityEngine.Random.Range(0, 10000) % 100 + 1;
        if (randomNum1 <= crit)
            return true;
        return false;
    }

    private static float LuckyCal(float damage, float lucky)
    {
        int randomNum1 = UnityEngine.Random.Range(0, 10000) % 40 + 81; // (81%-120%)
        return damage * randomNum1 / 100.0f;
    }

    private static IEnumerator DelayedDestroy(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(duration);

        GameObject.Destroy(obj);
    }

    private static WeaponType GetPlayerWeaponType()
    {
        if (ItemManager.GetInst().equippedData[0] != null)
            return ItemManager.GetInst().equippedData[0].weaponType;
        else
            return WeaponType.not_a_weapon;
    }
}