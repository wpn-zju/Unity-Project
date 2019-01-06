using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class TriggerLoader
{
    public static string dataPath = "data/txt/trigger";
    public static string dataPathTask = "data/txt/tasktrigger";
    public static string dataPathScene = "data/txt/scenetrigger";
    public static Dictionary<int, Trigger> data = new Dictionary<int, Trigger>();

    public static void LoadData()
    {
        LoadDataNormal();
        LoadDataTask();
        LoadDataScene();
    }

    private static void LoadDataNormal()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPath);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            Trigger trigger = new Trigger();

            int id = Convert.ToInt32(temp1[0]);

            trigger.id_int = Convert.ToInt32(temp1[0]);
            trigger.type = (TriggerType)Convert.ToInt32(temp1[1]);

            if (temp1[2] != "null")
            {
                string[] temp2 = temp1[2].Split(',');
                for (int j = 0; j < temp2.Length; ++j)
                    trigger.parameters.Add(Convert.ToInt32(temp2[j]));
            }

            data[id] = trigger;
        }

        Debug.Log("Loading Trigger Successfully!");
    }

    private static void LoadDataTask()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPathTask);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            Trigger trigger = new Trigger();

            int id = Convert.ToInt32(temp1[0]);

            trigger.id_int = Convert.ToInt32(temp1[0]);
            trigger.type = (TriggerType)Convert.ToInt32(temp1[1]);

            if (temp1[2] != "null")
            {
                string[] temp2 = temp1[2].Split(',');
                for (int j = 0; j < temp2.Length; ++j)
                    trigger.parameters.Add(Convert.ToInt32(temp2[j]));
            }

            data[id] = trigger;
        }

        Debug.Log("Loading Trigger Successfully!");
    }

    private static void LoadDataScene()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPathScene);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            Trigger trigger = new Trigger();

            int id = Convert.ToInt32(temp1[0]);

            trigger.id_int = Convert.ToInt32(temp1[0]);
            trigger.type = (TriggerType)Convert.ToInt32(temp1[1]);

            if (temp1[2] != "null")
            {
                string[] temp2 = temp1[2].Split(',');
                for (int j = 0; j < temp2.Length; ++j)
                    trigger.parameters.Add(Convert.ToInt32(temp2[j]));
            }

            data[id] = trigger;
        }

        Debug.Log("Loading Trigger Successfully!");
    }
}
