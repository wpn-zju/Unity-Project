using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class DialogLoader
{
    public static string dataPath = "data/txt/dialog";
    public static Dictionary<int, DialogData> data = new Dictionary<int, DialogData>();
    public static Dictionary<int, DialogData> dataCN = new Dictionary<int, DialogData>();

    public static void LoadData()
    {
        LoadDialogData();
    }

    private static void LoadDialogData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPath);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            DialogData dialog = new DialogData();
            DialogData dialogCN = new DialogData();

            int id = Convert.ToInt32(temp1[0]);

            dialog.id_int = Convert.ToInt32(temp1[0]);
            dialogCN.id_int = Convert.ToInt32(temp1[0]);

            if (temp1[1] != "null")
            {
                try
                {
                    string[] temp2 = temp1[1].Split('/');
                    for (int j = 0; j < temp2.Length; ++j)
                    {
                        Sentence temp3 = new Sentence();
                        string[] temp4 = temp2[j].Replace('&', '/').Split('$');
                        temp3.npcId = Convert.ToInt32(temp4[0]);
                        for (int k = 1; k < temp4.Length; ++k)
                        {
                            temp3.content.Add(temp4[k].Split('|')[0]);
                            List<int> newIntList = new List<int>();
                            if (temp4[k].Split('|')[1] != "null")
                            {
                                string[] temp5 = temp4[k].Split('|')[1].Split('-');
                                for (int l = 0; l < temp5.Length; ++l)
                                    newIntList.Add(Convert.ToInt32(temp5[l]));
                            }
                            temp3.triggers.Add(newIntList);
                        }

                        dialog.sentences.Add(temp3);
                    }
                }
                catch
                {
                    Debug.Log(dataTextLines[i]);
                }
            }

            if (temp1[2] != "null")
            {
                try
                {
                    string[] temp2 = temp1[2].Split('/');
                    for (int j = 0; j < temp2.Length; ++j)
                    {
                        Sentence temp3 = new Sentence();
                        string[] temp4 = temp2[j].Replace('&', '/').Split('$');
                        temp3.npcId = Convert.ToInt32(temp4[0]);
                        for (int k = 1; k < temp4.Length; ++k)
                        {
                            temp3.content.Add(temp4[k].Split('|')[0]);
                            List<int> newIntList = new List<int>();
                            if (temp4[k].Split('|')[1] != "null")
                            {
                                string[] temp5 = temp4[k].Split('|')[1].Split('-');
                                for (int l = 0; l < temp5.Length; ++l)
                                    newIntList.Add(Convert.ToInt32(temp5[l]));
                            }
                            temp3.triggers.Add(newIntList);
                        }

                        dialogCN.sentences.Add(temp3);
                    }
                }
                catch
                {
                    Debug.Log(i);
                }
            }

            data[id] = dialog;
            dataCN[id] = dialogCN;
        }

        Debug.Log("Loading Dialog Successfully!");
    }
}
