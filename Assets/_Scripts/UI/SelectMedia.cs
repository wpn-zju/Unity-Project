using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectMedia : BaseMedia
{
    private GameObject page1;
    private GameObject page2;
    private Button attack;
    private Button warrior;
    private Button magic;
    private Button archer;
    private Button runes;
    private Button page1Return;
    private Button page2Return;

    private GameObject skillButtonPrefab;

    private bool playerSetChoice = false;
    private int pageId;
    private int skillId;

    public CharacterB aimObj; // the aim object;

    public IEnumerator StartPlayerAction()
    {
        playerSetChoice = false;

        Open();

        while (!playerSetChoice)
        {
            yield return null;
        }

        yield return Battle.instance.StartCoroutine(CommonHandle.Acting(pageId, skillId));
    }

    public override void Init()
    {
        base.Init();
        page1 = panel.Find("Select1Panel").gameObject;
        page2 = panel.Find("Select2Panel").gameObject;
        attack = page1.transform.Find("Attack").GetComponent<Button>();
        warrior = page1.transform.Find("Warrior").GetComponent<Button>();
        magic = page1.transform.Find("Magic").GetComponent<Button>();
        archer = page1.transform.Find("Archer").GetComponent<Button>();
        runes = page1.transform.Find("Runes").GetComponent<Button>();
        page1Return = page2.transform.Find("Return").GetComponent<Button>();
        page2Return = panel.Find("Page2Return").GetComponent<Button>();
        skillButtonPrefab = Resources.Load<GameObject>("MyResource/Panel/SkillButton");

        page1.SetActive(true);
        page2.SetActive(false);
        page2Return.gameObject.SetActive(false);
    }

    public override void Open()
    {
        base.Open();
        page1.SetActive(true);
        page2.SetActive(false);
        page2Return.gameObject.SetActive(false);

        attack.onClick.RemoveAllListeners();
        attack.onClick.AddListener(() =>
        {
            pageId = 1;
            skillId = 0;

            page1.SetActive(false);
            page2.SetActive(false);
            page2Return.gameObject.SetActive(true);

            StartChooseAimObj();
        });

        warrior.onClick.RemoveAllListeners();
        warrior.onClick.AddListener(() =>
        {
            pageId = 2;

            page1.SetActive(false);
            page2.SetActive(true);
            page2Return.gameObject.SetActive(false);

            OpenSkillSelect();
        });

        magic.onClick.RemoveAllListeners();
        magic.onClick.AddListener(() =>
        {
            pageId = 3;

            page1.SetActive(false);
            page2.SetActive(true);
            page2Return.gameObject.SetActive(false);

            OpenSkillSelect();
        });

        archer.onClick.RemoveAllListeners();
        archer.onClick.AddListener(() =>
        {
            pageId = 4;

            page1.SetActive(false);
            page2.SetActive(true);
            page2Return.gameObject.SetActive(false);

            OpenSkillSelect();
        });

        runes.onClick.RemoveAllListeners();
        runes.onClick.AddListener(() =>
        {
            pageId = 5;

            page1.SetActive(false);
            page2.SetActive(true);
            page2Return.gameObject.SetActive(false);

            OpenSkillSelect();
        });

        page1Return.onClick.RemoveAllListeners();
        page1Return.onClick.AddListener(() =>
        {
            page1.SetActive(true);
            page2.SetActive(false);
            page2Return.gameObject.SetActive(false);
        });

        page2Return.onClick.RemoveAllListeners();
        page2Return.onClick.AddListener(() =>
        {
            switch (pageId)
            {
                case 1:
                    page1.SetActive(true);
                    page2.SetActive(false);
                    break;
                case 2:
                    page1.SetActive(false);
                    page2.SetActive(true);
                    break;
                case 3:
                    page1.SetActive(false);
                    page2.SetActive(true);
                    break;
                case 4:
                    page1.SetActive(false);
                    page2.SetActive(true);
                    break;
                case 5:
                    page1.SetActive(false);
                    page2.SetActive(true);
                    break;
                case 6:
                    page1.SetActive(true);
                    page2.SetActive(false);
                    break;
            }

            page2Return.gameObject.SetActive(false);
            CameraRayCast.instance.SetChoosing(false, false);
        });
    }

    private void OpenSkillSelect()
    {
        for (int i = 0; i < page2.transform.Find("Skills/Skill").childCount; ++i)
            GameObject.Destroy(page2.transform.Find("Skills/Skill").GetChild(i).gameObject);

        if (pageId == 2 || pageId == 3 || pageId == 4)
        {
            for (int i = 0; i < 5; ++i)
            {
                int tempInt = i;
                if (PlayerData.GetInst().skillData[(pageId - 2) * 5 + i] == 1)
                {
                    // todo : long click to show information about skills
                    GameObject temp = GameObject.Instantiate(skillButtonPrefab, page2.transform.Find("Skills/Skill"));
                    temp.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? SkillInfoLoader.data[3 * ((pageId - 2) * 5 + tempInt) + 1].name : SkillInfoLoader.data[3 * ((pageId - 2) * 5 + tempInt) + 1].cnName;
                    temp.GetComponent<Button>().onClick.RemoveAllListeners();
                    temp.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        skillId = tempInt;

                        if (Check())
                        {
                            page1.SetActive(false);
                            page2.SetActive(false);

                            StartChooseAimObj();
                        }
                    });
                }
            }
        }
        else if (pageId == 5)
        {
            for (int i = 0; i < ItemManager.GetInst().runeData.Length; ++i)
            {
                if (ItemManager.GetInst().runeData[i] != null)
                {
                    if (RuneLoader.data[ItemManager.GetInst().runeData[i].id_int % 10000].type == RuneType.active)
                    {
                        RuneData rune = RuneLoader.data[ItemManager.GetInst().runeData[i].id_int % 10000];

                        GameObject temp = GameObject.Instantiate(skillButtonPrefab, page2.transform.Find("Skills/Skill"));
                        temp.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? ItemManager.GetInst().runeData[i].name : ItemManager.GetInst().runeData[i].cnName;
                        temp.GetComponent<Button>().onClick.RemoveAllListeners();
                        temp.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            skillId = Convert.ToInt32(rune.data[0]);

                            if (Check())
                            {
                                page1.SetActive(false);
                                page2.SetActive(false);

                                StartChooseAimObj();
                            }
                        });
                    }
                }
            }
        }
    }

    // check if the choosen skill can be cast
    private bool Check()
    {
        switch(pageId)
        {
            case 2:
                return WarriorSkillHandle.Check(skillId);
            case 3:
                return MagicSkillHandle.Check(skillId);
            case 4:
                return ArcherSkillHandle.Check(skillId);
            case 5:
                return RuneHandle.Check(skillId);
            default:
                return false;
        }
    }

    private void StartChooseAimObj()
    {
        bool aoe = false;
        bool toself = false;

        switch(pageId)
        {
            case 2:
                aoe = WarriorSkillHandle.IsAoe(skillId);
                toself = WarriorSkillHandle.ToSelf(skillId);
                break;
            case 3:
                aoe = MagicSkillHandle.IsAoe(skillId);
                toself = MagicSkillHandle.ToSelf(skillId);
                break;
            case 4:
                aoe = ArcherSkillHandle.IsAoe(skillId);
                toself = ArcherSkillHandle.ToSelf(skillId);
                break;
            case 5:
                aoe = RuneHandle.IsAoe(skillId);
                toself = RuneHandle.ToSelf(skillId);
                break;
        }

        // judge aoe and self skills
        if(toself)
        {
            if (aoe)
            {
                aimObj = null;

                SendChoice();
            }
            else
            {
                page2Return.gameObject.SetActive(true);

                CameraRayCast.instance.SetChoosing(true, true);
            }
        }
        else
        {
            if (aoe)
            {
                aimObj = null;

                SendChoice();
            }
            else
            {
                page2Return.gameObject.SetActive(true);

                CameraRayCast.instance.SetChoosing(true, false);
            }
        }
    }

    public void SendChoice()
    {
        playerSetChoice = true;

        Close();
    }
}
