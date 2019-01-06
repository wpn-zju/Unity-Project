using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "City";
        campName = "City";
        sceneBGMID = 101;
        base.Init();
    }

    public override IEnumerator EnterScene(int entrance = 0)
    {
        yield return base.EnterScene(entrance);
        if (TaskLoader.data[17].state == TaskState.AcceptedAndUnfinished)
            SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(48);
    }

    public override void Refresh()
    {
        if (PlayerData.GetInst().conditions[10003])
            GameObject.Find("Triggers").transform.Find("10003").gameObject.SetActive(false);
        if (PlayerData.GetInst().conditions[10004])
            GameObject.Find("Triggers").transform.Find("10004").gameObject.SetActive(false);

        if (TaskLoader.data[17].state == TaskState.NotAcceptAndCouldNot || PlayerData.GetInst().conditions[1031])
        {
            npcInScene["NPC8 - 1"].visible = false;

            sceneBGMID = 101;
        }
        else
        {
            npcInScene["NPC8 - 1"].visible = true;

            sceneBGMID = 103;
        }

        if (PlayerData.GetInst().conditions[1031])
        {
            npcInScene["NPC2"].visible = true;
            npcInScene["NPC9"].visible = true;
            npcInScene["NPC12"].visible = true;
        }
        else
        {
            npcInScene["NPC2"].visible = false;
            npcInScene["NPC9"].visible = false;
            npcInScene["NPC12"].visible = false;
        }

        if (PlayerData.GetInst().conditions[1031] && TaskLoader.data[27].state == TaskState.NotAcceptAndCouldNot)
            npcInScene["NPC8 - 2"].visible = true;
        else
            npcInScene["NPC8 - 2"].visible = false;

        if (TaskLoader.data[25].state == TaskState.AcceptedAndUnfinished || TaskLoader.data[25].state == TaskState.FinishedButNotAwarded)
            npcInScene["NPC10"].visible = true;
        else
            npcInScene["NPC10"].visible = false;

        base.Refresh();
    }
}
