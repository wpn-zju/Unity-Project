using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTPassageManager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "HTPassage";
        campName = "Hometown";
        sceneBGMID = 103;
        base.Init();
    }

    public override IEnumerator EnterScene(int entrance = 0)
    {
        yield return base.EnterScene(entrance);
        if (TaskLoader.data[7].state == TaskState.AcceptedAndUnfinished)
            SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(21);
    }
}
