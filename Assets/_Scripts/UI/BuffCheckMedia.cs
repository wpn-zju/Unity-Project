using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffCheckMedia : BaseMedia
{
    private Buff buffInDisplay;

    private GameObject buffObj;
    private Text describeText;
    private Text levelText;
    private Text durationText;

    public override void Init()
    {
        base.Init();
        buffObj = panel.Find("Buff").gameObject;
        describeText = panel.Find("Describe").GetComponent<Text>();
        levelText = panel.Find("Level").GetComponent<Text>();
        durationText = panel.Find("Duration").GetComponent<Text>();
    }

    public override void Open()
    {
        base.Open();

        if (buffInDisplay.infinity)
            buffObj.transform.Find("Duration & Times").gameObject.SetActive(false);
        else
            buffObj.transform.Find("Duration & Times").GetComponent<Text>().text = buffInDisplay.lastRounds.ToString();

        if (buffInDisplay.addable)
            buffObj.transform.Find("Level").GetComponent<Text>().text = buffInDisplay.level.ToString();
        else
            buffObj.transform.Find("Level").gameObject.SetActive(false);

        // change image
        buffObj.GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(buffInDisplay.iconPath);

        if (GameSettings.language == Language.English)
        {
            describeText.text = buffInDisplay.name + "\n" + buffInDisplay.describe;
            durationText.text = "Last Rounds : " + buffInDisplay.lastRounds.ToString();
            levelText.text = "Buff Level : " + buffInDisplay.level.ToString();
        }
        else
        {
            describeText.text = buffInDisplay.cnName + "\n" + buffInDisplay.cnDescribe;
            durationText.text = "剩余回合数或次数 : " + buffInDisplay.lastRounds.ToString();
            levelText.text = "Buff等级 : " + buffInDisplay.level.ToString();
        }
    }

    public void SetBuff(Buff buff)
    {
        buffInDisplay = buff;
    }
}