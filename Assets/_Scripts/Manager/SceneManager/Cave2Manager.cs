using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave2Manager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Cave2";
        campName = "Camp1";
        sceneBGMID = 103;
        base.Init();
    }

    public override void Refresh()
    {
        if (PlayerData.GetInst().conditions[10002])
            GameObject.Find("Triggers").transform.Find("10002").gameObject.SetActive(false);

        if (TaskLoader.data[16].state != TaskState.FinishedAndAwarded)
            GameObject.Find("Doors").transform.Find("City-1").gameObject.SetActive(false);
        else
            GameObject.Find("Doors").transform.Find("City-1").gameObject.SetActive(true);

        if (TaskLoader.data[37].state != TaskState.AcceptedAndUnfinished)
            monsterInScene["Boss_01"].visible = false;
        else
            monsterInScene["Boss_01"].visible = true;

        if (TaskLoader.data[13].state == TaskState.AcceptedAndUnfinished || TaskLoader.data[14].state == TaskState.AcceptedAndUnfinished)
            monsterInScene["Rocky"].visible = true;
        else
            monsterInScene["Rocky"].visible = false;

        if (!PlayerData.GetInst().conditions[1032] && (TaskLoader.data[13].state == TaskState.FinishedButNotAwarded || TaskLoader.data[13].state == TaskState.FinishedAndAwarded || TaskLoader.data[14].state == TaskState.FinishedButNotAwarded || TaskLoader.data[14].state == TaskState.FinishedAndAwarded))
            npcInScene["NPC6"].visible = true;
        else
            npcInScene["NPC6"].visible = false;

        if (TaskLoader.data[11].state != TaskState.NotAcceptAndCouldNot && TaskLoader.data[12].state == TaskState.NotAcceptAndCouldNot && TaskLoader.data[15].state == TaskState.NotAcceptAndCouldNot)
            npcInScene["NPC7"].visible = true;
        else
            npcInScene["NPC7"].visible = false;

        base.Refresh();
    }
}
