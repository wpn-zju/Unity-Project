using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class BaseItemData
{
    public int id_int;
    public string name;
    public string cnName;
    public string describe;
    public string cnDescribe;
    public int priceIn;
    public int priceOut;
    public int number;
    public ItemType type;
    public string iconPath;
    public ItemQuality quality;

    public BaseItemData()
    {

    }

    public BaseItemData(BaseItemData that) // copy ctor
    {
        id_int = that.id_int;
        name = that.name;
        cnName = that.cnName;
        describe = that.describe;
        cnDescribe = that.cnDescribe;
        priceIn = that.priceIn;
        priceOut = that.priceOut;
        number = that.number;
        type = that.type;
        iconPath = that.iconPath;
        quality = that.quality;
    }
}

public class RuneData
{
    public int id_int;
    public RuneType type;
    public List<string> data = new List<string>();
}

public class EquipmentItemData : BaseItemData
{
    public EquipmentPart part; // part
    public WeaponType weaponType; // weaponType
    public uint attriArray; // read and write
    public int resourceId; // read and write

    public EquipmentItemData() : base()
    {
        number = 1;
    }

    public EquipmentItemData(EquipmentItemData that) : base(that) // copy ctor
    {
        part = that.part; // for copy construct
        attriArray = that.attriArray; // for exchange use
    }

    public void SetBit(int shift, bool value)
    {
        if (value) // set true
            attriArray |= (uint)1 << 31 - shift;
        else // set false
            attriArray &= 0xffffffff - ((uint)1 << 31 - shift);
    }

    public bool GetBit(int shift) // shift start from 0
    {
        return attriArray << shift >> 31 == 1;
    }

    public void RandomResource()
    {
        int randomNum;
        ItemResource temp;

        do
        {
            randomNum = UnityEngine.Random.Range(0, 10000) % ItemLoader.dataResource.Count + 1;
            temp = ItemLoader.dataResource[randomNum];
        } while (temp.part != part);

        resourceId = randomNum;
        weaponType = temp.weaponType;
        name = temp.name;
        cnName = temp.cnName;
        describe = temp.describe;
        cnDescribe = temp.cnDescribe;
        iconPath = temp.iconPath;
    }

    public void RandomAttri(ItemQuality quality = ItemQuality.Normal)
    {
        this.quality = quality;
        this.priceIn = ItemManager.quality2PriceIn[quality];
        this.priceOut = ItemManager.quality2PriceOut[quality];

        switch (quality)
        {
            case ItemQuality.Normal:
                {
                    int[] temp = new int[2];
                    GetRolledNumbers(ref temp);

                    for (int i = 0; i < temp.Length; ++i)
                        SetBit(temp[i], true);
                }
                break;
            case ItemQuality.Good:
                {
                    int[] temp = new int[3];
                    GetRolledNumbers(ref temp);

                    for (int i = 0; i < temp.Length; ++i)
                        SetBit(temp[i], true);
                }
                break;
            case ItemQuality.Superior:
                {
                    int[] temp = new int[4];
                    GetRolledNumbers(ref temp);

                    for (int i = 0; i < temp.Length; ++i)
                        SetBit(temp[i], true);
                }
                break;
            case ItemQuality.Epic:
                {
                    int[] temp = new int[5];
                    GetRolledNumbers(ref temp);

                    for (int i = 0; i < temp.Length; ++i)
                        SetBit(temp[i], true);
                }
                break;
        }
    }

    public void GetRolledNumbers(ref int[] temp)
    {
        for (int i = 0; i < temp.Length; ++i)
        {
            bool same;
            int randomNum1;
            do
            {
                same = false;
                randomNum1 = UnityEngine.Random.Range(0, 10000) % 12;
                for (int j = i - 1; j >= 0; --j)
                    if (temp[j] == randomNum1)
                        same = true;
            } while (same);
            temp[i] = randomNum1;
        }
    }

    public int Build(int index) // index 0 - 4
    {
        if (index <= -1 || index >= 5)
            return -1;

        int count = -1;
        int shift = 0;
        for (int i = 0; i <= 13; ++i)
        {
            if (GetBit(i))
                count++;
            if (count == index)
            {
                shift = i;
                break;
            }
        }

        SetBit(shift, false);

        int randomNum1;
        do
        {
            randomNum1 = UnityEngine.Random.Range(0, 10000) % 12;
        } while (GetBit(randomNum1) || randomNum1 == shift);

        SetBit(randomNum1, true);

        return randomNum1;
    }

