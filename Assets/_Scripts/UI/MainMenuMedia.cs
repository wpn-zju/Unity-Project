using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMedia : BaseMedia
{
    public delegate int TestDelegate(int x);

    private Button continueGame;
    private Button newGame;
    private Button credits;
    private Button exitGame;
    private Button settings;
    private GameObject creditsPage;
    private GameObject settingsPage;
    private Button creditsExit;
    private Button saveButton;
    private Button setNameExit;
    private Button setNameEnter;
    private InputField nameInput;
    private string inputString;
    private Button english;
    private Button chinese;
    private Language language;
    private Button showFPS;
    private GameObject quitCheck;
    private Button quitCheckY;
    private Button quitCheckN;
    private GameObject setName;

    public override void Init()
    {
        TestDelegate getint = delegate (int x) { return x; };
        TestDelegate getint2 = (int x) => { return x; };
        TestDelegate getint3 = x => { return x; };

        base.Init();
        continueGame = panel.Find("Continue").GetComponent<Button>();
        newGame = panel.Find("NewGame").GetComponent<Button>();
        credits = panel.Find("Credits").GetComponent<Button>();
        exitGame = panel.Find("Quit").GetComponent<Button>();
        settings = panel.Find("Settings").GetComponent<Button>();
        creditsPage = panel.Find("CreditsPage").gameObject;
        settingsPage = panel.Find("SettingsPage").gameObject;
        creditsExit = panel.Find("CreditsPage/Exit").GetComponent<Button>();
        saveButton = panel.Find("SettingsPage/Save").GetComponent<Button>();
        setName = panel.Find("SetName").gameObject;
        setNameExit = setName.transform.Find("Exit").GetComponent<Button>();
        setNameEnter = setName.transform.Find("EnterGame").GetComponent<Button>();
        nameInput = setName.GetComponentInChildren<InputField>();
        inputString = "";
        english = panel.Find("SettingsPage/Language/English").GetComponent<Button>();
        chinese = panel.Find("SettingsPage/Language/Chinese").GetComponent<Button>();
        language = GameSettings.language;
        showFPS = panel.Find("SettingsPage/FPS").GetComponent<Button>();
        quitCheck = settingsPage.transform.Find("QuitCheckBlocker").gameObject;
        quitCheckY = quitCheck.transform.Find("QuitCheck/Yes").GetComponent<Button>();
        quitCheckN = quitCheck.transform.Find("QuitCheck/No").GetComponent<Button>();


        continueGame.onClick.RemoveAllListeners();
        continueGame.onClick.AddListener(() =>
        {
            SingletonObject<ProfileMedia>.GetInst().isSave = false;
            SingletonObject<ProfileMedia>.GetInst().Open();
        });

        newGame.onClick.RemoveAllListeners();
        newGame.onClick.AddListener(() => 
        {
            setName.SetActive(true);
        });

        credits.onClick.RemoveAllListeners();
        credits.onClick.AddListener(() => 
        {
            creditsPage.SetActive(true);
        });

        exitGame.onClick.RemoveAllListeners();
        exitGame.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        settings.onClick.RemoveAllListeners();
        settings.onClick.AddListener(() => 
        {
            settingsPage.SetActive(true);
        });

        creditsExit.onClick.RemoveAllListeners();
        creditsExit.onClick.AddListener(() =>
        {
            creditsPage.SetActive(false);
        });

        setNameExit.onClick.RemoveAllListeners();
        setNameExit.onClick.AddListener(() =>
        {
            setName.SetActive(false);
        });

        setNameEnter.onClick.RemoveAllListeners();
        setNameEnter.onClick.AddListener(() =>
        {
            if (inputString == "")
                if (GameSettings.language == Language.English)
                    SingletonObject<HintMedia>.GetInst().SendHint("Please input your hero's name.", Color.yellow);
                else
                    SingletonObject<HintMedia>.GetInst().SendHint("请输入主角的名称", Color.yellow);
            else if (inputString.Length > 16)
                if (GameSettings.language == Language.English)
                    SingletonObject<HintMedia>.GetInst().SendHint("The name is no more than 16 characters", Color.yellow);
                else
                    SingletonObject<HintMedia>.GetInst().SendHint("主角名称不能长于16个字符", Color.yellow);
            else
            {
                PlayerData.GetInst().NewGameClear(inputString);
                Close();
            }
        });

        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(() =>
        {
            if (language != GameSettings.language)
                quitCheck.SetActive(true);
            else
            {
                GameSettings.SaveSettings(language);
                settingsPage.SetActive(false);
            }
        });

        quitCheckY.onClick.RemoveAllListeners();
        quitCheckY.onClick.AddListener(() =>
        {
            quitCheck.SetActive(false);
            GameSettings.SaveSettings(language);
            Application.Quit();
        });

        quitCheckN.onClick.RemoveAllListeners();
        quitCheckN.onClick.AddListener(() =>
        {
            quitCheck.SetActive(false);
            GameSettings.SaveSettings(language);
            settingsPage.SetActive(false);
        });

        english.onClick.RemoveAllListeners();
        english.onClick.AddListener(() =>
        {
            english.GetComponentInChildren<Text>().color = Color.green;
            chinese.GetComponentInChildren<Text>().color = Color.white;
            language = Language.English;
        });

        chinese.onClick.RemoveAllListeners();
        chinese.onClick.AddListener(() =>
        {
            chinese.GetComponentInChildren<Text>().color = Color.green;
            english.GetComponentInChildren<Text>().color = Color.white;
            language = Language.Chinese;
        });

        nameInput.onEndEdit.RemoveAllListeners();
        nameInput.onEndEdit.AddListener(delegate { inputString = nameInput.text; });

        showFPS.onClick.RemoveAllListeners();
        showFPS.onClick.AddListener(() =>
        {
            GameSettings.showFPS = !GameSettings.showFPS;
            showFPS.GetComponentInChildren<Text>().color = GameSettings.showFPS ? Color.green : Color.white;
        });
    }

    public override void Open()
    {
        base.Open();

        settingsPage.SetActive(false);
        creditsPage.SetActive(false);
        setName.SetActive(false);

        chinese.GetComponentInChildren<Text>().color = language == Language.Chinese ? Color.green : Color.white;
        english.GetComponentInChildren<Text>().color = language == Language.English ? Color.green : Color.white;
        showFPS.GetComponentInChildren<Text>().color = GameSettings.showFPS ? Color.green : Color.white;

        if (DataIO.hasProfile(1) || DataIO.hasProfile(2) || DataIO.hasProfile(3))
            continueGame.gameObject.SetActive(true);
        else
            continueGame.gameObject.SetActive(false);
      
        // Menu BGM
        AudioManager.GetInst().PlayBGM(102);
    }
}
