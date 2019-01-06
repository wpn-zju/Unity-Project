using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamlandManager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Dreamland";
        campName = "Hometown";
        sceneBGMID = 102;
        base.Init();
    }

    public override IEnumerator EnterScene(int entrance = 0)
    {
        yield return base.EnterScene(entrance);
        if (TaskLoader.data[1].state == TaskState.NotAcceptAndCouldNot)
            SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(2);

        SingletonObject<GuideMedia>.GetInst().SetMode(GuideMode.prologue);
        SingletonObject<GuideMedia>.GetInst().Open();
    }

    public override void Refresh()
    {
        if (PlayerData.GetInst().conditions[10001])
            GameObject.Find("Triggers").transform.Find("10001").gameObject.SetActive(false);

        if (TaskLoader.data[4].state == TaskState.FinishedAndAwarded)
        {
            if (GameObject.Find("Monster") != null)
                for (int i = 0; i < GameObject.Find("Monster").transform.childCount; ++i)
                    monsterInScene[GameObject.Find("Monster").transform.GetChild(i).name].visible = false;
            if (GameObject.Find("Npc") != null)
                for (int i = 0; i < GameObject.Find("Npc").transform.childCount; ++i)
                    npcInScene[GameObject.Find("Npc").transform.GetChild(i).name].visible = false;

            sceneBGMID = 101;
        }
        else if (TaskLoader.data[3].state == TaskState.FinishedAndAwarded)
        {
            if (GameObject.Find("Monster") != null)
                for (int i = 0; i < GameObject.Find("Monster").transform.childCount; ++i)
                    monsterInScene[GameObject.Find("Monster").transform.GetChild(i).name].visible = true;
            if (GameObject.Find("Npc") != null)
                for (int i = 0; i < GameObject.Find("Npc").transform.childCount; ++i)
                    npcInScene[GameObject.Find("Npc").transform.GetChild(i).name].visible = false;

            sceneBGMID = 103;
        }
        else
        {
            if (GameObject.Find("Monster") != null)
                for (int i = 0; i < GameObject.Find("Monster").transform.childCount; ++i)
                    monsterInScene[GameObject.Find("Monster").transform.GetChild(i).name].visible = false;
            if (GameObject.Find("Npc") != null)
                for (int i = 0; i < GameObject.Find("Npc").transform.childCount; ++i)
                    npcInScene[GameObject.Find("Npc").transform.GetChild(i).name].visible = true;

            sceneBGMID = 102;
        }

        base.Refresh();
    }
}