    public static void Exchange(EquipmentItemData a, EquipmentItemData b)
    {
        EquipmentItemData temp = new EquipmentItemData(a);

        a.quality = b.quality;
        a.attriArray = b.attriArray;

        b.quality = temp.quality;
        b.attriArray = temp.attriArray;
    }
}

public class ItemResource
{
    public int id_int;
    public EquipmentPart part;
    public WeaponType weaponType;
    public string name;
    public string cnName;
    public string describe;
    public string cnDescribe;
    public string iconPath;
    public string atlasPath;
    public List<string> spineSlots = new List<string>();
}

public class TaskData
{
    public int id_int;
    public TaskType type;
    public string name;
    public string cnName;
    public string describe;
    public string cnDescribe;
    public string awardDescribe;
    public string cnAwardDescribe;
    public string completeDescribe;
    public string cnCompleteDescribe;
    public TaskState state; // save
    public List<int> completeTriggers = new List<int>();

    // award and target
    // 目标分四种 1.杀怪 2.道具收集 3.触发条件 4.等级限制
    public Dictionary<int, int> killEnemies = new Dictionary<int, int>();
    public Dictionary<int, int> currentKill = new Dictionary<int, int>(); // save
    public Dictionary<int, int> itemsCollect = new Dictionary<int, int>();
    public Dictionary<int, bool> controlConditions = new Dictionary<int, bool>();
    public int levelLimit;

    // 奖励分三种 1.经验 2.金币 3.道具一件或多件
    public int awardGold;
    public int awardExp;
    public List<int> awardItems = new List<int>();

    public bool CheckProgress()
    {
        foreach (KeyValuePair<int, int> kvp in killEnemies)
        {
            if (currentKill[kvp.Key] < kvp.Value)
                return false;
        }

        foreach (KeyValuePair<int, int> kvp in itemsCollect)
        {
            if (ItemManager.GetInst().itemList[ItemManager.GetInst().FindItemSlot(kvp.Key)].number < kvp.Value)
                return false;
        }

        foreach (KeyValuePair<int, bool> kvp in controlConditions)
        {
            if (PlayerData.GetInst().conditions[kvp.Key] != kvp.Value)
                return false;
        }

        if (levelLimit != -1 && PlayerData.GetInst().level < levelLimit)
            return false;

        return true;
    }

    public void RefreshProgress(int monsterId) // for enemy kills
    {
        if (killEnemies.ContainsKey(monsterId))
            if (currentKill[monsterId] < killEnemies[monsterId])
                currentKill[monsterId]++;

        if (CheckProgress())
        {
            state = TaskState.FinishedButNotAwarded;
            TriggerManager.GetInst().EnableTriggers(completeTriggers);
            Main.instance.sceneManager.RefreshTaskHint();
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint(name + " finished.", Color.green);
            else
                SingletonObject<HintMedia>.GetInst().SendHint(cnName + " 已完成", Color.green);
        }

        SingletonObject<FightMedia>.GetInst().RefreshTaskText();
    }

    public void GetAward()
    {
        PlayerData.GetInst().GetGold(awardGold);
        PlayerData.GetInst().GetExp(awardExp);

        foreach (int id in awardItems)
            ItemManager.GetInst().GainNewItem(id);

        if (GameSettings.language == Language.English)
            SingletonObject<HintMedia>.GetInst().SendHint(name + " awarded.", Color.green);
        else
            SingletonObject<HintMedia>.GetInst().SendHint(cnName + " 已获得奖励", Color.green);
    }
}

public class DialogData
{
    public int id_int;
    public List<Sentence> sentences = new List<Sentence>();
}

public class Sentence
{
    public int npcId;
    public List<string> content = new List<string>();
    public List<List<int>> triggers = new List<List<int>>();
}

public class Trigger
{
    public int id_int;
    public TriggerType type;
    public List<int> parameters = new List<int>();
}

public class NpcData
{
    public int id_int;
    public string name;
    public string cnName;
    public int dialogId;
    public Dictionary<int, BondingTaskInfo> taskInfo = new Dictionary<int, BondingTaskInfo>();

