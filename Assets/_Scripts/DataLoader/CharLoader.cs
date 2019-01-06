using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class CharLoader
{
    public static string dataPathPlayer = "data/txt/player";
    public static string dataPathMonster = "data/txt/monster";
    public static Dictionary<int, CharacterB> dataTotal = new Dictionary<int, CharacterB>();
    public static Dictionary<int, CharacterB> dataPlayer = new Dictionary<int, CharacterB>();
    public static Dictionary<int, Monster> dataMonster = new Dictionary<int, Monster>();

    public static void LoadData()
    {
        LoadPlayer();

        LoadMonster();

        LinkData();
    }

    private static void LoadPlayer()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPathPlayer);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            CharacterB charB = new CharacterB();

            int id = Convert.ToInt32(temp1[0]);

            charB.name = temp1[1];
            charB.cnName = temp1[2];
            charB.objPath = temp1[3];
            charB.charType = CharacterType.Player;
            charB.isEnemy = false;
            charB.attriBasic.hp = (float)Convert.ToDouble(temp1[4]);
            charB.attriBasic.mp = (float)Convert.ToDouble(temp1[5]);
            charB.attriBasic.atk = (float)Convert.ToDouble(temp1[6]);
            charB.attriBasic.def = (float)Convert.ToDouble(temp1[7]);
            charB.attriBasic.spd = (float)Convert.ToDouble(temp1[8]);
            charB.attriBasic.pHp = (float)Convert.ToDouble(temp1[9]);
            charB.attriBasic.pMp = (float)Convert.ToDouble(temp1[10]);
            charB.attriBasic.pAtk = (float)Convert.ToDouble(temp1[11]);
            charB.attriBasic.pDef = (float)Convert.ToDouble(temp1[12]);
            charB.attriBasic.pSpd = (float)Convert.ToDouble(temp1[13]);
            charB.attriBasic.lucky = (float)Convert.ToDouble(temp1[14]);
            charB.attriBasic.crit = (float)Convert.ToDouble(temp1[15]);
            charB.attriBasic.cdmg = (float)Convert.ToDouble(temp1[16]);
            charB.attriBasic.armorPntr = (float)Convert.ToDouble(temp1[17]);
            charB.attriBasic.pDmg = 0;
            charB.attriBasic.rpDmgde = 0;
            charB.attriBasic.mpUsede = (float)Convert.ToDouble(temp1[18]);
            charB.attriBasic.reflect = (float)Convert.ToDouble(temp1[19]);
            charB.attriBasic.lifeSteal = (float)Convert.ToDouble(temp1[20]);
            charB.attriBasic.Calculate();
            charB.attriItems = new Attributes(charB.attriBasic);
            charB.attriRuntime = new Attributes(charB.attriBasic);

            dataTotal[id] = charB;
        }

        Debug.Log("Loading Player Successfully!");
    }

    private static void LoadMonster()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPathMonster);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            Monster monster = new Monster();

            int id = Convert.ToInt32(temp1[0]);

            monster.name = temp1[1];
            monster.cnName = temp1[2];
            monster.objPath = temp1[4];
            monster.charType = (CharacterType)Convert.ToInt32(temp1[3]);
            monster.isEnemy = true;
            monster.attriBasic.hp = (float)Convert.ToDouble(temp1[5]);
            monster.attriBasic.mp = (float)Convert.ToDouble(temp1[6]);
            monster.attriBasic.atk = (float)Convert.ToDouble(temp1[7]);
            monster.attriBasic.def = (float)Convert.ToDouble(temp1[8]);
            monster.attriBasic.spd = (float)Convert.ToDouble(temp1[9]);
            monster.attriBasic.pHp = (float)Convert.ToDouble(temp1[10]);
            monster.attriBasic.pMp = (float)Convert.ToDouble(temp1[11]);
            monster.attriBasic.pAtk = (float)Convert.ToDouble(temp1[12]);
            monster.attriBasic.pDef = (float)Convert.ToDouble(temp1[13]);
            monster.attriBasic.pSpd = (float)Convert.ToDouble(temp1[14]);
            monster.attriBasic.lucky = (float)Convert.ToDouble(temp1[15]);
            monster.attriBasic.crit = (float)Convert.ToDouble(temp1[16]);
            monster.attriBasic.cdmg = (float)Convert.ToDouble(temp1[17]);
            monster.attriBasic.armorPntr = (float)Convert.ToDouble(temp1[18]);
            monster.attriBasic.mpUsede = (float)Convert.ToDouble(temp1[19]);
            monster.attriBasic.reflect = (float)Convert.ToDouble(temp1[20]);
            monster.attriBasic.lifeSteal = (float)Convert.ToDouble(temp1[21]);
            monster.attriBasic.Calculate();
            monster.attriItems = new Attributes(monster.attriBasic);
            monster.attriRuntime = new Attributes(monster.attriBasic);

            monster.aiId = Convert.ToInt32(temp1[22]);

            if (temp1[23] != "null")
            {
                string[] temp2 = temp1[23].Split(',');

                monster.skillList = new List<int>();

                for (int j = 0; j < temp2.Length; ++j)
                    monster.skillList.Add(Convert.ToInt32(temp2[j]));
            }

            monster.awardExp = Convert.ToInt32(temp1[24]);
            monster.awardGold = Convert.ToInt32(temp1[25]);

            dataTotal[id] = monster;
        }

        Debug.Log("Loading Monster Successfully!");
    }

    private static void LinkData()
    {
        foreach (KeyValuePair<int, CharacterB> kvp in dataPlayer)
            dataTotal[kvp.Key] = kvp.Value;
        foreach (KeyValuePair<int, Monster> kvp in dataMonster)
            dataTotal[kvp.Key] = kvp.Value;
    }
}