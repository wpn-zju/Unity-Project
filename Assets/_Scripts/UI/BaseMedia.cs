using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMedia
{
    public Transform panel;
    public bool isOpening = false;
    public bool isClosing = false;

    public virtual void Init()
    {

    }

    public virtual void Open()
    {
        UIMonitor.GetInst().AddMedia(this);
        Main.instance.StartCoroutine(Main.instance.Delayed_UI_Open(this));
    }

    public virtual void Close()
    {
        Main.instance.StartCoroutine(Main.instance.Delayed_UI_Close(this));
        UIMonitor.GetInst().RemoveMedia(this);
    }

    public virtual void Update()
    {

    }
}