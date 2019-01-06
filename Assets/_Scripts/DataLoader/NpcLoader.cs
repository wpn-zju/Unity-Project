using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class NpcLoader
{
    public static string dataPath = "data/txt/npc";
    public static Dictionary<int, NpcData> data = new Dictionary<int, NpcData>();

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

            NpcData npc = new NpcData();

            int id = Convert.ToInt32(temp1[0]);

            npc.id_int = Convert.ToInt32(temp1[0]);
            npc.name = temp1[1];
            npc.cnName = temp1[2];
            npc.dialogId = Convert.ToInt32(temp1[3]);

            if (temp1[4] != "null")
            {
                string[] temp2 = temp1[4].Split('/');
                for (int j = 0; j < temp2.Length; ++j)
                {
                    string[] temp3 = temp2[j].Split(',');
                    for (int k = 0; k < temp3.Length; ++k)
                        npc.taskInfo[Convert.ToInt32(temp3[0])] = 
                            new BondingTaskInfo(temp3[1] == "1" ? true : false, temp3[2] == "1" ? true : false, temp3[3] == "1" ? true : false);
                }
            }

            data[id] = npc;
        }

        Debug.Log("Loading Npc Successfully!");
    }
}
