using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public static class AILoader
{
    public static string dataPath = "data/txt/ai";
    public static Dictionary<int, AIStructure4Char> data = new Dictionary<int, AIStructure4Char>();

    public static void LoadData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPath);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            AIStructure4Char Ai = new AIStructure4Char();

            int id = Convert.ToInt32(temp1[0]);

            Ai.name = temp1[1];

            int strategyNum = temp1.Length - 2;

            for (int j = 0; j < strategyNum; ++j)
            {
                string[] temp2 = temp1[j + 2].Split(',');

                AISkillStrategy newStrategy = new AISkillStrategy();

                newStrategy.type = (AIStrategyType)Convert.ToInt32(temp2[0]);

                for (int k = 1; k < temp2.Length; ++k)
                {
                    newStrategy.data.Add(temp2[k]);
                }

                Ai.skillstr.strategyList.Add(newStrategy);
            }

            data[id] = Ai;
        }

        Debug.Log("Loading AI1 Successfully!");
    }
}

public class AIStructure4Char
{
    public string name;

    // first choose skill then choose aimObj
    public AISkillStructure skillstr = new AISkillStructure();

    public int AIProcess(CharacterB obj, List<CharacterB> chars)
    {
        List<CharacterB> playerList = new List<CharacterB>();
        List<CharacterB> enemyList = new List<CharacterB>();

        foreach(CharacterB charB in chars)
        {
            if (charB.isEnemy)
                enemyList.Add(charB);
            else
                playerList.Add(charB);
        }

        return skillstr.AIProcessSkill(obj, playerList, enemyList);
    }
}

public class AISkillStructure
{
    public List<AISkillStrategy> strategyList = new List<AISkillStrategy>();

    public int AIProcessSkill(CharacterB obj, List<CharacterB> player, List<CharacterB> enemy)
    {
        // todo
        foreach(AISkillStrategy v in strategyList)
        {
            if(v.type == AIStrategyType.hplt)
            {
                if (obj.attriRuntime.tHp / obj.attriItems.tHp < (Convert.ToInt32(v.data[0]) / 100.0f))
                {
                    return Convert.ToInt32(v.data[1]);
                }
            }
            else if(v.type == AIStrategyType.hpemt)
            {
                if (obj.attriRuntime.tHp / obj.attriItems.tHp > (Convert.ToInt32(v.data[0]) / 100.0f))
                {
                    return Convert.ToInt32(v.data[1]);
                }
            }
            else if(v.type == AIStrategyType.def)
            {
                return Convert.ToInt32(v.data[0]);
            }
            else // TODO
            {
                continue;
            }
        }

        return 19;
    }
}

public class AISkillStrategy
{
    public AIStrategyType type;

    public List<string> data = new List<string>();
}