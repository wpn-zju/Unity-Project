using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class SkillLoader
{
    public static string dataPath = "data/txt/skill";
    public static Dictionary<int, Skill> data = new Dictionary<int, Skill>();

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

            Skill skill = new Skill();

            int id = Convert.ToInt32(temp1[0]);

            skill.name = temp1[1];
            skill.cnName = temp1[2];
            skill.isAoe = temp1[3] == "0" ? false : true;
            skill.toSelf = temp1[4] == "0" ? false : true;

            if (temp1[6] != "null")
            {
                string[] temp2 = temp1[6].Split('/');
                for (int j = 0; j < temp2.Length; ++j)
                    skill.animationOrders.Add(temp2[j]);
            }

            if (temp1[7] != "null")
            {
                string[] temp2 = temp1[7].Split('/');
                for (int j = 0; j < temp2.Length; ++j)
                    skill.moveOrders.Add(new Vector2((float)Convert.ToDouble(temp2[j].Split(',')[0]), (float)Convert.ToDouble(temp2[j].Split(',')[1])));
            }

            if (temp1[8] != "null")
            {
                string[] temp2 = temp1[8].Split('/');
                for (int j = 0; j < temp2.Length; ++j)
                    skill.flyObjectOrders[Convert.ToInt32(temp2[j].Split(',')[0])] = (float)Convert.ToDouble(temp2[j].Split(',')[1]);
            }

            if (temp1[9] != "null")
            {
                string[] temp2 = temp1[9].Split('/');
                for (int j = 0; j < temp2.Length; ++j)
                    skill.damageOrders[new DamageEle((float)Convert.ToDouble(temp2[j].Split(',')[0]), (DamageType)Convert.ToInt32(temp2[j].Split(',')[1]))] = (float)Convert.ToDouble(temp2[j].Split(',')[2]);
            }

            if (temp1[10] != "null")
            {
                string[] temp2 = temp1[10].Split('/');
                for (int j = 0; j < temp2.Length; ++j)
                    skill.buffOrders[Convert.ToInt32(temp2[j].Split(',')[0])] = (float)Convert.ToDouble(temp2[j].Split(',')[1]);
            }

            data[id] = skill;
        }

        Debug.Log("Loading Skill Successfully!");
    }
}
