using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class BossAIHandle
{
    public static Buff bloodAcc = new Buff();
    public static Buff pkSick = new Buff();
    private static Buff rockyTough = new Buff();
    private static Buff ghostClawSlowDown = new Buff();
    private static Buff ghostFear = new Buff();
    private static Buff sword = new Buff();
    private static Buff shield = new Buff();
    private static Buff lizardDamageUp = new Buff();
    private static Buff pkHeal = new Buff();
    private static Buff pkAtkUp = new Buff();
    private static Buff pkIgnite = new Buff();
    private static Buff pkNumb = new Buff();

    private static Buff arrowNumb = new Buff(); public static void Init()
    {
        rockyTough.name = "Rocky Tough";
        rockyTough.cnName = "岩石硬化";
        rockyTough.describe = "Defense increased by 20%.";
        rockyTough.cnDescribe = "石头怪使自己硬化，防御提高20%";
        rockyTough.iconPath = "Rune_God";
        rockyTough.infinity = false;
        rockyTough.addable = false;
        rockyTough.timeOrTimes = true;
        rockyTough.lastRounds = 3;
        rockyTough.totalRounds = 3;
        rockyTough.level = 1;
        rockyTough.maxLevel = 1;
        rockyTough.type = BuffType.shuxing;
        rockyTough.buffData = new List<string>();
        rockyTough.buffData.Add("pDef");
        rockyTough.buffData.Add("20");
        rockyTough.toself = false;

        ghostClawSlowDown.name = "Ghost Claw Slow Down";
        ghostClawSlowDown.cnName = "鬼爪减速";
        ghostClawSlowDown.describe = "Hurt by claw, slowed down by 20%.";
        ghostClawSlowDown.cnDescribe = "被鬼爪击伤，速度降低20%";
        ghostClawSlowDown.iconPath = "Rune_Frozen";
        ghostClawSlowDown.infinity = false;
        ghostClawSlowDown.addable = false;
        ghostClawSlowDown.timeOrTimes = true;
        ghostClawSlowDown.lastRounds = 3;
        ghostClawSlowDown.totalRounds = 3;
        ghostClawSlowDown.level = 1;
        ghostClawSlowDown.maxLevel = 1;
        ghostClawSlowDown.type = BuffType.shuxing;
        ghostClawSlowDown.buffData = new List<string>();
        ghostClawSlowDown.buffData.Add("pSpd");
        ghostClawSlowDown.buffData.Add("-20");
        ghostClawSlowDown.toself = false;

        ghostFear.name = "Ghost Fear";
        ghostFear.cnName = "鬼爪恐惧";
        ghostFear.describe = "Hurt by claw, fear for 1 round.";
        ghostFear.cnDescribe = "被鬼爪击伤，恐惧/晕眩1回合";
        ghostFear.iconPath = "Rune_Blower";
        ghostFear.infinity = false;
        ghostFear.addable = false;
        ghostFear.timeOrTimes = true;
        ghostFear.lastRounds = 1;
        ghostFear.totalRounds = 1;
        ghostFear.level = 1;
        ghostFear.maxLevel = 1;
        ghostFear.type = BuffType.yunxuan;
        ghostFear.buffData = new List<string>();
        ghostFear.buffData.Add("Dizzy");
        ghostFear.buffData.Add("100");
        ghostFear.toself = false;

        sword.name = "Sword";
        sword.cnName = "持剑状态";
        sword.describe = "Attack increased by 30%.";
        sword.cnDescribe = "持剑，攻击力提高30%";
        sword.iconPath = "Rune_RateUp";
        sword.infinity = false;
        sword.addable = false;
        sword.timeOrTimes = true;
        sword.lastRounds = 3;
        sword.totalRounds = 3;
        sword.level = 1;
        sword.maxLevel = 1;
        sword.type = BuffType.shuxing;
        sword.buffData = new List<string>();
        sword.buffData.Add("pAtk");
        sword.buffData.Add("30");
        sword.toself = false;

        shield.name = "Shield";
        shield.cnName = "举盾";
        shield.describe = "Defense increased by 30%";
        shield.cnDescribe = "举盾，防御提高30%";
        shield.iconPath = "Rune_God";
        shield.infinity = false;
        shield.addable = false;
        shield.timeOrTimes = true;
        shield.lastRounds = 3;
        shield.totalRounds = 3;
        shield.level = 1;
        shield.maxLevel = 1;
        shield.type = BuffType.shuxing;
        shield.buffData = new List<string>();
        shield.buffData.Add("pDef");
        shield.buffData.Add("30");
        shield.toself = false;

        bloodAcc.name = "Blood Accumulation";
        bloodAcc.cnName = "鲜血积攒";
        bloodAcc.describe = "Blood is accumulating.";
        bloodAcc.cnDescribe = "鲜血正在积攒";
        bloodAcc.iconPath = "Rune_Bleed";
        bloodAcc.infinity = true;
        bloodAcc.addable = false;
        bloodAcc.timeOrTimes = false;
        bloodAcc.lastRounds = 0;
        bloodAcc.totalRounds = 0;
        bloodAcc.level = 1;
        bloodAcc.maxLevel = 1;
        bloodAcc.type = BuffType.biaoji;
        bloodAcc.buffData = new List<string>();
        bloodAcc.buffData.Add("0");
        bloodAcc.toself = false;

        lizardDamageUp.name = "Damage Up";
        lizardDamageUp.cnName = "伤害提高";
        lizardDamageUp.describe = "Damage increased by 5% per level.";
        lizardDamageUp.cnDescribe = "每层状态提供5%伤害加成";
        lizardDamageUp.iconPath = "Rune_RateUp";
        lizardDamageUp.infinity = true;
        lizardDamageUp.addable = true;
        lizardDamageUp.timeOrTimes = false;
        lizardDamageUp.lastRounds = 0;
        lizardDamageUp.totalRounds = 0;
        lizardDamageUp.level = 1;
        lizardDamageUp.maxLevel = 100;
        lizardDamageUp.type = BuffType.shuxing;
        lizardDamageUp.buffData = new List<string>();
        lizardDamageUp.buffData.Add("pAtk");
        lizardDamageUp.buffData.Add("5");
        lizardDamageUp.toself = false;

        pkHeal.name = "Pumpkin Knight Health Regeneration";
        pkHeal.cnName = "南瓜骑士的自愈";
        pkHeal.describe = "Regenerate 500 HP per round.";
        pkHeal.cnDescribe = "每回合回复500点生命值";
        pkHeal.iconPath = "Rune_Heal";
        pkHeal.infinity = false;
        pkHeal.addable = false;
        pkHeal.timeOrTimes = true;
        pkHeal.lastRounds = 6;
        pkHeal.totalRounds = 6;
        pkHeal.level = 1;
        pkHeal.maxLevel = 1;
        pkHeal.type = BuffType.liushi;
        pkHeal.buffData = new List<string>();
        pkHeal.buffData.Add("Hp");
        pkHeal.buffData.Add("500");
        pkHeal.toself = false;

        pkAtkUp.name = "Pumpkin Knight Fury";
        pkAtkUp.cnName = "南瓜骑士的怒火";
        pkAtkUp.describe = "Pumpkin Knight's damage increased by 50%.";
        pkAtkUp.cnDescribe = "南瓜骑士造成的伤害增加50%";
        pkAtkUp.iconPath = "Rune_RateUp";
        pkAtkUp.infinity = false;
        pkAtkUp.addable = false;
        pkAtkUp.timeOrTimes = true;
        pkAtkUp.lastRounds = 6;
        pkAtkUp.totalRounds = 6;
        pkAtkUp.level = 1;
        pkAtkUp.maxLevel = 1;
        pkAtkUp.type = BuffType.shuxing;
        pkAtkUp.buffData = new List<string>();
        pkAtkUp.buffData.Add("pDmg");
        pkAtkUp.buffData.Add("50");
        pkAtkUp.toself = false;

        pkIgnite.name = "Pumpkin Knight Ignite";
        pkIgnite.cnName = "点燃";
        pkIgnite.describe = "Receive damage per round, the value is 10% of your maxium HP.";
        pkIgnite.cnDescribe = "每秒受到相当于最大生命值10%的伤害";
        pkIgnite.iconPath = "Rune_Burn";
        pkIgnite.infinity = false;
        pkIgnite.addable = false;
        pkIgnite.timeOrTimes = true;
        pkIgnite.lastRounds = 3;
        pkIgnite.totalRounds = 3;
        pkIgnite.level = 1;
        pkIgnite.maxLevel = 1;
        pkIgnite.type = BuffType.liushi;
        pkIgnite.buffData = new List<string>();
        pkIgnite.buffData.Add("Hp");
        pkIgnite.buffData.Add("0");
        pkIgnite.toself = false;

        pkNumb.name = "Pumpkin Numb";
        pkNumb.cnName = "南瓜骑士晕眩";
        pkNumb.describe = "50% Numb for 1 round.";
        pkNumb.cnDescribe = "被南瓜骑士释放的强烈冲击波震晕，50%几率晕眩";
        pkNumb.iconPath = "Rune_Numb";
        pkNumb.infinity = false;
        pkNumb.addable = false;
        pkNumb.timeOrTimes = true;
        pkNumb.lastRounds = 1;
        pkNumb.totalRounds = 1;
        pkNumb.level = 1;
        pkNumb.maxLevel = 1;
        pkNumb.type = BuffType.yunxuan;
        pkNumb.buffData = new List<string>();
        pkNumb.buffData.Add("Dizzy");
        pkNumb.buffData.Add("50");
        pkNumb.toself = false;

        pkSick.name = "Pumpkin Knight Sick";
        pkSick.cnName = "南瓜骑士虚弱";
        pkSick.describe = "The attack/defense/speed of the Pumpkin Knight decreased by 10% per level";
        pkSick.cnDescribe = "已击杀怪物首领幻象，每层可使南瓜骑士的攻击/防御/速度均降低10%";
        pkSick.iconPath = "Rune_Blower";
        pkSick.infinity = true;
        pkSick.addable = true;
        pkSick.timeOrTimes = false;
        pkSick.lastRounds = 0;
        pkSick.totalRounds = 0;
        pkSick.level = 1;
        pkSick.maxLevel = 5;
        pkSick.type = BuffType.shuxing;
        pkSick.buffData = new List<string>();
        pkSick.buffData.Add("pAtk");
        pkSick.buffData.Add("-10");
        pkSick.buffData.Add("pDef");
        pkSick.buffData.Add("-10");
        pkSick.buffData.Add("pSpd");
        pkSick.buffData.Add("-10");
        pkSick.toself = false;
    }

    // Boss & Veall's AI
    public static IEnumerator StartBossAI(Monster obj)
    {
        switch(obj.aiId)
        {
            case 10001:
                yield return StartB1AI(obj);
                break;
            case 10006:
                yield return StartB1AI(obj);
                break;
            case 10002:
                yield return StartB2AI(obj);
                break;
            case 10007:
                yield return StartB2AI(obj);
                break;
            case 10003:
                yield return StartB3AI(obj);
                break;
            case 10008:
                yield return StartB3AI(obj);
                break;
            case 10004:
                yield return StartB4AI(obj);
                break;
            case 10009:
                yield return StartB4AI(obj);
                break;
            case 10005:
                yield return StartB5AI(obj);
                break;
            case 10010:
                yield return StartB52AI(obj);
                break;
            case 10100:
                yield return StartVeallAI(obj);
                break;
            default:
                yield return null;
                break;
        }

        obj.actionLoading = 0.0f;
    }

    private static IEnumerator StartVeallAI(Monster obj)
    {
        CharacterB player = PlayerData.GetInst().charB;

        if (!player.dead && player.attriRuntime.tHp / player.attriItems.tHp <= 0.5f)
        {
            int randomNum1 = UnityEngine.Random.Range(0, 10000) % 2;
            if (randomNum1 / 2 == 0)
                yield return VeallHeal(obj);
            else
                yield return VeallAttack(obj);
        }
        else
            yield return VeallAttack(obj);
    }

    private static IEnumerator StartB1AI(Monster obj)
    {
        if(obj.GetBuff("Rocky Tough")!=null)
        {
            int randomNum1 = UnityEngine.Random.Range(0, 10000) % 2 + 1;

            switch (randomNum1)
            {
                case 1:
                    yield return B1A1(obj);
                    break;
                case 2:
                    yield return B1A3(obj);
                    break;
            }
        }
        else
        {
            int randomNum1 = UnityEngine.Random.Range(0, 10000) % 3 + 1;

            switch (randomNum1)
            {
                case 1:
                    yield return B1A1(obj);
                    break;
                case 2:
                    yield return B1A2(obj);
                    break;
                case 3:
                    yield return B1A3(obj);
                    break;
            }
        }
    }

    private static IEnumerator B1A1(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Stone Attack");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "岩石重击");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        yield return CommonHandle.ChangeAnimation(obj, "run");
        Vector2 destiny = (Vector2)aim.battleObj.transform.position + new Vector2(1.5f, 0);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(obj.battleObj, destiny, moveSpeed);
        float atkBonus = 1.0f;
        float delayTime = 40.0f * CommonHandle.secPerFrame;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle, obj, aim, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "attack");
        yield return CommonHandle.ChangeAnimation(obj, "run");
        obj.battleObj.transform.localScale = new Vector3(1, 1, 1);
        yield return CommonHandle.MoveTo(obj.battleObj, obj.returnPos, moveSpeed);
        obj.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }
    private static IEnumerator B1A2(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Stone Harden");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "岩石硬化");

        float delayTime = 60.0f * CommonHandle.secPerFrame;
        Battle.instance.StartCoroutine(CommonHandle.AddBuff(rockyTough, obj, obj, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "ability_2");
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B1A3(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Ground Thorn");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "群体地刺");

        List<CharacterB> aims = new List<CharacterB>();
        foreach (CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy != obj.isEnemy && !charB.dead)
                aims.Add(charB);

        float castTime = 60.0f * CommonHandle.secPerFrame;
        float atkBonus = 1.0f;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Physical);

        foreach (CharacterB aim in aims)
            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/dici", aim.battleObj.transform.Find("Center").position, aim.battleObj.transform.Find("Center").position, 1.0f, castTime, 2.0f, obj, aim, damageEle, 1.0f, null));

        yield return CommonHandle.ChangeAnimation(obj, "ability_2");
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator StartB2AI(Monster obj)
    {
        int randomNum1 = UnityEngine.Random.Range(0, 10000) % 3 + 1;

        switch (randomNum1)
        {
            case 1:
                yield return B2A1(obj);
                break;
            case 2:
                yield return B2A2(obj);
                break;
            case 3:
                yield return B2A3(obj);
                break;
        }
    }

    private static IEnumerator B2A1(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Normal Attack");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "瞬移突袭");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        yield return CommonHandle.ChangeAnimation(obj, "shunyi_down");
        obj.battleObj.transform.localScale = new Vector3(1, 1, 1);
        Vector2 destiny = (Vector2)aim.battleObj.transform.position + new Vector2(1.5f, 0);
        float moveSpeed = 1.0f;
        yield return CommonHandle.MoveTo(obj.battleObj, destiny, moveSpeed);
        yield return CommonHandle.ChangeAnimation(obj, "shunyi_up");
        float atkBonus = 1.0f;
        float delayTime = 55.0f * CommonHandle.secPerFrame;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle, obj, aim, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "attack");
        yield return CommonHandle.ChangeAnimation(obj, "shunyi_down");
        obj.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.MoveTo(obj.battleObj, obj.returnPos, moveSpeed);
        yield return CommonHandle.ChangeAnimation(obj, "shunyi_up");
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }
    private static IEnumerator B2A2(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Wrapping Claw");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "缠绕鬼爪");

        List<CharacterB> aims = new List<CharacterB>();
        foreach (CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy != obj.isEnemy && !charB.dead)
                aims.Add(charB);

        float castTime = 90.0f * CommonHandle.secPerFrame;
        float atkBonus = 1.0f;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Magical);

        foreach (CharacterB aim in aims)
        {
            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/maoshou", aim.battleObj.transform.Find("Center").position, aim.battleObj.transform.Find("Center").position, 1.0f, castTime, 2.0f, obj, aim, damageEle, 1.0f, null));
            Battle.instance.StartCoroutine(CommonHandle.AddBuff(ghostClawSlowDown, obj, aim, castTime));
        }

        yield return CommonHandle.ChangeAnimation(obj, "ability_1_a");
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B2A3(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Fear Claw");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "恐惧鬼爪");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        float castTime = 90.0f * CommonHandle.secPerFrame;
        float atkBonus = 1.0f;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Magical);

        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/maoshou", aim.battleObj.transform.Find("Center").position, aim.battleObj.transform.Find("Center").position, 1.0f, castTime, 2.0f, obj, aim, damageEle, 1.0f, null));
        Battle.instance.StartCoroutine(CommonHandle.AddBuff(ghostFear, obj, aim, castTime));

        yield return CommonHandle.ChangeAnimation(obj, "ability_1_a");
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator StartB3AI(Monster obj)
    {
        if (obj.GetBuff("Sword") != null && obj.GetBuff("Shield") != null)
            yield return B3A1(obj);
        else if (obj.GetBuff("Sword") != null)
        {
            int randomNum1 = UnityEngine.Random.Range(0, 10000) % 2 + 1;

            switch (randomNum1)
            {
                case 1:
                    yield return B3A1(obj);
                    break;
                case 2:
                    yield return B3A3(obj);
                    break;
            }
        }
        else if (obj.GetBuff("Shield") != null)
        {
            int randomNum1 = UnityEngine.Random.Range(0, 10000) % 2 + 1;

            switch (randomNum1)
            {
                case 1:
                    yield return B3A1(obj);
                    break;
                case 2:
                    yield return B3A2(obj);
                    break;
            }
        }
        else
        {
            int randomNum1 = UnityEngine.Random.Range(0, 10000) % 3 + 1;

            switch (randomNum1)
            {
                case 1:
                    yield return B3A1(obj);
                    break;
                case 2:
                    yield return B3A2(obj);
                    break;
                case 3:
                    yield return B3A3(obj);
                    break;
            }
        }
    }

    private static IEnumerator B3A1(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Normal Attack");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "斩击");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        yield return CommonHandle.ChangeAnimation(obj, "run");
        Vector2 destiny = (Vector2)aim.battleObj.transform.position + new Vector2(1.5f, 0);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(obj.battleObj, destiny, moveSpeed);
        float atkBonus = 1.0f;
        float delayTime = 30.0f * CommonHandle.secPerFrame;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle, obj, aim, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "attack");
        yield return CommonHandle.ChangeAnimation(obj, "run");
        obj.battleObj.transform.localScale = new Vector3(1, 1, 1);
        yield return CommonHandle.MoveTo(obj.battleObj, obj.returnPos, moveSpeed);
        obj.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B3A2(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Holding a Sword");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "持剑");

        float delayTime = 60.0f * CommonHandle.secPerFrame;
        Battle.instance.StartCoroutine(CommonHandle.AddBuff(sword, obj, obj, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "ability_1");
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B3A3(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Holding a Shield");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "举盾");

        float delayTime = 60.0f * CommonHandle.secPerFrame;
        Battle.instance.StartCoroutine(CommonHandle.AddBuff(shield, obj, obj, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "ability_2");
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator StartB4AI(Monster obj)
    {
        if (obj.GetBuff("Blood Accumulation") != null && Convert.ToInt32(obj.GetBuff("Blood Accumulation").buffData[0]) >= 1000)
            yield return B4A2(obj);
        else
            yield return B4A1(obj);
    }

    private static IEnumerator B4A1(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Explosion");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "爆裂");

        List<CharacterB> aims = new List<CharacterB>();
        foreach (CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy != obj.isEnemy && !charB.dead)
                aims.Add(charB);

        float castTime = 60.0f * CommonHandle.secPerFrame;
        float atkBonus = 1.0f;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Magical);

        foreach (CharacterB aim in aims)
            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/huoyanzadi", aim.battleObj.transform.Find("Center").position, aim.battleObj.transform.Find("Center").position, 1.0f, castTime, 2.0f, obj, aim, damageEle, 1.0f, null));

        yield return CommonHandle.ChangeAnimation(obj, "ability_1");
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B4A2(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Sacrifice");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "鲜血献祭");

        float castTime = 60.0f * CommonHandle.secPerFrame;
        float recover = (float)Convert.ToDouble(obj.GetBuff("Blood Accumulation").buffData[0]);
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/zhiliao", obj.battleObj.transform.Find("Center").position, obj.battleObj.transform.Find("Center").position, 1.0f, castTime, 2.0f, obj, obj));
        yield return CommonHandle.ChangeAnimation(obj, "attack");
        obj.attriRuntime.tHp += recover;
        obj.GetBuff("Blood Accumulation").buffData[0] = "0";
        obj.GetBuff("Blood Accumulation").describe = "Blood is accumulating. Current : 0.";
        obj.GetBuff("Blood Accumulation").cnDescribe = "鲜血正在积攒，当前值 : 0";
        obj.UpdateBuff();
        SingletonObject<FlywordMedia>.GetInst().AddFlyWord(obj.battleObj.transform.Find("Center").position, (int)recover, FlyWordType.heal);
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator StartB5AI(Monster obj)
    {
        if (obj.attriRuntime.tHp / obj.attriItems.tHp < 0.2f)
            yield return B5A6(obj);
        else if(obj.GetBuff("Pumpkin Knight Fury")!=null)
        {
            int randomNum1 = UnityEngine.Random.Range(0, 10000) % 4 + 1;

            switch (randomNum1)
            {
                case 1:
                    yield return B5A1(obj);
                    break;
                case 2:
                    yield return B5A2(obj);
                    break;
                case 3:
                    yield return B5A4(obj);
                    break;
                case 4:
                    yield return B5A5(obj);
                    break;
            }
        }
        else
        {
            int randomNum1 = UnityEngine.Random.Range(0, 10000) % 5 + 1;

            switch (randomNum1)
            {
                case 1:
                    yield return B5A1(obj);
                    break;
                case 2:
                    yield return B5A2(obj);
                    break;
                case 3:
                    yield return B5A3(obj);
                    break;
                case 4:
                    yield return B5A4(obj);
                    break;
                case 5:
                    yield return B5A5(obj);
                    break;
            }
        }
    }

    private static IEnumerator B5A1(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Normal Attack");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "普通攻击");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        yield return CommonHandle.ChangeAnimation(obj, "run");
        Vector2 destiny = (Vector2)aim.battleObj.transform.position + new Vector2(2.5f, 0);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(obj.battleObj, destiny, moveSpeed);
        float atkBonus = 1.0f;
        float delayTime = 50.0f * CommonHandle.secPerFrame;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle, obj, aim, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "attack");
        yield return CommonHandle.ChangeAnimation(obj, "run");
        obj.battleObj.transform.localScale = new Vector3(1, 1, 1);
        yield return CommonHandle.MoveTo(obj.battleObj, obj.returnPos, moveSpeed);
        obj.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B5A2(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Continuous Attack");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "重锤连击");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        yield return CommonHandle.ChangeAnimation(obj, "run");
        Vector2 destiny = (Vector2)aim.battleObj.transform.position + new Vector2(2.5f, 0);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(obj.battleObj, destiny, moveSpeed);
        float atkBonus1 = 0.5f;
        float atkBonus2 = 0.5f;
        float atkBonus3 = 0.5f;
        float delayTime1 = 90.0f * CommonHandle.secPerFrame;
        float delayTime2 = 120.0f * CommonHandle.secPerFrame;
        float delayTime3 = 150.0f * CommonHandle.secPerFrame;
        DamageEle damageEle1 = new DamageEle(atkBonus1, DamageType.Physical);
        DamageEle damageEle2 = new DamageEle(atkBonus2, DamageType.Physical);
        DamageEle damageEle3 = new DamageEle(atkBonus3, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle1, obj, aim, delayTime1));
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle2, obj, aim, delayTime2));
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle3, obj, aim, delayTime3));
        yield return CommonHandle.ChangeAnimation(obj, "ability_2beifen");
        yield return CommonHandle.ChangeAnimation(obj, "run");
        obj.battleObj.transform.localScale = new Vector3(1, 1, 1);
        yield return CommonHandle.MoveTo(obj.battleObj, obj.returnPos, moveSpeed);
        obj.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B5A3(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Pumpkin Knight's Fury");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "南瓜骑士的愤怒");

        float delayTime = 50.0f * CommonHandle.secPerFrame;
        Battle.instance.StartCoroutine(CommonHandle.AddBuff(pkAtkUp, obj, obj, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "ability_3");
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B5A4(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Shock Wave");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "冲击波");

        List<CharacterB> aims = new List<CharacterB>();
        foreach (CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy != obj.isEnemy && !charB.dead)
                aims.Add(charB);

        yield return CommonHandle.ChangeAnimation(obj, "run");
        Vector2 destiny = new Vector2(0.0f, 0.5f);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(obj.battleObj, destiny, moveSpeed);
        float castTime = 205.0f * CommonHandle.secPerFrame;
        float atkBonus = 1.0f;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/zadi_nangua", (Vector2)obj.battleObj.transform.position + new Vector2(-9.6f, 1.5f), (Vector2)obj.battleObj.transform.position + new Vector2(-9.6f, 1.5f), 1.0f, castTime, 2.0f, obj, obj));
        foreach (CharacterB aim in aims)
        {
            Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle, obj, aim, castTime));
            Battle.instance.StartCoroutine(CommonHandle.AddBuff(new Buff(pkNumb), obj, aim, castTime));
        }
        yield return CommonHandle.ChangeAnimation(obj, "ability_1");
        yield return CommonHandle.ChangeAnimation(obj, "run");
        obj.battleObj.transform.localScale = new Vector3(1, 1, 1);
        yield return CommonHandle.MoveTo(obj.battleObj, obj.returnPos, moveSpeed);
        obj.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B5A5(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Column of Flame");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "火焰之柱");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        float castTime = 205.0f * CommonHandle.secPerFrame;
        float atkBonus = 1.0f;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Magical);
        Buff ignite = new Buff(pkIgnite);
        ignite.buffData[1] = ((int)(-aim.attriItems.tHp * 0.1f)).ToString();

        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/huoyanzhu", aim.battleObj.transform.position, aim.battleObj.transform.position, 1.0f, castTime, 2.0f, obj, aim, damageEle, 1.0f, null));
        Battle.instance.StartCoroutine(CommonHandle.AddBuff(ignite, obj, aim, castTime));

        yield return CommonHandle.ChangeAnimation(obj, "ability_1");
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B5A6(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Healing");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "伤口愈合");

        float delayTime = 75.0f * CommonHandle.secPerFrame;
        float recover = obj.attriItems.tHp * (1 + obj.attriRuntime.pAtk / 100.0f) * 0.2f;
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/zhiliao", obj.battleObj.transform.Find("Center").position, obj.battleObj.transform.Find("Center").position, 1.0f, delayTime, 2.0f, obj, obj));
        yield return CommonHandle.ChangeAnimation(obj, "buff");
        yield return CommonHandle.ChangeAnimation(obj, "idle");
        obj.attriRuntime.tHp += recover;
        SingletonObject<FlywordMedia>.GetInst().AddFlyWord(obj.battleObj.transform.Find("Center").position, (int)recover, FlyWordType.heal);
        Battle.instance.StartCoroutine(CommonHandle.AddBuff(pkHeal, obj, obj, delayTime));

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator StartB52AI(Monster obj)
    {
        int randomNum1 = UnityEngine.Random.Range(0, 10000) % 4 + 1;

        switch (randomNum1)
        {
            case 1:
                yield return B52A1(obj);
                break;
            case 2:
                yield return B52A2(obj);
                break;
            case 3:
                yield return B52A3(obj);
                break;
            case 4:
                yield return B52A4(obj);
                break;
        }

        Battle.instance.StartCoroutine(CommonHandle.AddBuff(lizardDamageUp, obj, obj)); // Add 5% damage after every attack
    }

    private static IEnumerator B52A1(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Spear Throw");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "投矛");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        yield return CommonHandle.ChangeAnimation(obj, "run2");
        Vector2 destiny = new Vector2(0.0f, 0.5f);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(obj.battleObj, destiny, moveSpeed);
        float castTime = 33.0f * CommonHandle.secPerFrame;
        float atkBonus = 1.0f;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/Giant_Lizard_Spear_Shoot", (Vector2)obj.battleObj.transform.Find("Center").position + new Vector2(0, 1.0f), aim.battleObj.transform.Find("Center").position, 0.5f, castTime, -1.0f, obj, aim, damageEle, 1.0f, null));
        yield return CommonHandle.ChangeAnimation(obj, "ability");
        yield return CommonHandle.ChangeAnimation(obj, "run2");
        obj.battleObj.transform.localScale = new Vector3(1, 1, 1);
        yield return CommonHandle.MoveTo(obj.battleObj, obj.returnPos, moveSpeed);
        obj.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B52A2(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Stab");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "刺击");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        yield return CommonHandle.ChangeAnimation(obj, "run");
        Vector2 destiny = (Vector2)aim.battleObj.transform.position + new Vector2(1.5f, 0);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(obj.battleObj, destiny, moveSpeed);
        float atkBonus = 1.0f;
        float delayTime = 20.0f * CommonHandle.secPerFrame;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle, obj, aim, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "attack_2");
        yield return CommonHandle.ChangeAnimation(obj, "run");
        obj.battleObj.transform.localScale = new Vector3(1, 1, 1);
        yield return CommonHandle.MoveTo(obj.battleObj, obj.returnPos, moveSpeed);
        obj.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B52A3(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Bite");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "撕裂");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        yield return CommonHandle.ChangeAnimation(obj, "run");
        Vector2 destiny = (Vector2)aim.battleObj.transform.position + new Vector2(1.5f, 0);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(obj.battleObj, destiny, moveSpeed);
        float atkBonus = 1.0f;
        float delayTime = 40.0f * CommonHandle.secPerFrame;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle, obj, aim, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "ability2");
        yield return CommonHandle.ChangeAnimation(obj, "run");
        obj.battleObj.transform.localScale = new Vector3(1, 1, 1);
        yield return CommonHandle.MoveTo(obj.battleObj, obj.returnPos, moveSpeed);
        obj.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator B52A4(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Slash");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "斩击");

        CharacterB aim = CommonHandle.SelectAim(!obj.isEnemy);

        yield return CommonHandle.ChangeAnimation(obj, "run");
        Vector2 destiny = (Vector2)aim.battleObj.transform.position + new Vector2(1.5f, 0);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(obj.battleObj, destiny, moveSpeed);
        float atkBonus = 1.0f;
        float delayTime = 50.0f * CommonHandle.secPerFrame;
        DamageEle damageEle = new DamageEle(atkBonus, DamageType.Physical);
        Battle.instance.StartCoroutine(CommonHandle.CalculateDamage(damageEle, obj, aim, delayTime));
        yield return CommonHandle.ChangeAnimation(obj, "attack_1");
        yield return CommonHandle.ChangeAnimation(obj, "run");
        obj.battleObj.transform.localScale = new Vector3(1, 1, 1);
        yield return CommonHandle.MoveTo(obj.battleObj, obj.returnPos, moveSpeed);
        obj.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.ChangeAnimation(obj, "idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator VeallHeal(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Element Heal");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "元素治疗");

        List<CharacterB> aims = new List<CharacterB>();
        foreach(CharacterB charB in Battle.instance.charList)
            if (!charB.isEnemy)
                aims.Add(charB);

        foreach(CharacterB aim in aims)
        {
            float recover = aim.attriItems.tHp * 0.25f;
            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/zhiliao", aim.battleObj.transform.position, aim.battleObj.transform.position, 1.0f, 0.0f, 2.0f, obj, aim, null, 1.0f, null));
            SingletonObject<FlywordMedia>.GetInst().AddFlyWord(aim.battleObj.transform.Find("Center").position, (int)recover, FlyWordType.heal);
            aim.attriRuntime.tHp += recover;
        }

        yield return CommonHandle.ChangeAnimation(obj, "ability01");
        yield return CommonHandle.ChangeAnimation(obj, "gongjian_idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }

    private static IEnumerator VeallAttack(Monster obj)
    {
        if (GameSettings.language == Language.English)
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.name, "Archer Attack");
        else
            SingletonObject<BattleUIMedia>.GetInst().SetBattleInfo(obj.cnName, "弓箭射击");

        CharacterB aim = CommonHandle.LeastHpAim(true);

        Vector2 arrowStart = (Vector2)obj.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);
        Vector2 arrowEnd = (Vector2)aim.battleObj.transform.Find("Center").position;
        float arrowSpeed = 0.5f;
        float castTime = 30.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = 1.0f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Physical);
        Buff arrowBuff = new Buff(ArcherSkillHandle.arrowGotShot);
        List<Buff> buffList = new List<Buff>();
        buffList.Add(arrowBuff);
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/gongjian", arrowStart, arrowEnd, arrowSpeed, castTime, -1.0f, obj, aim, dmgEle, damageDelayPercent, buffList));
        yield return CommonHandle.ChangeAnimation(obj, "gongjian_attack");
        yield return CommonHandle.ChangeAnimation(obj, "gongjian_idle");

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action
        yield return new WaitForSeconds(1.0f);

        SingletonObject<BattleUIMedia>.GetInst().CloseBattleInfo();
    }
}