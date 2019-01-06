using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic2Manager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Relic2";
        campName = "City";
        sceneBGMID = 103;
        base.Init();
    }

    public override void Refresh()
    {
        if (PlayerData.GetInst().conditions[10005])
            GameObject.Find("Triggers").transform.Find("10005").gameObject.SetActive(false);
        if (PlayerData.GetInst().conditions[10006])
            GameObject.Find("Triggers").transform.Find("10006").gameObject.SetActive(false);

        if (TaskLoader.data[28].state != TaskState.FinishedAndAwarded)
            GameObject.Find("Doors").transform.Find("Forest1-0").gameObject.SetActive(false);
        else
            GameObject.Find("Doors").transform.Find("Forest1-0").gameObject.SetActive(true);

        if (TaskLoader.data[28].state == TaskState.AcceptedAndUnfinished)
            monsterInScene["Boss_03"].visible = true;
        else
            monsterInScene["Boss_03"].visible = false;

        base.Refresh();
    }
}
