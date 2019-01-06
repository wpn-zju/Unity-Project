using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// resources load and scene manage
public class Main : MonoBehaviour
{
    public static Main instance;
    public BaseSceneManager sceneManager;
    public AsyncOperation operation = null;
    public List<BaseSceneManager> sceneList = new List<BaseSceneManager>();
    private const string playerPath = "MyResource/Monster/Player&Summon/ZhanShi";

#if UNITY_EDITOR
    private float TESTTIME;
#endif

    private void Awake()
    {
        instance = this;

        UnityEngine.Object.DontDestroyOnLoad(gameObject);
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("EventSystem"));
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("RuntimeScripts"));
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("Main Camera"));
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("UI Camera"));
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("AudioBackground"));
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("AudioEffectSound"));

#if UNITY_EDITOR
        TESTTIME = Time.realtimeSinceStartup;
        Init();
        Debug.Log("Init Time : " + (Time.realtimeSinceStartup - TESTTIME).ToString() + "s");
#else
        Init();
#endif

        StartCoroutine(Load());
    }

    private void Init()
    {
        // Manager Init
        AtlasManager.GetInst().Init();      // read-only
        AudioManager.GetInst().Init();      // read-only
        ItemManager.GetInst().Init();       // read-write
        TaskManager.GetInst().Init();       // read-only
        TriggerManager.GetInst().Init();    // read-only

        // Skill Handler Init
        WarriorSkillHandle.Init();          // read-only
        MagicSkillHandle.Init();            // read-only
        ArcherSkillHandle.Init();           // read-only
        RuneHandle.Init();                  // read-only
        BossAIHandle.Init();                // read-only

        SceneInit();
    }

    private void SceneInit()
    {
        // SceneManager Init
        SingletonObject<DreamlandManager>.GetInst().Init();
        SingletonObject<HometownManager>.GetInst().Init();
        SingletonObject<HTPassageManager>.GetInst().Init();
        SingletonObject<Camp1Manager>.GetInst().Init();
        SingletonObject<Cave1Manager>.GetInst().Init();
        SingletonObject<Cave2Manager>.GetInst().Init();
        SingletonObject<CityManager>.GetInst().Init();
        SingletonObject<Camp2Manager>.GetInst().Init();
        SingletonObject<Forest1Manager>.GetInst().Init();
        SingletonObject<Forest2Manager>.GetInst().Init();
        SingletonObject<Relic1Manager>.GetInst().Init();
        SingletonObject<Relic2Manager>.GetInst().Init();
        SingletonObject<ArenaManager>.GetInst().Init();
    }

    private IEnumerator Load()
    {
#if UNITY_EDITOR
        TESTTIME = Time.realtimeSinceStartup;
        GameSettings.LoadSettings();
        Debug.Log("Load Setting Time : " + (Time.realtimeSinceStartup - TESTTIME).ToString() + "s");
#else
        GameSettings.LoadSettings();
#endif

#if UNITY_EDITOR
        TESTTIME = Time.realtimeSinceStartup;
        LoadStaticData();
        Debug.Log("Load Static Data Time : " + (Time.realtimeSinceStartup - TESTTIME).ToString() + "s");
#else
        LoadStaticData();
#endif

#if UNITY_EDITOR
        TESTTIME = Time.realtimeSinceStartup;
        UIMonitor.GetInst().Init();
        Debug.Log("Load UI Time : " + (Time.realtimeSinceStartup - TESTTIME).ToString() + "s");
#else
        UIMonitor.GetInst().Init();
#endif

        SingletonObject<StartMedia>.GetInst().Open();
        SingletonObject<StartMedia>.GetInst().Close();
        yield return new WaitForSeconds(2.0f);

#if UNITY_EDITOR
        FixScript.Fix();
#endif

        SingletonObject<MainMenuMedia>.GetInst().Open();
    }

    private void LoadStaticData()
    {
        SkillLoader.LoadData();             // read-only
        CharLoader.LoadData();              // read-only
        FlyingObjectLoader.LoadData();      // read-only
        BuffLoader.LoadData();              // read-only
        SkillInfoLoader.LoadData();         // read-only
        AILoader.LoadData();                // read-only
        ItemLoader.LoadData();              // read-only
        DialogLoader.LoadData();            // read-only
        TaskLoader.LoadData();              // read-write
        NpcLoader.LoadData();               // read-write
        TriggerLoader.LoadData();           // read-only
        RuneLoader.LoadData();              // read-only
        AudioLoader.LoadData();             // read-only
    }

    public IEnumerator EnterScene(string sceneName, int entrance = 0)
    {
        if (sceneManager != null)
            yield return sceneManager.ExitScene();

        switch (sceneName)
        {
            case "Dreamland":
                {
                    sceneManager = SingletonObject<DreamlandManager>.GetInst();
                }
                break;
            case "Hometown":
                {
                    sceneManager = SingletonObject<HometownManager>.GetInst();
                }
                break;
            case "HTPassage":
                {
                    sceneManager = SingletonObject<HTPassageManager>.GetInst();
                }
                break;
            case "Camp1":
                {
                    sceneManager = SingletonObject<Camp1Manager>.GetInst();
                }
                break;
            case "Cave1":
                {
                    sceneManager = SingletonObject<Cave1Manager>.GetInst();
                }
                break;
            case "Cave2":
                {
                    sceneManager = SingletonObject<Cave2Manager>.GetInst();
                }
                break;
            case "City":
                {
                    sceneManager = SingletonObject<CityManager>.GetInst();
                }
                break;
            case "Camp2":
                {
                    sceneManager = SingletonObject<Camp2Manager>.GetInst();
                }
                break;
            case "Forest1":
                {
                    sceneManager = SingletonObject<Forest1Manager>.GetInst();
                }
                break;
            case "Forest2":
                {
                    sceneManager = SingletonObject<Forest2Manager>.GetInst();
                }
                break;
            case "Relic1":
                {
                    sceneManager = SingletonObject<Relic1Manager>.GetInst();
                }
                break;
            case "Relic2":
                {
                    sceneManager = SingletonObject<Relic2Manager>.GetInst();
                }
                break;
            case "Arena":
                {
                    sceneManager = SingletonObject<ArenaManager>.GetInst();
                }
                break;
        }

        yield return sceneManager.EnterScene(entrance);
    }

    public void GeneratePlayer()
    {
        if (MyPlayer.instance != null)
            GameObject.Destroy(MyPlayer.instance.gameObject);

        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>(playerPath));
        player.transform.position = new Vector3(10000, 10000, 0); // avoid bug
        UnityEngine.Object.DontDestroyOnLoad(player);
        CommonHandle.RefreshSpine();
        CommonHandle.PlayerSetIdle(MyPlayer.instance.gameObject);
    }

    public IEnumerator EnterBattle(List<CharacterB> characters)
    {
        MyPlayer.instance.cameraLock = true;
        MyPlayer.instance.gameObject.SetActive(false);

        if (GameObject.Find("Monster") != null)
            GameObject.Find("Monster").SetActive(false);
        if (GameObject.Find("Npc") != null)
            GameObject.Find("Npc").SetActive(false);

        Sprite screenshot = Capture.CaptureCamera();

        sceneManager.returnPos = MyPlayer.instance.transform.position;

        SingletonObject<FightMedia>.GetInst().Close();

        {
            SingletonObject<LoadingMedia>.GetInst().Open();
            yield return operation = SceneManager.LoadSceneAsync("BattleScene");
            SingletonObject<LoadingMedia>.GetInst().Close();
        }

        Camera.main.transform.localPosition = new Vector3(0, 0, -1);

        GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = screenshot;

        SingletonObject<BattleUIMedia>.GetInst().Open();

        Battle.instance.BattleInit(characters);
    }

    public IEnumerator Battle_End(bool win)
    {
        if (sceneManager.sceneName == "Dreamland")
        {
            yield return EnterScene("Hometown", 10);
        }
        else
        {
            if (win && sceneManager.monsterName != " ")
            {
                yield return sceneManager.ReturnScene();
                PlayerData.GetInst().RefreshProgress(sceneManager.monsterId);

                switch(sceneManager.monsterId)
                {
                    case 101:
                        // 手足相残
                        SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(42);
                        break;
                    case 104:
                        // 南瓜骑士信息
                        SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(82);
                        break;
                    case 105:
                        // 击杀南瓜骑士
                        if (PlayerData.GetInst().conditions[1035])
                            SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(90);
                        else
                            SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(91);
                        break;
                    case 106:
                        // 南瓜骑士削弱
                        SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(104);
                        break;
                    case 107:
                        // 南瓜骑士削弱
                        SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(105);
                        break;
                    case 108:
                        // 南瓜骑士削弱
                        SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(106);
                        break;
                    case 109:
                        // 南瓜骑士削弱
                        SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(107);
                        break;
                    case 110:
                        // 南瓜骑士削弱
                        SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(108);
                        break;
                    default:
                        break;
                }

                ItemManager.GetInst().GetBattleAward(sceneManager.monsterId);

                // 维尔的宝物 战斗结束后回血回蓝25%
                if (PlayerData.GetInst().conditions[1041])
                {
                    PlayerData.GetInst().charB.attriRuntime.tHp += PlayerData.GetInst().charB.attriItems.tHp * 0.25f;
                    PlayerData.GetInst().charB.attriRuntime.tMp += PlayerData.GetInst().charB.attriItems.tMp * 0.25f;
                    if (GameSettings.language == Language.English)
                        SingletonObject<HintMedia>.GetInst().SendHint("Veall's Treasure, Recover 25% HP and MP.", Color.green);
                    else
                        SingletonObject<HintMedia>.GetInst().SendHint("维尔的宝物生效 恢复25%的生命值和法力值", Color.green);
                }
            }
            else
            {
                SceneInit();
                SingletonObject<MainMenuMedia>.GetInst().Open();
            }
        }

        MyPlayer.instance.cameraLock = false;
    }

    public IEnumerator Delayed_UI_Open(BaseMedia media)
    {
        float time = Time.time;

        while (!((!media.isOpening && !media.isClosing) || Time.time - time > 5.0f))
        {
            yield return null;
        }

        if (Time.time - time <= 5.0f)
        {
            media.isOpening = true;
            media.panel.gameObject.SetActive(true);

            // In Action
            if (media.panel.GetComponent<DynamicAnimation>() != null && media.panel.GetComponent<DynamicAnimation>().hasIn)
            {
                media.panel.GetComponent<DynamicAnimation>().Open();

                yield return new WaitForSeconds(media.panel.GetComponent<DynamicAnimation>().GetAnimation().GetClip("In").length);
            }

            media.isOpening = false;
        }
    }

    public IEnumerator Delayed_UI_Close(BaseMedia media)
    {
        float time = Time.time;

        while (!((!media.isOpening && !media.isClosing) || Time.time - time > 5.0f))
        {
            yield return null;
        }

        if (Time.time - time <= 5.0f)
        {
            media.isClosing = true;

            // Out Action
            if (media.panel.GetComponent<DynamicAnimation>() != null && media.panel.GetComponent<DynamicAnimation>().hasOut)
            {
                media.panel.GetComponent<DynamicAnimation>().Close();

                yield return new WaitForSeconds(media.panel.GetComponent<DynamicAnimation>().GetAnimation().GetClip("Out").length);
            }

            media.panel.gameObject.SetActive(false);
            media.isClosing = false;
        }
    }

    private void Update()
    {
        UIMonitor.GetInst().Update();

        if(operation != null)
            if (!operation.isDone)
                SingletonObject<LoadingMedia>.GetInst().SetProgress(operation.progress);
    }
}
