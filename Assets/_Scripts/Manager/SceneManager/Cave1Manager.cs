using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cave1Manager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Cave1";
        campName = "Camp1";
        sceneBGMID = 103;
        base.Init();
    }
}
