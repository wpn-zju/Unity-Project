using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class SkillInfoLoader
{
    public static string dataPath = "data/txt/skillinfo";
    public static Dictionary<int, SkillInfo> data = new Dictionary<int, SkillInfo>();

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

            SkillInfo skillInfo = new SkillInfo();

            int id = Convert.ToInt32(temp1[0]);

            skillInfo.name = temp1[1];
            skillInfo.cnName = temp1[2];
            skillInfo.mainSkill = temp1[3] == "0" ? false : true;
            skillInfo.describe = temp1[4];
            skillInfo.cnDescribe = temp1[5];
            skillInfo.iconPath = temp1[6];

            data[id] = skillInfo;
        }

        Debug.Log("Loading SkillInfo Successfully!");
    }
}

public class SkillInfo
{
    public string name;
    public string cnName;
    public bool mainSkill;
    public string describe;
    public string cnDescribe;
    public string iconPath;
}