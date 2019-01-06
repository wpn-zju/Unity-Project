using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FightMedia : BaseMedia
{
    private GameObject hpSlider;
    private GameObject mpSlider;
    private GameObject expSlider;
    private Text goldText;
    private Text expText;

    private GameObject miniMenu;
    private Button menuStart;
    private Button taskButton;
    private Button skillButton;
    private Button itemButton;
    private Button resume;
    private Button quit;

    private float radius = 1.0f;
    private Vector2 inputVector = new Vector2();
    public bool cantMove = false;
    private bool touching = false;
    private Image bgImg;
    private Image joystickImg;
    private GameObject joystickDetector;

    private Button enableButton;
    private GameObject taskBoard;
    private Text taskText;

    private Button dialogButton;
    private Button saveButton;
    private Button loadButton;

    private GameObject levelFill;
    private GameObject skillButtonFill;
    private GameObject itemButtonFill;

    public override void Init()
    {
        base.Init();

        hpSlider = panel.Find("Hp").gameObject;
        mpSlider = panel.Find("Mp").gameObject;
        expSlider = panel.Find("Exp").gameObject;
        goldText = panel.Find("Gold/TotalGold").GetComponent<Text>();
        expText = panel.Find("ExpN/TotalGold").GetComponent<Text>();

        miniMenu = panel.Find("MiniMenu").gameObject;
        menuStart = panel.Find("Exp").GetComponent<Button>();
        taskButton = miniMenu.transform.Find("ButtonBar/TaskPage").GetComponent<Button>();
        skillButton = miniMenu.transform.Find("ButtonBar/SkillPage").GetComponent<Button>();
        itemButton = miniMenu.transform.Find("ButtonBar/BackpackPage").GetComponent<Button>();
        resume = miniMenu.transform.Find("ButtonBar/Resume").GetComponent<Button>();
        quit = miniMenu.transform.Find("ButtonBar/Quit").GetComponent<Button>();

        bgImg = panel.Find("VirtualJoyStick").GetComponent<Image>();
        joystickImg = panel.Find("VirtualJoyStick/JoyStick").GetComponent<Image>();
        joystickDetector = panel.Find("JoyStickDetector").gameObject;

        enableButton = panel.Find("TaskBoard/EnableButton").GetComponent<Button>();
        taskBoard = panel.Find("TaskBoard/Board").gameObject;
        taskText = panel.Find("TaskBoard/Board/Mask2D/Text").GetComponent<Text>();

        dialogButton = panel.Find("DialogButton").GetComponent<Button>();
        saveButton = miniMenu.transform.Find("ButtonBar/Save").GetComponent<Button>();
        loadButton = miniMenu.transform.Find("ButtonBar/Load").GetComponent<Button>();

        miniMenu.SetActive(false);

        EventTriggerListener.Get(bgImg.gameObject).onDrag = OnJsDrag;
        EventTriggerListener.Get(bgImg.gameObject).onDown = OnJsDown;
        EventTriggerListener.Get(bgImg.gameObject).onUp = OnJsUp;

        EventTriggerListener.Get(joystickDetector).onDrag = OnDetectorDrag;
        EventTriggerListener.Get(joystickDetector).onDown = OnDetectorDown;
        EventTriggerListener.Get(joystickDetector).onUp = OnDetectorUp;

        menuStart.onClick.RemoveAllListeners();
        menuStart.onClick.AddListener(() =>
        {
            cantMove = true;
            miniMenu.SetActive(true);
        });

        taskButton.onClick.RemoveAllListeners();
        taskButton.onClick.AddListener(() =>
        {
            miniMenu.SetActive(false);
            cantMove = false;
            SingletonObject<CmdMedia>.GetInst().Open();
        });

        skillButton.onClick.RemoveAllListeners();
        skillButton.onClick.AddListener(() =>
        {
            SingletonObject<SkillMedia>.GetInst().Open();
        });

        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() =>
        {
            SingletonObject<ItemMedia>.GetInst().Open();
        });

        resume.onClick.RemoveAllListeners();
        resume.onClick.AddListener(() =>
        {
            miniMenu.SetActive(false);
            cantMove = false;
        });

        quit.onClick.RemoveAllListeners();
        quit.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        enableButton.onClick.RemoveAllListeners();
        enableButton.onClick.AddListener(() =>
        {
            if (taskBoard.activeSelf)
                taskBoard.SetActive(false);
            else
                taskBoard.SetActive(true);
        });

        dialogButton.onClick.RemoveAllListeners();
        dialogButton.onClick.AddListener(() =>
        {
            SingletonObject<DialogMedia>.GetInst().StartDialog(DialogControl.instance.GetDialogNpcId());
        });

        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(() =>
        {
            SingletonObject<ProfileMedia>.GetInst().isSave = true;
            SingletonObject<ProfileMedia>.GetInst().Open();
        });

        loadButton.onClick.RemoveAllListeners();
        loadButton.onClick.AddListener(() =>
        {
            SingletonObject<ProfileMedia>.GetInst().isSave = false;
            SingletonObject<ProfileMedia>.GetInst().Open();
        });

        levelFill = expSlider.transform.Find("Fill").gameObject;
        skillButtonFill = skillButton.transform.Find("Fill").gameObject;
        itemButtonFill = itemButton.transform.Find("Fill").gameObject;
    }

    public override void Open()
    {
        miniMenu.SetActive(false);
        RefreshTaskText();
        RefreshHint();
        base.Open();
    }

    public void RefreshTaskText()
    {
        string text = "";

        if (GameSettings.language == Language.English)
        {
            foreach (KeyValuePair<int, TaskData> kvp in TaskLoader.data)
            {
                if (kvp.Value.state == TaskState.Failed || kvp.Value.state == TaskState.NotAcceptAndCould || kvp.Value.state == TaskState.NotAcceptAndCouldNot || kvp.Value.state == TaskState.FinishedAndAwarded)
                    continue;
                else
                {
                    if (kvp.Value.type == TaskType.MainTask)
                        text += "<color=#ff0000><b>[Main]</b></color>";
                    else
                        text += "<color=#ff0000><b>[Branch]</b></color>";

                    if (kvp.Value.state == TaskState.AcceptedAndUnfinished)
                    {
                        text += " <color=#00ff00>" + kvp.Value.name + "</color>\n";

                        if (kvp.Value.killEnemies.Count != 0)
                            foreach (KeyValuePair<int, int> kvp2 in kvp.Value.killEnemies)
                                text += "Defeat : " + CharLoader.dataTotal[kvp2.Key].name + " (" + kvp.Value.currentKill[kvp2.Key].ToString() + "/" + kvp2.Value.ToString() + ")\n";

                        if (kvp.Value.itemsCollect.Count != 0)
                            foreach (KeyValuePair<int, int> kvp2 in kvp.Value.itemsCollect)
                                text += "Collect : " + ItemLoader.dataTotal[kvp2.Key].name + " (" + ItemManager.GetInst().ItemNumber(kvp2.Key).ToString() + "/" + kvp2.Value.ToString() + ")\n";

                        if (kvp.Value.controlConditions.Count != 0)
                            foreach (KeyValuePair<int, bool> kvp2 in kvp.Value.controlConditions)
                                if (PlayerData.GetInst().conditions[kvp2.Key])
                                    text += TaskLoader.dataConditionInfo[kvp2.Key] + " (Finished)" + "\n";
                                else
                                    text += TaskLoader.dataConditionInfo[kvp2.Key] + "\n";

                        text += "Describe :\n";
                        text += kvp.Value.describe + "\n";
                        text += "Award :\n";
                        text += kvp.Value.awardDescribe + "\n";
                    }
                    else if (kvp.Value.state == TaskState.FinishedButNotAwarded)
                    {
                        text += " <color=#00ff00>" + kvp.Value.name + "</color>" + " (Finished)\n";
                        text += kvp.Value.completeDescribe + "\n";
                        text += "Describe :\n";
                        text += kvp.Value.describe + "\n";
                        text += "Award :\n";
                        text += kvp.Value.awardDescribe + "\n";
                    }
                }

                text += "\n";
            }
        }
        else
        {
            foreach (KeyValuePair<int, TaskData> kvp in TaskLoader.data)
            {
                if (kvp.Value.state == TaskState.Failed || kvp.Value.state == TaskState.NotAcceptAndCould || kvp.Value.state == TaskState.NotAcceptAndCouldNot || kvp.Value.state == TaskState.FinishedAndAwarded)
                    continue;
                else
                {
                    if (kvp.Value.type == TaskType.MainTask)
                        text += "<color=#ff0000><b>[主线]</b></color>";
                    else
                        text += "<color=#ff0000><b>[支线]</b></color>";

                    if (kvp.Value.state == TaskState.AcceptedAndUnfinished)
                    {
                        text += " <color=#00ff00>" + kvp.Value.cnName + "</color>\n";

                        if (kvp.Value.killEnemies.Count != 0)
                            foreach (KeyValuePair<int, int> kvp2 in kvp.Value.killEnemies)
                                text += "击杀 : " + CharLoader.dataTotal[kvp2.Key].cnName + " (" + kvp.Value.currentKill[kvp2.Key].ToString() + "/" + kvp2.Value.ToString() + ")\n";

                        if (kvp.Value.itemsCollect.Count != 0)
                            foreach (KeyValuePair<int, int> kvp2 in kvp.Value.itemsCollect)
                                text += "收集 : " + ItemLoader.dataTotal[kvp2.Key].cnName + " (" + ItemManager.GetInst().ItemNumber(kvp2.Key).ToString() + "/" + kvp2.Value.ToString() + ")\n";

                        if (kvp.Value.controlConditions.Count != 0)
                            foreach (KeyValuePair<int, bool> kvp2 in kvp.Value.controlConditions)
                                if (PlayerData.GetInst().conditions[kvp2.Key])
                                    text += TaskLoader.dataConditionInfoCN[kvp2.Key] + " (完成)" + "\n";
                                else
                                    text += TaskLoader.dataConditionInfoCN[kvp2.Key] + "\n";

                        text += "任务描述 :\n";
                        text += kvp.Value.cnDescribe + "\n";
                        text += "任务奖励 :\n";
                        text += kvp.Value.cnAwardDescribe + "\n";
                    }
                    else if (kvp.Value.state == TaskState.FinishedButNotAwarded)
                    {
                        text += " <color=#00ff00>" + kvp.Value.cnName + "</color>" + " (完成)\n";
                        text += kvp.Value.cnCompleteDescribe + "\n";
                        text += "任务描述 :\n";
                        text += kvp.Value.cnDescribe + "\n";
                        text += "任务奖励 :\n";
                        text += kvp.Value.cnAwardDescribe + "\n";
                    }
                }

                text += "\n";
            }
        }

        if (text.Length > 0)
            text = text.Substring(0, text.Length - 1);

        taskText.text = text;
    }

    private void SendJoyStickInfo()
    {
        if (cantMove)
        {
            touching = false;
            inputVector = Vector2.zero;
            joystickImg.rectTransform.anchoredPosition = Vector3.zero;
            MyPlayer.instance.rb.velocity = Vector2.zero;
            MyPlayer.instance.stateManager.state.EnterState(StateType.idle);
            return;
        }
        if (!touching) // 防止对话的时候能用键盘移动
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {
                inputVector.y = 0;
            }
            else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {
                inputVector.y = -1;
            }
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                inputVector.y = 1;
            }
            else
            {
                inputVector.y = 0;
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                inputVector.x -= 0;
            }
            else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                inputVector.x = 1;
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                inputVector.x = -1;
            }
            else
            {
                inputVector.x = 0;
            }
            
            inputVector = inputVector.normalized;
        }

        if (SingletonObject<DialogMedia>.GetInst().panel.gameObject.activeSelf)
            inputVector = Vector2.zero;

        MyPlayer.instance.Run(inputVector * 7.0f); // speed = 7.0f;
    }

    public void OnJsDrag(PointerEventData ped)
    {
        touching = true;
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            Vector3 tempVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
            inputVector = tempVector.normalized;//(inputVector.magnitude > 0.3f) ? inputVector.normalized : inputVector;
            //inputVector = (inputVector.magnitude < 0.1f) ? Vector2.zero : inputVector;
            float len = tempVector.magnitude;
            if (tempVector.magnitude >= 1)
            {
                tempVector = new Vector3(tempVector.x / len, tempVector.y / len, tempVector.z / len);
            }
            joystickImg.rectTransform.anchoredPosition = (Vector3)tempVector * (bgImg.rectTransform.sizeDelta.x / 2) * radius;
        }
    }

    public void OnJsDown(PointerEventData ped)
    {
        //touching = true;
        //bgImg.GetComponent<RectTransform>().position = ped.position - bgImg.GetComponent<RectTransform>().sizeDelta / 2;
        //OnJsDrag(ped);
    }

    public void OnJsUp(PointerEventData ped)
    {
        touching = false;
        inputVector = Vector2.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public void OnDetectorDrag(PointerEventData ped)
    {
        //OnJsDrag(ped);
    }

    public void OnDetectorDown(PointerEventData ped)
    {
        //bgImg.GetComponent<RectTransform>().position = ped.position - bgImg.GetComponent<RectTransform>().sizeDelta / 2;
    }

    public void OnDetectorUp(PointerEventData ped)
    {
        //OnJsUp(ped);
    }

    public void RefreshHint()
    {
        levelFill.SetActive(false);
        skillButtonFill.SetActive(false);
        itemButtonFill.SetActive(false);

        if (ItemManager.GetInst().dirtyBit)
        {
            levelFill.SetActive(true);
            itemButtonFill.SetActive(true);
        }

        if (PlayerData.GetInst().lastSkillPoint > 0)
        {
            levelFill.SetActive(true);
            skillButtonFill.SetActive(true);
        }
    }

    public void UpdatePlayerUI()
    {
        hpSlider.GetComponent<Slider>().value = PlayerData.GetInst().charB.attriRuntime.tHp / PlayerData.GetInst().charB.attriItems.tHp;
        mpSlider.GetComponent<Slider>().value = PlayerData.GetInst().charB.attriRuntime.tMp / PlayerData.GetInst().charB.attriItems.tMp;
        hpSlider.transform.Find("Text").GetComponent<Text>().text = ((int)PlayerData.GetInst().charB.attriRuntime.tHp).ToString() + " / " + ((int)PlayerData.GetInst().charB.attriItems.tHp).ToString();
        mpSlider.transform.Find("Text").GetComponent<Text>().text = ((int)PlayerData.GetInst().charB.attriRuntime.tMp).ToString() + " / " + ((int)PlayerData.GetInst().charB.attriItems.tMp).ToString();
        expSlider.transform.Find("Level").GetComponent<Text>().text = PlayerData.GetInst().level.ToString();
        goldText.text = PlayerData.GetInst().gold.ToString();
        if (PlayerData.GetInst().level != 10)
            expText.text = PlayerData.GetInst().exp.ToString() + " / " + PlayerData.GetInst().levelExpDic[PlayerData.GetInst().level];
        else
            expText.text = PlayerData.GetInst().exp.ToString() + " / " + " NaN";

        if (DialogControl.instance.last != null)
            dialogButton.gameObject.SetActive(true);
        else
            dialogButton.gameObject.SetActive(false);
    }

    public override void Update()
    {
        SendJoyStickInfo();
        UpdatePlayerUI();

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.P) && !SingletonObject<CmdMedia>.GetInst().enable)
            SingletonObject<CmdMedia>.GetInst().Open();
#endif
    }
}
