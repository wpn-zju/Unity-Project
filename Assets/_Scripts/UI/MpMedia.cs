using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpMedia : BaseMedia
{
    private Transform mpParent;
    private GameObject mpSlider;
    private string sliderPath = "MyResource/Panel/MP";

    public override void Init()
    {
        base.Init();
        mpParent = panel.Find("ParentObj");
        mpSlider = Resources.Load<GameObject>(sliderPath);
    }

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < mpParent.childCount; ++i)
            GameObject.Destroy(mpParent.GetChild(i).gameObject);
    }

    public GameObject AddMpFrame()
    {
        GameObject slider = GameObject.Instantiate(mpSlider, mpParent);
        return slider;
    }
}