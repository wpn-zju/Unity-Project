using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Camp1Manager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Camp1";
        campName = "Camp1";
        sceneBGMID = 101;
        base.Init();
    }

    public override IEnumerator EnterScene(int entrance = 0)
    {
        yield return base.EnterScene(entrance);
        if (TaskLoader.data[7].state == TaskState.AcceptedAndUnfinished)
            SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(22);
    }

    public override void Refresh()
    {
        if (TaskLoader.data[9].state != TaskState.FinishedAndAwarded)
            GameObject.Find("Doors").transform.Find("Cave1-0").gameObject.SetActive(false);
        else
            GameObject.Find("Doors").transform.Find("Cave1-0").gameObject.SetActive(true);

        base.Refresh();
    }
}