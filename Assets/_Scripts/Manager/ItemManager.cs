using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ItemManager : SingletonObject<ItemManager>
{
    public List<BaseItemData> itemList = new List<BaseItemData>();
    public EquipmentItemData[] equippedData = new EquipmentItemData[6];
    public BaseItemData[] runeData = new BaseItemData[4];

    public bool dirtyBit = false;

    public void Init()
    {

    }

    public void Equip(int slotId, bool unEquip = false)
    {
        if (unEquip)
        {
            GainNewItem(equippedData[slotId]);
            equippedData[slotId] = null;
        }
        else
        {
            int partId = (int)((EquipmentItemData)itemList[slotId]).part;

            if (equippedData[partId] == null)
            {
                equippedData[partId] = (EquipmentItemData)itemList[slotId];
                RemoveItem(slotId, SlotType.InBag, -1);
            }
            else
            {
                GainNewItem(equippedData[partId]);
                equippedData[partId] = (EquipmentItemData)itemList[slotId];
                RemoveItem(slotId, SlotType.InBag, -1);
            }
        }

        SyncItemAttributes();

        Arrange();
    }

    public void UseRune(int slotId, bool unUse = false)
    {
        bool found = false;

        if(unUse)
        {
            GainNewItem(runeData[slotId]);
            runeData[slotId] = null;
        }
        else
        {
            foreach(BaseItemData v in runeData)
            {
                if (v != null)
                {
                    if (v.id_int == itemList[slotId].id_int)
                    {
                        if (GameSettings.language == Language.English)
                            SingletonObject<HintMedia>.GetInst().SendHint("You can not equip same runes.", Color.red);
                        else
                            SingletonObject<HintMedia>.GetInst().SendHint("你不能装备两块相同的符石", Color.red);
                        return;
                    }
                }
            }

            for (int i = 0; i < runeData.Length; ++i)
            {
                if (runeData[i] == null)
                {
                    found = true;
                    runeData[i] = itemList[slotId];
                    RemoveItem(slotId, SlotType.InBag, 1);

                    break; // find empty slot, break loop
                }
            }

            if (!found)
            {
                GainNewItem(runeData[0]);
                runeData[0] = itemList[slotId];
                RemoveItem(slotId, SlotType.InBag, 1);
            }
        }

        SyncItemAttributes();

        Arrange();
    }

    public void UseItem(int slotId)
    {
        int randomNum;
        int rollItemId = 10000;
        ItemQuality quality = ItemQuality.Normal;

        switch(itemList[slotId].id_int)
        {
            case 70001:
                randomNum = UnityEngine.Random.Range(0, 10000) % 9;
                rollItemId = 80000 + randomNum + 1;
                break;
            case 70002:
                randomNum = UnityEngine.Random.Range(0, 10000) % 6 + 1;
                rollItemId = randomNum * 10000 + 1;
                quality = ItemQuality.Normal;
                break;
            case 70003:
                randomNum = UnityEngine.Random.Range(0, 10000) % 6 + 1;
                rollItemId = randomNum * 10000 + 1;
                quality = ItemQuality.Good;
                break;
            case 70004:
                randomNum = UnityEngine.Random.Range(0, 10000) % 6 + 1;
                rollItemId = randomNum * 10000 + 1;
                quality = ItemQuality.Superior;
                break;
            case 70005:
                randomNum = UnityEngine.Random.Range(0, 10000) % 6 + 1;
                rollItemId = randomNum * 10000 + 1;
                quality = ItemQuality.Epic;
                break;
        }

        if (rollItemId != 10000)
            GainNewItem(rollItemId, 1, quality);

        RemoveItem(slotId, SlotType.InBag);

        Arrange();
    }

    public void SyncItemAttributes()
    {
        int warriorLevel = 0;
        int magicLevel = 0;
        int archerLevel = 0;
        for (int i = 0; i < 5; i++)
        {
            if (PlayerData.GetInst().skillData[i] == 1)
                warriorLevel++;
            if (PlayerData.GetInst().skillData[5 + i] == 1)
                magicLevel++;
            if (PlayerData.GetInst().skillData[10 + i] == 1)
                archerLevel++;
        }

        PlayerData.GetInst().charB.attriBasic.def = CharLoader.dataTotal[10000 + PlayerData.GetInst().level].attriBasic.def * (1.0f + 0.05f * warriorLevel);
        PlayerData.GetInst().charB.attriBasic.atk = CharLoader.dataTotal[10000 + PlayerData.GetInst().level].attriBasic.atk * (1.0f + 0.05f * magicLevel);
        PlayerData.GetInst().charB.attriBasic.spd = CharLoader.dataTotal[10000 + PlayerData.GetInst().level].attriBasic.spd * (1.0f + 0.05f * archerLevel);

        Attributes withItem = new Attributes(PlayerData.GetInst().charB.attriBasic);

        for (int i = 0; i < equippedData.Length; ++i)
        {
            if (equippedData[i] != null)
            {
                if(equippedData[i].GetBit(0))
                    withItem.pHp += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(1))
                    withItem.pMp += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(2))
                    withItem.pAtk += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(3))
                    withItem.pDef += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(4))
                    withItem.pSpd += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(5))
                    withItem.lucky += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(6))
                    withItem.crit += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(7))
                    withItem.cdmg += 10.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(8))
                    withItem.armorPntr += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(9))
                    withItem.mpUsede += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(10))
                    withItem.reflect += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
                if (equippedData[i].GetBit(11))
                    withItem.lifeSteal += 5.0f * ItemManager.quality2Entries[equippedData[i].quality];
            }
        }

        for (int i = 0; i < runeData.Length; ++i)
        {
            if (runeData[i] != null)
            {
                if (RuneLoader.data[runeData[i].id_int % 10000].type == RuneType.attributes)
                {
                    RuneData rune = RuneLoader.data[runeData[i].id_int % 10000];

                    for (int j = 0; j < rune.data.Count; j += 2)
                    {
                        switch (rune.data[j])
                        {
                            case "pHp":
                                withItem.pHp += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "pMp":
                                withItem.pMp += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "pAtk":
                                withItem.pAtk += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "pDef":
                                withItem.pDef += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "pSpd":
                                withItem.pSpd += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "lucky":
                                withItem.lucky += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "crit":
                                withItem.crit += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "cdmg":
                                withItem.cdmg += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "mpUsede":
                                withItem.mpUsede += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "reflect":
                                withItem.reflect += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "atkHeal":
                                withItem.lifeSteal += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                            case "igDef":
                                withItem.armorPntr += (float)Convert.ToDouble(rune.data[j + 1]);
                                break;
                        }
                    }
                }
            }
        }

        PlayerData.GetInst().charB.attriItems = new Attributes(withItem);
        PlayerData.GetInst().charB.SyncRuntimeAttributes();
    }

    public void RemoveItem(int slotId, SlotType type, int count = 1)
    {
        switch(type)
        {
            case SlotType.InBag:
                if (count == -1)
                    itemList.RemoveAt(slotId);
                else
                {
                    itemList[slotId].number -= count;
                    if (itemList[slotId].number == 0)
                        itemList.RemoveAt(slotId);
                }
                break;
            case SlotType.Equipment:
                equippedData[slotId] = null;
                break;
            case SlotType.Rune:
                runeData[slotId] = null;
                break;
        }

        UpdateItem();
    }

    public void GainNewItem(BaseItemData item)
    {
        if (item.type == ItemType.Equipment)
        {
            itemList.Add(item);
        }
        else
        {
            if (FindItemSlot(item.id_int) != -1)
            {
                itemList[FindItemSlot(item.id_int)].number++;
            }
            else
            {
                item.number = 1;

                itemList.Add(item);
            }
        }

        UpdateItem();

        dirtyBit = true;SingletonObject<FightMedia>.GetInst().RefreshHint();
    }

    public void GainNewItem(int id_int, int count = 1, ItemQuality quality = ItemQuality.Normal, WeaponType weaponType = WeaponType.not_a_weapon)
    {
        bool found = false;

        if (ItemLoader.dataTotal[id_int].type == ItemType.Equipment)
        {
            for (int i = 0; i < count; ++i)
            {
                EquipmentItemData temp = new EquipmentItemData((EquipmentItemData)ItemLoader.dataTotal[id_int]);
                temp.RandomResource();
                temp.RandomAttri(quality);

                if (weaponType == WeaponType.not_a_weapon)
                    itemList.Add(temp);
                else
                {
                    while (temp.weaponType != weaponType)
                        temp.RandomResource();
                    itemList.Add(temp);
                }

                if (GameSettings.language == Language.English)
                    SingletonObject<HintMedia>.GetInst().SendHint("Gain New Equipment [" + temp.name + "]", Color.green);
                else
                    SingletonObject<HintMedia>.GetInst().SendHint("获得装备 [" + temp.cnName + "]", Color.green);
            }
        }
        else
        {
            // have same item addable
            foreach (BaseItemData v in itemList)
            {
                if(v.id_int == id_int)
                {
                    found = true;
                    v.number += count;
                }
            }
            // cant find same item
            if (!found)
            {
                BaseItemData temp = new BaseItemData(ItemLoader.dataTotal[id_int]);
                temp.number = count;
                itemList.Add(temp);
            }

            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("Gain New Item [" + ItemLoader.dataTotal[id_int].name + "]", Color.green);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("获得道具 [" + ItemLoader.dataTotal[id_int].cnName + "]", Color.green);
        }

        UpdateItem();

        dirtyBit = true;SingletonObject<FightMedia>.GetInst().RefreshHint();
    }

    public void GetBattleAward(int monsterId)
    {
        CharacterB charB = CharLoader.dataTotal[monsterId];

        if(charB is Monster)
        {
            PlayerData.GetInst().GetExp((int)(charB as Monster).awardExp);
            PlayerData.GetInst().GetGold((int)(charB as Monster).awardGold);
        }

        int randomNum1 = UnityEngine.Random.Range(0, 10000) % 100 + 1; // Equipment Percent
        int randomNum2 = UnityEngine.Random.Range(0, 10000) % 100 + 1; // Rune Percent
        int randomNum3 = UnityEngine.Random.Range(0, 10000) % 6 + 1; // Equipment Part
        int randomNum4 = UnityEngine.Random.Range(0, 10000) % (ItemLoader.dataOther.Count - 5) + 1; // Rune Id

        switch (charB.charType)
        {
            case CharacterType.NormalMonster:
                {
                    if (randomNum1 == 1)
                        ItemManager.GetInst().GainNewItem(10000 * randomNum3 + 1, 1, ItemQuality.Epic);
                    else if (randomNum1 <= 4)
                        ItemManager.GetInst().GainNewItem(10000 * randomNum3 + 1, 1, ItemQuality.Superior);
                    else if (randomNum1 <= 14)
                        ItemManager.GetInst().GainNewItem(10000 * randomNum3 + 1, 1, ItemQuality.Good);
                    else if (randomNum1 <= 44)
                        ItemManager.GetInst().GainNewItem(10000 * randomNum3 + 1, 1, ItemQuality.Normal);
                    if (randomNum2 == 1)
                        ItemManager.GetInst().GainNewItem(80000 + randomNum4, 1);
                }
                break;
            case CharacterType.EliteMonster:
                {
                    if (randomNum1 <= 3)
                        ItemManager.GetInst().GainNewItem(10000 * randomNum3 + 1, 1, ItemQuality.Epic);
                    else if (randomNum1 <= 13)
                        ItemManager.GetInst().GainNewItem(10000 * randomNum3 + 1, 1, ItemQuality.Superior);
                    else if (randomNum1 <= 43)
                        ItemManager.GetInst().GainNewItem(10000 * randomNum3 + 1, 1, ItemQuality.Good);
                    else if (randomNum1 <= 100)
                        ItemManager.GetInst().GainNewItem(10000 * randomNum3 + 1, 1, ItemQuality.Normal);
                    if (randomNum2 <= 5)
                        ItemManager.GetInst().GainNewItem(80000 + randomNum4, 1);
                }
                break;
            case CharacterType.Boss:
                {
                    if (randomNum1 <= 100)
                        ItemManager.GetInst().GainNewItem(10000 * randomNum3 + 1, 1, ItemQuality.Epic);
                    if (randomNum2 <= 100)
                        ItemManager.GetInst().GainNewItem(80000 + randomNum4, 1);
                }
                break;
        }
    }

    public int ItemNumber(int id_int)
    {
        if (FindItemSlot(id_int) == -1)
            return 0;
        else
            return itemList[FindItemSlot(id_int)].number;
    }

    public int FindItemSlot(int id_int)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].id_int == id_int)
                return i;
        }

        return -1;
    }

    public void Arrange()
    {
        itemList = itemList.OrderByDescending(p => (int)p.type).ThenByDescending(p => (int)p.quality).ThenByDescending(p => p.id_int).ToList();

        UpdateItem();
    }

    public void UpdateItem()
    {
        SingletonObject<ItemMedia>.GetInst().Refresh(itemList, equippedData, runeData);
    }

    public static Dictionary<ItemQuality, int> quality2PriceIn = new Dictionary<ItemQuality, int>()
    {
        { ItemQuality.Normal, 5},
        { ItemQuality.Good, 20},
        { ItemQuality.Superior,  100},
        { ItemQuality.Epic, 500},
    };

    public static Dictionary<ItemQuality, int> quality2PriceOut = new Dictionary<ItemQuality, int>()
    {
        { ItemQuality.Normal,  4},
        { ItemQuality.Good, 16},
        { ItemQuality.Superior,  80},
        { ItemQuality.Epic, 400},
    };

    public static Dictionary<ItemQuality, float> quality2Entries = new Dictionary<ItemQuality, float>()
    {
        { ItemQuality.Normal, 1.3f},
        { ItemQuality.Good, 1.2f},
        { ItemQuality.Superior, 1.1f},
        { ItemQuality.Epic, 1.0f},
    };
}