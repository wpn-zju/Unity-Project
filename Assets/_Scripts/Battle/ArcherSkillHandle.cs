using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArcherSkillHandle
{
    public static Buff arrowGotShot = new Buff();
    private static Buff arrowSlowDown = new Buff();
    private static Buff arrowTired = new Buff();
    private static Buff arrowPoison = new Buff();
    private static Buff arrowSunderArmor = new Buff();
    private static Buff arrowIce = new Buff();
    private static Buff arrowBurn = new Buff();
    private static Buff arrowNumb = new Buff();

    public static void Init()
    {
        arrowGotShot.name = "Arrow Got Shot";
        arrowGotShot.cnName = "中箭";
        arrowGotShot.describe = "Hurt by arrow.";
        arrowGotShot.cnDescribe = "中箭";
        arrowGotShot.iconPath = "Rune_LifeSteal";
        arrowGotShot.infinity = true;
        arrowGotShot.addable = true;
        arrowGotShot.timeOrTimes = false;
        arrowGotShot.lastRounds = 0;
        arrowGotShot.totalRounds = 0;
        arrowGotShot.level = 1;
        arrowGotShot.maxLevel = 100;
        arrowGotShot.type = BuffType.biaoji;
        arrowGotShot.buffData = new List<string>();
        arrowGotShot.toself = false;

        arrowSlowDown.name = "Arrow Slow Down";
        arrowSlowDown.cnName = "中箭过多";
        arrowSlowDown.describe = "Hurt by arrow, slowed down by 30%.";
        arrowSlowDown.cnDescribe = "中箭过多，速度降低30%";
        arrowSlowDown.iconPath = "Rune_Frozen";
        arrowSlowDown.infinity = false;
        arrowSlowDown.addable = false;
        arrowSlowDown.timeOrTimes = true;
        arrowSlowDown.lastRounds = 2;
        arrowSlowDown.totalRounds = 2;
        arrowSlowDown.level = 1;
        arrowSlowDown.maxLevel = 1;
        arrowSlowDown.type = BuffType.shuxing;
        arrowSlowDown.buffData = new List<string>();
        arrowSlowDown.buffData.Add("pSpd");
        arrowSlowDown.buffData.Add("-30");
        arrowSlowDown.toself = false;

        arrowTired.name = "Arrow Tired";
        arrowTired.cnName = "疲惫";
        arrowTired.describe = "Cast death arrow, you are tired now, slowed down by 50%.";
        arrowTired.cnDescribe = "进行了一次蓄力攻击，很疲惫，速度降低50%";
        arrowTired.iconPath = "Rune_ManaShell";
        arrowTired.infinity = false;
        arrowTired.addable = false;
        arrowTired.timeOrTimes = true;
        arrowTired.lastRounds = 1;
        arrowTired.totalRounds = 1;
        arrowTired.level = 1;
        arrowTired.maxLevel = 1;
        arrowTired.type = BuffType.shuxing;
        arrowTired.buffData = new List<string>();
        arrowTired.toself = false;
        arrowTired.buffData.Add("pSpd");
        arrowTired.buffData.Add("-50");

        arrowPoison.name = "Arrow Poison";
        arrowPoison.cnName = "毒箭";
        arrowPoison.describe = "Hurt by poisoned arrow, receive damage per round.";
        arrowPoison.cnDescribe = "被毒箭击中，每回合损失一定生命值";
        arrowPoison.iconPath = "Rune_Poison";
        arrowPoison.infinity = false;
        arrowPoison.addable = false;
        arrowPoison.timeOrTimes = true;
        arrowPoison.lastRounds = 2;
        arrowPoison.totalRounds = 2;
        arrowPoison.level = 1;
        arrowPoison.maxLevel = 1;
        arrowPoison.type = BuffType.liushi;
        arrowPoison.buffData = new List<string>();
        arrowPoison.buffData.Add("Hp");
        arrowPoison.buffData.Add("0");
        arrowPoison.toself = false;

        arrowSunderArmor.name = "Arrow Sunder Armor";
        arrowSunderArmor.cnName = "破甲箭";
        arrowSunderArmor.describe = "Hurt by penetrating arrow, defense decreased by 30%";
        arrowSunderArmor.cnDescribe = "被破甲箭命中，防御力降低30%";
        arrowSunderArmor.iconPath = "Rune_Critical";
        arrowSunderArmor.infinity = false;
        arrowSunderArmor.addable = false;
        arrowSunderArmor.timeOrTimes = true;
        arrowSunderArmor.lastRounds = 2;
        arrowSunderArmor.totalRounds = 2;
        arrowSunderArmor.level = 1;
        arrowSunderArmor.maxLevel = 1;
        arrowSunderArmor.type = BuffType.shuxing;
        arrowSunderArmor.buffData = new List<string>();
        arrowSunderArmor.buffData.Add("pDef");
        arrowSunderArmor.buffData.Add("-30");
        arrowSunderArmor.toself = false;

        arrowIce.name = "Arrow Ice";
        arrowIce.cnName = "寒冰箭";
        arrowIce.describe = "Hurt by ice arrow, speed decreased by 30%.";
        arrowIce.cnDescribe = "被寒冰箭击中，速度降低30%.";
        arrowIce.iconPath = "Rune_Frozen";
        arrowIce.infinity = false;
        arrowIce.addable = false;
        arrowIce.timeOrTimes = true;
        arrowIce.lastRounds = 2;
        arrowIce.totalRounds = 2;
        arrowIce.level = 1;
        arrowIce.maxLevel = 1;
        arrowIce.type = BuffType.shuxing;
        arrowIce.buffData = new List<string>();
        arrowIce.buffData.Add("pSpd");
        arrowIce.buffData.Add("-30");
        arrowIce.toself = false;

        arrowBurn.name = "Arrow Burn";
        arrowBurn.cnName = "燃烧箭";
        arrowBurn.describe = "Hurt by burning arrow, receive damage increased by 30%.";
        arrowBurn.cnDescribe = "被燃烧箭命中，受到的伤害提升30%";
        arrowBurn.iconPath = "Rune_Burn";
        arrowBurn.infinity = false;
        arrowBurn.addable = false;
        arrowBurn.timeOrTimes = true;
        arrowBurn.lastRounds = 2;
        arrowBurn.totalRounds = 2;
        arrowBurn.level = 1;
        arrowBurn.maxLevel = 1;
        arrowBurn.type = BuffType.shuxing;
        arrowBurn.buffData = new List<string>();
        arrowBurn.buffData.Add("rpDmg");
        arrowBurn.buffData.Add("-30");
        arrowBurn.toself = false;

        arrowNumb.name = "Arrow Numb";
        arrowNumb.cnName = "麻痹箭";
        arrowNumb.describe = "Numb for 1 round.";
        arrowNumb.cnDescribe = "被麻痹箭集中，晕眩1回合";
        arrowNumb.iconPath = "Rune_Numb";
        arrowNumb.infinity = false;
        arrowNumb.addable = false;
        arrowNumb.timeOrTimes = true;
        arrowNumb.lastRounds = 1;
        arrowNumb.totalRounds = 1;
        arrowNumb.level = 1;
        arrowNumb.maxLevel = 1;
        arrowNumb.type = BuffType.yunxuan;
        arrowNumb.buffData = new List<string>();
        arrowNumb.buffData.Add("Dizzy");
        arrowNumb.buffData.Add("100");
        arrowNumb.toself = false;
    }

    public static bool IsAoe(int skillId)
    {
        switch (skillId)
        {
            case 0:
                return true;
            case 1:
                return false;
            case 2:
                return true;
            case 3:
                return false;
            case 4:
                return false;
            default:
                return false;
        }
    }

    public static bool ToSelf(int skillId)
    {
        switch (skillId)
        {
            case 0:
                return false;
            case 1:
                return false;
            case 2:
                return false;
            case 3:
                return false;
            case 4:
                return false;
            default:
                return false;
        }
    }

    public static bool Check(int skillId)
    {
        CharacterB player = PlayerData.GetInst().charB;

        if (PlayerData.GetInst().branchData[14] == 2)
            player.attriRuntime.mpUsede += 20.0f;

        bool couldUse = false;

        switch (skillId)
        {
            case 0:
                couldUse = player.attriRuntime.tMp > 70 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
            case 1:
                couldUse = player.attriRuntime.tMp > 100 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
            case 2:
                couldUse = true;
                break;
            case 3:
                couldUse = player.attriRuntime.tMp > 120 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
            case 4:
                couldUse = player.attriRuntime.tMp > 100 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
        }

        if (!couldUse)
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("You don't have enough mana point.", Color.red);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("法力值不足", Color.red);

        if (skillId ==2 && !CheckArrowBuff())
        {
            couldUse = false;
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("No enemy have arrow hurt buff/note, can't use this skill.", Color.yellow);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("请先对敌人产生中箭效果", Color.yellow);
        }

        if (PlayerData.GetInst().branchData[14] == 2)
            player.attriRuntime.mpUsede -= 20.0f;

        return couldUse;
    }

    public static IEnumerator Acting(int skillId)
    {
        CharacterB player = PlayerData.GetInst().charB;

        CommonHandle.pageId = 4;
        CommonHandle.skillId = skillId;

        if (PlayerData.GetInst().branchData[10] == 2)
            player.attriRuntime.lifeSteal += 10.0f;

        if (PlayerData.GetInst().branchData[14] == 2)
            player.attriRuntime.pDmg += 20.0f;

        if (PlayerData.GetInst().branchData[14] == 2)
            player.attriRuntime.mpUsede += 20.0f;

        CommonHandle.ResetLock();

        switch (skillId)
        {
            case 0:
                player.attriRuntime.tMp -= 70 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting1();
                break;
            case 1:
                player.attriRuntime.tMp -= 100 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting2();
                break;
            case 2:
                yield return Acting3();
                break;
            case 3:
                player.attriRuntime.tMp -= 120 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting4();
                break;
            case 4:
                player.attriRuntime.tMp -= 100 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting5();
                break;
        }

        if (PlayerData.GetInst().branchData[10] == 2)
            player.attriRuntime.lifeSteal -= 10.0f;

        if (PlayerData.GetInst().branchData[14] == 2)
            player.attriRuntime.pDmg -= 20.0f;

        if (PlayerData.GetInst().branchData[14] == 2)
            player.attriRuntime.mpUsede -= 20.0f;

        if (PlayerData.GetInst().branchData[11] == 2)
            player.actionLoading = 20.0f;
        else
            player.actionLoading = 0.0f;
    }

    private static IEnumerator Acting1()
    {
        CharacterB player = PlayerData.GetInst().charB;
        List<CharacterB> aims = new List<CharacterB>();
        foreach(CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy && !charB.dead)
                aims.Add(charB);

        CommonHandle.ChangeWeapon(WeaponType.range);
        Vector2 arrowStart = (Vector2)player.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);
        List<Vector2> arrowEnds = new List<Vector2>();
        foreach (CharacterB charB in aims)
            arrowEnds.Add(charB.battleObj.transform.Find("Center").position);
        float arrowSpeed = 0.5f;
        float castTime = 30.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = 0.5f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Physical);

        for (int i = 0; i < aims.Count; ++i)
        {
            List<Buff> buffList = new List<Buff>();
            buffList.Add(new Buff(arrowGotShot));
            MasterArcher(ref buffList);

            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/gongjian", arrowStart, arrowEnds[i], arrowSpeed, castTime, -1.0f, player, aims[i], dmgEle, damageDelayPercent, buffList));
        }

        yield return CommonHandle.ChangeAnimation(player, "gongjian_attack");

        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action
        yield return new WaitForSeconds(1.0f);
    }

    private static IEnumerator Acting2()
    {
        int arrowNum = UnityEngine.Random.Range(0, 10000) % 3 + 3; // from 3 to 5

        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = SingletonObject<SelectMedia>.GetInst().aimObj;

        CommonHandle.ChangeWeapon(WeaponType.range);
        Vector2 arrowStart = (Vector2)player.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);
        Vector2 arrowEnd = (Vector2)aim.battleObj.transform.Find("Center").position;
        float arrowSpeed = 0.5f;
        float castTime = 30.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = 0.3f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Physical);

        for (int i = 0; i < arrowNum; ++i)
        {
            List<Buff> buffList = new List<Buff>();
            buffList.Add(new Buff(arrowGotShot));
            MasterArcher(ref buffList);

            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/gongjian", arrowStart, arrowEnd, arrowSpeed, castTime, -1.0f, player, aim, dmgEle, damageDelayPercent, buffList));
            yield return CommonHandle.ChangeAnimation(player, "gongjian_attack3");
        }

        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action
        yield return new WaitForSeconds(1.0f);
    }

    private static IEnumerator Acting3()
    {
        CharacterB player = PlayerData.GetInst().charB;
        List<CharacterB> aims = new List<CharacterB>();
        List<Buff> arrowHurtBuffs = new List<Buff>();
        foreach (CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy && !charB.dead)
                if(charB.GetBuff("Arrow Got Shot")!=null)
                {
                    aims.Add(charB);
                    arrowHurtBuffs.Add(charB.GetBuff("Arrow Got Shot"));
                }

        CommonHandle.ChangeWeapon(WeaponType.range);

        float atkBonus = 0.3f;

        for (int i = 0; i < aims.Count; ++i)
        {
            float atkBonusReal = atkBonus * arrowHurtBuffs[i].level;
            DamageEle dmgEle = new DamageEle(atkBonusReal, DamageType.Physical);

            List<Buff> buffList = new List<Buff>();
            if (arrowHurtBuffs[i].level >= 3)
                buffList.Add(new Buff(arrowSlowDown));

            arrowHurtBuffs[i].level = 0;
            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/ice_hurt", aims[i].battleObj.transform.Find("Center").position, aims[i].battleObj.transform.Find("Center").position, 1.0f, 0.0f, 2.0f, player, aims[i], dmgEle, 1.0f, buffList));
            aims[i].UpdateBuff();
        }

        yield return CommonHandle.ChangeAnimation(player, "ability01");
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action
        yield return new WaitForSeconds(1.0f);
    }

    private static IEnumerator Acting4()
    {
        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = SingletonObject<SelectMedia>.GetInst().aimObj;

        CommonHandle.ChangeWeapon(WeaponType.range);
        Vector2 arrowStart = (Vector2)player.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);
        Vector2 arrowEnd = (Vector2)aim.battleObj.transform.Find("Center").position;
        float arrowSpeed = 0.5f;
        float castTime = 30.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = 2.5f;

        if (PlayerData.GetInst().branchData[13] == 2)
            if (player.GetBuff("Arrow Got Shot") != null)
                atkBonus += player.GetBuff("Arrow Got Shot").level * 0.2f;

        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Physical);

        Buff arrowBuff = new Buff(arrowGotShot);
        arrowBuff.level = 3;
        List<Buff> buffList = new List<Buff>();
        buffList.Add(arrowBuff);
        MasterArcher(ref buffList);

        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/gongjian_fachu_normal", arrowStart, arrowEnd, arrowSpeed, castTime, -1.0f, player, aim, dmgEle, damageDelayPercent, buffList));
        yield return CommonHandle.ChangeAnimation(player, "gongjian_attack");
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        Battle.instance.StartCoroutine(CommonHandle.AddBuff(new Buff(arrowTired), player, player));

        // return to next action
        yield return new WaitForSeconds(1.0f);
    }

    private static IEnumerator Acting5()
    {
        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = SingletonObject<SelectMedia>.GetInst().aimObj;

        CommonHandle.ChangeWeapon(WeaponType.range);
        Vector2 arrowStart = (Vector2)player.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);
        Vector2 arrowEnd = (Vector2)aim.battleObj.transform.Find("Center").position;
        float arrowSpeed = 0.5f;
        float castTime = 30.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = 0.8f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Physical);

        List<Buff> buffList = new List<Buff>();
        buffList.Add(arrowGotShot);
        MasterArcher(ref buffList);

        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/gongjian", arrowStart, arrowEnd, arrowSpeed, castTime, -1.0f, player, aim, dmgEle, damageDelayPercent, buffList));
        yield return CommonHandle.ChangeAnimation(player, "gongjian_attack");
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action
        yield return new WaitForSeconds(1.0f);

        // extra round
        if (Battle.instance.BattleEnd() == 0)
            yield return Battle.instance.NewAction(player);
    }

    public static void MasterArcher(ref List<Buff> buffList)
    {
        bool allLearned = true;
        bool arrowpoisonB = false;
        bool arrowsunderarmorB = false;
        bool arrowiceB = false;
        bool arrowburnB = false;
        bool arrownumbB = false;

        Buff arrowpoison = new Buff(arrowPoison);
        Buff arrowsunderarmor = new Buff(arrowSunderArmor);
        Buff arrowice = new Buff(arrowIce);
        Buff arrowburn = new Buff(arrowBurn);
        Buff arrownumb = new Buff(arrowNumb);

        float damage = -PlayerData.GetInst().charB.attriRuntime.tAtk * 0.3f;
        arrowpoison.buffData[1] = ((int)damage).ToString();
        arrowpoison.describe = "Hurt by poisoned arrow, receive " + arrowpoison.buffData[1] + " damage per round.";
        arrowpoison.cnDescribe = "被毒箭击中，每回合受到 " + arrowpoison.buffData[1] + " 伤害";

        if (PlayerData.GetInst().branchData[10] == 1)
            arrowpoisonB = true;
        else
            allLearned = false;
        if(PlayerData.GetInst().branchData[11] == 1)
            arrowsunderarmorB = true;
        else
            allLearned = false;
        if (PlayerData.GetInst().branchData[12] == 1)
            arrowiceB = true;
        else
            allLearned = false;
        if (PlayerData.GetInst().branchData[13] == 1)
            arrowburnB = true;
        else
            allLearned = false;
        if (PlayerData.GetInst().branchData[14] == 1)
            arrownumbB = true;
        else
            allLearned = false;

        int randomNum1 = UnityEngine.Random.Range(0, 10000) % 100 + 1;
        int randomNum2 = UnityEngine.Random.Range(0, 10000) % 100 + 1;
        int randomNum3 = UnityEngine.Random.Range(0, 10000) % 100 + 1;
        int randomNum4 = UnityEngine.Random.Range(0, 10000) % 100 + 1;
        int randomNum5 = UnityEngine.Random.Range(0, 10000) % 100 + 1;

        if (allLearned)
        {
            if (arrowpoisonB && randomNum1 <= 20)
                buffList.Add(arrowpoison);
            if (arrowsunderarmorB && randomNum2 <= 20)
                buffList.Add(arrowsunderarmor);
            if (arrowiceB && randomNum3 <= 20)
                buffList.Add(arrowice);
            if (arrowburnB && randomNum4 <= 20)
                buffList.Add(arrowburn);
            if (arrownumbB && randomNum5 <= 20)
                buffList.Add(arrownumb);
        }
        else
        {
            if (arrowpoisonB && randomNum1 <= 10)
                buffList.Add(arrowpoison);
            if (arrowsunderarmorB && randomNum2 <= 10)
                buffList.Add(arrowsunderarmor);
            if (arrowiceB && randomNum3 <= 10)
                buffList.Add(arrowice);
            if (arrowburnB && randomNum4 <= 10)
                buffList.Add(arrowburn);
            if (arrownumbB && randomNum5 <= 10)
                buffList.Add(arrownumb);
        }
    }

    private static bool CheckArrowBuff()
    {
        foreach (CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy && !charB.dead)
                if (charB.GetBuff("Arrow Got Shot") != null)
                    return true;
        return false;
    }
}