using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MagicSkillHandle
{
    public static Buff manaAccumulation = new Buff();
    public static Buff manaAccumulationWithShield = new Buff();
    public static Buff curse = new Buff();
    private static Buff magicSunderArmor = new Buff();
    private static Buff magicDamageIncreased = new Buff();
    private static Buff magicDamageIncreased15 = new Buff();
    private static Buff magicIce = new Buff();
    private static Buff thunderBall = new Buff();
    private static Buff thunderBallNumb = new Buff();
    private static Buff preparingAttack = new Buff();
    private static Buff preparedAttack = new Buff();

    public static void Init()
    {
        magicSunderArmor.name = "Fireball Sunder Armor";
        magicSunderArmor.cnName = "火球减防";
        magicSunderArmor.describe = "Hurt by fireball, defense decreased by 20%.";
        magicSunderArmor.cnDescribe = "被火球击中，防御力降低20%";
        magicSunderArmor.iconPath = "Rune_Burn";
        magicSunderArmor.infinity = false;
        magicSunderArmor.addable = false;
        magicSunderArmor.timeOrTimes = true;
        magicSunderArmor.lastRounds = 2;
        magicSunderArmor.totalRounds = 2;
        magicSunderArmor.level = 1;
        magicSunderArmor.maxLevel = 1;
        magicSunderArmor.type = BuffType.shuxing;
        magicSunderArmor.buffData = new List<string>();
        magicSunderArmor.buffData.Add("tDef");
        magicSunderArmor.buffData.Add("-20");
        magicSunderArmor.toself = false;

        manaAccumulation.name = "Mana Accumulation";
        manaAccumulation.cnName = "法力积攒";
        manaAccumulation.describe = "Mana is accumulating.";
        manaAccumulation.cnDescribe = "法力正在积攒";
        manaAccumulation.iconPath = "Rune_ExFireball";
        manaAccumulation.infinity = true;
        manaAccumulation.addable = true;
        manaAccumulation.timeOrTimes = false;
        manaAccumulation.lastRounds = 0;
        manaAccumulation.totalRounds = 0;
        manaAccumulation.level = 1;
        manaAccumulation.maxLevel = 100;
        manaAccumulation.type = BuffType.biaoji;
        manaAccumulation.buffData = new List<string>();
        manaAccumulation.toself = false;

        manaAccumulationWithShield.name = "Mana Accumulation";
        manaAccumulationWithShield.cnName = "法力积攒";
        manaAccumulationWithShield.describe = "Mana is accumulating. Decrease receive damage by 2% per level.";
        manaAccumulationWithShield.cnDescribe = "法力正在积攒，同时每层法力积攒增加伤害减免2%效果";
        manaAccumulationWithShield.iconPath = "Rune_ExFireball";
        manaAccumulationWithShield.infinity = true;
        manaAccumulationWithShield.addable = true;
        manaAccumulationWithShield.timeOrTimes = false;
        manaAccumulationWithShield.lastRounds = 0;
        manaAccumulationWithShield.totalRounds = 0;
        manaAccumulationWithShield.level = 1;
        manaAccumulationWithShield.maxLevel = 100;
        manaAccumulationWithShield.type = BuffType.shuxing;
        manaAccumulationWithShield.buffData = new List<string>();
        manaAccumulationWithShield.buffData.Add("rpDmg");
        manaAccumulationWithShield.buffData.Add("2");
        manaAccumulationWithShield.toself = false;

        magicDamageIncreased.name = "Magic Damage Increased";
        magicDamageIncreased.cnName = "法力增幅";
        magicDamageIncreased.describe = "Damage increased by 10% per level.";
        magicDamageIncreased.cnDescribe = "每层提高伤害10%，最高可叠加5层";
        magicDamageIncreased.iconPath = "Rune_God";
        magicDamageIncreased.infinity = true;
        magicDamageIncreased.addable = true;
        magicDamageIncreased.timeOrTimes = false;
        magicDamageIncreased.lastRounds = 0;
        magicDamageIncreased.totalRounds = 0;
        magicDamageIncreased.level = 1;
        magicDamageIncreased.maxLevel = 5;
        magicDamageIncreased.type = BuffType.shuxing;
        magicDamageIncreased.buffData = new List<string>();
        magicDamageIncreased.buffData.Add("pDmg");
        magicDamageIncreased.buffData.Add("10");
        magicDamageIncreased.toself = false;

        magicDamageIncreased15.name = "Magic Damage Increased";
        magicDamageIncreased15.cnName = "法力增幅";
        magicDamageIncreased15.describe = "Damage increased by 15% per level.";
        magicDamageIncreased15.cnDescribe = "每层提高伤害15%，最高可叠加5层";
        magicDamageIncreased15.iconPath = "Rune_God";
        magicDamageIncreased15.infinity = true;
        magicDamageIncreased15.addable = true;
        magicDamageIncreased15.timeOrTimes = false;
        magicDamageIncreased15.lastRounds = 0;
        magicDamageIncreased15.totalRounds = 0;
        magicDamageIncreased15.level = 1;
        magicDamageIncreased15.maxLevel = 5;
        magicDamageIncreased15.type = BuffType.shuxing;
        magicDamageIncreased15.buffData = new List<string>();
        magicDamageIncreased15.buffData.Add("pDmg");
        magicDamageIncreased15.buffData.Add("15");
        magicDamageIncreased15.toself = false;

        magicIce.name = "Magic Ice";
        magicIce.cnName = "冰冻";
        magicIce.describe = "Hurt by ice magic, speed decreased by 30%.";
        magicIce.cnDescribe = "被冰冻魔法击伤，速度降低30%";
        magicIce.iconPath = "Rune_Frozen";
        magicIce.infinity = false;
        magicIce.addable = false;
        magicIce.timeOrTimes = true;
        magicIce.lastRounds = 3;
        magicIce.totalRounds = 3;
        magicIce.level = 1;
        magicIce.maxLevel = 1;
        magicIce.type = BuffType.shuxing;
        magicIce.buffData = new List<string>();
        magicIce.buffData.Add("pSpd");
        magicIce.buffData.Add("-30");
        magicIce.toself = false;

        thunderBall.name = "Thunder Ball";
        thunderBall.cnName = "雷电球";
        thunderBall.describe = "Cast a thunder ball.";
        thunderBall.cnDescribe = "释放了一个雷电球";
        thunderBall.iconPath = "Rune_LightningChain";
        thunderBall.infinity = true;
        thunderBall.addable = true;
        thunderBall.timeOrTimes = false;
        thunderBall.lastRounds = 0;
        thunderBall.totalRounds = 0;
        thunderBall.level = 3;
        thunderBall.maxLevel = 3;
        thunderBall.type = BuffType.biaoji;
        thunderBall.buffData = new List<string>();
        thunderBall.toself = false;

        thunderBallNumb.name = "Thunder Ball Numb";
        thunderBallNumb.cnName = "雷电球晕眩";
        thunderBallNumb.describe = "Numb for 1 round.";
        thunderBallNumb.cnDescribe = "被雷电球击伤，晕眩1回合";
        thunderBallNumb.iconPath = "Rune_Numb";
        thunderBallNumb.infinity = false;
        thunderBallNumb.addable = false;
        thunderBallNumb.timeOrTimes = true;
        thunderBallNumb.lastRounds = 1;
        thunderBallNumb.totalRounds = 1;
        thunderBallNumb.level = 1;
        thunderBallNumb.maxLevel = 1;
        thunderBallNumb.type = BuffType.yunxuan;
        thunderBallNumb.buffData = new List<string>();
        thunderBallNumb.buffData.Add("Dizzy");
        thunderBallNumb.buffData.Add("100");
        thunderBallNumb.toself = false;

        preparingAttack.name = "Preparing Attack";
        preparingAttack.cnName = "法术蓄力中";
        preparingAttack.describe = "You are preparing an attack with 50% extra damage.";
        preparingAttack.cnDescribe = "法力正在积蓄，达到3层后下1回合提高50%伤害";
        preparingAttack.iconPath = "Rune_Invincible";
        preparingAttack.infinity = true;
        preparingAttack.addable = true;
        preparingAttack.timeOrTimes = false;
        preparingAttack.lastRounds = 0;
        preparingAttack.totalRounds = 0;
        preparingAttack.level = 1;
        preparingAttack.maxLevel = 3;
        preparingAttack.type = BuffType.biaoji;
        preparingAttack.buffData = new List<string>();
        preparingAttack.toself = false;

        preparedAttack.name = "Prepared Attack";
        preparedAttack.cnName = "法术蓄力完毕";
        preparedAttack.describe = "Your damage increased by 50% this round.";
        preparedAttack.cnDescribe = "本回合造成的伤害提高50%";
        preparedAttack.iconPath = "Rune_Invincible";
        preparedAttack.infinity = true;
        preparedAttack.addable = true;
        preparedAttack.timeOrTimes = false;
        preparedAttack.lastRounds = 0;
        preparedAttack.totalRounds = 0;
        preparedAttack.level = 1;
        preparedAttack.maxLevel = 1;
        preparedAttack.type = BuffType.biaoji;
        preparedAttack.buffData = new List<string>();
        preparedAttack.toself = false;

        curse.name = "Curse";
        curse.cnName = "诅咒";
        curse.describe = "Cursed, lose Hp per round.";
        curse.cnDescribe = "被诅咒，每回合流失大量生命值";
        curse.iconPath = "Rune_Poison";
        curse.infinity = false;
        curse.addable = false;
        curse.timeOrTimes = true;
        curse.lastRounds = 2;
        curse.totalRounds = 2;
        curse.level = 1;
        curse.maxLevel = 1;
        curse.type = BuffType.liushi;
        curse.buffData = new List<string>();
        curse.buffData.Add("Hp");
        curse.buffData.Add("0");
        curse.toself = false;
    }

    public static bool IsAoe(int skillId)
    {
        switch (skillId)
        {
            case 0:
                return false;
            case 1:
                return true;
            case 2:
                return true;
            case 3:
                return true;
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
                return true;
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
                couldUse = player.attriRuntime.tMp > 120 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
            case 1:
                couldUse = true;
                break;
            case 2:
                if(PlayerData.GetInst().branchData[7]==1)
                    couldUse = player.attriRuntime.tMp > 225 * (1 - player.attriRuntime.mpUsede / 100.0f);
                else
                    couldUse = player.attriRuntime.tMp > 150 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
            case 3:
                couldUse = player.attriRuntime.tMp > 150 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
            case 4:
                if (PlayerData.GetInst().branchData[7] == 1)
                    couldUse = player.attriRuntime.tMp > 210 * (1 - player.attriRuntime.mpUsede / 100.0f);
                else
                    couldUse = player.attriRuntime.tMp > 300 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
        }

        if (PlayerData.GetInst().branchData[5] == 1 && PlayerData.GetInst().branchData[6] == 1 && PlayerData.GetInst().branchData[7] == 1 && PlayerData.GetInst().branchData[8] == 1 && PlayerData.GetInst().branchData[9] == 1)
        {
            if ((skillId == 1 || skillId == 2 || skillId == 3) && CheckMagicPowerLevel() < 3)
            {
                couldUse = false;
                if (GameSettings.language == Language.English)
                    SingletonObject<HintMedia>.GetInst().SendHint("You don't have enough magic power", Color.yellow);
                else
                    SingletonObject<HintMedia>.GetInst().SendHint("法力积攒层数不足", Color.yellow);
            }
            else if (skillId == 4 && CheckMagicPowerLevel() < 5)
            {
                couldUse = false;
                if (GameSettings.language == Language.English)
                    SingletonObject<HintMedia>.GetInst().SendHint("You don't have enough magic power", Color.yellow);
                else
                    SingletonObject<HintMedia>.GetInst().SendHint("法力积攒层数不足", Color.yellow);
            }
        }
        else
        {
            if ((skillId == 1 || skillId == 2 || skillId == 3) && CheckMagicPowerLevel() < 5)
            {
                couldUse = false;
                if (GameSettings.language == Language.English)
                    SingletonObject<HintMedia>.GetInst().SendHint("You don't have enough magic power", Color.yellow);
                else
                    SingletonObject<HintMedia>.GetInst().SendHint("法力积攒层数不足", Color.yellow);
            }
            else if (skillId == 4 && CheckMagicPowerLevel() < 10)
            {
                couldUse = false;
                if (GameSettings.language == Language.English)
                    SingletonObject<HintMedia>.GetInst().SendHint("You don't have enough magic power", Color.yellow);
                else
                    SingletonObject<HintMedia>.GetInst().SendHint("法力积攒层数不足", Color.yellow);
            }
        }

        if (!couldUse)
            SingletonObject<HintMedia>.GetInst().SendHint("You don't have enough mana point.", Color.red);

        return couldUse;
    }

    public static IEnumerator Acting(int skillId)
    {
        CharacterB player = PlayerData.GetInst().charB;

        CommonHandle.pageId = 3;
        CommonHandle.skillId = skillId;

        bool prepared = false;
        Buff prepareBuff = null;
        if(PlayerData.GetInst().branchData[8]==2)
        {
            prepareBuff = player.GetBuff("Preparing Attack");

            if (prepareBuff != null && prepareBuff.level >= 2)
                prepared = true;
        }

        if (prepared)
        {
            player.attriRuntime.pDmg += 50.0f;
            prepareBuff.level -= 2;
            Battle.instance.StartCoroutine(CommonHandle.AddBuff(new Buff(preparedAttack), player, player));
            player.UpdateBuff();
        }


        CommonHandle.ResetLock();

        switch (skillId)
        {
            case 0:
                player.attriRuntime.tMp -= 120 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting1();
                break;
            case 1:
                yield return Acting2();
                break;
            case 2:
                if (PlayerData.GetInst().branchData[7] == 1)
                    player.attriRuntime.tMp -= 225 * (1 - player.attriRuntime.mpUsede / 100.0f);
                else
                    player.attriRuntime.tMp -= 150 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting3();
                break;
            case 3:
                player.attriRuntime.tMp -= 150 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting4();
                break;
            case 4:
                if (PlayerData.GetInst().branchData[9] == 1)
                    player.attriRuntime.tMp -= 210 * (1 - player.attriRuntime.mpUsede / 100.0f);
                else
                    player.attriRuntime.tMp -= 300 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return Acting5();
                break;
        }

        if(PlayerData.GetInst().branchData[6] == 2)
        {
            switch (skillId)
            {
                case 0:
                    player.attriRuntime.tMp += 30 * (1 - player.attriRuntime.mpUsede / 100.0f);
                    break;
                case 2:
                    if (PlayerData.GetInst().branchData[7] == 1)
                        player.attriRuntime.tMp += 90 * (1 - player.attriRuntime.mpUsede / 100.0f);
                    else
                        player.attriRuntime.tMp += 60 * (1 - player.attriRuntime.mpUsede / 100.0f);
                    break;
                case 3:
                    player.attriRuntime.tMp += 75 * (1 - player.attriRuntime.mpUsede / 100.0f);
                    break;
                case 4:
                    if (PlayerData.GetInst().branchData[9] == 1)
                        player.attriRuntime.tMp += 84 * (1 - player.attriRuntime.mpUsede / 100.0f);
                    else
                        player.attriRuntime.tMp += 120 * (1 - player.attriRuntime.mpUsede / 100.0f);
                    break;
                default:
                    break;
            }
        }

        if (prepared)
        {
            player.attriRuntime.pDmg -= 50.0f;
            player.GetBuff("Prepared Attack").level--;
            player.UpdateBuff();
        }
        else if (PlayerData.GetInst().branchData[8] == 2)
            Battle.instance.StartCoroutine(CommonHandle.AddBuff(new Buff(preparingAttack), player, player));

        player.actionLoading = 0.0f;
    }

    private static IEnumerator Acting1()
    {
        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = SingletonObject<SelectMedia>.GetInst().aimObj;

        CommonHandle.ChangeWeapon(WeaponType.magic);
        Vector2 start = (Vector2)player.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);
        Vector2 end = (Vector2)aim.battleObj.transform.Find("Center").position;
        float speed = 0.5f;
        float castTime = 20.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = PlayerData.GetInst().branchData[5] == 1 ? 1.4f : 1.2f;

        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Magical);

        List<Buff> buffList = new List<Buff>();

        int randomNum = UnityEngine.Random.Range(0, 10000) % 100 + 1;
        if (PlayerData.GetInst().branchData[5] == 1)
            if (randomNum <= 50)
                buffList.Add(new Buff(magicSunderArmor));
        else
            if (randomNum <= 30)
                buffList.Add(new Buff(magicSunderArmor));

        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/FireballSprite", start, end, speed, castTime, -1.0f, player, aim, dmgEle, damageDelayPercent, buffList));
        yield return CommonHandle.ChangeAnimation(player, "fazhang_attack");

        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        Buff manaAcc;
        if (PlayerData.GetInst().branchData[5] == 2)
            manaAcc = new Buff(manaAccumulationWithShield);
        else
            manaAcc = new Buff(manaAccumulation);
        manaAcc.level = PlayerData.GetInst().branchData[7] == 2 ? 3 : 2;
        Battle.instance.StartCoroutine(CommonHandle.AddBuff(manaAcc, player, player));

        // return to next action
        yield return new WaitForSeconds(1.0f);
    }

    private static IEnumerator Acting2()
    {
        CharacterB player = PlayerData.GetInst().charB;

        CommonHandle.ChangeWeapon(WeaponType.magic);
        List<Buff> buffList = new List<Buff>();
        Buff magicPower;
        if (PlayerData.GetInst().branchData[6] == 1)
            magicPower = new Buff(magicDamageIncreased15);
        else
            magicPower = new Buff(magicDamageIncreased);
        buffList.Add(magicPower);
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/zhiliao", player.battleObj.transform.position, player.battleObj.transform.position, 1.0f, 0.0f, 2.0f, player, player, null, 1.0f, buffList));
        yield return CommonHandle.ChangeAnimation(player, "ability01");
        // magic branch 2 - 1
        float magicRecover = PlayerData.GetInst().branchData[6] == 1 ? player.attriItems.tMp * 0.35f : player.attriItems.tMp * 0.25f;
        player.attriRuntime.tMp += magicRecover;
        SingletonObject<FlywordMedia>.GetInst().AddFlyWord(player.battleObj.transform.Find("Center").position, (int)magicRecover, FlyWordType.magic);
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        if (player.GetBuff("Mana Accumulation") != null)
            player.GetBuff("Mana Accumulation").level -= 5;
        player.UpdateBuff();

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

        CommonHandle.ChangeWeapon(WeaponType.magic);
        float atkBonus = PlayerData.GetInst().branchData[7] == 1 ? 1.0f : 0.5f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Magical);

        for (int i = 0; i < aims.Count; ++i)
        {
            List<Buff> buffList = new List<Buff>();
            buffList.Add(new Buff(magicIce));

            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/ice_hurt", aims[i].battleObj.transform.Find("Center").position, aims[i].battleObj.transform.Find("Center").position, 1.0f, 0.0f, 2.0f, player, aims[i], dmgEle, 1.0f, buffList));
        }

        yield return CommonHandle.ChangeAnimation(player, "fazhang_attack");
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock())
        {
            yield return null;
        }

        Buff manaAcc;
        if (PlayerData.GetInst().branchData[5] == 2)
            manaAcc = new Buff(manaAccumulationWithShield);
        else
            manaAcc = new Buff(manaAccumulation);
        manaAcc.level = PlayerData.GetInst().branchData[7] == 2 ? 3 : 2;
        Battle.instance.StartCoroutine(CommonHandle.AddBuff(manaAcc, player, player));

        // return to next action
        yield return new WaitForSeconds(1.0f);
    }

    private static IEnumerator Acting4()
    {
        CharacterB player = PlayerData.GetInst().charB;

        CommonHandle.ChangeWeapon(WeaponType.magic);
        Vector2 thunderBallPos = new Vector2(3, 0.5f);
        float castTime = 20.0f * CommonHandle.secPerFrame;
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/daleidianqiu", thunderBallPos, thunderBallPos, 1.0f, castTime, -2.0f, player));
        yield return CommonHandle.ChangeAnimation(player, "fazhang_attack");
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        Buff tBall = new Buff(thunderBall);
        if (PlayerData.GetInst().branchData[8] == 1)
            tBall.lastRounds = tBall.totalRounds = 4;

        Battle.instance.StartCoroutine(CommonHandle.AddBuff(tBall, player, player));
        yield return ThunderBall();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        Buff manaAcc;
        if (PlayerData.GetInst().branchData[5] == 2)
            manaAcc = new Buff(manaAccumulationWithShield);
        else
            manaAcc = new Buff(manaAccumulation);
        manaAcc.level = PlayerData.GetInst().branchData[7] == 2 ? 3 : 2;
        Battle.instance.StartCoroutine(CommonHandle.AddBuff(manaAcc, player, player));

        // return to next action
        yield return new WaitForSeconds(1.0f);
    }

    private static IEnumerator Acting5()
    {
        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = SingletonObject<SelectMedia>.GetInst().aimObj;

        CommonHandle.ChangeWeapon(WeaponType.magic);
        Vector2 start = (Vector2)player.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);
        Vector2 end = (Vector2)aim.battleObj.transform.Find("Center").position;
        float speed = 0.5f;
        float castTime = 20.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = 4.0f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Magical);
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/fire_bird", start, end, speed, castTime, -1.0f, player, aim, dmgEle, damageDelayPercent, null));
        yield return CommonHandle.ChangeAnimation(player, "fazhang_attack");
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action
        yield return new WaitForSeconds(1.0f);
    }

    public static IEnumerator ThunderBall()
    {
        CharacterB player = PlayerData.GetInst().charB;
        List<CharacterB> aims = new List<CharacterB>();
        foreach (CharacterB charB in Battle.instance.charList)
            if (charB.isEnemy && !charB.dead)
                aims.Add(charB);

        float atkBonus = PlayerData.GetInst().branchData[8] == 1 ? 0.5f : 0.3f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Magical);

        foreach (CharacterB aim in aims)
        {
            int randomNum = UnityEngine.Random.Range(0, 10000) % 100 + 1;
            bool numb = randomNum <= 20 ? true : false;

            List<Buff> buffList = new List<Buff>();
            if (numb)
                buffList.Add(new Buff(thunderBallNumb));

            Battle.instance.StartCoroutine(CommonHandle.ThunderSkrikeCast(Battle.instance.thunderBall.transform.position, aim.battleObj.transform.Find("Center").position));
            Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/ice_hurt", aim.battleObj.transform.Find("Center").position, aim.battleObj.transform.Find("Center").position, 1.0f, 1.0f, 2.0f, player, aim, dmgEle, 1.0f, buffList));
            aim.UpdateBuff();
        }

        yield return new WaitForSeconds(1.0f);

        player.GetBuff("Thunder Ball").level--;
        player.UpdateBuff();

        if (player.GetBuff("Thunder Ball") == null)
            if (Battle.instance.thunderBall != null)
                GameObject.Destroy(Battle.instance.thunderBall);

        while (CommonHandle.CheckLock())
        {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
    }

    private static int CheckMagicPowerLevel()
    {
        CharacterB player = PlayerData.GetInst().charB;

        if (player.GetBuff("Mana Accumulation") != null)
            return player.GetBuff("Mana Accumulation").level;
        return 0;
    }
}