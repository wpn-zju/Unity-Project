using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlywordMedia : BaseMedia
{
    private Transform flywordParent;
    private GameObject flyword;
    private string flywordPath = "MyResource/Panel/Flyword";

    public override void Init()
    {
        base.Init();
        flywordParent = panel.Find("ParentObj");
        flyword = Resources.Load<GameObject>(flywordPath);
    }

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < flywordParent.childCount; ++i)
            GameObject.Destroy(flywordParent.GetChild(i).gameObject);
    }

    public void AddFlyWord(Vector2 pos, float value, FlyWordType type)
    {
        GameObject fly = GameObject.Instantiate(flyword, flywordParent);
        fly.GetComponent<Flyword>().Init(pos, value.ToString(), fly, type);
    }
}