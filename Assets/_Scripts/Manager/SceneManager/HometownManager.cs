using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HometownManager : BaseSceneManager
{
    public override void Init()
    {
        sceneName = "Hometown";
        campName = "Hometown";
        sceneBGMID = 101;
        base.Init();
    }

    public override IEnumerator EnterScene(int entrance = 0)
    {
        yield return base.EnterScene(entrance);
        if (TaskLoader.data[5].state == TaskState.NotAcceptAndCouldNot)
            SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(16);
    }

    public override void Refresh()
    {
        if (TaskLoader.data[6].state != TaskState.FinishedAndAwarded)
            GameObject.Find("Doors").transform.Find("HTPassage-0").gameObject.SetActive(false);
        else
            GameObject.Find("Doors").transform.Find("HTPassage-0").gameObject.SetActive(true);

        if (TaskLoader.data[6].state == TaskState.FinishedAndAwarded)
        {
            if (GameObject.Find("Monster") != null)
                for (int i = 0; i < GameObject.Find("Monster").transform.childCount; ++i)
                    monsterInScene[GameObject.Find("Monster").transform.GetChild(i).name].visible = false;

            sceneBGMID = 101;
        }
        else
        {
            if (GameObject.Find("Monster") != null)
                for (int i = 0; i < GameObject.Find("Monster").transform.childCount; ++i)
                    monsterInScene[GameObject.Find("Monster").transform.GetChild(i).name].visible = true;

            sceneBGMID = 103;
        }

        base.Refresh();
    }
}
