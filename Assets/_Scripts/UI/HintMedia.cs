using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintMedia : BaseMedia
{
    private Text hintText;
    private bool hint = false;
    private Queue<HintInfo> hintQueue = new Queue<HintInfo>();
    private float duration = 0.35f;

    public override void Init()
    {
        base.Init();

        hintText = panel.Find("Hint").GetComponent<Text>();
        hintText.GetComponent<DynamicAnimation>().Init();
    }

    public void SendHint(string text, Color color)
    {
        hintQueue.Enqueue(new HintInfo(text, color));
    }

    public IEnumerator DisplayHint()
    {
        hint = true;

        while (hintQueue.Count != 0)
        {
            HintInfo temp = hintQueue.Dequeue();
            hintText.text = temp.text;
            hintText.color = temp.color;

            hintText.GetComponent<DynamicAnimation>().Open();
            yield return new WaitForSeconds(duration + 0.35f);
            hintText.GetComponent<DynamicAnimation>().Close();
            yield return new WaitForSeconds(0.35f);
        }

        hint = false;
    }

    public override void Update()
    {
        base.Update();

        if (hintQueue.Count != 0 && !hint)
            Main.instance.StartCoroutine(DisplayHint());
    }
}

public class HintInfo
{
    public string text;
    public Color color;

    public HintInfo(string text, Color color)
    {
        this.text = text;
        this.color = color;
    }
}