    public NpcHintMode GetHintMode()
    {
        if (taskInfo.Count == 0)
            return NpcHintMode.noHint;
        else
        {
            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.returnBonding && temp.state == TaskState.FinishedButNotAwarded && temp.type == TaskType.MainTask)
                    return NpcHintMode.complete;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.returnBonding && temp.state == TaskState.FinishedButNotAwarded && temp.type == TaskType.BranchTask)
                    return NpcHintMode.complete;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.doingBonding && temp.state == TaskState.AcceptedAndUnfinished && temp.type == TaskType.MainTask)
                    return NpcHintMode.doing;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.doingBonding && temp.state == TaskState.AcceptedAndUnfinished && temp.type == TaskType.BranchTask)
                    return NpcHintMode.doing;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.acceptBonding && temp.state == TaskState.NotAcceptAndCould && temp.type == TaskType.MainTask)
                    return NpcHintMode.accept;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.acceptBonding && temp.state == TaskState.NotAcceptAndCould && temp.type == TaskType.BranchTask)
                    return NpcHintMode.accept;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.returnBonding && temp.state == TaskState.AcceptedAndUnfinished && temp.type == TaskType.MainTask)
                    return NpcHintMode.wait;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.returnBonding && temp.state == TaskState.AcceptedAndUnfinished && temp.type == TaskType.BranchTask)
                    return NpcHintMode.wait;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.acceptBonding && temp.state == TaskState.NotAcceptAndCouldNot && temp.type == TaskType.MainTask)
                    return NpcHintMode.prepare;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.acceptBonding && temp.state == TaskState.NotAcceptAndCouldNot && temp.type == TaskType.BranchTask)
                    return NpcHintMode.prepare;
            }
        }

        return NpcHintMode.noHint;
    }

    public int GetCurrentDialogId()
    {
        if (taskInfo.Count == 0)
            return dialogId;
        else
        {
            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.returnBonding && temp.state == TaskState.FinishedButNotAwarded && temp.type == TaskType.MainTask)
                    return dialogId;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.returnBonding && temp.state == TaskState.FinishedButNotAwarded && temp.type == TaskType.BranchTask)
                    return dialogId;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.doingBonding && temp.state == TaskState.AcceptedAndUnfinished && temp.type == TaskType.MainTask)
                    return dialogId;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.doingBonding && temp.state == TaskState.AcceptedAndUnfinished && temp.type == TaskType.BranchTask)
                    return dialogId;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.acceptBonding && temp.state == TaskState.NotAcceptAndCould && temp.type == TaskType.MainTask)
                    return dialogId;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.acceptBonding && temp.state == TaskState.NotAcceptAndCould && temp.type == TaskType.BranchTask)
                    return dialogId;
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.returnBonding && temp.state == TaskState.AcceptedAndUnfinished && temp.type == TaskType.MainTask)
                    return 10002; // TEMP
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.returnBonding && temp.state == TaskState.AcceptedAndUnfinished && temp.type == TaskType.BranchTask)
                    return 10002; // TEMP
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.acceptBonding && temp.state == TaskState.NotAcceptAndCouldNot && temp.type == TaskType.MainTask)
                    return 10001; // TEMP
            }

            foreach (KeyValuePair<int, BondingTaskInfo> kvp in taskInfo)
            {
                TaskData temp = TaskLoader.data[kvp.Key];
                if (kvp.Value.acceptBonding && temp.state == TaskState.NotAcceptAndCouldNot && temp.type == TaskType.BranchTask)
                    return 10001; // TEMP
            }
        }

        return dialogId;
    }

    public void SetNpcObj(Transform transform)
    {
        NpcHintMode mode = GetHintMode();

        if (mode ==  NpcHintMode.noHint)
        {
           transform.Find("Task").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Task").gameObject.SetActive(true);

            switch (mode)
            {
                case NpcHintMode.prepare:
                    transform.Find("Task").GetComponent<SpriteRenderer>().sprite = AtlasManager.GetInst().GetOtherItemIcon("cantAccept");
                    break;
                case NpcHintMode.accept:
                    transform.Find("Task").GetComponent<SpriteRenderer>().sprite = AtlasManager.GetInst().GetOtherItemIcon("canAccept");
                    break;
                case NpcHintMode.wait:
                    transform.Find("Task").GetComponent<SpriteRenderer>().sprite = AtlasManager.GetInst().GetOtherItemIcon("NotFinish");
                    break;
                case NpcHintMode.doing:
                    transform.Find("Task").GetComponent<SpriteRenderer>().sprite = AtlasManager.GetInst().GetOtherItemIcon("canAccept");
                    break;
                case NpcHintMode.complete:
                    transform.Find("Task").GetComponent<SpriteRenderer>().sprite = AtlasManager.GetInst().GetOtherItemIcon("ToFinish");
                    break;
            }
        }
    }
}

