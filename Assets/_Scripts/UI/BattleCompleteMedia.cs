using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCompleteMedia : BaseMedia
{
    private bool worl = false;
    private GameObject winPanel;
    private GameObject losePanel;
    private Button winEnd;
    private Button loseEnd;

    public override void Init()
    {
        base.Init();
        winPanel = panel.Find("WinPanel").gameObject;
        losePanel = panel.Find("LosePanel").gameObject;
        winEnd = winPanel.transform.Find("End").GetComponent<Button>();
        loseEnd = losePanel.transform.Find("Return").GetComponent<Button>();

        winEnd.onClick.RemoveAllListeners();
        winEnd.onClick.AddListener(() =>
        {
            SingletonObject<HpMedia>.GetInst().Close();
            SingletonObject<MpMedia>.GetInst().Close();
            SingletonObject<SpeedMedia>.GetInst().Close();
            SingletonObject<BuffMedia>.GetInst().Close();
            SingletonObject<FlywordMedia>.GetInst().Close();
            SingletonObject<BattleUIMedia>.GetInst().CloseTurnInfo();
            SingletonObject<BattleUIMedia>.GetInst().Close();
            Close();
            Main.instance.StartCoroutine(Main.instance.Battle_End(true));
        });

        loseEnd.onClick.RemoveAllListeners();
        loseEnd.onClick.AddListener(() =>
        {
            SingletonObject<HpMedia>.GetInst().Close();
            SingletonObject<MpMedia>.GetInst().Close();
            SingletonObject<SpeedMedia>.GetInst().Close();
            SingletonObject<BuffMedia>.GetInst().Close();
            SingletonObject<FlywordMedia>.GetInst().Close();
            SingletonObject<BattleUIMedia>.GetInst().CloseTurnInfo();
            SingletonObject<BattleUIMedia>.GetInst().Close();
            Close();
            Main.instance.StartCoroutine(Main.instance.Battle_End(false));
        });
    }

    public override void Open()
    {
        base.Open();

        if(worl)
        {
            winPanel.SetActive(true);
            losePanel.SetActive(false);
        }
        else
        {
            winPanel.SetActive(false);
            losePanel.SetActive(true);
        }
    }

    public void SetWorL(bool win)
    {
        worl = win;
    }
}
