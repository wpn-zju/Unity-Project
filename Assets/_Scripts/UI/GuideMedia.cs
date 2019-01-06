using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideMedia : BaseMedia
{
    // 结局1 带了维尔没加入最后战斗
    // 结局2 带了维尔加入最后战斗
    // 结局3 没带维尔
    private Button nextButton;
    private GameObject prologue;
    private GameObject end1;
    private GameObject end2;
    private GameObject end3;
    private GameObject completeHint;
    private GuideMode mode = GuideMode.prologue;

    private float printInterval = 0.02f;

    public override void Init()
    {
        base.Init();

        nextButton = panel.Find("Next").GetComponent<Button>();
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() =>
        {
            Next();
        });

        prologue = panel.Find("Prologue").gameObject;
        end1 = panel.Find("End1").gameObject;
        end2 = panel.Find("End2").gameObject;
        end3 = panel.Find("End3").gameObject;
        completeHint = panel.Find("CompleteHint").gameObject;
    }

    public override void Open()
    {
        base.Open();

        nextButton.gameObject.SetActive(false);

        switch(mode)
        {
            case GuideMode.prologue:
                prologue.SetActive(true);
                end1.SetActive(false);
                end2.SetActive(false);
                end3.SetActive(false);
                completeHint.SetActive(false);
                break;
            case GuideMode.end1:
                prologue.SetActive(false);
                end1.SetActive(true);
                end2.SetActive(false);
                end3.SetActive(false);
                completeHint.SetActive(false);
                break;
            case GuideMode.end2:
                prologue.SetActive(false);
                end1.SetActive(false);
                end2.SetActive(true);
                end3.SetActive(false);
                completeHint.SetActive(false);
                break;
            case GuideMode.end3:
                prologue.SetActive(false);
                end1.SetActive(false);
                end2.SetActive(false);
                end3.SetActive(true);
                completeHint.SetActive(false);
                break;
            case GuideMode.completeHint:
                prologue.SetActive(false);
                end1.SetActive(false);
                end2.SetActive(false);
                end3.SetActive(false);
                completeHint.SetActive(true);
                break;
        }

        Main.instance.StartCoroutine(Print());
    }

    private IEnumerator Print()
    {
        if (mode == GuideMode.completeHint)
            yield break;

        Text displayingText = null;
        string content = null;
        string display = null;

        switch (mode)
        {
            case GuideMode.prologue:
                displayingText = prologue.transform.GetComponentInChildren<Text>();
                break;
            case GuideMode.end1:
                displayingText = end1.transform.GetComponentInChildren<Text>();
                break;
            case GuideMode.end2:
                displayingText = end2.transform.GetComponentInChildren<Text>();
                break;
            case GuideMode.end3:
                displayingText = end3.transform.GetComponentInChildren<Text>();
                break;
            case GuideMode.completeHint:
                displayingText = completeHint.transform.GetComponentInChildren<Text>();
                break;
        }

        content = displayingText.text;

        for (int i = 0; i <= content.Length; ++i)
        {
            display = content.Substring(0, i);
            displayingText.text = display;
            yield return new WaitForSeconds(printInterval);
        }

        nextButton.gameObject.SetActive(true);
    }

    private void Next()
    {
        if (mode == GuideMode.completeHint || mode == GuideMode.prologue)
            Close();
        else
        {
            mode = GuideMode.completeHint;
            Close();
            Open();
        }
    }

    public void SetMode(GuideMode mode)
    {
        this.mode = mode;
    }
}