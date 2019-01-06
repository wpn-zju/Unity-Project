using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataIO
{
    public static int saveId = 1;
    public static System.TimeSpan totalTime;
    public static System.DateTime lastSaveTime;

    public static string savePath
    {
        get
        {
#if UNITY_ANDROID
            return Application.persistentDataPath + "Player" + saveId.ToString() + ".txt";
#else
            return "Player" + saveId.ToString() + ".txt";
#endif
        }
    }

    public static bool hasProfile(int id)
    {
#if UNITY_ANDROID
        string path = Application.persistentDataPath + "Player" + id.ToString() + ".txt";
#else
        string path = "Player" + id.ToString() + ".txt";
#endif
        if (ES2.Exists(path))
            return true;
        return false;
    }

    public static IEnumerator Save()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Dreamland" || TaskLoader.data[33].state == TaskState.AcceptedAndUnfinished)
        {
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Can't save", Color.red);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("无法存档", Color.red);

            yield break;
        }

        SingletonObject<ProfileMedia>.GetInst().hintPage.SetActive(true);
        yield return null;

        // time data
        totalTime = System.DateTime.Now - lastSaveTime + totalTime;
        lastSaveTime = System.DateTime.Now;
        ES2.Save(totalTime, savePath + "?tag=totaltime");

        // basic data
        ES2.Save(PlayerData.GetInst().nickName, savePath + "?tag=name");
        ES2.Save(PlayerData.GetInst().level, savePath + "?tag=level");
        ES2.Save(PlayerData.GetInst().exp, savePath + "?tag=exp");
        ES2.Save(PlayerData.GetInst().gold, savePath + "?tag=gold");

        // skill data
        ES2.Save(PlayerData.GetInst().lastSkillPoint, savePath + "?tag=lastskillpoint");
        ES2.Save(PlayerData.GetInst().totalSkillPoint, savePath + "?tag=totalskillpoint");
        ES2.Save(PlayerData.GetInst().skillData, savePath + "?tag=skilldata");
        ES2.Save(PlayerData.GetInst().branchData, savePath + "?tag=branchdata");

        Dictionary<string, bool> sceneStateDicSave = new Dictionary<string, bool>();
        foreach (KeyValuePair<string, NpcState> kvp in Main.instance.sceneManager.monsterInScene)
            sceneStateDicSave[kvp.Key] = kvp.Value.dead;

        // runtime data
        ES2.Save(PlayerData.GetInst().charB.attriRuntime.tHp, savePath + "?tag=hp");
        ES2.Save(PlayerData.GetInst().charB.attriRuntime.tMp, savePath + "?tag=mp");
        ES2.Save(Main.instance.sceneManager.sceneName, savePath + "?tag=scene");
        ES2.Save(sceneStateDicSave, savePath + "?tag=scenestatedic");
        ES2.Save(MyPlayer.instance.transform.position, savePath + "?tag=position");

        Dictionary<int, EquipmentItemData> equippedDataSave = new Dictionary<int, EquipmentItemData>();
        Dictionary<int, BaseItemData> runeDataSave = new Dictionary<int, BaseItemData>();
        List<BaseItemData> BaseItemDataSave = new List<BaseItemData>();
        List<EquipmentItemData> equipmentDataSave = new List<EquipmentItemData>();
        Dictionary<int, int> npcSave = new Dictionary<int, int>();
        Dictionary<int, TaskData> taskDataSave = new Dictionary<int, TaskData>();
        string mainTaskName = "Already Completed";
        string mainTaskNameCN = "已完成主线";

        for (int i = 0; i < ItemManager.GetInst().equippedData.Length; ++i)
        {
            if (ItemManager.GetInst().equippedData[i] != null)
                equippedDataSave[i] = ItemManager.GetInst().equippedData[i];
        }

        for (int i = 0; i < ItemManager.GetInst().runeData.Length; ++i)
        {
            if (ItemManager.GetInst().runeData[i] != null)
                runeDataSave[i] = ItemManager.GetInst().runeData[i];
        }

        for (int i = 0; i < ItemManager.GetInst().itemList.Count; ++i)
        {
            if (ItemManager.GetInst().itemList[i].type != ItemType.Equipment)
                BaseItemDataSave.Add(ItemManager.GetInst().itemList[i]);
            else
                equipmentDataSave.Add((EquipmentItemData)ItemManager.GetInst().itemList[i]);
        }

        foreach (KeyValuePair<int, TaskData> kvp in TaskLoader.data)
        {
            taskDataSave[kvp.Key] = kvp.Value;

            if (kvp.Value.type == TaskType.MainTask && kvp.Value.state == TaskState.AcceptedAndUnfinished)
            {
                mainTaskName = kvp.Value.name;
                mainTaskNameCN = kvp.Value.cnName;
            }
        }

        foreach (KeyValuePair<int, NpcData> kvp in NpcLoader.data)
        {
            npcSave[kvp.Key] = kvp.Value.dialogId;
        }

        // item data
        ES2.Save(equippedDataSave, savePath + "?tag=equippeddatasave");
        ES2.Save(runeDataSave, savePath + "?tag=runedatasave");
        ES2.Save(BaseItemDataSave, savePath + "?tag=baseitemdatasave");
        ES2.Save(equipmentDataSave, savePath + "?tag=equipmentdatasave");
        
        // story data
        ES2.Save(PlayerData.GetInst().conditions, savePath + "?tag=conditions");
        ES2.Save(npcSave, savePath + "?tag=npcsave");
        ES2.Save(taskDataSave, savePath + "?tag=taskdatasave");
        ES2.Save(mainTaskName, savePath + "?tag=progress");
        ES2.Save(mainTaskNameCN, savePath + "?tag=progressCN");

        // capture
        Sprite capture = Capture.CaptureCamera(960, 540);
        ES2.Save(capture, savePath + "?tag=capture");

        SingletonObject<ProfileMedia>.GetInst().Refresh();
        SingletonObject<ProfileMedia>.GetInst().hintPage.SetActive(false);

        if (GameSettings.language == Language.English)
            SingletonObject<HintMedia>.GetInst().SendHint("Game Saved", Color.green);
        else
            SingletonObject<HintMedia>.GetInst().SendHint("游戏已保存", Color.green);
    }

    public static void Load()
    {
        PlayerData.GetInst().nickName = ES2.Load<string>(savePath + "?tag=name");
        PlayerData.GetInst().level = ES2.Load<int>(savePath + "?tag=level");
        PlayerData.GetInst().exp = ES2.Load<int>(savePath + "?tag=exp");
        PlayerData.GetInst().gold = ES2.Load<int>(savePath + "?tag=gold");

        PlayerData.GetInst().lastSkillPoint = ES2.Load<int>(savePath + "?tag=lastskillpoint");
        PlayerData.GetInst().totalSkillPoint = ES2.Load<int>(savePath + "?tag=totalskillpoint");
        PlayerData.GetInst().skillData = ES2.LoadArray<int>(savePath + "?tag=skilldata");
        PlayerData.GetInst().branchData = ES2.LoadArray<int>(savePath + "?tag=branchdata");

        Dictionary<int, EquipmentItemData> equippedDataLoad = ES2.LoadDictionary<int, EquipmentItemData>(savePath + "?tag=equippeddatasave");
        Dictionary<int, BaseItemData> runeDataLoad = ES2.LoadDictionary<int, BaseItemData>(savePath + "?tag=runedatasave");
        List<BaseItemData> BaseItemDataLoad = ES2.LoadList<BaseItemData>(savePath + "?tag=baseitemdatasave");
        List<EquipmentItemData> equipmentDataLoad = ES2.LoadList<EquipmentItemData>(savePath + "?tag=equipmentdatasave");
        PlayerData.GetInst().conditions = ES2.LoadDictionary<int, bool>(savePath + "?tag=conditions");
        Dictionary<int, int> npcLoad = ES2.LoadDictionary<int, int>(savePath + "?tag=npcsave");

        // load task info
        ES2.LoadDictionary<int, TaskData>(savePath + "?tag=taskdatasave");

        // Clear Item
        {
            ItemManager.GetInst().itemList.Clear();
            ItemManager.GetInst().equippedData = new EquipmentItemData[6];
            ItemManager.GetInst().runeData = new BaseItemData[4];
        }

        string sceneName = ES2.Load<string>(savePath + "?tag=scene");

        // Clear Scene
        foreach (BaseSceneManager mgr in Main.instance.sceneList)
            if (mgr.sceneName == sceneName)
                mgr.dirty = true; // use dirty bit
            else
                mgr.dirty = false;

        foreach (KeyValuePair<int, EquipmentItemData> kvp in equippedDataLoad)
        {
            ItemManager.GetInst().equippedData[kvp.Key] = new EquipmentItemData((EquipmentItemData)ItemLoader.dataTotal[kvp.Value.id_int]);
            ItemManager.GetInst().equippedData[kvp.Key].quality = kvp.Value.quality;
            ItemManager.GetInst().equippedData[kvp.Key].attriArray = kvp.Value.attriArray;
            ItemManager.GetInst().equippedData[kvp.Key].resourceId = kvp.Value.resourceId;
            ItemManager.GetInst().equippedData[kvp.Key].name = ItemLoader.dataResource[kvp.Value.resourceId].name;
            ItemManager.GetInst().equippedData[kvp.Key].cnName = ItemLoader.dataResource[kvp.Value.resourceId].cnName;
            ItemManager.GetInst().equippedData[kvp.Key].describe = ItemLoader.dataResource[kvp.Value.resourceId].describe;
            ItemManager.GetInst().equippedData[kvp.Key].cnDescribe = ItemLoader.dataResource[kvp.Value.resourceId].cnDescribe;
            ItemManager.GetInst().equippedData[kvp.Key].iconPath = ItemLoader.dataResource[kvp.Value.resourceId].iconPath;
            ItemManager.GetInst().equippedData[kvp.Key].part = ItemLoader.dataResource[kvp.Value.resourceId].part;
            ItemManager.GetInst().equippedData[kvp.Key].weaponType = ItemLoader.dataResource[kvp.Value.resourceId].weaponType;
            ItemManager.GetInst().equippedData[kvp.Key].priceIn = ItemManager.quality2PriceIn[kvp.Value.quality];
            ItemManager.GetInst().equippedData[kvp.Key].priceOut = ItemManager.quality2PriceOut[kvp.Value.quality];
            ItemManager.GetInst().equippedData[kvp.Key].number = 1;
        }

        foreach (KeyValuePair<int, BaseItemData> kvp in runeDataLoad)
            ItemManager.GetInst().runeData[kvp.Key] = new BaseItemData(ItemLoader.dataTotal[kvp.Value.id_int]);

        foreach (BaseItemData v in BaseItemDataLoad)
        {
            BaseItemData temp = new BaseItemData(ItemLoader.dataTotal[v.id_int]);
            temp.number = v.number;
            ItemManager.GetInst().itemList.Add(temp);
        }

        foreach (EquipmentItemData v in equipmentDataLoad)
        {
            EquipmentItemData temp = new EquipmentItemData((EquipmentItemData)ItemLoader.dataTotal[v.id_int]);
            temp.quality = v.quality;
            temp.attriArray = v.attriArray;
            temp.resourceId = v.resourceId;
            temp.name = ItemLoader.dataResource[v.resourceId].name;
            temp.cnName = ItemLoader.dataResource[v.resourceId].cnName;
            temp.describe = ItemLoader.dataResource[v.resourceId].describe;
            temp.cnDescribe = ItemLoader.dataResource[v.resourceId].cnDescribe;
            temp.iconPath = ItemLoader.dataResource[v.resourceId].iconPath;
            temp.part = ItemLoader.dataResource[v.resourceId].part;
            temp.weaponType = ItemLoader.dataResource[v.resourceId].weaponType;
            temp.priceIn = ItemManager.quality2PriceIn[v.quality];
            temp.priceOut = ItemManager.quality2PriceOut[v.quality];
            temp.number = 1;
            ItemManager.GetInst().itemList.Add(temp);
        }

        foreach (KeyValuePair<int, int> kvp in npcLoad)
            NpcLoader.data[kvp.Key].dialogId = kvp.Value;

        PlayerData.GetInst().charB = new CharacterB(CharLoader.dataTotal[10000 + PlayerData.GetInst().level]);
        ItemManager.GetInst().SyncItemAttributes(); // Sync Before Load Hp and Mp data
        PlayerData.GetInst().charB.attriRuntime.tHp = ES2.Load<float>(savePath + "?tag=hp");
        PlayerData.GetInst().charB.attriRuntime.tMp = ES2.Load<float>(savePath + "?tag=mp");

        totalTime = ES2.Load<System.TimeSpan>(savePath + "?tag=totaltime");
        lastSaveTime = System.DateTime.Now;

        // Loading
        Main.instance.GeneratePlayer();
        Main.instance.StartCoroutine(Main.instance.EnterScene(ES2.Load<string>(savePath + "?tag=scene"), -1));
    }
}