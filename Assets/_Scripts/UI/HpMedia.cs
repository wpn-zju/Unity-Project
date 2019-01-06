using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpMedia : BaseMedia
{
    private Transform hpParent;
    private GameObject hpSlider;
    private string sliderPath = "MyResource/Panel/HP";

    public override void Init()
    {
        base.Init();
        hpParent = panel.Find("ParentObj");
        hpSlider = Resources.Load<GameObject>(sliderPath);
    }

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < hpParent.childCount; ++i)
            GameObject.Destroy(hpParent.GetChild(i).gameObject);
    }

    public GameObject AddHpFrame()
    {
        GameObject slider = GameObject.Instantiate(hpSlider, hpParent);
        return slider;
    }
}