public class BondingTaskInfo
{
    public bool acceptBonding;
    public bool doingBonding;
    public bool returnBonding;

    public BondingTaskInfo()
    {

    }

    public BondingTaskInfo(bool arg1, bool arg2, bool arg3)
    {
        acceptBonding = arg1;
        doingBonding = arg2;
        returnBonding = arg3;
    }
}

public class Attributes
{
    // based on basic attributes
    public float hp = 0;
    public float mp = 0;
    public float atk = 0;
    public float def = 0;
    public float spd = 0;

    // based on gears and others
    public float pHp = 0;
    public float pMp = 0;
    private float patk = 0; // no less than -70%, at least 30% attack

    public float pAtk
    {
        get
        {
            if (patk < -70)
                return -70;
            else
                return patk;
        }
        set
        {
            patk = value;
        }
    }

    public float pDef = 0;
    private float pspd = 0; // no less than -50%, at least 50% speed

    public float pSpd
    {
        get
        {
            if (pspd < -70)
                return -70;
            else
                return pspd;
        }
        set
        {
            pspd = value;
        }
    }

    public float lucky = 0;
    public float crit = 0;
    public float cdmg = 0;
    public float armorPntr = 0;
    private float pdmg = 0; // no less than -70%, at least 30% damage

    public float pDmg
    {
        get
        {
            if (pdmg < -70)
                return -70;
            else
                return pdmg;
        }
        set
        {
            pdmg = value;
        }
    }

    private float rpdmgde = 0; // no less than -70%, at least receive 30% damage

    public float rpDmgde
    {
        get
        {
            if (rpdmgde > 70)
                return 70;
            else
                return rpdmgde;
        }
        set
        {
            rpdmgde = value;
        }
    }

    private float mpusede = 0; // no more than 70%, at least use 30% mana point

    public float mpUsede
    {
        get
        {
            if (mpusede > 70)
                return 70;
            else
                return mpusede;
        }
        set
        {
            mpusede = value;
        }
    }

    public float reflect = 0;
    public float lifeSteal = 0;

    // total, after calculate
    private float thp = 0;

    public float tHp
    {
        get
        {
            if (thp > 0)
                return thp;
            else
                return 0;
        }
        set
        {
            thp = Mathf.Min(value, (1.0f + pHp / 100.0f) * hp);
        }
    }

    private float tmp = 0;

    public float tMp
    {
        get
        {
            if (tmp > 0)
                return tmp;
            else
                return 0;
        }
        set
        {
            if (value < 0)
                tmp = 0;
            else
                tmp = Mathf.Min(value, (1.0f + pMp / 100.0f) * mp);
        }
    }

    public float tAtk = 0;
    public float tDef = 0;
    public float tSpd = 0;

    public Attributes()
    {

    }

    public Attributes(Attributes that)
    {
        this.hp = that.hp;
        this.mp = that.mp;
        this.atk = that.atk;
        this.def = that.def;
        this.spd = that.spd;
        this.pHp = that.pHp;
        this.pMp = that.pMp;
        this.pAtk = that.pAtk;
        this.pDef = that.pDef;
        this.pSpd = that.pSpd;
        this.lucky = that.lucky;
        this.crit = that.crit;
        this.cdmg = that.cdmg;
        this.armorPntr = that.armorPntr;
        this.pDmg = that.pDmg;
        this.rpDmgde = that.rpDmgde;
        this.mpUsede = that.mpUsede;
        this.reflect = that.reflect;
        this.lifeSteal = that.lifeSteal;
        Calculate();
    }

    public void Calculate(bool inFight = false)
    {
        if(!inFight)
        {
            tHp = hp * (1 + pHp / 100.0f);
            tMp = mp * (1 + pMp / 100.0f);
        }
        tAtk = atk * (1 + pAtk / 100.0f);
        tDef = def * (1 + pDef / 100.0f);

        if (pSpd >= -50)
            tSpd = spd * (1 + pSpd / 100.0f);
        else
            tSpd = spd * 0.5f;
    }

    public static float DefCurve(float defense)
    {
        if (defense < 0)
            return 0.0f;
        else if (defense > 330)
            return 90.0f;
        else
            return ItemLoader.defCurve[(int)defense];
    }
}

public class Monster : CharacterB
{
    // AI & Skill for monster
    public int aiId;
    public List<int> skillList = new List<int>();

