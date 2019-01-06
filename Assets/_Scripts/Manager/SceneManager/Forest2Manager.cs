using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest2Manager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Forest2";
        campName = "Camp2";
        sceneBGMID = 103;
        base.Init();
    }

    public override void Refresh()
    {
        if (TaskLoader.data[31].state != TaskState.FinishedAndAwarded && TaskLoader.data[33].state != TaskState.FinishedAndAwarded && TaskLoader.data[33].state != TaskState.FinishedButNotAwarded)
            GameObject.Find("Doors").transform.Find("Arena-0").gameObject.SetActive(false);
        else
            GameObject.Find("Doors").transform.Find("Arena-0").gameObject.SetActive(true);

        if (TaskLoader.data[31].state == TaskState.AcceptedAndUnfinished)
            monsterInScene["Conjurer"].visible = true;
        else
            monsterInScene["Conjurer"].visible = false;

        base.Refresh();
    }
}
