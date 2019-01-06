using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RuneHandle
{
    public static void Init()
    {

    }

    public static bool IsAoe(int skillId)
    {
        switch (skillId)
        {
            case 1:
                return false;
            default:
                return false;
        }
    }

    public static bool ToSelf(int skillId)
    {
        switch (skillId)
        {
            case 1:
                return true;
            default:
                return false;
        }
    }

    public static bool Check(int skillId)
    {
        CharacterB player = PlayerData.GetInst().charB;

        bool couldUse = false;

        switch(skillId)
        {
            case 1:
                couldUse = player.attriRuntime.tMp > 100 * (1 - player.attriRuntime.mpUsede / 100.0f);
                break;
            default:
                couldUse = false;
                break;
        }

        if (!couldUse)
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("You don't have enough mana point.", Color.red);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("法力值不足", Color.red);

        return couldUse;
    }

    public static IEnumerator Acting(int skillId)
    {
        CharacterB player = PlayerData.GetInst().charB;

        CommonHandle.pageId = 5;
        CommonHandle.skillId = skillId;

        CommonHandle.ResetLock();

        switch (skillId)
        {
            case 1:
                player.attriRuntime.tMp -= 100 * (1 - player.attriRuntime.mpUsede / 100.0f);
                yield return RuneHeal();
                break;
            default:
                break;
        }

        player.actionLoading = 0.0f;
    }

    private static IEnumerator RuneHeal()
    {
        CharacterB player = PlayerData.GetInst().charB;
        CharacterB aim = SingletonObject<SelectMedia>.GetInst().aimObj;

        Battle.instance.StartCoroutine(CommonHandle.FlyingObjectCast("MyResource/FlyingObject/zhiliao", aim.battleObj.transform.position, aim.battleObj.transform.position, 1.0f, 0.0f, 2.0f, player, aim, null, 1.0f, null));
        yield return CommonHandle.ChangeAnimation(player, "ability01");

        float recover = aim.attriItems.tHp * 0.25f;
        aim.attriRuntime.tHp += recover;
        SingletonObject<FlywordMedia>.GetInst().AddFlyWord(aim.battleObj.transform.Find("Center").position, (int)recover, FlyWordType.heal);

        CommonHandle.PlayerSetIdle();

        while (CommonHandle.CheckLock()) // waiting for animation end and cast end
        {
            yield return null;
        }

        // return to next action
        yield return new WaitForSeconds(1.0f);
    }

    public static Dictionary<int, string> runeSkillNameDic = new Dictionary<int, string>()
    {
        {1, "Heal"},
        {2, "Heal"},
        {3, "Heal"},
        {4, "Heal"},
        {5, "Heal"},
    };

    public static Dictionary<int, string> runeSkillNameDicCN = new Dictionary<int, string>()
    {
        {1, "治疗术"},
        {2, "治疗术"},
        {3, "治疗术"},
        {4, "治疗术"},
        {5, "治疗术"},
    };
}