using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileMedia : BaseMedia
{
    public bool isSave = false;

    private GameObject profile1;
    private GameObject profile2;
    private GameObject profile3;
    private Button selectCancel;

    private GameObject checkView;
    private Button checkExit;
    private Button checkEnter;
    private GameObject warning1; // save Warning
    private GameObject warning2; // load Warning

    public GameObject hintPage;

    public override void Init()
    {
        base.Init();

        checkView = panel.Find("CheckView").gameObject;
        checkExit = checkView.transform.Find("Exit").GetComponent<Button>();
        checkEnter = checkView.transform.Find("NewGame").GetComponent<Button>();
        warning1 = checkView.transform.Find("Warning1").gameObject;
        warning2 = checkView.transform.Find("Warning2").gameObject;
        hintPage = panel.Find("HintPage").gameObject;

        checkExit.onClick.RemoveAllListeners();
        checkExit.onClick.AddListener(() =>
        {
            checkView.SetActive(false);
        });

        checkEnter.onClick.RemoveAllListeners();
        checkEnter.onClick.AddListener(() =>
        {
            checkView.SetActive(false);

            if (isSave)
                Main.instance.StartCoroutine(DataIO.Save());
            else
            {
                Close();
                SingletonObject<MainMenuMedia>.GetInst().Close();
                DataIO.Load();
            }
        });

        profile1 = panel.Find("Scroll/Content/Data1").gameObject;
        profile2 = panel.Find("Scroll/Content/Data2").gameObject;
        profile3 = panel.Find("Scroll/Content/Data3").gameObject;
        selectCancel = panel.Find("Scroll/Content/Exit").GetComponent<Button>();

        selectCancel.onClick.RemoveAllListeners();
        selectCancel.onClick.AddListener(() =>
        {
            Close();
        });

        profile1.GetComponent<Button>().onClick.RemoveAllListeners();
        profile1.GetComponent<Button>().onClick.AddListener(() =>
        {
            DataIO.saveId = 1;

            if (isSave)
            {
                if (DataIO.hasProfile(1))
                {
                    checkView.SetActive(true);
                    warning1.SetActive(true);
                    warning2.SetActive(false);
                }
                else
                    Main.instance.StartCoroutine(DataIO.Save());
            }
            else
            {
                if (DataIO.hasProfile(1))
                {
                    if (Main.instance.sceneManager == null)
                    {
                        Close();
                        SingletonObject<MainMenuMedia>.GetInst().Close();
                        DataIO.Load();
                    }
                    else
                    {
                        checkView.SetActive(true);
                        warning1.SetActive(false);
                        warning2.SetActive(true);
                    }
                }
                else
                {
                    if (GameSettings.language == Language.English)
                        SingletonObject<HintMedia>.GetInst().SendHint("No Profile 1", Color.red);
                    else
                        SingletonObject<HintMedia>.GetInst().SendHint("存档 1 不存在", Color.red);
                }
            }
        });

        profile2.GetComponent<Button>().onClick.RemoveAllListeners();
        profile2.GetComponent<Button>().onClick.AddListener(() =>
        {
            DataIO.saveId = 2;

            if (isSave)
            {
                if (DataIO.hasProfile(2))
                {
                    checkView.SetActive(true);
                    warning1.SetActive(true);
                    warning2.SetActive(false);
                }
                else
                    Main.instance.StartCoroutine(DataIO.Save());
            }
            else
            {
                if (DataIO.hasProfile(2))
                {
                    if (Main.instance.sceneManager == null)
                    {
                        Close();
                        SingletonObject<MainMenuMedia>.GetInst().Close();
                        DataIO.Load();
                    }
                    else
                    {
                        checkView.SetActive(true);
                        warning1.SetActive(false);
                        warning2.SetActive(true);
                    }
                }
                else
                {
                    if (GameSettings.language == Language.English)
                        SingletonObject<HintMedia>.GetInst().SendHint("No Profile 2", Color.red);
                    else
                        SingletonObject<HintMedia>.GetInst().SendHint("存档 2 不存在", Color.red);
                }
            }
        });

        profile3.GetComponent<Button>().onClick.RemoveAllListeners();
        profile3.GetComponent<Button>().onClick.AddListener(() =>
        {
            DataIO.saveId = 3;

            if (isSave)
            {
                if (DataIO.hasProfile(3))
                {
                    checkView.SetActive(true);
                    warning1.SetActive(true);
                    warning2.SetActive(false);
                }
                else
                    Main.instance.StartCoroutine(DataIO.Save());
            }
            else
            {
                if (DataIO.hasProfile(3))
                {
                    if (Main.instance.sceneManager == null)
                    {
                        Close();
                        SingletonObject<MainMenuMedia>.GetInst().Close();
                        DataIO.Load();
                    }
                    else
                    {
                        checkView.SetActive(true);
                        warning1.SetActive(false);
                        warning2.SetActive(true);
                    }
                }
                else
                {
                    if (GameSettings.language == Language.English)
                        SingletonObject<HintMedia>.GetInst().SendHint("No Profile 3", Color.red);
                    else
                        SingletonObject<HintMedia>.GetInst().SendHint("存档 3 不存在", Color.red);
                }
            }
        });

        Refresh();
    }

    public void Refresh()
    {
        if (DataIO.hasProfile(1))
        {
            DataIO.saveId = 1;
            profile1.transform.Find("Image").gameObject.SetActive(true);
            profile1.transform.Find("Image").GetComponent<Image>().sprite = ES2.Load<Sprite>(DataIO.savePath + "?tag=capture");

            if (GameSettings.language == Language.English)
            {
                string text = "Profile - 1";
                System.TimeSpan ts = ES2.Load<System.TimeSpan>(DataIO.savePath + "?tag=totaltime");
                text += "\t\tTime : " + (24 * ts.Days + ts.Hours).ToString() + "h " + ts.Minutes.ToString() + "min\n";
                text += "Player Name : " + ES2.Load<string>(DataIO.savePath + "?tag=name") + "\n";
                text += "Level : " + ES2.Load<int>(DataIO.savePath + "?tag=level") + "\t\tGold : " + ES2.Load<int>(DataIO.savePath + "?tag=gold") + "\n";
                text += "Progress : " + ES2.Load<string>(DataIO.savePath + "?tag=progress");

                profile1.transform.Find("Text").GetComponent<Text>().text = text;
            }
            else
            {
                string text = "存档 - 1";
                System.TimeSpan ts = ES2.Load<System.TimeSpan>(DataIO.savePath + "?tag=totaltime");
                text += "\t\t游戏时长 : " + (24 * ts.Days + ts.Hours).ToString() + "小时 " + ts.Minutes.ToString() + "分钟\n";
                text += "玩家姓名 : " + ES2.Load<string>(DataIO.savePath + "?tag=name") + "\n";
                text += "等级 : " + ES2.Load<int>(DataIO.savePath + "?tag=level") + "\t\t金币 : " + ES2.Load<int>(DataIO.savePath + "?tag=gold") + "\n";
                text += "游戏进程 : " + ES2.Load<string>(DataIO.savePath + "?tag=progressCN");

                profile1.transform.Find("Text").GetComponent<Text>().text = text;
            }
        }
        else
        {
            profile1.transform.Find("Image").gameObject.SetActive(false);
            if (GameSettings.language == Language.English)
                profile1.transform.Find("Text").GetComponent<Text>().text = "No Data";
            else
                profile1.transform.Find("Text").GetComponent<Text>().text = "无该存档";
        }

        if (DataIO.hasProfile(2))
        {
            DataIO.saveId = 2;
            profile2.transform.Find("Image").gameObject.SetActive(true);
            profile2.transform.Find("Image").GetComponent<Image>().sprite = ES2.Load<Sprite>(DataIO.savePath + "?tag=capture");
            if (GameSettings.language == Language.English)
            {
                string text = "Profile - 2";
                System.TimeSpan ts = ES2.Load<System.TimeSpan>(DataIO.savePath + "?tag=totaltime");
                text += "\t\tTime : " + (24 * ts.Days + ts.Hours).ToString() + "h " + ts.Minutes.ToString() + "min\n";
                text += "Player Name : " + ES2.Load<string>(DataIO.savePath + "?tag=name") + "\n";
                text += "Level : " + ES2.Load<int>(DataIO.savePath + "?tag=level") + "\t\tGold : " + ES2.Load<int>(DataIO.savePath + "?tag=gold") + "\n";
                text += "Progress : " + ES2.Load<string>(DataIO.savePath + "?tag=progress");

                profile2.transform.Find("Text").GetComponent<Text>().text = text;
            }
            else
            {
                string text = "存档 - 2";
                System.TimeSpan ts = ES2.Load<System.TimeSpan>(DataIO.savePath + "?tag=totaltime");
                text += "\t\t游戏时长 : " + (24 * ts.Days + ts.Hours).ToString() + "小时 " + ts.Minutes.ToString() + "分钟\n";
                text += "玩家姓名 : " + ES2.Load<string>(DataIO.savePath + "?tag=name") + "\n";
                text += "等级 : " + ES2.Load<int>(DataIO.savePath + "?tag=level") + "\t\t金币 : " + ES2.Load<int>(DataIO.savePath + "?tag=gold") + "\n";
                text += "游戏进程 : " + ES2.Load<string>(DataIO.savePath + "?tag=progressCN");

                profile2.transform.Find("Text").GetComponent<Text>().text = text;
            }
        }
        else
        {
            profile2.transform.Find("Image").gameObject.SetActive(false);
            if (GameSettings.language == Language.English)
                profile2.transform.Find("Text").GetComponent<Text>().text = "No Data";
            else
                profile2.transform.Find("Text").GetComponent<Text>().text = "无该存档";
        }

        if (DataIO.hasProfile(3))
        {
            DataIO.saveId = 3;
            profile3.transform.Find("Image").gameObject.SetActive(true);
            profile3.transform.Find("Image").GetComponent<Image>().sprite = ES2.Load<Sprite>(DataIO.savePath + "?tag=capture");
            if (GameSettings.language == Language.English)
            {
                string text = "Profile - 3";
                System.TimeSpan ts = ES2.Load<System.TimeSpan>(DataIO.savePath + "?tag=totaltime");
                text += "\t\tTime : " + (24 * ts.Days + ts.Hours).ToString() + "h " + ts.Minutes.ToString() + "min\n";
                text += "Player Name : " + ES2.Load<string>(DataIO.savePath + "?tag=name") + "\n";
                text += "Level : " + ES2.Load<int>(DataIO.savePath + "?tag=level") + "\t\tGold : " + ES2.Load<int>(DataIO.savePath + "?tag=gold") + "\n";
                text += "Progress : " + ES2.Load<string>(DataIO.savePath + "?tag=progress");

                profile3.transform.Find("Text").GetComponent<Text>().text = text;
            }
            else
            {
                string text = "存档 - 3";
                System.TimeSpan ts = ES2.Load<System.TimeSpan>(DataIO.savePath + "?tag=totaltime");
                text += "\t\t游戏时长 : " + (24 * ts.Days + ts.Hours).ToString() + "小时 " + ts.Minutes.ToString() + "分钟\n";
                text += "玩家姓名 : " + ES2.Load<string>(DataIO.savePath + "?tag=name") + "\n";
                text += "等级 : " + ES2.Load<int>(DataIO.savePath + "?tag=level") + "\t\t金币 : " + ES2.Load<int>(DataIO.savePath + "?tag=gold") + "\n";
                text += "游戏进程 : " + ES2.Load<string>(DataIO.savePath + "?tag=progressCN");

                profile3.transform.Find("Text").GetComponent<Text>().text = text;
            }
        }
        else
        {
            profile3.transform.Find("Image").gameObject.SetActive(false);
            if (GameSettings.language == Language.English)
                profile3.transform.Find("Text").GetComponent<Text>().text = "No Data";
            else
                profile3.transform.Find("Text").GetComponent<Text>().text = "无该存档";
        }
    }
}