using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffMedia : BaseMedia
{
    private Transform buffParent;
    private GameObject buffParentObj;
    private GameObject buffObj;
    private string buffParentPath = "MyResource/Panel/BuffParent";
    private string buffPath = "MyResource/Panel/Buff";

    public override void Init()
    {
        base.Init();
        buffParent = panel.Find("ParentObj");
        buffParentObj = Resources.Load<GameObject>(buffParentPath);
        buffObj = Resources.Load<GameObject>(buffPath);
    }

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < buffParent.childCount; ++i)
            GameObject.Destroy(buffParent.GetChild(i).gameObject);
    }

    public GameObject AddBuffBar(string name)
    {
        GameObject parentObj = GameObject.Instantiate(buffParentObj, buffParent);
        parentObj.name = name; // rename

        return parentObj;
    }

    public GameObject AddBuff(ref GameObject parentObj, string name, Buff buff)
    {
        if(parentObj.gameObject == null)
        {
            parentObj = GameObject.Instantiate(buffParentObj, buffParent);
            parentObj.name = name; // rename
        }

        GameObject buffGameObj = GameObject.Instantiate(buffObj, parentObj.transform);

        RefreshBuff(buffGameObj, buff);

        return parentObj;
    }

    public void RefreshBuff(GameObject obj, Buff buff)
    {
        if(buff.infinity)
        {
            obj.transform.Find("Duration & Times").gameObject.SetActive(false);
        }
        else
        {
            obj.transform.Find("Duration & Times").GetComponent<Text>().text = buff.lastRounds.ToString();
        }

        if(buff.addable)
        {
            obj.transform.Find("Level").GetComponent<Text>().text = buff.level.ToString();
        }
        else
        {
            obj.transform.Find("Level").gameObject.SetActive(false);
        }

        // change image
        obj.GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(buff.iconPath);

        // button define
        obj.GetComponent<Button>().onClick.RemoveAllListeners();
        obj.GetComponent<Button>().onClick.AddListener(() =>
        {
            SingletonObject<BuffCheckMedia>.GetInst().SetBuff(buff);
            SingletonObject<BuffCheckMedia>.GetInst().Open();
        });
    }
}