using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogMedia : BaseMedia
{
    private DialogData dialogData = null;
    private Image npcFigure;
    private Text npcName;
    private Text dialogText;
    private Transform chooseBar;
    private GameObject chooseButton1Prefab;
    private GameObject chooseButton2Prefab;
    private Button nextButton;
    private int progress = 0;

    public override void Init()
    {
        base.Init();
        
        npcFigure = panel.Find("Figure").GetComponent<Image>();
        npcName = panel.Find("Figure/NameBoard/Name").GetComponent<Text>();
        dialogText = panel.Find("MainArea/BackGround/Text").GetComponent<Text>();
        chooseBar = panel.Find("MainArea/ChooseBar");
        nextButton = panel.Find("MainArea/BackGround").GetComponent<Button>();
        chooseButton1Prefab = Resources.Load<GameObject>("MyResource/Panel/ChooseButton1");
        chooseButton2Prefab = Resources.Load<GameObject>("MyResource/Panel/ChooseButton2");
    }

    public void StartDialog(int npcId)
    {
        Close();
        progress = 0;
        dialogData = GameSettings.language == Language.English ? DialogLoader.data[NpcLoader.data[npcId].GetCurrentDialogId()] : DialogLoader.dataCN[NpcLoader.data[npcId].GetCurrentDialogId()];
        Open();
    }

    public void StartDialogDirectly(int dialogId)
    {
        Close();
        progress = 0;
        dialogData = GameSettings.language == Language.English ? DialogLoader.data[dialogId] : DialogLoader.dataCN[dialogId];
        Open();
    }

    public override void Open()
    {
        base.Open();

        if (progress >= dialogData.sentences.Count - 1)
        {
            progress = 0;
            Close();
        }
        else
        {
            SetNPCFigure(dialogData.sentences[progress].npcId);
            dialogText.text = dialogData.sentences[progress].content[0];

            if (dialogData.sentences[progress].content.Count == 1)
            {
                chooseBar.gameObject.SetActive(false);

                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(() =>
                {
                    int tempI = progress;
                    Close();
                    progress++;
                    TriggerManager.GetInst().EnableTriggers(dialogData.sentences[tempI].triggers[0]);
                    Open();
                });
            }
            else
            {
                chooseBar.gameObject.SetActive(true);

                nextButton.onClick.RemoveAllListeners();

                for (int i = 0; i < chooseBar.childCount; ++i)
                    GameObject.Destroy(chooseBar.GetChild(i).gameObject);

                for (int i = 1; i < dialogData.sentences[progress].content.Count; ++i)
                {
                    GameObject chooseBtn;
                    if (i % 2 == 0)
                        chooseBtn = GameObject.Instantiate(chooseButton1Prefab, chooseBar);
                    else
                        chooseBtn = GameObject.Instantiate(chooseButton2Prefab, chooseBar);
                    chooseBtn.GetComponentInChildren<Text>().text = dialogData.sentences[progress].content[i];
                    chooseBtn.GetComponent<Button>().onClick.RemoveAllListeners();
                    int tempInt = i;
                    chooseBtn.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        int tempI = progress;
                        progress = 0;
                        Close();
                        TriggerManager.GetInst().EnableTriggers(dialogData.sentences[tempI].triggers[tempInt]);
                    });
                }
            }
        }
    }

    private void SetNPCFigure(int npcId)
    {
        if (npcId != 0)
        {
            npcName.text = GameSettings.language == Language.English ? NpcLoader.data[npcId].name : NpcLoader.data[npcId].cnName;
            npcFigure.enabled = true;
            npcFigure.sprite = AtlasManager.GetInst().GetLihuiItemIcon(NpcLoader.data[npcId].name);
            if (npcFigure.sprite == null)
                npcFigure.enabled = false;
        }
        else
        {
            npcName.text = PlayerData.GetInst().nickName;
            npcFigure.enabled = false;
        }
    }
}
