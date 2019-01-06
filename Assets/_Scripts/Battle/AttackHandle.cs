using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public static class AttackHandle
{
    public static void Init()
    {

    }

    public static IEnumerator Acting()
    {
        CharacterB player = PlayerData.GetInst().charB;

        CommonHandle.pageId = 1;
        CommonHandle.ResetLock();

        if (ItemManager.GetInst().equippedData[0] == null || ItemManager.GetInst().equippedData[0].weaponType == WeaponType.melee)
            yield return ActingMelee();
        else if (ItemManager.GetInst().equippedData[0].weaponType == WeaponType.magic)
            yield return ActingMagic();
        else if (ItemManager.GetInst().equippedData[0].weaponType == WeaponType.range)
            yield return ActingRange();

        if (PlayerData.GetInst().branchData[11] == 2 && ItemManager.GetInst().equippedData[0] != null && ItemManager.GetInst().equippedData[0].weaponType == WeaponType.range)
            player.actionLoading = 20.0f;
        else
            player.actionLoading = 0.0f;
    }

    private static IEnumerator ActingMelee()
    {
        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = Battle.instance.autoFight ? CommonHandle.SelectAim(true) : SingletonObject<SelectMedia>.GetInst().aimObj;

        CommonHandle.ChangeWeapon(WeaponType.melee);
        yield return CommonHandle.ChangeAnimation(player, "jian_run");
        Vector2 destiny = (Vector2)aim.battleObj.transform.position + new Vector2(-1.5f, 0);
        float moveSpeed = 0.5f;
        yield return CommonHandle.MoveTo(player.battleObj, destiny, moveSpeed);
        float atkBonus1 = 0.3f;
        float atkBonus2 = 0.4f;
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
        yield return CommonHandle.ChangeAnimation(player, "jian_attack——kan");
        yield return CommonHandle.ChangeAnimation(player, "jian_run");
        player.battleObj.transform.localScale = new Vector3(-1, 1, 1);
        yield return CommonHandle.MoveTo(player.battleObj, player.returnPos, moveSpeed);
        player.battleObj.transform.localScale = new Vector3(1, 1, 1);
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        Battle.instance.StartCoroutine(CommonHandle.AddBuff(new Buff(WarriorSkillHandle.warriorFury), player, player));

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action   
        yield return new WaitForSeconds(1.0f);
    }

    private static IEnumerator ActingMagic()
    {
        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = Battle.instance.autoFight ? CommonHandle.SelectAim(true) : SingletonObject<SelectMedia>.GetInst().aimObj;

        CommonHandle.ChangeWeapon(WeaponType.magic);
        Vector2 ballStart = (Vector2)player.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);
        Vector2 ballEnd = (Vector2)aim.battleObj.transform.Find("Center").position;
        float ballSpeed = 0.5f;
        float castTime = 20.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = 1.0f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Magical);
        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/fashi_danmu", ballStart, ballEnd, ballSpeed, castTime, -1.0f, player, aim, dmgEle, damageDelayPercent, null));
        yield return CommonHandle.ChangeAnimation(player, "fazhang_attack");
        CommonHandle.RefreshSpine();

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        Buff manaAcc;
        if (PlayerData.GetInst().branchData[5] == 2)
            manaAcc = new Buff(MagicSkillHandle.manaAccumulationWithShield);
        else
            manaAcc = new Buff(MagicSkillHandle.manaAccumulation);
        manaAcc.level = 2;

        Battle.instance.StartCoroutine(CommonHandle.AddBuff(manaAcc, player, player));

        // return to next action    
        yield return new WaitForSeconds(1.0f);
    }

    private static IEnumerator ActingRange()
    {
        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = Battle.instance.autoFight ? CommonHandle.SelectAim(true) : SingletonObject<SelectMedia>.GetInst().aimObj;

        CommonHandle.ChangeWeapon(WeaponType.range);
        Vector2 arrowStart = (Vector2)player.battleObj.transform.Find("Center").position + new Vector2(1.0f, 0);
        Vector2 arrowEnd = (Vector2)aim.battleObj.transform.Find("Center").position;
        float arrowSpeed = 0.5f;
        float castTime = 30.0f * CommonHandle.secPerFrame;
        float damageDelayPercent = 1.00f;
        float atkBonus = 1.0f;
        DamageEle dmgEle = new DamageEle(atkBonus, DamageType.Physical);
        Buff arrowBuff = new Buff(ArcherSkillHandle.arrowGotShot);
        List<Buff> buffList = new List<Buff>();
        buffList.Add(arrowBuff);
        ArcherSkillHandle.MasterArcher(ref buffList);
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
    }
}