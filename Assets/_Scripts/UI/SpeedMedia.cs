using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMedia : BaseMedia
{
    private Transform speedParent;
    private GameObject speedSlider;
    private string sliderPath = "MyResource/Panel/Speed";

    public override void Init()
    {
        base.Init();
        speedParent = panel.Find("ParentObj");
        speedSlider = Resources.Load<GameObject>(sliderPath);
    }

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < speedParent.childCount; ++i)
            GameObject.Destroy(speedParent.GetChild(i).gameObject);
    }

    public GameObject AddSpeedFrame()
    {
        GameObject slider = GameObject.Instantiate(speedSlider, speedParent);
        return slider;
    }
}