using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp2Manager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Camp2";
        campName = "Camp2";
        sceneBGMID = 101;
        base.Init();
    }

    public override IEnumerator EnterScene(int entrance = 0)
    {
        yield return base.EnterScene(entrance);
        if (TaskLoader.data[22].state == TaskState.AcceptedAndUnfinished)
            SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(63);
    }

    public override void Refresh()
    {
        if ((TaskLoader.data[27].state != TaskState.NotAcceptAndCouldNot && TaskLoader.data[32].state == TaskState.NotAcceptAndCouldNot) || TaskLoader.data[33].state == TaskState.FinishedAndAwarded)
            npcInScene["NPC8"].visible = true;
        else
            npcInScene["NPC8"].visible = false;

        if (TaskLoader.data[22].state != TaskState.NotAcceptAndCouldNot && TaskLoader.data[24].state == TaskState.NotAcceptAndCouldNot && TaskLoader.data[25].state != TaskState.FinishedAndAwarded)
            npcInScene["NPC10"].visible = true;
        else
            npcInScene["NPC10"].visible = false;

        base.Refresh();
    }
}
