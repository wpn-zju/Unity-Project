public enum DamageType
{
    Physical = 0,
    Magical = 1,
    Heal = 2,
}

public enum ItemType
{
    Equipment = 0,
    Rune = 1,
    Others = 2,
    CanUse = 3,
}

public enum ItemTypeCN
{
    装备 = 0,
    符石 = 1,
    其他物品 = 2,
    消耗品 = 3,
}

public enum ItemQuality
{
    No = 0,         // for equipment slots that is not used
    Normal = 1,
    Good = 2,
    Superior = 3,
    Epic = 4,
}

public enum ItemQualityCN
{
    无 = 0,
    普通 = 1,
    精良 = 2,
    稀有 = 3,
    极品 = 4,
}

public enum EquipmentPart
{
    Weapon = 0,
    SubWeapon = 1,
    Armor = 2,
    Hat = 3,
    Necklace = 4,
    Ring = 5,
}

public enum EquipmentPartCN
{
    武器 = 0,
    副武器 = 1,
    上衣 = 2,
    帽子 = 3,
    项链 = 4,
    戒指 = 5,
}

public enum TriggerType
{
    changeNpcDialog = 1,
    openShop = 2,
    openTask = 3,
    acceptTask = 4,
    completeTask = 5,
    taskConditionEnabled = 6,
    startDialogDirectly = 7,
    sameSceneGoTo = 8,
    gainItem = 9,
    bgmMusic = 10,
    effectsMusic = 11,
    revive = 100,
    theend = 101,
}

public enum TaskState
{
    NotAcceptAndCouldNot = 0,
    NotAcceptAndCould = 1,
    AcceptedAndUnfinished = 2,
    FinishedButNotAwarded = 3,
    FinishedAndAwarded = 4,
    Failed = 5,
}

public enum TaskType
{
    MainTask = 0,
    BranchTask = 1,
}

public enum RuneType
{
    active = 1,
    attributes = 2,
    unactive = 3,
}

public enum WeaponType
{
    not_a_weapon = 0,
    melee = 1,
    magic = 2,
    range = 3,
}

public enum BuffType
{
    shuxing = 1,
    liushi = 2,
    biaoji = 3,
    yunxuan = 4,
}

public enum CharacterType
{
    Player = 0,
    NormalMonster = 1,
    EliteMonster = 2,
    Boss = 3,
    Summon = 4,
    Teammate = 5,
}

public enum CharacterTypeCN
{
    玩家 = 0,
    普通怪物 = 1,
    精英怪物 = 2,
    首领怪物 = 3,
    召唤物 = 4,
    队友 = 5,
}

public enum NpcHintMode
{
    noHint = 0,
    prepare = 1,
    accept = 2,
    wait = 3,
    doing = 4,
    complete = 5,
}

public enum GuideMode
{
    prologue = 0,
    end1 = 1,
    end2 = 2,
    end3 = 3,
    completeHint = 4,
}

public enum SlotType
{
    Equipment = 1,  // Slots on the equipment board
    Rune = 2,       // Slots on the rune board
    InBag = 3,      // Slots on the backpack board
    Reference = 4,  // Reference
}

public enum ShopMode
{
    buy = 1,
    sell = 2,
    build = 3,
    customize = 4,
}

public enum AIStrategyType
{
    imposing = 1,   // imposing
    hplt = 2,       // active if hp less than
    hpemt = 3,      // active if hp more than or equal to
    def = 4,        // default
}

public enum Language
{
    English = 0,
    Chinese = 1,
}

public enum ActionType
{
    xScale = 0,
    yScale = 1,
    xPosition = 2,
    yPosition = 3,
    alpha = 4,
}

public enum StateType
{
    idle = 0,
    run = 1,
}

public enum SceneCharacterType
{
    monster = 1,
    npc = 2,
}