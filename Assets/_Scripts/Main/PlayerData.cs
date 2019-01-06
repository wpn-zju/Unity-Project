using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData : SingletonObject<PlayerData>
{
    // attributes
    public CharacterB charB;

    // basic info here
    public string nickName;
    public int level;
    public int exp;
    public int gold;

    // story condition bools
    public Dictionary<int, bool> conditions = new Dictionary<int, bool>();

    // skill info here
    public int lastSkillPoint;
    public int totalSkillPoint;
    public int[] skillData = new int[15];
    public int[] branchData = new int[15];

    public void ResetSkill()
    {
        lastSkillPoint = totalSkillPoint;
        for (int i = 0; i < skillData.Length; ++i)
            skillData[i] = 0;
        for (int i = 0; i < branchData.Length; ++i)
            branchData[i] = 0;
        if (GameSettings.language == Language.English)
            SingletonObject<HintMedia>.GetInst().SendHint("Skill Reset.", Color.yellow);
        else
            SingletonObject<HintMedia>.GetInst().SendHint("技能已重置", Color.yellow);
        SingletonObject<FightMedia>.GetInst().RefreshHint();
    }

    public void RefreshProgress(int monsterId) // for enemy kills
    {
        foreach(KeyValuePair<int, TaskData> kvp in TaskLoader.data)
            if (kvp.Value.state == TaskState.AcceptedAndUnfinished)
                kvp.Value.RefreshProgress(monsterId);
    }

    public void Revive()
    {
        charB.attriRuntime.tHp = charB.attriItems.tHp;
        charB.attriRuntime.tMp = charB.attriItems.tMp;
    }

    public void GetGold(int count)
    {
        gold += count;

        if (count >= 0)
        {
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Get " + count + " Gold.", Color.green);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("获得 " + count + " 金币.", Color.green);
        }
        else
        {
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Cost " + -count + " Gold.", Color.red);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("花费 " + -count + " 金币.", Color.red);
        }
    }

    public void GetExp(int count)
    {
        exp += count;

        if (GameSettings.language == Language.English)
            SingletonObject<HintMedia>.GetInst().SendHint("Get " + count + " Exp.", Color.green);
        else
            SingletonObject<HintMedia>.GetInst().SendHint("获得 " + count + " 经验值", Color.green);

        while (exp > levelExpDic[level]) // level up
        {
            exp -= levelExpDic[level];
            level++;
            lastSkillPoint++;
            totalSkillPoint++;
            charB = new CharacterB(CharLoader.dataTotal[10000 + level]);
            ItemManager.GetInst().SyncItemAttributes();
            Revive();
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Level up.", Color.green);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("角色已升级", Color.green);
            SingletonObject<FightMedia>.GetInst().RefreshHint();
        }
    }

    public string GetAttributesString()
    {
        string temp = "";

        if(GameSettings.language == Language.English)
        {
            temp += " Health : " + charB.attriItems.tHp.ToString("F0") + "(" + charB.attriItems.hp.ToString("F0") + " * " + (100.0f + charB.attriItems.pHp).ToString() + "%)" + "\n";
            temp += " Mana : " + charB.attriItems.tMp.ToString("F0") + "(" + charB.attriItems.mp.ToString("F0") + " * " + (100.0f + charB.attriItems.pMp).ToString() + "%)" + "\n";
            temp += " Attack : " + charB.attriItems.tAtk.ToString("F0") + "(" + charB.attriItems.atk.ToString("F0") + " * " + (100.0f + charB.attriItems.pAtk).ToString() + "%)" + "\n";
            temp += " Defense : " + charB.attriItems.tDef.ToString("F0") + "(" + charB.attriItems.def.ToString("F0") + " * " + (100.0f + charB.attriItems.pDef).ToString() + "%)" + "\n";
            temp += " Speed : " + charB.attriItems.tSpd.ToString("F2") + "(" + charB.attriItems.spd.ToString("F2") + " * " + (100.0f + charB.attriItems.pSpd).ToString() + "%)" + "\n";
            temp += " \n";
            temp += " Crit : " + charB.attriItems.crit.ToString("F2") + " %\n";
            temp += " CritDmg : " + charB.attriItems.cdmg.ToString("F2") + " %\n";
            temp += " ArmorPntr : " + charB.attriItems.armorPntr.ToString("F2") + " %\n";
            temp += " ManaCost : - " + charB.attriItems.mpUsede.ToString("F2") + " %\n";
            temp += " Reflect : " + charB.attriItems.reflect.ToString("F2") + " %\n";
            temp += " LifeSteal : " + charB.attriItems.lifeSteal.ToString("F2") + " %\n";
            temp += " Lucky : " + charB.attriItems.lucky.ToString("F0");
        }
        else
        {
            temp += " 生命值 : " + charB.attriItems.tHp.ToString("F0") + "(" + charB.attriItems.hp.ToString("F0") + " * " + (100.0f + charB.attriItems.pHp).ToString() + "%)" + "\n";
            temp += " 法力值 : " + charB.attriItems.tMp.ToString("F0") + "(" + charB.attriItems.mp.ToString("F0") + " * " + (100.0f + charB.attriItems.pMp).ToString() + "%)" + "\n";
            temp += " 攻击力 : " + charB.attriItems.tAtk.ToString("F0") + "(" + charB.attriItems.atk.ToString("F0") + " * " + (100.0f + charB.attriItems.pAtk).ToString() + "%)" + "\n";
            temp += " 防御力 : " + charB.attriItems.tDef.ToString("F0") + "(" + charB.attriItems.def.ToString("F0") + " * " + (100.0f + charB.attriItems.pDef).ToString() + "%)" + "\n";
            temp += " 速度 : " + charB.attriItems.tSpd.ToString("F2") + "(" + charB.attriItems.spd.ToString("F2") + " * " + (100.0f + charB.attriItems.pSpd).ToString() + "%)" + "\n";
            temp += " \n";
            temp += " 暴击几率 : " + charB.attriItems.crit.ToString("F2") + " %\n";
            temp += " 暴击伤害 : " + charB.attriItems.cdmg.ToString("F2") + " %\n";
            temp += " 护甲穿透 : " + charB.attriItems.armorPntr.ToString("F2") + " %\n";
            temp += " 法力消耗 : - " + charB.attriItems.mpUsede.ToString("F2") + " %\n";
            temp += " 伤害反弹 : " + charB.attriItems.reflect.ToString("F2") + " %\n";
            temp += " 生命偷取 : " + charB.attriItems.lifeSteal.ToString("F2") + " %\n";
            temp += " 幸运值 : " + charB.attriItems.lucky.ToString("F0");
        }

        return temp;
    }

    public void NewGameClear(string name)
    {
        // basic info clear
        PlayerData.GetInst().nickName = name;
        PlayerData.GetInst().level = 1;
        PlayerData.GetInst().exp = 0;
        PlayerData.GetInst().gold = 0;

        // task info clear
        TaskLoader.LoadData();
        NpcLoader.LoadData();
        for (int i = 1; i < 110; ++i) // now 100 condition booleans
        {
            PlayerData.GetInst().conditions[i] = false;
            PlayerData.GetInst().conditions[i + 1000] = false;
            PlayerData.GetInst().conditions[i + 10000] = false;
        }

        // skill info clear
        PlayerData.GetInst().lastSkillPoint = 1;
        PlayerData.GetInst().totalSkillPoint = 1;
        for (int i = 0; i < PlayerData.GetInst().skillData.Length; ++i)
            PlayerData.GetInst().skillData[i] = 0;
        for (int i = 0; i < PlayerData.GetInst().branchData.Length; ++i)
            PlayerData.GetInst().branchData[i] = 0;
        SingletonObject<FightMedia>.GetInst().RefreshHint();

        // item info clear
        {
            ItemManager.GetInst().itemList.Clear();
            ItemManager.GetInst().equippedData[0] = null;
            ItemManager.GetInst().equippedData[1] = null;
            ItemManager.GetInst().equippedData[2] = null;
            ItemManager.GetInst().equippedData[3] = null;
            ItemManager.GetInst().equippedData[4] = null;
            ItemManager.GetInst().equippedData[5] = null;
            ItemManager.GetInst().runeData[0] = null;
            ItemManager.GetInst().runeData[1] = null;
            ItemManager.GetInst().runeData[2] = null;
            ItemManager.GetInst().runeData[3] = null;
        }

        PlayerData.GetInst().charB = new CharacterB(CharLoader.dataTotal[10000 + PlayerData.GetInst().level]);

        DataIO.totalTime = System.TimeSpan.Zero;
        DataIO.lastSaveTime = System.DateTime.Now;

        // Loading
        Main.instance.GeneratePlayer();
        Main.instance.StartCoroutine(Main.instance.EnterScene("Dreamland", 0));
    }

    public Dictionary<int, int> levelExpDic = new Dictionary<int, int>()
    {
        {1,  100 },
        {2,  140 },
        {3,  200 },
        {4,  280 },
        {5,  400 },
        {6,  560 },
        {7,  800 },
        {8,  1120 },
        {9,  1570 },
        {10, 100000000 },
    };
}