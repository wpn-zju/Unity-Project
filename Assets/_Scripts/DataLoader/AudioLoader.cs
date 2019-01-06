using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class AudioLoader
{
    public static string dataPath = "data/txt/audio";
    public static Dictionary<int, Audio> data = new Dictionary<int, Audio>();

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

            Audio audio = new Audio();

            int id = Convert.ToInt32(temp1[0]);

            audio.id_int = Convert.ToInt32(temp1[0]);
            audio.name = temp1[1];
            audio.path = temp1[2];
            audio.loop = temp1[3] == "0" ? false : true;
            audio.volume = (float)Convert.ToDouble(temp1[4]);
            audio.pitch = (float)Convert.ToDouble(temp1[5]);
            audio.priority = Convert.ToInt32(temp1[6]);

            data[id] = audio;
        }

        Debug.Log("Loading Audio Successfully!");

        AudioManager.GetInst().LoadAudioClip();
    }
}

public class Audio
{
    public int id_int;
    public string name;
    public string path;
    public bool loop;
    public float volume;
    public float pitch;
    public int priority;
}