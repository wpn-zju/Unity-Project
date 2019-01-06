using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class FlyingObjectLoader
{
    public static string dataPath = "data/txt/flyingobjects";
    public static Dictionary<int, FlyingObject> data = new Dictionary<int, FlyingObject>();

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

            FlyingObject flyingObject = new FlyingObject();

            int id = Convert.ToInt32(temp1[0]);

            flyingObject.name = temp1[1];
            flyingObject.path = temp1[2];

            {
                string[] temp2 = temp1[3].Split(',');
                flyingObject.start = new Vector2((float)Convert.ToDouble(temp2[0]), (float)Convert.ToDouble(temp2[1]));
            }

            {
                string[] temp2 = temp1[4].Split(',');
                flyingObject.end = new Vector2((float)Convert.ToDouble(temp2[0]), (float)Convert.ToDouble(temp2[1]));
            }

            flyingObject.moveSpeed = (float)Convert.ToDouble(temp1[5]);

            {
                string[] temp2 = temp1[6].Split(',');
                flyingObject.damageEle = temp1[6] == "null" ? null : new DamageEle((float)Convert.ToDouble(temp2[0]), (DamageType)Convert.ToInt32(temp2[1]));
            }

            flyingObject.duration = (float)Convert.ToDouble(temp1[7]);

            if (temp1[8] != "null")
            {
                string[] temp2 = temp1[8].Split(',');
                for (int j = 0; j < temp2.Length; ++j)
                    flyingObject.addBuffID.Add(Convert.ToInt32(temp2[j]));
            }

            data[id] = flyingObject;
        }

        Debug.Log("Loading FlyingObject Successfully!");
    }
}
