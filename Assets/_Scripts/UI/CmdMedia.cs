using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CmdMedia : BaseMedia
{
    public bool enable = false;

    private InputField input;

    public override void Init()
    {
        base.Init();
        input = panel.GetComponentInChildren<InputField>();
        input.onEndEdit.RemoveAllListeners();
        input.onEndEdit.AddListener(delegate { CMD(input.text); });
    }

    private void CMD(string s)
    {
        switch (s)
        {
            case "re":
                PlayerData.GetInst().Revive();
                break;
            case "exp":
                PlayerData.GetInst().GetExp(100);
                break;
            case "Exp":
                PlayerData.GetInst().GetExp(1000);
                break;
            case "gold":
                PlayerData.GetInst().GetGold(1000);
                break;
            case "Gold":
                PlayerData.GetInst().GetGold(10000);
                break;
            case "skp":
                PlayerData.GetInst().lastSkillPoint++;
                PlayerData.GetInst().totalSkillPoint++;
                SingletonObject<FightMedia>.GetInst().RefreshHint();
                break;
            case "ct":
                PlayerData.GetInst().conditions[1100] = true;
                break;
            case "nct":
                PlayerData.GetInst().conditions[1100] = false;
                break;
            default:
                break;
        }

        try
        {
            if (s.Split(' ')[0] == "!")
                if (s.Split(' ')[1] != null)
                    Main.instance.StartCoroutine(Main.instance.EnterScene(s.Split(' ')[1]));

            if (s.Split(' ')[0] == "#")
                if (s.Split(' ')[1] != null)
                    if (CharLoader.dataTotal.ContainsKey(Convert.ToInt32(s.Split(' ')[1])) && Convert.ToInt32(s.Split(' ')[1]) < 10000)
                        MyPlayer.instance.StartBattle(Convert.ToInt32(s.Split(' ')[1]), " ");
        }
        catch
        {

        }

        Close();

        enable = false;
    }

    public override void Open()
    {
        base.Open();
        enable = true;
        input.ActivateInputField();
    }

    public override void Update()
    {
        base.Update();
    }
}
