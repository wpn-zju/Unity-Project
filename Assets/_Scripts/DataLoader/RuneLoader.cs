using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class RuneLoader
{
    public static string dataPath = "data/txt/runedata";
    public static Dictionary<int, RuneData> data = new Dictionary<int, RuneData>();

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

            RuneData rune = new RuneData();

            int id = Convert.ToInt32(temp1[0]);

            rune.id_int = Convert.ToInt32(temp1[0]);
            rune.type = (RuneType)Convert.ToInt32(temp1[1]);

            {
                string[] temp2 = temp1[2].Split(',');
                for (int j = 0; j < temp2.Length; ++j)
                    rune.data.Add(temp2[j]);
            }

            data[id] = rune;
        }

        Debug.Log("Loading Rune Successfully!");
    }
}
