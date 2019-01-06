using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest1Manager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Forest1";
        campName = "Camp2";
        sceneBGMID = 103;
        base.Init();
    }

    public override IEnumerator EnterScene(int entrance = 0)
    {
        yield return base.EnterScene(entrance);
        if (TaskLoader.data[29].state == TaskState.AcceptedAndUnfinished)
            SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(79);
    }
}