    // Award for monster
    public float awardExp;
    public float awardGold;

    public Monster() : base()
    {

    }

    public Monster(Monster that) : base(that)
    {
        this.aiId = that.aiId;
        this.skillList = new List<int>(that.skillList);
        this.awardExp = that.awardExp;
        this.awardGold = that.awardGold;
    }
}

public class CharacterB
{
    public string name;
    public string cnName;
    public string objPath;
    public CharacterType charType;
    public bool isEnemy;

    // Attributes
    public Attributes attriBasic = new Attributes();
    public Attributes attriItems = new Attributes();
    public Attributes attriRuntime = new Attributes();

    // Runtime
    public GameObject battleObj;
    public int absRank;
    public Vector2 returnPos;
    public bool dizzy;
    public bool dead;
    public float actionLoading;
    public List<Buff> buffList = new List<Buff>();

    public GameObject buffBar;
    public GameObject hpSlider;
    public GameObject mpSlider;
    public GameObject speedSlider;

    public CharacterB()
    {

    }

    public CharacterB(CharacterB that)
    {
        this.name = that.name;
        this.cnName = that.cnName;
        this.objPath = that.objPath;
        this.charType = that.charType;
        this.isEnemy = that.isEnemy;
        this.attriBasic = new Attributes(that.attriBasic);
        {
            this.attriItems = new Attributes(that.attriBasic);
            this.attriRuntime = new Attributes(that.attriBasic);
        }

        this.BattleEndClean();
    }

    public void SyncRuntimeAttributes()
    {
        attriRuntime.hp = attriItems.hp;
        attriRuntime.mp = attriItems.mp;
        attriRuntime.atk = attriItems.atk;
        attriRuntime.def = attriItems.def;
        attriRuntime.spd = attriItems.spd;
        attriRuntime.pHp = attriItems.pHp;
        attriRuntime.pMp = attriItems.pMp;
        attriRuntime.pAtk = attriItems.pAtk;
        attriRuntime.pDef = attriItems.pDef;
        attriRuntime.pSpd = attriItems.pSpd;
        attriRuntime.lucky = attriItems.lucky;
        attriRuntime.crit = attriItems.crit;
        attriRuntime.cdmg = attriItems.cdmg;
        attriRuntime.armorPntr = attriItems.armorPntr;
        attriRuntime.pDmg = attriItems.pDmg;
        attriRuntime.rpDmgde = attriItems.rpDmgde;
        attriRuntime.mpUsede = attriItems.mpUsede;
        attriRuntime.reflect = attriItems.reflect;
        attriRuntime.lifeSteal = attriItems.lifeSteal;
        attriRuntime.Calculate(true); // don't sync total hp and total mp
    }

    public void UpdateHpPosition()
    {
        if (hpSlider == null)
        {
            hpSlider = SingletonObject<HpMedia>.GetInst().AddHpFrame();
        }

        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(battleObj.transform.position);

        hpSlider.transform.position = new Vector2(player2DPosition.x, player2DPosition.y + 130);
    }

    public void UpdateMpPosition()
    {
        if (mpSlider == null)
        {
            mpSlider = SingletonObject<MpMedia>.GetInst().AddMpFrame();
        }

        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(battleObj.transform.position);

        mpSlider.transform.position = new Vector2(player2DPosition.x, player2DPosition.y + 115);
    }

    public void UpdateSpeedPosition()
    {
        if (speedSlider == null)
        {
            speedSlider = SingletonObject<SpeedMedia>.GetInst().AddSpeedFrame();
        }

        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(battleObj.transform.position);

        speedSlider.transform.position = new Vector2(player2DPosition.x, player2DPosition.y + 100);
    }

    public void UpdateHp()
    {
        if (hpSlider == null)
        {
            hpSlider = SingletonObject<HpMedia>.GetInst().AddHpFrame();
        }
        hpSlider.GetComponent<Slider>().value = attriRuntime.tHp / attriItems.tHp;
    }

    public void UpdateMp()
    {
        if (mpSlider == null)
        {
            mpSlider = SingletonObject<MpMedia>.GetInst().AddMpFrame();
        }
        mpSlider.GetComponent<Slider>().value = attriRuntime.tMp / attriItems.tMp;
    }

    public void UpdateSpeed()
    {
        if (speedSlider == null)
        {
            speedSlider = SingletonObject<SpeedMedia>.GetInst().AddSpeedFrame();
        }
        speedSlider.GetComponent<Slider>().value = actionLoading / 100.0f;
    }

