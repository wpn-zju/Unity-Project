using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

// add : task item cannot abandon
public static class TaskLoader
{
    public static string dataPath = "data/txt/task";
    public static string dataPathConditionInfo = "data/txt/condition";
    public static Dictionary<int, string> dataConditionInfo = new Dictionary<int, string>();
    public static Dictionary<int, string> dataConditionInfoCN = new Dictionary<int, string>();
    public static Dictionary<int, TaskData> data = new Dictionary<int, TaskData>();

    public static void LoadData()
    {
        LoadTask();
        LoadConditionInfo();
    }

    private static void LoadTask()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPath);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            TaskData task = new TaskData();

            int id = Convert.ToInt32(temp1[0]);

            task.id_int = Convert.ToInt32(temp1[0]);
            task.type = (TaskType)Convert.ToInt32(temp1[1]);
            task.name = temp1[2];
            task.cnName = temp1[3];
            task.describe = temp1[4];
            task.cnDescribe = temp1[5];
            task.awardDescribe = temp1[6];
            task.cnAwardDescribe = temp1[7];
            task.completeDescribe = temp1[8];
            task.cnCompleteDescribe = temp1[9];
            task.state = TaskState.NotAcceptAndCouldNot;
            task.awardGold = Convert.ToInt32(temp1[10]);
            task.awardExp = Convert.ToInt32(temp1[11]);

            if (temp1[12] != "null")
            {
                string[] temp2 = temp1[12].Split('$');
                for (int j = 0; j < temp2.Length; ++j)
                    task.awardItems.Add(Convert.ToInt32(temp2[j].Split(',')[0]));
            }

            if (temp1[13] != "null")
            {
                string[] temp2 = temp1[13].Split('$');
                for (int j = 0; j < temp2.Length; ++j)
                {
                    task.killEnemies[Convert.ToInt32(temp2[j].Split(',')[0])] = Convert.ToInt32(temp2[j].Split(',')[1]);
                    task.currentKill[Convert.ToInt32(temp2[j].Split(',')[0])] = 0;
                }
            }

            if (temp1[14] != "null")
            {
                string[] temp2 = temp1[14].Split('$');
                for (int j = 0; j < temp2.Length; ++j)
                    task.itemsCollect[Convert.ToInt32(temp2[j].Split(',')[0])] = Convert.ToInt32(temp2[j].Split(',')[1]);
            }

            if (temp1[15] != "null")
            {
                string[] temp2 = temp1[15].Split('$');
                for (int j = 0; j < temp2.Length; ++j)
                    task.controlConditions[Convert.ToInt32(temp2[j].Split(',')[0])] = true;
            }

            task.levelLimit = Convert.ToInt32(temp1[16]);

            if (temp1[17] != "null")
            {
                string[] temp2 = temp1[17].Split(',');
                for (int j = 0; j < temp2.Length; ++j)
                    task.completeTriggers.Add(Convert.ToInt32(temp2[j]));
            }

            data[id] = task;
        }

        Debug.Log("Loading Task Successfully!");
    }

    private static void LoadConditionInfo()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPathConditionInfo);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            dataConditionInfo[Convert.ToInt32(temp1[0])] = temp1[1];
            dataConditionInfoCN[Convert.ToInt32(temp1[0])] = temp1[2];
        }

        Debug.Log("Loading ConditionInfo Successfully!");
    }
}
