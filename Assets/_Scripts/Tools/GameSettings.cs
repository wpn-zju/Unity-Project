using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    private static string savePath = "Settings.txt";

    public static Language language = Language.English;
    public static bool showFPS = false;

    public static void SaveSettings(Language language = Language.English)
    {
        ES2.Save((int)language, savePath + "?tag=language");
    }

    public static void LoadSettings()
    {
#if UNITY_ANDROID
        savePath = Application.persistentDataPath + "Settings.txt";
#endif
        if (ES2.Exists(savePath))
            language = (Language)ES2.Load<int>(savePath + "?tag=language");
    }
}