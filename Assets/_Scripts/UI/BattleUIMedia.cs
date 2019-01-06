using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIMedia : BaseMedia
{
    private GameObject turnHintObj;
    private Text turnHint;
    private GameObject battleDescribe;
    private Text battleDescribeText;
    private GameObject charInfo;
    private Button charInfoClose;
    private Text roundText;
    private Button fasterButton;
    public Button autoFightButton;

    public override void Init()
    {
        base.Init();

        turnHintObj = panel.Find("TurnHint").gameObject;
        turnHint = panel.Find("TurnHint").GetComponentInChildren<Text>();
        battleDescribe = panel.Find("BattleDescribe").gameObject;
        battleDescribeText = panel.Find("BattleDescribe").GetComponentInChildren<Text>();
        charInfo = panel.Find("CharInfo").gameObject;
        charInfoClose = charInfo.GetComponentInChildren<Button>();
        roundText = panel.Find("RoundText").GetComponent<Text>();
        fasterButton = panel.Find("2XSpeed").GetComponent<Button>();
        autoFightButton = panel.Find("AutoFight").GetComponent<Button>();

        charInfoClose.onClick.RemoveAllListeners();
        charInfoClose.onClick.AddListener(() =>
        {
            charInfo.SetActive(false);
        });

        fasterButton.onClick.RemoveAllListeners();
        fasterButton.onClick.AddListener(() =>
        {
            if(Time.timeScale == 1.0f)
            {
                Time.timeScale = 2.0f;
                fasterButton.GetComponentInChildren<Text>().color = Color.green;
            }
            else
            {
                Time.timeScale = 1.0f;
                fasterButton.GetComponentInChildren<Text>().color = Color.white;
            }
        });

        autoFightButton.onClick.RemoveAllListeners();
        autoFightButton.onClick.AddListener(() =>
        {
            Battle.instance.autoFight = !Battle.instance.autoFight;

            if (Battle.instance.autoFight)
                autoFightButton.GetComponentInChildren<Text>().color = Color.green;
            else
                autoFightButton.GetComponentInChildren<Text>().color = Color.white;
        });

        SetRoundText(0);
    }

    public override void Open()
    {
        turnHintObj.SetActive(false);
        battleDescribe.SetActive(false);

        fasterButton.gameObject.SetActive(true);
        autoFightButton.gameObject.SetActive(true);

        fasterButton.GetComponentInChildren<Text>().color = Color.white;
        autoFightButton.GetComponentInChildren<Text>().color = Color.white;

        if(GameSettings.language == Language.English)
        {
            fasterButton.GetComponentInChildren<Text>().text = "Speed 2X";
            autoFightButton.GetComponentInChildren<Text>().text = "Auto Fight";
        }
        else
        {
            fasterButton.GetComponentInChildren<Text>().text = "两倍速战斗";
            autoFightButton.GetComponentInChildren<Text>().text = "自动战斗";
        }

        base.Open();
    }

    public void SetBattleInfo(string name, string skillname)
    {
        battleDescribe.SetActive(true);
        battleDescribeText.text = GameSettings.language == Language.English ? name + "<color=#ff00000> Using : </color>" + skillname : name + "<color=#ff00000> 正在使用 : </color>" + skillname;
    }

    public void CloseBattleInfo()
    {
        battleDescribe.SetActive(false);
    }

    public void SetTurnInfo(string name, bool enemy = false)
    {
        turnHintObj.SetActive(true);
        turnHint.text = GameSettings.language == Language.English ? name + "'s Turn" : name + "的回合";

        if (enemy)
            turnHint.color = Color.red;
        else
            turnHint.color = Color.green;
    }

    public void CloseTurnInfo()
    {
        turnHintObj.SetActive(false);
    }

    public void SetRoundText(int roundNum)
    {
        roundText.text = GameSettings.language == Language.English ? "Current Round : " + roundNum.ToString() + "/100" : "当前回合 : " + roundNum.ToString() + "/100";
        roundText.color = roundNum >= 90 ? Color.red : Color.white;
    }

    public void OpenCharInfo(CharacterB charB)
    {
        charInfo.SetActive(true);

        if(GameSettings.language == Language.English)
        {
            charInfo.transform.Find("Describe/Texts/Name").GetComponent<Text>().text = charB.name;
            charInfo.transform.Find("Describe/Texts/Type").GetComponent<Text>().text = charB.charType.ToString();
            charInfo.transform.Find("Describe/Texts/ActionLoading").GetComponent<Text>().text = "Action Loading : " + charB.actionLoading.ToString("F0") + "%";
            charInfo.transform.Find("Describe/Texts/Hp").GetComponent<Text>().text = "Health Point : " + charB.attriRuntime.tHp.ToString("F0") + "/" + charB.attriItems.tHp.ToString("F0");
            charInfo.transform.Find("Describe/Texts/Mp").GetComponent<Text>().text = "Mana Point : " + charB.attriRuntime.tMp.ToString("F0") + "/" + charB.attriItems.tMp.ToString("F0");
            charInfo.transform.Find("Describe/Texts/Atk").GetComponent<Text>().text = "Attack : " + charB.attriRuntime.tAtk.ToString("F0");
            charInfo.transform.Find("Describe/Texts/Def").GetComponent<Text>().text = "Defense : " + charB.attriRuntime.tDef.ToString("F0");
            charInfo.transform.Find("Describe/Texts/Spd").GetComponent<Text>().text = "Speed : " + charB.attriRuntime.tSpd.ToString("F2");
            charInfo.transform.Find("Describe/Texts/Lucky").GetComponent<Text>().text = "Lucky : " + charB.attriRuntime.lucky.ToString("F0");
            charInfo.transform.Find("Describe/Texts/Crit").GetComponent<Text>().text = "Critical : " + charB.attriRuntime.crit.ToString("F2") + "%";
            charInfo.transform.Find("Describe/Texts/Cdmg").GetComponent<Text>().text = "Critical Damage : " + charB.attriRuntime.cdmg.ToString("F2") + "%";
            charInfo.transform.Find("Describe/Texts/IgDef").GetComponent<Text>().text = "Armor Penetration : " + charB.attriRuntime.armorPntr.ToString("F2") + "%";
            charInfo.transform.Find("Describe/Texts/MpUsede").GetComponent<Text>().text = "Mana Point Cost " + charB.attriRuntime.mpUsede.ToString("- #.##;+ #.##;0") + "%";
            charInfo.transform.Find("Describe/Texts/Reflect").GetComponent<Text>().text = "Reflect : " + charB.attriRuntime.reflect.ToString("F2") + "%";
            charInfo.transform.Find("Describe/Texts/AtkHeal").GetComponent<Text>().text = "Life Steal : " + charB.attriRuntime.lifeSteal.ToString("F2") + "%";
            charInfo.transform.Find("Describe/Texts/PDmg").GetComponent<Text>().text = "Total Damage : " + charB.attriRuntime.pDmg.ToString("+ #.##;- #.##;0") + "%";
            charInfo.transform.Find("Describe/Texts/RpDmg").GetComponent<Text>().text = "Total Receive Damage : " + charB.attriRuntime.rpDmgde.ToString("- #.##;+ #.##;0") + "%";
        }
        else
        {
            charInfo.transform.Find("Describe/Texts/Name").GetComponent<Text>().text = charB.cnName;
            charInfo.transform.Find("Describe/Texts/Type").GetComponent<Text>().text = ((CharacterTypeCN)charB.charType).ToString();
            charInfo.transform.Find("Describe/Texts/ActionLoading").GetComponent<Text>().text = "攻击准备进度 : " + charB.actionLoading.ToString("F0") + "%";
            charInfo.transform.Find("Describe/Texts/Hp").GetComponent<Text>().text = "生命值 : " + charB.attriRuntime.tHp.ToString("F0") + "/" + charB.attriItems.tHp.ToString("F0");
            charInfo.transform.Find("Describe/Texts/Mp").GetComponent<Text>().text = "法力值 : " + charB.attriRuntime.tMp.ToString("F0") + "/" + charB.attriItems.tMp.ToString("F0");
            charInfo.transform.Find("Describe/Texts/Atk").GetComponent<Text>().text = "攻击力 : " + charB.attriRuntime.tAtk.ToString("F0");
            charInfo.transform.Find("Describe/Texts/Def").GetComponent<Text>().text = "防御力 : " + charB.attriRuntime.tDef.ToString("F0");
            charInfo.transform.Find("Describe/Texts/Spd").GetComponent<Text>().text = "速度 : " + charB.attriRuntime.tSpd.ToString("F2");
            charInfo.transform.Find("Describe/Texts/Lucky").GetComponent<Text>().text = "幸运值 : " + charB.attriRuntime.lucky.ToString("F0");
            charInfo.transform.Find("Describe/Texts/Crit").GetComponent<Text>().text = "暴击几率 : " + charB.attriRuntime.crit.ToString("F2") + "%";
            charInfo.transform.Find("Describe/Texts/Cdmg").GetComponent<Text>().text = "暴击伤害 : " + charB.attriRuntime.cdmg.ToString("F2") + "%";
            charInfo.transform.Find("Describe/Texts/IgDef").GetComponent<Text>().text = "护甲穿透 : " + charB.attriRuntime.armorPntr.ToString("F2") + "%";
            charInfo.transform.Find("Describe/Texts/MpUsede").GetComponent<Text>().text = "法力消耗 " + charB.attriRuntime.mpUsede.ToString("- #.##;+ #.##;0") + "%";
            charInfo.transform.Find("Describe/Texts/Reflect").GetComponent<Text>().text = "伤害反弹 : " + charB.attriRuntime.reflect.ToString("F2") + "%";
            charInfo.transform.Find("Describe/Texts/AtkHeal").GetComponent<Text>().text = "生命偷取 : " + charB.attriRuntime.lifeSteal.ToString("F2") + "%";
            charInfo.transform.Find("Describe/Texts/PDmg").GetComponent<Text>().text = "总伤害输出 " + charB.attriRuntime.pDmg.ToString("+ #.##;- #.##;0") + "%";
            charInfo.transform.Find("Describe/Texts/RpDmg").GetComponent<Text>().text = "总承受伤害 " + charB.attriRuntime.rpDmgde.ToString("- #.##;+ #.##;0") + "%";
        }

        if (charB.attriRuntime.tAtk == charB.attriItems.tAtk)
            charInfo.transform.Find("Describe/Texts/Atk").GetComponent<Text>().color = Color.white;
        else if (charB.attriRuntime.tAtk >= charB.attriItems.tAtk)
            charInfo.transform.Find("Describe/Texts/Atk").GetComponent<Text>().color = Color.green;
        else
            charInfo.transform.Find("Describe/Texts/Atk").GetComponent<Text>().color = Color.red;

        if (charB.attriRuntime.tDef == charB.attriItems.tDef)
            charInfo.transform.Find("Describe/Texts/Def").GetComponent<Text>().color = Color.white;
        else if (charB.attriRuntime.tDef >= charB.attriItems.tDef)
            charInfo.transform.Find("Describe/Texts/Def").GetComponent<Text>().color = Color.green;
        else
            charInfo.transform.Find("Describe/Texts/Def").GetComponent<Text>().color = Color.red;

        if (charB.attriRuntime.tSpd == charB.attriItems.tSpd)
            charInfo.transform.Find("Describe/Texts/Spd").GetComponent<Text>().color = Color.white;
        else if (charB.attriRuntime.tSpd >= charB.attriItems.tSpd)
            charInfo.transform.Find("Describe/Texts/Spd").GetComponent<Text>().color = Color.green;
        else
            charInfo.transform.Find("Describe/Texts/Spd").GetComponent<Text>().color = Color.red;
    }
}