    public void UpdateBuffPosition()
    {
        if (buffBar == null)
        {
            buffBar = SingletonObject<BuffMedia>.GetInst().AddBuffBar("TEST");
        }

        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(battleObj.transform.position);

        buffBar.transform.position = new Vector2(player2DPosition.x, player2DPosition.y - 25);
    }

    public void UpdateBuff()
    {
        if (buffBar == null)
            buffBar = SingletonObject<BuffMedia>.GetInst().AddBuffBar("TEST");

        for (int i = 0; i < buffBar.transform.childCount; ++i)
            buffBar.transform.GetChild(i).gameObject.SetActive(false);

        for (int i = 0; i < buffList.Count; ++i)
            SingletonObject<BuffMedia>.GetInst().AddBuff(ref buffBar, "TEST", buffList[i]);

        CommonHandle.SyncBuffAttributes(this);
    }

    public void BuffDestory()
    {
        if (buffBar == null)
            buffBar = SingletonObject<BuffMedia>.GetInst().AddBuffBar("TEST");

        List<int> deleteList = new List<int>();

        for (int i = 0; i < buffList.Count; ++i)
        {
            if (!buffList[i].infinity && buffList[i].lastRounds == 0)
                deleteList.Add(i);
            if (buffList[i].infinity && buffList[i].level == 0)
                deleteList.Add(i);
        }

        for (int i = deleteList.Count - 1; i >= 0; --i)
            buffList.RemoveAt(deleteList[i]);

        UpdateBuff();
    }

    public Buff GetBuff(string name)
    {
        foreach (Buff buff in buffList)
            if (buff.name == name)
                return buff;
        return null;
    }

    public void DeathCloseUI()
    {
        hpSlider.SetActive(false);
        mpSlider.SetActive(false);
        speedSlider.SetActive(false);
        buffBar.gameObject.SetActive(false);
    }

    public void BattleEndClean()
    {
        battleObj = null;
        buffBar = null;
        hpSlider = null;
        mpSlider = null;
        speedSlider = null;
        buffList = new List<Buff>();
    }
}

public class Buff
{
    public string name;
    public string cnName;
    public string describe;
    public string cnDescribe;
    public string iconPath;
    public bool infinity;
    public bool addable;
    public bool timeOrTimes;
    public int totalRounds;
    public int maxLevel;
    public BuffType type;
    public List<string> buffData = new List<string>();

    public bool toself;

    // Runtime
    public int lastRounds;
    public int level;
    public CharacterB subjectC;
    public CharacterB objectC;

    public Buff()
    {

    }

    public Buff(Buff that)
    {
        this.name = that.name;
        this.cnName = that.cnName;
        this.describe = that.describe;
        this.cnDescribe = that.cnDescribe;
        this.iconPath = that.iconPath;
        this.infinity = that.infinity;
        this.addable = that.addable;
        this.timeOrTimes = that.timeOrTimes;
        this.lastRounds = that.lastRounds;
        this.totalRounds = that.totalRounds;
        this.level = that.level;
        this.maxLevel = that.maxLevel;
        this.type = that.type;
        this.buffData = new List<string>(that.buffData);
        this.toself = that.toself;
    }
}

public class Skill
{
    public string name;
    public string cnName;
    public bool isAoe;
    public bool toSelf;

    public List<string> animationOrders = new List<string>();
    public List<Vector2> moveOrders = new List<Vector2>();
    public Dictionary<int, float> flyObjectOrders = new Dictionary<int, float>();
    public Dictionary<DamageEle, float> damageOrders = new Dictionary<DamageEle, float>();
    public Dictionary<int, float> buffOrders = new Dictionary<int, float>();
}

public class FlyingObject
{
    public string name;
    public string path;
    public Vector2 start;
    public Vector2 end;
    public float moveSpeed;
    public DamageEle damageEle = new DamageEle();
    public float duration;
    public List<int> addBuffID = new List<int>();
}

public class DamageEle
{
    public float atkBonus;
    public DamageType dmgType;

    public DamageEle()
    {

    }

    public DamageEle(float atkBonus, DamageType dmgType)
    {
        this.atkBonus = atkBonus;
        this.dmgType = dmgType;
    }

    public DamageEle(DamageEle that)
    {
        this.atkBonus = that.atkBonus;
        this.dmgType = that.dmgType;
    }
}