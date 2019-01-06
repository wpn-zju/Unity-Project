using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class BuffLoader
{
    public static string dataPath = "data/txt/buff";
    public static Dictionary<int, Buff> data = new Dictionary<int, Buff>();

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

            Buff buff = new Buff();

            int id = Convert.ToInt32(temp1[0]);

            buff.name = temp1[1];
            buff.cnName = temp1[2];
            buff.infinity = temp1[3] == "0" ? false : true;
            buff.addable = temp1[4] == "0" ? false : true;
            buff.timeOrTimes = temp1[5] == "0" ? false : true;
            buff.totalRounds = Convert.ToInt32(temp1[6]);
            buff.maxLevel = Convert.ToInt32(temp1[7]);
            buff.type = (BuffType)Convert.ToInt32(temp1[8]);

            if (temp1[9] != "null")
            {
                string[] temp2 = temp1[9].Split(',');
                for (int j = 0; j < temp2.Length; ++j)
                    buff.buffData.Add(temp2[j]);
            }

            buff.toself = temp1[10] == "0" ? false : true;

            buff.iconPath = temp1[11];
            buff.describe = temp1[12];
            buff.cnDescribe = temp1[13];

            buff.lastRounds = buff.totalRounds;
            buff.level = 1;

            data[id] = buff;
        }

        Debug.Log("Loading Buff Successfully!");
    }
}
