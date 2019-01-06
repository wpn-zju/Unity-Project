using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMonitor : SingletonObject<UIMonitor>
{
    private GameObject m_canvas1;
    private GameObject m_canvas2;
    private GameObject m_canvas3;
    private GameObject m_canvas4;
    private GameObject m_canvas5;

    private List<UILoadInfo> loadingList = new List<UILoadInfo>();
    private List<BaseMedia> m_openMediatorList = new List<BaseMedia>();

    public void Init()
    {
        // 5-level UI
        m_canvas1 = GameObject.Find("Canvas1");
        m_canvas2 = GameObject.Find("Canvas2");
        m_canvas3 = GameObject.Find("Canvas3");
        m_canvas4 = GameObject.Find("Canvas4(Dynamic)");
        m_canvas5 = GameObject.Find("Canvas5");

        UnityEngine.Object.DontDestroyOnLoad(m_canvas1);
        UnityEngine.Object.DontDestroyOnLoad(m_canvas2);
        UnityEngine.Object.DontDestroyOnLoad(m_canvas3);
        UnityEngine.Object.DontDestroyOnLoad(m_canvas4);
        UnityEngine.Object.DontDestroyOnLoad(m_canvas5);

        if(GameSettings.language == Language.English) // English UI
        {
            loadingList.Add(new UILoadInfo("fightPanel", SingletonObject<FightMedia>.GetInst(), 1));
            loadingList.Add(new UILoadInfo("battleuiPanel", SingletonObject<BattleUIMedia>.GetInst(), 2));
            loadingList.Add(new UILoadInfo("itempagePanel", SingletonObject<ItemMedia>.GetInst(), 2));
            loadingList.Add(new UILoadInfo("skillPanel", SingletonObject<SkillMedia>.GetInst(), 2));
            loadingList.Add(new UILoadInfo("shopPanel", SingletonObject<ShopMedia>.GetInst(), 2));
            loadingList.Add(new UILoadInfo("dialogPanel", SingletonObject<DialogMedia>.GetInst(), 2));
            loadingList.Add(new UILoadInfo("selectPanel", SingletonObject<SelectMedia>.GetInst(), 3));
            loadingList.Add(new UILoadInfo("itempropertyPanel", SingletonObject<ItemPropertyMedia>.GetInst(), 3));
            loadingList.Add(new UILoadInfo("flywordPanel", SingletonObject<FlywordMedia>.GetInst(), 4));
            loadingList.Add(new UILoadInfo("hpPanel", SingletonObject<HpMedia>.GetInst(), 4));
            loadingList.Add(new UILoadInfo("mpPanel", SingletonObject<MpMedia>.GetInst(), 4));
            loadingList.Add(new UILoadInfo("speedPanel", SingletonObject<SpeedMedia>.GetInst(), 4));
            loadingList.Add(new UILoadInfo("buffPanel", SingletonObject<BuffMedia>.GetInst(), 4));
            loadingList.Add(new UILoadInfo("startPanel", SingletonObject<StartMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("mainmenuPanel", SingletonObject<MainMenuMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("profilePanel", SingletonObject<ProfileMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("loadingPanel", SingletonObject<LoadingMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("guidePanel", SingletonObject<GuideMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("battlecompletePanel", SingletonObject<BattleCompleteMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("buffcheckPanel", SingletonObject<BuffCheckMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("hintPanel", SingletonObject<HintMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("cmdPanel", SingletonObject<CmdMedia>.GetInst(), 5));
        }
        else // Chinese UI
        {
            loadingList.Add(new UILoadInfo("fightPanelCN", SingletonObject<FightMedia>.GetInst(), 1));
            loadingList.Add(new UILoadInfo("battleuiPanel", SingletonObject<BattleUIMedia>.GetInst(), 2));
            loadingList.Add(new UILoadInfo("itempagePanel", SingletonObject<ItemMedia>.GetInst(), 2));
            loadingList.Add(new UILoadInfo("skillPanelCN", SingletonObject<SkillMedia>.GetInst(), 2));
            loadingList.Add(new UILoadInfo("shopPanelCN", SingletonObject<ShopMedia>.GetInst(), 2));
            loadingList.Add(new UILoadInfo("dialogPanel", SingletonObject<DialogMedia>.GetInst(), 2));
            loadingList.Add(new UILoadInfo("selectPanelCN", SingletonObject<SelectMedia>.GetInst(), 3));
            loadingList.Add(new UILoadInfo("itempropertyPanel", SingletonObject<ItemPropertyMedia>.GetInst(), 3));
            loadingList.Add(new UILoadInfo("flywordPanel", SingletonObject<FlywordMedia>.GetInst(), 4));
            loadingList.Add(new UILoadInfo("hpPanel", SingletonObject<HpMedia>.GetInst(), 4));
            loadingList.Add(new UILoadInfo("mpPanel", SingletonObject<MpMedia>.GetInst(), 4));
            loadingList.Add(new UILoadInfo("speedPanel", SingletonObject<SpeedMedia>.GetInst(), 4));
            loadingList.Add(new UILoadInfo("buffPanel", SingletonObject<BuffMedia>.GetInst(), 4));
            loadingList.Add(new UILoadInfo("startPanel", SingletonObject<StartMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("mainmenuPanelCN", SingletonObject<MainMenuMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("profilePanelCN", SingletonObject<ProfileMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("loadingPanelCN", SingletonObject<LoadingMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("guidePanelCN", SingletonObject<GuideMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("battlecompletePanelCN", SingletonObject<BattleCompleteMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("buffcheckPanel", SingletonObject<BuffCheckMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("hintPanel", SingletonObject<HintMedia>.GetInst(), 5));
            loadingList.Add(new UILoadInfo("cmdPanel", SingletonObject<CmdMedia>.GetInst(), 5));
        }

        LoadUI();
    }

    public void LoadUI()
    {
        foreach(UILoadInfo ui in loadingList)
        {
            GameObject newUI;
            switch (ui.sortOrder)
            {
                case 1:
                    newUI = GameObject.Instantiate(Resources.Load<GameObject>("MyResource/Panel/" + ui.name), m_canvas1.transform);
                    break;
                case 2:
                    newUI = GameObject.Instantiate(Resources.Load<GameObject>("MyResource/Panel/" + ui.name), m_canvas2.transform);
                    break;
                case 3:
                    newUI = GameObject.Instantiate(Resources.Load<GameObject>("MyResource/Panel/" + ui.name), m_canvas3.transform);
                    break;
                case 4:
                    newUI = GameObject.Instantiate(Resources.Load<GameObject>("MyResource/Panel/" + ui.name), m_canvas4.transform);
                    break;
                case 5:
                    newUI = GameObject.Instantiate(Resources.Load<GameObject>("MyResource/Panel/" + ui.name), m_canvas5.transform);
                    break;
                default:
                    newUI = new GameObject();
                    break;
            }
            if (newUI.GetComponent<DynamicAnimation>() != null)
                newUI.GetComponent<DynamicAnimation>().Init();
            ui.baseMedia.panel = newUI.transform;
            ui.baseMedia.Init();
            ui.baseMedia.panel.gameObject.SetActive(false);
        }

        SingletonObject<HintMedia>.GetInst().Open(); // Open Hint until leaving application
    }

    public void AddMedia(BaseMedia baseMedia)
    {
        if(!m_openMediatorList.Contains(baseMedia))
        {
            m_openMediatorList.Add(baseMedia);
        }
    }

    public void RemoveMedia(BaseMedia baseMedia)
    {
        if (m_openMediatorList.Contains(baseMedia))
        {
            m_openMediatorList.Remove(baseMedia);
        }
    }

    public void Update()
    {
        for (int i = 0; i < m_openMediatorList.Count; ++i)
        {
            m_openMediatorList[i].Update();
        }
    }
}

public class UILoadInfo
{
    public string name;
    public BaseMedia baseMedia;
    public int sortOrder;

    public UILoadInfo(string name, BaseMedia baseMedia, int sortOrder)
    {
        this.name = name;
        this.baseMedia = baseMedia;
        this.sortOrder = sortOrder;
    }
}