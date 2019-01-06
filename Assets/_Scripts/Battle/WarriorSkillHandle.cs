using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WarriorSkillHandle
{
    public static Buff warriorFury = new Buff();
    public static Buff warriorImpasse = new Buff();
    public static Buff warriorNumb = new Buff();
    public static Buff warriorHarden = new Buff();
    public static Buff warrior100Battle = new Buff();
    public static Buff warriorArmorUp = new Buff();
    private static Buff warriorSlowDown = new Buff();
    private static Buff warriorBladeStormReflect = new Buff();
    private static Buff warriorBladeStormReflectx2 = new Buff();
    private static Buff warriorBladeStormArmor = new Buff();
    private static Buff warriorHoly = new Buff();
    private static Buff warriorImmortal = new Buff();

    public static Buff playerArenaTough = new Buff();

    public static void Init()
    {
        warriorFury.name = "Warrior Fury";
        warriorFury.cnName = "愤怒";
        warriorFury.describe = "Fury.";
        warriorFury.cnDescribe = "愤怒印记";
        warriorFury.iconPath = "Rune_Burn";
        warriorFury.infinity = true;
        warriorFury.addable = true;
        warriorFury.timeOrTimes = false;
        warriorFury.lastRounds = 0;
        warriorFury.totalRounds = 0;
        warriorFury.level = 1;
        warriorFury.maxLevel = 100;
        warriorFury.type = BuffType.biaoji;
        warriorFury.buffData = new List<string>();
        warriorFury.toself = false;

        warriorHarden.name = "Warrior Harden";
        warriorHarden.cnName = "战士坚韧";
        warriorHarden.describe = "Your defense increased by 30%.";
        warriorHarden.cnDescribe = "受到攻击使你更加坚韧，防御提高30%";
        warriorHarden.iconPath = "Rune_God";
        warriorHarden.infinity = false;
        warriorHarden.addable = false;
        warriorHarden.timeOrTimes = true;
        warriorHarden.lastRounds = 3;
        warriorHarden.totalRounds = 3;
        warriorHarden.level = 1;
        warriorHarden.maxLevel = 1;
        warriorHarden.type = BuffType.shuxing;
        warriorHarden.buffData = new List<string>();
        warriorHarden.buffData.Add("pDef");
        warriorHarden.buffData.Add("30");
        warriorHarden.toself = false;

        warriorSlowDown.name = "Warrior Slow Down";
        warriorSlowDown.cnName = "剑气刺骨";
        warriorSlowDown.describe = "Hurt by sword wind, slow down by 20%.";
        warriorSlowDown.cnDescribe = "被刺骨的剑气击伤，速度降低20%";
        warriorSlowDown.iconPath = "Rune_Dash";
        warriorSlowDown.infinity = false;
        warriorSlowDown.addable = false;
        warriorSlowDown.timeOrTimes = true;
        warriorSlowDown.lastRounds = 3;
        warriorSlowDown.totalRounds = 3;
        warriorSlowDown.level = 1;
        warriorSlowDown.maxLevel = 1;
        warriorSlowDown.type = BuffType.shuxing;
        warriorSlowDown.buffData = new List<string>();
        warriorSlowDown.toself = false;
        warriorSlowDown.buffData.Add("pSpd");
        warriorSlowDown.buffData.Add("-20");

        warriorBladeStormReflect.name = "Warrior Reflect";
        warriorBladeStormReflect.cnName = "剑刃风暴反伤";
        warriorBladeStormReflect.describe = "Cast BladeStorm, reflect damage by 100%.";
        warriorBladeStormReflect.cnDescribe = "使用了剑刃风暴，伤害反弹提高100%";
        warriorBladeStormReflect.iconPath = "Rune_Block";
        warriorBladeStormReflect.infinity = false;
        warriorBladeStormReflect.addable = false;
        warriorBladeStormReflect.timeOrTimes = true;
        warriorBladeStormReflect.lastRounds = 1;
        warriorBladeStormReflect.totalRounds = 1;
        warriorBladeStormReflect.level = 1;
        warriorBladeStormReflect.maxLevel = 1;
        warriorBladeStormReflect.type = BuffType.shuxing;
        warriorBladeStormReflect.buffData = new List<string>();
        warriorBladeStormReflect.buffData.Add("reflect");
        warriorBladeStormReflect.buffData.Add("100");
        warriorBladeStormReflect.toself = false;

        warriorBladeStormReflectx2.name = "Warrior Reflect";
        warriorBladeStormReflectx2.cnName = "剑刃风暴反伤";
        warriorBladeStormReflectx2.describe = "Cast BladeStorm, reflect damage by 200%.";
        warriorBladeStormReflectx2.cnDescribe = "使用了剑刃风暴，伤害反弹提高200%";
        warriorBladeStormReflectx2.iconPath = "Rune_Block";
        warriorBladeStormReflectx2.infinity = false;
        warriorBladeStormReflectx2.addable = false;
        warriorBladeStormReflectx2.timeOrTimes = true;
        warriorBladeStormReflectx2.lastRounds = 1;
        warriorBladeStormReflectx2.totalRounds = 1;
        warriorBladeStormReflectx2.level = 1;
        warriorBladeStormReflectx2.maxLevel = 1;
        warriorBladeStormReflectx2.type = BuffType.shuxing;
        warriorBladeStormReflectx2.buffData = new List<string>();
        warriorBladeStormReflectx2.buffData.Add("reflect");
        warriorBladeStormReflectx2.buffData.Add("200");
        warriorBladeStormReflectx2.toself = false;

        warriorBladeStormArmor.name = "Warrior Blade Storm Shield";
        warriorBladeStormArmor.cnName = "剑刃风暴减伤";
        warriorBladeStormArmor.describe = "Cast BladeStorm, receive damage decreased by 50%";
        warriorBladeStormArmor.cnDescribe = "在施展剑刃风暴时，减少受到的伤害50%";
        warriorBladeStormArmor.iconPath = "Rune_Block";
        warriorBladeStormArmor.infinity = false;
        warriorBladeStormArmor.addable = false;
        warriorBladeStormArmor.timeOrTimes = true;
        warriorBladeStormArmor.lastRounds = 1;
        warriorBladeStormArmor.totalRounds = 1;
        warriorBladeStormArmor.level = 1;
        warriorBladeStormArmor.maxLevel = 1;
        warriorBladeStormArmor.type = BuffType.shuxing;
        warriorBladeStormArmor.buffData = new List<string>();
        warriorBladeStormArmor.buffData.Add("rpDmg");
        warriorBladeStormArmor.buffData.Add("50");
        warriorBladeStormArmor.toself = false;

        warriorHoly.name = "Holy Sword";
        warriorHoly.cnName = "圣剑";
        warriorHoly.describe = "Hurt by holy sword, speed decreased by 30%.";
        warriorHoly.cnDescribe = "被圣剑击伤，速度降低30%";
        warriorHoly.iconPath = "Rune_ManaShell";
        warriorHoly.infinity = false;
        warriorHoly.addable = false;
        warriorHoly.timeOrTimes = true;
        warriorHoly.lastRounds = 2;
        warriorHoly.totalRounds = 2;
        warriorHoly.level = 1;
        warriorHoly.maxLevel = 1;
        warriorHoly.type = BuffType.shuxing;
        warriorHoly.buffData = new List<string>();
        warriorHoly.buffData.Add("pDmg");
        warriorHoly.buffData.Add("-30");
        warriorHoly.toself = false;

        warriorNumb.name = "Warrior Reflect Numb";
        warriorNumb.cnName = "反弹晕眩";
        warriorNumb.describe = "Numb for 1 round.";
        warriorNumb.cnDescribe = "在攻击时被盾牌光芒反射，晕眩1回合";
        warriorNumb.iconPath = "Rune_Numb";
        warriorNumb.infinity = false;
        warriorNumb.addable = false;
        warriorNumb.timeOrTimes = true;
        warriorNumb.lastRounds = 1;
        warriorNumb.totalRounds = 1;
        warriorNumb.level = 1;
        warriorNumb.maxLevel = 1;
        warriorNumb.type = BuffType.yunxuan;
        warriorNumb.buffData = new List<string>();
        warriorNumb.buffData.Add("Dizzy");
        warriorNumb.buffData.Add("100");
        warriorNumb.toself = false;

        warriorImmortal.name = "Warrior Immortal";
        warriorImmortal.cnName = "不死";
        warriorImmortal.describe = "Immortal for 1 round.";
        warriorImmortal.cnDescribe = "1回合内不死";
        warriorImmortal.iconPath = "Rune_ManaShell";
        warriorImmortal.infinity = false;
        warriorImmortal.addable = false;
        warriorImmortal.timeOrTimes = true;
        warriorImmortal.lastRounds = 1;
        warriorImmortal.totalRounds = 1;
        warriorImmortal.level = 1;
        warriorImmortal.maxLevel = 1;
        warriorImmortal.type = BuffType.biaoji;
        warriorImmortal.buffData = new List<string>();
        warriorImmortal.toself = false;

        warrior100Battle.name = "Warrior Battle Shield";
        warrior100Battle.cnName = "百战不殆";
        warrior100Battle.describe = "Increase defense by 10% and decrease receive damage by 5% per level.";
        warrior100Battle.cnDescribe = "每层状态增加10%防御力与5%最终伤害减免，最多叠加5层";
        warrior100Battle.iconPath = "Rune_God";
        warrior100Battle.infinity = true;
        warrior100Battle.addable = true;
        warrior100Battle.timeOrTimes = false;
        warrior100Battle.lastRounds = 0;
        warrior100Battle.totalRounds = 0;
        warrior100Battle.level = 1;
        warrior100Battle.maxLevel = 5;
        warrior100Battle.type = BuffType.shuxing;
        warrior100Battle.buffData = new List<string>();
        warrior100Battle.buffData.Add("pDef");
        warrior100Battle.buffData.Add("10");
        warrior100Battle.buffData.Add("rpDmg");
        warrior100Battle.buffData.Add("5");
        warrior100Battle.toself = false;

        warriorImpasse.name = "Warrior Impasse";
        warriorImpasse.cnName = "绝境反击";
        warriorImpasse.describe = "Increase damage, base on your hp lost.";
        warriorImpasse.cnDescribe = "已损失的生命值将提供额外的伤害加成";
        warriorImpasse.iconPath = "Rune_Invincible";
        warriorImpasse.infinity = true;
        warriorImpasse.addable = false;
        warriorImpasse.timeOrTimes = false;
        warriorImpasse.lastRounds = 0;
        warriorImpasse.totalRounds = 0;
        warriorImpasse.level = 1;
        warriorImpasse.maxLevel = 1;
        warriorImpasse.type = BuffType.shuxing;
        warriorImpasse.buffData = new List<string>();
        warriorImpasse.buffData.Add("pDmg");
        warriorImpasse.buffData.Add("0");
        warriorImpasse.toself = false;

        warriorArmorUp.name = "Warrior Armor Up";
        warriorArmorUp.cnName = "重甲";
        warriorArmorUp.describe = "Increase defense by 20%, but damage decreased by 20% at the same time.";
        warriorArmorUp.cnDescribe = "穿戴重甲，防御提高20%，但是造成的伤害降低20%";
        warriorArmorUp.iconPath = "Rune_God";
        warriorArmorUp.infinity = true;
        warriorArmorUp.addable = false;
        warriorArmorUp.timeOrTimes = false;
        warriorArmorUp.lastRounds = 0;
        warriorArmorUp.totalRounds = 0;
        warriorArmorUp.level = 1;
        warriorArmorUp.maxLevel = 1;
        warriorArmorUp.type = BuffType.shuxing;
        warriorArmorUp.buffData = new List<string>();
        warriorArmorUp.buffData.Add("pDmg");
        warriorArmorUp.buffData.Add("-20");
        warriorArmorUp.buffData.Add("pDef");
        warriorArmorUp.buffData.Add("20");
        warriorArmorUp.toself = false;

        playerArenaTough.name = "Commander's Guard";
        playerArenaTough.cnName = "指挥官的守护";
        playerArenaTough.describe = "All direct damage you received decreased by 30% in forest arena.";
        playerArenaTough.cnDescribe = "在丛林竞技场中，所受到的直接伤害降低30%";
        playerArenaTough.iconPath = "Rune_God";
        playerArenaTough.infinity = true;
        playerArenaTough.addable = false;
        playerArenaTough.timeOrTimes = false;
        playerArenaTough.lastRounds = 0;
        playerArenaTough.totalRounds = 0;
        playerArenaTough.level = 1;
        playerArenaTough.maxLevel = 1;
        playerArenaTough.type = BuffType.shuxing;
        playerArenaTough.buffData = new List<string>();
        playerArenaTough.buffData.Add("rpDmg");
        playerArenaTough.buffData.Add("30");
        playerArenaTough.toself = false;
    }

    public static bool IsAoe(int skillId)
    {
        switch (skillId)
        {
            case 0:
                return false;
            case 1:
                return false;
            case 2:
                return true;
            case 3:
                return false;
            case 4:
                return true;
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
                couldUse = player.attriRuntime.tMp > 150 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
            case 3:
                couldUse = player.attriRuntime.tMp > 150 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
            case 4:
                couldUse = player.attriRuntime.tMp > 50 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
        }

        if (!couldUse)
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("You don't have enough mana point.", Color.red);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("法力值不足", Color.red);

        if (skillId == 4 && !CheckFuryBuff())
        {
            couldUse = false;
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Don't have enough fury buffs.", Color.yellow);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("怒火状态层数不足", Color.yellow);
        }

        return couldUse;
    }

    public static IEnumerator Acting(int skillId)
    {
        CharacterB player = PlayerData.GetInst().charB;

        CommonHandle.pageId = 2;
        CommonHandle.skillId = skillId;

        CommonHandle.ResetLock();

        if (skillId == 0)
        {
            player.attriRuntime.armorPntr += 50.0f;
            // warrior branch 1 - 2
            if (PlayerData.GetInst().branchData[0] == 2)
                player.attriRuntime.crit += 50.0f;
        }


        // warrior branch 1 - 1
        if (PlayerData.GetInst().branchData[0] == 1)
        {
            player.attriRuntime.tHp += player.attriItems.tHp * 0.05f;
            SingletonObject<FlywordMedia>.GetInst().AddFlyWord(player.battleObj.transform.Find("Center").position, (int)(player.attriItems.tHp * 0.05f), FlyWordType.heal);
        }

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
                player.attriRuntime.tMp -= 150 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting3();
                break;
            case 3:
                player.attriRuntime.tMp -= 150 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting4();
                break;
            case 4:
                player.attriRuntime.tMp -= 50 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting5();
                break;
        }

        Battle.instance.StartCoroutine(CommonHandle.AddBuff(new Buff(warriorFury), player, player));

        if (skillId == 0)
        {
            player.attriRuntime.armorPntr -= 50.0f;
            // warrior branch 1 - 2
            if (PlayerData.GetInst().branchData[0] == 2)
                player.attriRuntime.crit -= 50.0f;
        }

        player.actionLoading = 0.0f;
    }

    private static IEnumerator Acting1()
    {
        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = SingletonObject<SelectMedia>.GetInst().aimObj;

        CommonHandle.ChangeWeapon(WeaponType.melee);
        yield return CommonHandle.ChangeAnimation(player, "jian_run");
        Vector2 destiny = (Vector2)aim.battleObj.transform.position + new Vector2(-1.5f, 0);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(player.battleObj, destiny, moveSpeed);
        float atkBonus1 = 0.3f;
        float atkBonus2 = 0.6f;
        float atkBonus3 = 0.3f;
        float delayTime1 = 35.0f * CommonHandle.secPerFrame;
        float delayTime2 = 70.0f * CommonHandle.secPerFrame;
        float delayTime3 = 130.0f * CommonHandle.secPerFrame;
        DamageEle damageEle1 = new DamageEle(atkBonus1, DamageType.Physical);
        DamageEle damageEle2 = new DamageEle(atkBonus2, DamageType.Physical);
        DamageEle damageEle3 = new DamageEle(atkBonus3, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle1, player, aim, delayTime1));
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle2, player, aim, delayTime2));
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle3, player, aim, delayTime3));
        yield return CommonHandle.ChangeAnimation(player, "jian_attack_ci");
        yield return CommonHandle.ChangeAnimation(player, "jian_run");
        destiny = player.returnPos;
        player.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.MoveTo(player.battleObj, destiny, moveSpeed);
        player.battleObj.transform.localScale = new Vector3(1, 1, 1);
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
        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = SingletonObject<SelectMedia>.GetInst().aimObj;
        List<CharacterB> aims = new List<CharacterB>();
        foreach (CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy && !charB.dead && charB != aim)
                aims.Add(charB);

        CommonHandle.ChangeWeapon(WeaponType.melee);
        yield return CommonHandle.ChangeAnimation(player, "jian_run");
        Vector2 destiny = new Vector2(0, 0.5f);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(player.battleObj, destiny, moveSpeed);
        Vector2 swordStart = (Vector2)player.battleObj.transform.Find("Center").position + new Vector2(0, 0.25f);
        List<Vector2> swordEnd = new List<Vector2>();
        foreach (CharacterB charB in aims)
            swordEnd.Add(charB.battleObj.transform.Find("Center").position);
        float speed = 0.5f;
        float castTime = 20.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = 0.8f;
        float atkBonus2 = PlayerData.GetInst().branchData[1] == 2 ? 0.8f : 0.4f; // warrior branch
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Physical);
        DamageEle dmgEle2 = new DamageEle(atkBonus2, DamageType.Physical);
        List<Buff> buffList = new List<Buff>();
        if (PlayerData.GetInst().branchData[1] == 2) // warrior branch
            buffList.Add(new Buff(warriorSlowDown));
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/dg", swordStart, aim.battleObj.transform.Find("Center").position, speed, castTime, -1.0f, player, aim, dmgEle, damageDelayPercent, buffList));
        for (int i = 0; i < aims.Count; ++i)
            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/dg", swordStart, swordEnd[i], speed, castTime, -1.0f, player, aims[i], dmgEle2, damageDelayPercent, null));
        yield return CommonHandle.ChangeAnimation(player, "ability01");
        yield return CommonHandle.ChangeAnimation(player, "jian_run");
        player.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.MoveTo(player.battleObj, player.returnPos, moveSpeed);
        player.battleObj.transform.localScale = new Vector3(1, 1, 1);
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
        foreach (CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy && !charB.dead)
                aims.Add(charB);

        CommonHandle.ChangeWeapon(WeaponType.melee);
        float castTime = 20.0f * CommonHandle.secPerFrame;
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/BladeStorm", player.battleObj.transform.position, player.battleObj.transform.position, 1.0f, castTime, -2.0f, player));
        yield return CommonHandle.ChangeAnimation(player, "ability01");
        float atkBonus = 0.3f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Physical);
        for (int i = 0; i < aims.Count; ++i)
            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/ice_hurt", aims[i].battleObj.transform.Find("Center").position, aims[i].battleObj.transform.Find("Center").position, 1.0f, 0.0f, 2.0f, player, aims[i], dmgEle, 1.0f, null));
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        // warrior branch
        Buff bladeStorm;
        if (PlayerData.GetInst().branchData[2] == 2)
            bladeStorm = new Buff(warriorBladeStormReflectx2);
        else
            bladeStorm = new Buff(warriorBladeStormReflect);

        Battle.instance.StartCoroutine(CommonHandle.AddBuff(bladeStorm, player, player));

        if (PlayerData.GetInst().branchData[2] == 2)
            Battle.instance.StartCoroutine(CommonHandle.AddBuff(new Buff(warriorBladeStormArmor), player, player));

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

        CommonHandle.ChangeWeapon(WeaponType.melee);
        Vector2 start = (Vector2)player.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);
        Vector2 end = (Vector2)aim.battleObj.transform.Find("Center").position;
        float speed = 0.5f;
        float castTime = 20.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = 2.0f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Physical);
        List<Buff> buffList = new List<Buff>();
        buffList.Add(new Buff(warriorHoly));
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/boss_sword2", start, end, speed, castTime, -1.0f, player, aim, dmgEle, damageDelayPercent, buffList));
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

    private static IEnumerator Acting5()
    {
        CharacterB player = PlayerData.GetInst().charB;
        List<CharacterB> aims = new List<CharacterB>();
        foreach (CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy && !charB.dead)
                aims.Add(charB);

        CommonHandle.ChangeWeapon(WeaponType.melee);
        float atkBonus = 0.3f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Physical);
        for (int i = 0; i < aims.Count; ++i)
            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/ice_hurt", aims[i].battleObj.transform.Find("Center").position, aims[i].battleObj.transform.Find("Center").position, 1.0f, 0.0f, 2.0f, player, aims[i], dmgEle, 1.0f, null));
        yield return CommonHandle.ChangeAnimation(player, "ability01");
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        Battle.instance.StartCoroutine(CommonHandle.AddBuff(new Buff(warriorImmortal), player, player));
        player.GetBuff("Warrior Fury").level -= 5;
        player.UpdateBuff();

        // return to next action
        yield return new WaitForSeconds(1.0f);
    }

    private static bool CheckFuryBuff()
    {
        CharacterB player = PlayerData.GetInst().charB;

        if (player.GetBuff("Warrior Fury") != null)
            if (player.GetBuff("Warrior Fury").level >= 5)
                return true;
        return false;
    }
}