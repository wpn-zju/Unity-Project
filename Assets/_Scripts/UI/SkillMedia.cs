using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMedia : BaseMedia
{
    private int skillId;

    private GameObject page1;
    private GameObject page2;
    private Transform skillButtonBar;
    private Button quitB;
    private Button resetB;
    private Text skillPointText;

    private Image skillImg;
    private Image subskill1Img;
    private Image subskill2Img;
    private Button skillUnlock;
    private Button subskillUnlock1;
    private Button subskillUnlock2;
    private Text skillDescribe;
    private Text subskillDescribe1;
    private Text subskillDescribe2;
    private Button cancelB;

    private Text textW;
    private Text textM;
    private Text textA;

    public override void Init()
    {
        base.Init();

        page1 = panel.Find("Page1").gameObject;
        page2 = panel.Find("Page2").gameObject;
        skillButtonBar = page1.transform.Find("SkillButton");
        quitB = page1.transform.Find("Exit").GetComponent<Button>();
        resetB = page1.transform.Find("Reset").GetComponent<Button>();
        skillPointText = page1.transform.Find("SkillPoint").GetComponentInChildren<Text>();

        skillImg = page2.transform.Find("Skill/Skill").GetComponent<Image>();
        subskill1Img = page2.transform.Find("SubSkill1/SubSkill1").GetComponent<Image>();
        subskill2Img = page2.transform.Find("SubSkill2/SubSkill2").GetComponent<Image>();
        skillUnlock = page2.transform.Find("Skill/SkillUnlock").GetComponent<Button>();
        subskillUnlock1 = page2.transform.Find("SubSkill1/SubSkill1Unlock").GetComponent<Button>();
        subskillUnlock2 = page2.transform.Find("SubSkill2/SubSkill2Unlock").GetComponent<Button>();
        skillDescribe = page2.transform.Find("Skill/SkillDescribe").GetComponent<Text>();
        subskillDescribe1 = page2.transform.Find("SubSkill1/SubSkill1Describe").GetComponent<Text>();
        subskillDescribe2 = page2.transform.Find("SubSkill2/SubSkill2Describe").GetComponent<Text>();
        cancelB = page2.transform.Find("Cancel").GetComponent<Button>();

        textW = page1.transform.Find("TextW").GetComponent<Text>();
        textM = page1.transform.Find("TextM").GetComponent<Text>();
        textA = page1.transform.Find("TextA").GetComponent<Text>();

        quitB.onClick.RemoveAllListeners();
        quitB.onClick.AddListener(() =>
        {
            Close();
        });

        cancelB.onClick.RemoveAllListeners();
        cancelB.onClick.AddListener(() =>
        {
            page2.SetActive(false);
            page1.SetActive(true);

            RefreshPage1();
        });

        skillUnlock.onClick.RemoveAllListeners();
        skillUnlock.onClick.AddListener(() =>
        {
            UnlockSkill();
        });

        subskillUnlock1.onClick.RemoveAllListeners();
        subskillUnlock1.onClick.AddListener(() =>
        {
            UnlockSubSkill(1);
        });

        subskillUnlock2.onClick.RemoveAllListeners();
        subskillUnlock2.onClick.AddListener(() =>
        {
            UnlockSubSkill(2);
        });

        resetB.onClick.RemoveAllListeners();
        resetB.onClick.AddListener(() =>
        {
            if (PlayerData.GetInst().gold < 100)
            {
                if(GameSettings.language == Language.English)
                    SingletonObject<HintMedia>.GetInst().SendHint("Lack of enough Gold.", Color.red);
                else
                    SingletonObject<HintMedia>.GetInst().SendHint("金币不足", Color.red);
            }
            else
            {
                PlayerData.GetInst().GetGold(-100);
                PlayerData.GetInst().ResetSkill();
                skillPointText.text = GameSettings.language == Language.English ? "Skill Point : " + PlayerData.GetInst().lastSkillPoint.ToString() + "/" + PlayerData.GetInst().totalSkillPoint.ToString() : "技能点 : " + PlayerData.GetInst().lastSkillPoint.ToString() + "/" + PlayerData.GetInst().totalSkillPoint.ToString();
            }

            RefreshPage1();
        });

        for (int i = 0; i < skillButtonBar.childCount; ++i)
        {
            int tempInt = i;
            skillButtonBar.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
            skillButtonBar.GetChild(i).GetComponent<Button>().onClick.AddListener(() =>
            {
                skillId = tempInt;

                OpenSubSkillSelect();
            });
        }
    }

    public override void Open()
    {
        base.Open();

        page1.SetActive(true);
        page2.SetActive(false);

        RefreshPage1();
    }

    public override void Close()
    {
        base.Close();

        ItemManager.GetInst().SyncItemAttributes();
    }

    private void RefreshPage1()
    {
        for (int i = 0; i < 5; ++i)
            if (PlayerData.GetInst().skillData[i] == 1)
                page1.transform.Find("SkillButton/WS" + (i + 1).ToString()).GetComponent<Image>().color = Color.green;
            else if (i % 5 != 0 && PlayerData.GetInst().skillData[i - 1] == 0)
                page1.transform.Find("SkillButton/WS" + (i + 1).ToString()).GetComponent<Image>().color = Color.red;
            else
                page1.transform.Find("SkillButton/WS" + (i + 1).ToString()).GetComponent<Image>().color = Color.white;

        for (int i = 5; i < 10; ++i)
            if (PlayerData.GetInst().skillData[i] == 1)
                page1.transform.Find("SkillButton/MS" + (i - 4).ToString()).GetComponent<Image>().color = Color.green;
            else if (i % 5 != 0 && PlayerData.GetInst().skillData[i - 1] == 0)
                page1.transform.Find("SkillButton/MS" + (i - 4).ToString()).GetComponent<Image>().color = Color.red;
            else
                page1.transform.Find("SkillButton/MS" + (i - 4).ToString()).GetComponent<Image>().color = Color.white;

        for (int i = 10; i < 15; ++i)
            if (PlayerData.GetInst().skillData[i] == 1)
                page1.transform.Find("SkillButton/AS" + (i - 9).ToString()).GetComponent<Image>().color = Color.green;
            else if (i % 5 != 0 && PlayerData.GetInst().skillData[i - 1] == 0)
                page1.transform.Find("SkillButton/AS" + (i - 9).ToString()).GetComponent<Image>().color = Color.red;
            else
                page1.transform.Find("SkillButton/AS" + (i - 9).ToString()).GetComponent<Image>().color = Color.white;

        int wLevel = 0, mLevel = 0, aLevel = 0;
        for (int i = 0; i < 5; ++i)
            if (PlayerData.GetInst().skillData[i] == 1) wLevel++;
            else break;
        for (int i = 5; i < 10; ++i)
            if (PlayerData.GetInst().skillData[i] == 1) mLevel++;
            else break;
        for (int i = 10; i < 15; ++i)
            if (PlayerData.GetInst().skillData[i] == 1) aLevel++;
            else break;

        if(GameSettings.language == Language.English)
        {
            textW.text = "Basic Defense + " + (wLevel * 5).ToString() + "/25 %";
            textM.text = "Basic Attack + " + (mLevel * 5).ToString() + "/25 %";
            textA.text = "Basic Speed + " + (aLevel * 5).ToString() + "/25 %";
        }
        else
        {
            textW.text = "基础防御力 + " + (wLevel * 5).ToString() + "/25 %";
            textM.text = "基础攻击力 + " + (mLevel * 5).ToString() + "/25 %";
            textA.text = "基础速度 + " + (aLevel * 5).ToString() + "/25 %";
        }

        skillPointText.text = GameSettings.language == Language.English ? "Skill Point : " + PlayerData.GetInst().lastSkillPoint.ToString() + "/" + PlayerData.GetInst().totalSkillPoint.ToString() : "技能点 : " + PlayerData.GetInst().lastSkillPoint.ToString() + "/" + PlayerData.GetInst().totalSkillPoint.ToString();
    }

    private void RefreshPage2()
    {

        if (PlayerData.GetInst().skillData[skillId] == 1)
            skillImg.color = Color.green;
        else if (skillId % 5 != 0 && PlayerData.GetInst().skillData[skillId - 1] == 0)
            skillImg.color = Color.red;
        else
            skillImg.color = Color.white;

        if (PlayerData.GetInst().skillData[skillId] == 0)
        {
            subskill1Img.color = Color.red;
            subskill2Img.color = Color.red;
        }
        else if (PlayerData.GetInst().branchData[skillId] == 0)
        {
            subskill1Img.color = Color.white;
            subskill2Img.color = Color.white;
        }
        else if (PlayerData.GetInst().branchData[skillId] == 1)
        {
            subskill1Img.color = Color.green;
            subskill2Img.color = Color.red;
        }
        else if (PlayerData.GetInst().branchData[skillId] == 2)
        {
            subskill1Img.color = Color.red;
            subskill2Img.color = Color.green;
        }
    }

    private void OpenSubSkillSelect()
    {
        // change icon image branch image and describes
        page1.SetActive(false);
        page2.SetActive(true);

        skillImg.sprite = AtlasManager.GetInst().GetSkillBtnIcon(SkillInfoLoader.data[skillId * 3 + 1].iconPath);
        subskill1Img.sprite = AtlasManager.GetInst().GetSkillBtnIcon(SkillInfoLoader.data[skillId * 3 + 2].iconPath);
        subskill2Img.sprite = AtlasManager.GetInst().GetSkillBtnIcon(SkillInfoLoader.data[skillId * 3 + 3].iconPath);

        if(GameSettings.language == Language.English)
        {
            skillDescribe.text = SkillInfoLoader.data[skillId * 3 + 1].name + "\n\n" + SkillInfoLoader.data[skillId * 3 + 1].describe;
            subskillDescribe1.text = SkillInfoLoader.data[skillId * 3 + 2].name + "\n\n" + SkillInfoLoader.data[skillId * 3 + 2].describe;
            subskillDescribe2.text = SkillInfoLoader.data[skillId * 3 + 3].name + "\n\n" + SkillInfoLoader.data[skillId * 3 + 3].describe;
        }
        else
        {
            skillDescribe.text = SkillInfoLoader.data[skillId * 3 + 1].cnName + "\n\n" + SkillInfoLoader.data[skillId * 3 + 1].cnDescribe;
            subskillDescribe1.text = SkillInfoLoader.data[skillId * 3 + 2].cnName + "\n\n" + SkillInfoLoader.data[skillId * 3 + 2].cnDescribe;
            subskillDescribe2.text = SkillInfoLoader.data[skillId * 3 + 3].cnName + "\n\n" + SkillInfoLoader.data[skillId * 3 + 3].cnDescribe;
        }

        RefreshPage2();
    }

    private void UnlockSkill()
    {
        int skillType = skillId / 5; // 0, 1, 2
        int skillLevel = skillId % 5; // 0, 1, 2, 3, 4

        if (PlayerData.GetInst().skillData[skillId] == 1)
        {
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Already Learned.", Color.red);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("此技能已解锁", Color.red);
            return;
        }

        if (skillLevel != 0 && PlayerData.GetInst().skillData[skillId - 1] == 0)
        {
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Please Learn Lower Level Skills.", Color.red);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("请先学习低等级技能", Color.red);
            return;
        }

        if (PlayerData.GetInst().lastSkillPoint > 0)
        {
            PlayerData.GetInst().lastSkillPoint--;
            PlayerData.GetInst().skillData[skillId] = 1;
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Skill Unlocked.", Color.green);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("技能已解锁", Color.green);
            SingletonObject<FightMedia>.GetInst().RefreshHint();
        }
        else
        {
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("No available skill points.", Color.red);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("技能点不足", Color.red);
        }

        RefreshPage2();
    }

    private void UnlockSubSkill(int subId)
    {
        if (PlayerData.GetInst().skillData[skillId] == 0)
        {
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Please learn main skill first.", Color.red);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("请先解锁主技能", Color.red);
            return;
        }

        if (PlayerData.GetInst().branchData[skillId] == 0)
        {
            PlayerData.GetInst().branchData[skillId] = subId;
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("SubSkill unlocked.", Color.green);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("被动天赋已解锁", Color.green);
        }
        else
        {
            if (PlayerData.GetInst().branchData[skillId] == subId)
                if (GameSettings.language == Language.English)
                    SingletonObject<HintMedia>.GetInst().SendHint("Already learned.", Color.red);
                else
                    SingletonObject<HintMedia>.GetInst().SendHint("此被动天赋已解锁", Color.red);
            else
                if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Already learned another subSkill, please reset first.", Color.red);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("同技能的另一个被动天赋已解锁，请先重置", Color.red);
        }

        RefreshPage2();
    }
}