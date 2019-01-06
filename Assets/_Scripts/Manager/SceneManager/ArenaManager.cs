using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Arena";
        campName = "Camp2";
        sceneBGMID = 104;
        base.Init();
    }

    public override void Refresh()
    {
        if (TaskLoader.data[33].state == TaskState.NotAcceptAndCouldNot || TaskLoader.data[33].state == TaskState.NotAcceptAndCould)
            GameObject.Find("Triggers").transform.Find("10007").gameObject.SetActive(true);
        else
            GameObject.Find("Triggers").transform.Find("10007").gameObject.SetActive(false);

        if (TaskLoader.data[33].state == TaskState.AcceptedAndUnfinished)
        {
            if (GameObject.Find("Monster") != null)
                for (int i = 0; i < GameObject.Find("Monster").transform.childCount; ++i)
                    monsterInScene[GameObject.Find("Monster").transform.GetChild(i).name].visible = true;

            GameObject.Find("Doors").transform.Find("Forest2-1").gameObject.SetActive(false);
        }
        else
        {
            if (GameObject.Find("Monster") != null)
                for (int i = 0; i < GameObject.Find("Monster").transform.childCount; ++i)
                    monsterInScene[GameObject.Find("Monster").transform.GetChild(i).name].visible = false;

            GameObject.Find("Doors").transform.Find("Forest2-1").gameObject.SetActive(true);
        }

        if (TaskLoader.data[33].state == TaskState.FinishedAndAwarded && !PlayerData.GetInst().conditions[1033])
            npcInScene["NPC4"].visible = true;
        else
            npcInScene["NPC4"].visible = false;

        if (TaskLoader.data[32].state != TaskState.NotAcceptAndCouldNot && TaskLoader.data[32].state != TaskState.FinishedAndAwarded)
            npcInScene["NPC8"].visible = true;
        else
            npcInScene["NPC8"].visible = false;

        base.Refresh();
    }
}
