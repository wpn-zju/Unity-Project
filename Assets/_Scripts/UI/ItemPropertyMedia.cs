using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPropertyMedia : BaseMedia
{
    private GameObject itemPropertyPrefab;
    private GameObject btnPrefab;

    private SlotType openMode;
    private int slotId;

    private Button exitButton;

    public override void Init()
    {
        base.Init();

        itemPropertyPrefab = Resources.Load<GameObject>("MyResource/Panel/ItemProperty");
        btnPrefab = Resources.Load<GameObject>("MyResource/Panel/ItemButton");

        exitButton = panel.Find("Blocker").GetComponent<Button>();
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(() =>
        {
            Close();
        });
    }

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < panel.Find("Properties").childCount; ++i)
        {
            GameObject.Destroy(panel.Find("Properties").GetChild(i).gameObject);
        }

        switch(openMode)
        {
            case SlotType.Equipment:
                OpenEquipment();
                break;
            case SlotType.Rune:
                OpenRune();
                break;
            case SlotType.InBag:
                OpenInBag();
                break;
            case SlotType.Reference:
                OpenReference();
                break;
            default:
                break;
        }
    }

    public override void Close()
    {
        base.Close();
    }

    public void SetMode(SlotType openMode, int slotId)
    {
        this.openMode = openMode;
        this.slotId = slotId;
    }

    private void OpenEquipment()
    {
        BaseItemData item1Data = ItemManager.GetInst().equippedData[slotId];
        GameObject item1 = GameObject.Instantiate(itemPropertyPrefab, panel.Find("Properties"));

        SetDescribe(item1.transform, item1Data);

        Transform buttonBar = item1.transform.Find("ButtonBar");
        GameObject button1 = GameObject.Instantiate(btnPrefab, buttonBar);
        GameObject button2 = GameObject.Instantiate(btnPrefab, buttonBar);

        button1.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Unequip" : "卸下";
        button1.GetComponent<Image>().color = Color.white;
        button1.GetComponent<Button>().onClick.RemoveAllListeners();
        button1.GetComponent<Button>().onClick.AddListener(() =>
        {
            ItemManager.GetInst().Equip(slotId, true);
            Close();
        });

        button2.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Abandon" : "丢弃";
        button2.GetComponent<Image>().color = Color.white;
        button2.GetComponent<Button>().onClick.RemoveAllListeners();
        button2.GetComponent<Button>().onClick.AddListener(() =>
        {
            ItemManager.GetInst().RemoveItem(slotId, SlotType.Equipment);
            Close();
        });
    }

    private void OpenRune()
    {
        BaseItemData item1Data = ItemManager.GetInst().runeData[slotId];
        GameObject item1 = GameObject.Instantiate(itemPropertyPrefab, panel.Find("Properties"));

        SetDescribe(item1.transform, item1Data);

        Transform buttonBar = item1.transform.Find("ButtonBar");
        GameObject button1 = GameObject.Instantiate(btnPrefab, buttonBar);
        GameObject button2 = GameObject.Instantiate(btnPrefab, buttonBar);

        button1.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Disuse" : "卸下";
        button1.GetComponent<Image>().color = Color.white;
        button1.GetComponent<Button>().onClick.RemoveAllListeners();
        button1.GetComponent<Button>().onClick.AddListener(() =>
        {
            ItemManager.GetInst().UseRune(slotId, true);
            Close();
        });

        button2.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Abandon" : "丢弃";
        button2.GetComponent<Image>().color = Color.white;
        button2.GetComponent<Button>().onClick.RemoveAllListeners();
        button2.GetComponent<Button>().onClick.AddListener(() =>
        {
            ItemManager.GetInst().RemoveItem(slotId, SlotType.Rune);
            Close();
        });
    }

    private void OpenInBag()
    {
        BaseItemData item1Data, item2Data;
        GameObject item1, item2;
        Transform buttonBar;
        GameObject button1, button2;

        switch(ItemManager.GetInst().itemList[slotId].type)
        {
            case ItemType.Equipment:
                int partId = (int)(((EquipmentItemData)ItemManager.GetInst().itemList[slotId]).part);
                bool hasEquipped = ItemManager.GetInst().equippedData[partId] != null;

                if (hasEquipped)
                {
                    item2Data = ItemManager.GetInst().equippedData[partId];
                    item2 = GameObject.Instantiate(itemPropertyPrefab, panel.Find("Properties"));

                    SetDescribe(item2.transform, item2Data);
                }

                item1Data = ItemManager.GetInst().itemList[slotId];
                item1 = GameObject.Instantiate(itemPropertyPrefab, panel.Find("Properties"));

                SetDescribe(item1.transform, item1Data);

                buttonBar = item1.transform.Find("ButtonBar");
                button1 = GameObject.Instantiate(btnPrefab, buttonBar);
                button2 = GameObject.Instantiate(btnPrefab, buttonBar);

                button1.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Equip" : "穿戴";
                button1.GetComponent<Image>().color = Color.white;
                button1.GetComponent<Button>().onClick.RemoveAllListeners();
                button1.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ItemManager.GetInst().Equip(slotId, false);
                    Close();
                });

                button2.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Abandon" : "丢弃";
                button2.GetComponent<Image>().color = Color.white;
                button2.GetComponent<Button>().onClick.RemoveAllListeners();
                button2.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ItemManager.GetInst().RemoveItem(slotId, SlotType.InBag, -1);
                    Close();
                });
                break;
            case ItemType.Rune:
                item1Data = ItemManager.GetInst().itemList[slotId];
                item1 = GameObject.Instantiate(itemPropertyPrefab, panel.Find("Properties"));

                SetDescribe(item1.transform, item1Data);

                buttonBar = item1.transform.Find("ButtonBar");
                button1 = GameObject.Instantiate(btnPrefab, buttonBar);
                button2 = GameObject.Instantiate(btnPrefab, buttonBar);

                button1.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Use" : "使用";
                button1.GetComponent<Image>().color = Color.white;
                button1.GetComponent<Button>().onClick.RemoveAllListeners();
                button1.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ItemManager.GetInst().UseRune(slotId, false);
                    Close();
                });

                button2.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Abandon" : "丢弃";
                button2.GetComponent<Image>().color = Color.white;
                button2.GetComponent<Button>().onClick.RemoveAllListeners();
                button2.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ItemManager.GetInst().RemoveItem(slotId, SlotType.InBag, -1);
                    Close();
                });
                break;
            case ItemType.CanUse:
                item1Data = ItemManager.GetInst().itemList[slotId];
                item1 = GameObject.Instantiate(itemPropertyPrefab, panel.Find("Properties"));

                SetDescribe(item1.transform, item1Data);

                buttonBar = item1.transform.Find("ButtonBar");
                button1 = GameObject.Instantiate(btnPrefab, buttonBar);
                button2 = GameObject.Instantiate(btnPrefab, buttonBar);

                button1.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Use" : "使用";
                button1.GetComponent<Image>().color = Color.white;
                button1.GetComponent<Button>().onClick.RemoveAllListeners();
                button1.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ItemManager.GetInst().UseItem(slotId);
                    Close();
                });

                button2.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Abandon" : "丢弃";
                button2.GetComponent<Image>().color = Color.white;
                button2.GetComponent<Button>().onClick.RemoveAllListeners();
                button2.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ItemManager.GetInst().RemoveItem(slotId, SlotType.InBag, -1);
                    Close();
                });
                break;
            case ItemType.Others:
                item1Data = ItemManager.GetInst().itemList[slotId];
                item1 = GameObject.Instantiate(itemPropertyPrefab, panel.Find("Properties"));

                SetDescribe(item1.transform, item1Data);

                buttonBar = item1.transform.Find("ButtonBar");
                button1 = GameObject.Instantiate(btnPrefab, buttonBar);

                button1.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Abandon" : "丢弃";
                button1.GetComponent<Image>().color = Color.white;
                button1.GetComponent<Button>().onClick.RemoveAllListeners();
                button1.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ItemManager.GetInst().RemoveItem(slotId, SlotType.InBag, -1);
                    Close();
                });
                break;
        }
    }

    private void OpenReference()
    {
        BaseItemData item1Data = ItemLoader.dataTotal[slotId];
        GameObject item1 = GameObject.Instantiate(itemPropertyPrefab, panel);

        SetDescribe(item1.transform, item1Data);

        if (item1Data.type == ItemType.Equipment)
        {
            if (ItemManager.GetInst().equippedData[(int)((EquipmentItemData)item1Data).part] != null)
            {
                BaseItemData item2Data = ItemManager.GetInst().equippedData[(int)((EquipmentItemData)item1Data).part];
                GameObject item2 = GameObject.Instantiate(itemPropertyPrefab, panel);

                SetDescribe(item2.transform, item2Data);
            }
        }
    }

    /* shift -> attributes
       0  -> pHp
       1  -> pMp
       2  -> pAtk
       3  -> pDef
       4  -> pSpd
       5  -> lucky
       6  -> crit
       7  -> cdmg
       8  -> igDef
       9  -> mpUsede
       10 -> reflect
       11 -> atkHeal
       ............
    */

    // static for shopmedia
    public static void SetDescribe(Transform transform, BaseItemData item)
    {
        transform.Find("Icon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(item.iconPath);

        Transform textTransform = transform.Find("Describe/Texts");

        // public
        if(GameSettings.language == Language.English)
        {
            textTransform.Find("Name").GetComponent<Text>().text = item.name;
            textTransform.Find("Price").GetComponent<Text>().text = "Sell Price : " + item.priceOut.ToString();
            textTransform.Find("Type").GetComponent<Text>().text = item.type.ToString();
            textTransform.Find("Quality").GetComponent<Text>().text = "Quality : " + item.quality.ToString();
        }
        else
        {
            textTransform.Find("Name").GetComponent<Text>().text = item.cnName;
            textTransform.Find("Price").GetComponent<Text>().text = "出售价格 : " + item.priceOut.ToString();
            textTransform.Find("Type").GetComponent<Text>().text = ((ItemTypeCN)item.type).ToString();
            textTransform.Find("Quality").GetComponent<Text>().text = "品质 : " + ((ItemQualityCN)item.quality).ToString();
        }

        textTransform.Find("Hp").gameObject.SetActive(false);
        textTransform.Find("Mp").gameObject.SetActive(false);
        textTransform.Find("Atk").gameObject.SetActive(false);
        textTransform.Find("Def").gameObject.SetActive(false);
        textTransform.Find("Spd").gameObject.SetActive(false);
        textTransform.Find("Lucky").gameObject.SetActive(false);
        textTransform.Find("Crit").gameObject.SetActive(false);
        textTransform.Find("Cdmg").gameObject.SetActive(false);
        textTransform.Find("IgDef").gameObject.SetActive(false);
        textTransform.Find("MpUsede").gameObject.SetActive(false);
        textTransform.Find("Reflect").gameObject.SetActive(false);
        textTransform.Find("AtkHeal").gameObject.SetActive(false);
        textTransform.Find("Space2").gameObject.SetActive(false);

        // private
        if(GameSettings.language == Language.English)
        {
            switch (item.type)
            {
                case ItemType.Equipment:

                    EquipmentItemData temp = (EquipmentItemData)item;

                    if (temp.GetBit(0))
                    {
                        textTransform.Find("Hp").gameObject.SetActive(true);
                        textTransform.Find("Hp").GetComponent<Text>().text = "Hp + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(1))
                    {
                        textTransform.Find("Mp").gameObject.SetActive(true);
                        textTransform.Find("Mp").GetComponent<Text>().text = "Mp + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(2))
                    {
                        textTransform.Find("Atk").gameObject.SetActive(true);
                        textTransform.Find("Atk").GetComponent<Text>().text = "Atk + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(3))
                    {
                        textTransform.Find("Def").gameObject.SetActive(true);
                        textTransform.Find("Def").GetComponent<Text>().text = "Def + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(4))
                    {
                        textTransform.Find("Spd").gameObject.SetActive(true);
                        textTransform.Find("Spd").GetComponent<Text>().text = "Spd + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(5))
                    {
                        textTransform.Find("Lucky").gameObject.SetActive(true);
                        textTransform.Find("Lucky").GetComponent<Text>().text = "Lucky + " + 5.0f * ItemManager.quality2Entries[temp.quality];
                    }
                    if (temp.GetBit(6))
                    {
                        textTransform.Find("Crit").gameObject.SetActive(true);
                        textTransform.Find("Crit").GetComponent<Text>().text = "Crit + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(7))
                    {
                        textTransform.Find("Cdmg").gameObject.SetActive(true);
                        textTransform.Find("Cdmg").GetComponent<Text>().text = "Cdmg + " + 10.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(8))
                    {
                        textTransform.Find("IgDef").gameObject.SetActive(true);
                        textTransform.Find("IgDef").GetComponent<Text>().text = "Armor Pntr + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(9))
                    {
                        textTransform.Find("MpUsede").gameObject.SetActive(true);
                        textTransform.Find("MpUsede").GetComponent<Text>().text = "Mp Cost - " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(10))
                    {
                        textTransform.Find("Reflect").gameObject.SetActive(true);
                        textTransform.Find("Reflect").GetComponent<Text>().text = "Reflect + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(11))
                    {
                        textTransform.Find("AtkHeal").gameObject.SetActive(true);
                        textTransform.Find("AtkHeal").GetComponent<Text>().text = "Life Steal + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }

                    textTransform.Find("Space2").gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (item.type)
            {
                case ItemType.Equipment:

                    EquipmentItemData temp = (EquipmentItemData)item;

                    if (temp.GetBit(0))
                    {
                        textTransform.Find("Hp").gameObject.SetActive(true);
                        textTransform.Find("Hp").GetComponent<Text>().text = "生命值 + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(1))
                    {
                        textTransform.Find("Mp").gameObject.SetActive(true);
                        textTransform.Find("Mp").GetComponent<Text>().text = "法力值 + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(2))
                    {
                        textTransform.Find("Atk").gameObject.SetActive(true);
                        textTransform.Find("Atk").GetComponent<Text>().text = "攻击力 + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(3))
                    {
                        textTransform.Find("Def").gameObject.SetActive(true);
                        textTransform.Find("Def").GetComponent<Text>().text = "防御力 + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(4))
                    {
                        textTransform.Find("Spd").gameObject.SetActive(true);
                        textTransform.Find("Spd").GetComponent<Text>().text = "速度 + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(5))
                    {
                        textTransform.Find("Lucky").gameObject.SetActive(true);
                        textTransform.Find("Lucky").GetComponent<Text>().text = "幸运值 + " + 5.0f * ItemManager.quality2Entries[temp.quality];
                    }
                    if (temp.GetBit(6))
                    {
                        textTransform.Find("Crit").gameObject.SetActive(true);
                        textTransform.Find("Crit").GetComponent<Text>().text = "暴击几率 + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(7))
                    {
                        textTransform.Find("Cdmg").gameObject.SetActive(true);
                        textTransform.Find("Cdmg").GetComponent<Text>().text = "暴击伤害 + " + 10.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(8))
                    {
                        textTransform.Find("IgDef").gameObject.SetActive(true);
                        textTransform.Find("IgDef").GetComponent<Text>().text = "护甲穿透 + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(9))
                    {
                        textTransform.Find("MpUsede").gameObject.SetActive(true);
                        textTransform.Find("MpUsede").GetComponent<Text>().text = "法力消耗 - " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(10))
                    {
                        textTransform.Find("Reflect").gameObject.SetActive(true);
                        textTransform.Find("Reflect").GetComponent<Text>().text = "伤害反弹 + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }
                    if (temp.GetBit(11))
                    {
                        textTransform.Find("AtkHeal").gameObject.SetActive(true);
                        textTransform.Find("AtkHeal").GetComponent<Text>().text = "生命偷取 + " + 5.0f * ItemManager.quality2Entries[temp.quality] + "%";
                    }

                    textTransform.Find("Space2").gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        textTransform.Find("Describe").GetComponent<Text>().text = GameSettings.language == Language.English ? item.describe : item.cnDescribe;

        LayoutRebuilder.ForceRebuildLayoutImmediate(textTransform.GetComponent<RectTransform>());
    }
}