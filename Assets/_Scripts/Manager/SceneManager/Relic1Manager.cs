using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic1Manager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Relic1";
        campName = "City";
        sceneBGMID = 103;
        base.Init();
    }

    public override void Refresh()
    {
        if(TaskLoader.data[20].state != TaskState.NotAcceptAndCouldNot && TaskLoader.data[20].state != TaskState.FinishedAndAwarded)
        {
            if (!PlayerData.GetInst().conditions[1014])
                npcInScene["NPC101"].visible = true;
            else
                npcInScene["NPC101"].visible = false;

            if (!PlayerData.GetInst().conditions[1015])
                npcInScene["NPC102"].visible = true;
            else
                npcInScene["NPC102"].visible = false;

            if (!PlayerData.GetInst().conditions[1016])
                npcInScene["NPC103"].visible = true;
            else
                npcInScene["NPC103"].visible = false;

            if (!PlayerData.GetInst().conditions[1017])
                npcInScene["NPC104"].visible = true;
            else
                npcInScene["NPC104"].visible = false;

            if (!PlayerData.GetInst().conditions[1018])
                npcInScene["NPC105"].visible = true;
            else
                npcInScene["NPC105"].visible = false;
        }
        else
        {
            npcInScene["NPC101"].visible = false;
            npcInScene["NPC102"].visible = false;
            npcInScene["NPC103"].visible = false;
            npcInScene["NPC104"].visible = false;
            npcInScene["NPC105"].visible = false;
        }

        base.Refresh();
    }
}
