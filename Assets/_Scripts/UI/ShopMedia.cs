using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMedia : BaseMedia
{
    public ShopMode mode = ShopMode.buy;

    private Button exitButton;
    private Transform shopSlotParent;
    private Transform playerProperties;
    private Button changeButton;
    private GameObject item1;
    private GameObject item2;
    private Button enableButton;
    private GameObject shopSlotPrefab;
    private GameObject buildPage;
    private GameObject checkObj;
    private Image icon;
    private Text priceText;
    private GameObject buttonBar;
    private Button upButton;
    private Button downButton;
    private Text numberText;
    private Button yesButton;
    private Button noButton;
    private Text checkText;

    // Runtime var
    private int number;
    private bool hasItem1 = false;
    private bool hasItem2 = false;
    private int item_Slot_Id;
    private int item_Slot_Id2;
    private int builtId;

    private GameObject lastAttri = null;

    public override void Init()
    {
        base.Init();

        exitButton = panel.Find("Blocker").GetComponent<Button>();
        shopSlotParent = panel.Find("ItemList/ViewPoint/Content");
        playerProperties = panel.Find("PlayerA");
        changeButton = panel.Find("ChangeButton").GetComponent<Button>();
        item1 = panel.Find("ItemProperty1").gameObject;
        item2 = panel.Find("ItemProperty2").gameObject;
        enableButton = item1.transform.Find("ButtonBar/Buy").GetComponent<Button>();

        checkObj = panel.Find("CheckView").gameObject;
        icon = checkObj.transform.Find("Icon").GetComponent<Image>();
        priceText = checkObj.transform.Find("PriceText").GetComponent<Text>();
        buttonBar = checkObj.transform.Find("ButtonBar").gameObject;
        upButton = checkObj.transform.Find("ButtonBar/Up").GetComponent<Button>();
        downButton = checkObj.transform.Find("ButtonBar/Down").GetComponent<Button>();
        numberText = checkObj.transform.Find("ButtonBar/Text").GetComponent<Text>();
        yesButton = checkObj.transform.Find("Yes").GetComponent<Button>();
        noButton = checkObj.transform.Find("No").GetComponent<Button>();
        checkText = checkObj.transform.Find("CheckText").GetComponent<Text>();

        shopSlotPrefab = Resources.Load<GameObject>("MyResource/Panel/ShopSlot");

        buildPage = panel.Find("BuildPage").gameObject;

        changeButton.onClick.RemoveAllListeners();
        changeButton.onClick.AddListener(() =>
        {
            if (mode == ShopMode.buy)
                mode = ShopMode.sell;
            else if (mode == ShopMode.sell)
                mode = ShopMode.buy;
            else if (mode == ShopMode.build)
                mode = ShopMode.customize;
            else
                mode = ShopMode.build;

            Refresh();
        });

        enableButton.onClick.RemoveAllListeners();
        enableButton.onClick.AddListener(() =>
        {
            number = 1;

            if (mode == ShopMode.build)
            {
                builtId = -1;
                OpenBuildPage();
            }
            else
                RefreshCheckView();
        });

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(() =>
        {
            checkObj.SetActive(false);
            buildPage.SetActive(false);
            Refresh();
        });

        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(() =>
        {
            Close();
        });
    }

    public override void Open()
    {
        base.Open();

        Refresh();

        time = Time.time;
    }

    public override void Close()
    {
        base.Close();
    }

    public void SetMode(ShopMode mode)
    {
        this.mode = mode;
    }

    private void Refresh()
    {
        hasItem1 = false;
        hasItem2 = false;
        item1.SetActive(false);
        item2.SetActive(false);
        checkObj.SetActive(false);
        buildPage.SetActive(false);

        for (int i = 0; i < shopSlotParent.childCount; ++i)
            GameObject.Destroy(shopSlotParent.GetChild(i).gameObject);

        if (mode == ShopMode.buy)
        {
            shopSlotParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(175, 235);

            changeButton.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Sell" : "卖";

            {
                GameObject temp = GameObject.Instantiate(shopSlotPrefab, shopSlotParent);
                temp.transform.Find("ItemSlot/Slot").GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(ItemLoader.dataTotal[70001].quality);
                temp.transform.Find("ItemSlot/Slot/ItemIcon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(ItemLoader.dataTotal[70001].iconPath);
                temp.transform.Find("ItemSlot/Slot/NumberText").gameObject.SetActive(false);
                temp.transform.Find("CostBoard/CostNum").GetComponent<Text>().text = ItemLoader.dataTotal[70001].priceIn.ToString();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.RemoveAllListeners();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.AddListener(() =>
                {
                    item_Slot_Id = 70001;
                    OpenProperty();
                });
            }

            {
                GameObject temp = GameObject.Instantiate(shopSlotPrefab, shopSlotParent);
                temp.transform.Find("ItemSlot/Slot").GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(ItemLoader.dataTotal[70002].quality);
                temp.transform.Find("ItemSlot/Slot/ItemIcon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(ItemLoader.dataTotal[70002].iconPath);
                temp.transform.Find("ItemSlot/Slot/NumberText").gameObject.SetActive(false);
                temp.transform.Find("CostBoard/CostNum").GetComponent<Text>().text = ItemLoader.dataTotal[70002].priceIn.ToString();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.RemoveAllListeners();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.AddListener(() =>
                {
                    item_Slot_Id = 70002;
                    OpenProperty();
                });
            }

            {
                GameObject temp = GameObject.Instantiate(shopSlotPrefab, shopSlotParent);
                temp.transform.Find("ItemSlot/Slot").GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(ItemLoader.dataTotal[70003].quality);
                temp.transform.Find("ItemSlot/Slot/ItemIcon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(ItemLoader.dataTotal[70003].iconPath);
                temp.transform.Find("ItemSlot/Slot/NumberText").gameObject.SetActive(false);
                temp.transform.Find("CostBoard/CostNum").GetComponent<Text>().text = ItemLoader.dataTotal[70003].priceIn.ToString();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.RemoveAllListeners();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.AddListener(() =>
                {
                    item_Slot_Id = 70003;
                    OpenProperty();
                });
            }

            {
                GameObject temp = GameObject.Instantiate(shopSlotPrefab, shopSlotParent);
                temp.transform.Find("ItemSlot/Slot").GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(ItemLoader.dataTotal[70004].quality);
                temp.transform.Find("ItemSlot/Slot/ItemIcon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(ItemLoader.dataTotal[70004].iconPath);
                temp.transform.Find("ItemSlot/Slot/NumberText").gameObject.SetActive(false);
                temp.transform.Find("CostBoard/CostNum").GetComponent<Text>().text = ItemLoader.dataTotal[70004].priceIn.ToString();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.RemoveAllListeners();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.AddListener(() =>
                {
                    item_Slot_Id = 70004;
                    OpenProperty();
                });
            }

            {
                GameObject temp = GameObject.Instantiate(shopSlotPrefab, shopSlotParent);
                temp.transform.Find("ItemSlot/Slot").GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(ItemLoader.dataTotal[70005].quality);
                temp.transform.Find("ItemSlot/Slot/ItemIcon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(ItemLoader.dataTotal[70005].iconPath);
                temp.transform.Find("ItemSlot/Slot/NumberText").gameObject.SetActive(false);
                temp.transform.Find("CostBoard/CostNum").GetComponent<Text>().text = ItemLoader.dataTotal[70005].priceIn.ToString();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.RemoveAllListeners();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.AddListener(() =>
                {
                    item_Slot_Id = 70005;
                    OpenProperty();
                });
            }
        }
        else if (mode == ShopMode.sell)
        {
            shopSlotParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(175, 235);

            changeButton.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Buy" : "买";

            for (int i = 0; i < ItemManager.GetInst().itemList.Count; ++i)
            {
                GameObject temp = GameObject.Instantiate(shopSlotPrefab, shopSlotParent);
                temp.transform.Find("ItemSlot/Slot").GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(ItemManager.GetInst().itemList[i].quality);
                temp.transform.Find("ItemSlot/Slot/ItemIcon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(ItemManager.GetInst().itemList[i].iconPath);
                temp.transform.Find("ItemSlot/Slot/NumberText").gameObject.SetActive(true);
                temp.transform.Find("ItemSlot/Slot/NumberText").GetComponent<Text>().text = ItemManager.GetInst().itemList[i].number.ToString();
                temp.transform.Find("CostBoard/CostNum").GetComponent<Text>().text = ItemManager.GetInst().itemList[i].priceOut.ToString();
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.RemoveAllListeners();
                int tempInt = i;
                temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.AddListener(() =>
                {
                    item_Slot_Id = tempInt;
                    OpenProperty();
                });
            }
        }
        else if (mode == ShopMode.build)
        {
            shopSlotParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(175, 235);

            changeButton.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Customize" : "定制";

            for (int i = 0; i < ItemManager.GetInst().itemList.Count; ++i)
            {
                if (ItemManager.GetInst().itemList[i].type == ItemType.Equipment)
                {
                    GameObject temp = GameObject.Instantiate(shopSlotPrefab, shopSlotParent);
                    temp.transform.Find("ItemSlot/Slot").GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(ItemManager.GetInst().itemList[i].quality);
                    temp.transform.Find("ItemSlot/Slot/ItemIcon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(ItemManager.GetInst().itemList[i].iconPath);
                    temp.transform.Find("ItemSlot/Slot/NumberText").gameObject.SetActive(true);
                    temp.transform.Find("ItemSlot/Slot/NumberText").GetComponent<Text>().text = ItemManager.GetInst().itemList[i].number.ToString();
                    temp.transform.Find("CostBoard/CostNum").GetComponent<Text>().text = buildCost[ItemManager.GetInst().itemList[i].quality].ToString();
                    temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.RemoveAllListeners();
                    int tempInt = i;
                    temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.AddListener(() =>
                    {
                        item_Slot_Id = tempInt;
                        OpenProperty();
                    });
                }
            }
        }
        else if (mode == ShopMode.customize)
        {
            shopSlotParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(175, 235);

            changeButton.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Build" : "打造";

            for (int i = 0; i < ItemManager.GetInst().itemList.Count; ++i)
            {
                if (ItemManager.GetInst().itemList[i].type == ItemType.Equipment)
                {
                    GameObject temp = GameObject.Instantiate(shopSlotPrefab, shopSlotParent);
                    temp.transform.Find("ItemSlot/Slot").GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(ItemManager.GetInst().itemList[i].quality);
                    temp.transform.Find("ItemSlot/Slot/ItemIcon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(ItemManager.GetInst().itemList[i].iconPath);
                    temp.transform.Find("ItemSlot/Slot/NumberText").gameObject.SetActive(true);
                    temp.transform.Find("ItemSlot/Slot/NumberText").GetComponent<Text>().text = ItemManager.GetInst().itemList[i].number.ToString();
                    temp.transform.Find("CostBoard/CostNum").GetComponent<Text>().text = customizeCost[ItemManager.GetInst().itemList[i].quality].ToString();
                    temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.RemoveAllListeners();
                    int tempInt = i;
                    temp.transform.Find("ItemSlot/Slot").GetComponent<Button>().onClick.AddListener(() =>
                    {
                        if (!hasItem1)
                        {
                            hasItem1 = true;
                            item_Slot_Id = tempInt;
                            OpenProperty();
                        }
                        else
                        {
                            hasItem2 = true;
                            item_Slot_Id2 = tempInt;
                            OpenProperty();
                        }
                    });
                }
            }
        }

        // display gold
        playerProperties.Find("TotalGold").GetComponent<Text>().text = PlayerData.GetInst().gold.ToString();
    }

    float time;
    public override void Update()
    {
        base.Update();

        if(Time.time-time< 0.1f)
        {
            // reset content position
            panel.Find("ItemList").GetComponent<ScrollRect>().StopMovement();
            shopSlotParent.localPosition = new Vector2(shopSlotParent.localPosition.x, 0.0f);
        }
    }

    private void OpenProperty()
    {
        item1.SetActive(true);

        switch (mode)
        {
            case ShopMode.buy:
                ItemPropertyMedia.SetDescribe(item1.transform, ItemLoader.dataTotal[item_Slot_Id]);
                enableButton.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Buy" : "买";
                if (ItemLoader.dataTotal[item_Slot_Id].type == ItemType.Equipment)
                {
                    int partId = (int)((EquipmentItemData)ItemLoader.dataTotal[item_Slot_Id]).part;
                    if (ItemManager.GetInst().equippedData[partId] != null)
                    {
                        item2.SetActive(true);
                        ItemPropertyMedia.SetDescribe(item2.transform, ItemManager.GetInst().equippedData[partId]);
                    }
                }
                break;
            case ShopMode.sell:
                ItemPropertyMedia.SetDescribe(item1.transform, ItemManager.GetInst().itemList[item_Slot_Id]);
                enableButton.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Sell" : "卖";
                if (ItemManager.GetInst().itemList[item_Slot_Id].type == ItemType.Equipment)
                {
                    int partId = (int)((EquipmentItemData)ItemManager.GetInst().itemList[item_Slot_Id]).part;
                    if (ItemManager.GetInst().equippedData[partId] != null)
                    {
                        item2.SetActive(true);
                        ItemPropertyMedia.SetDescribe(item2.transform, ItemManager.GetInst().equippedData[partId]);
                    }
                }
                break;
            case ShopMode.build:
                ItemPropertyMedia.SetDescribe(item1.transform, ItemManager.GetInst().itemList[item_Slot_Id]);
                enableButton.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Build" : "打造";
                if (ItemManager.GetInst().itemList[item_Slot_Id].type == ItemType.Equipment)
                {
                    int partId = (int)((EquipmentItemData)ItemManager.GetInst().itemList[item_Slot_Id]).part;
                    if (ItemManager.GetInst().equippedData[partId] != null)
                    {
                        item2.SetActive(true);
                        ItemPropertyMedia.SetDescribe(item2.transform, ItemManager.GetInst().equippedData[partId]);
                    }
                }
                break;
            case ShopMode.customize:
                ItemPropertyMedia.SetDescribe(item1.transform, ItemManager.GetInst().itemList[item_Slot_Id]);
                enableButton.GetComponentInChildren<Text>().text = GameSettings.language == Language.English ? "Customize" : "定制";
                if (hasItem2)
                {
                    item2.SetActive(true);
                    ItemPropertyMedia.SetDescribe(item2.transform, ItemManager.GetInst().itemList[item_Slot_Id2]);
                }
                break;
        }
    }

    // enable button click and mode == build
    private void OpenBuildPage()
    {
        number = 5;

        lastAttri = null;

        buildPage.SetActive(true);

        EquipmentItemData temp = (EquipmentItemData)ItemManager.GetInst().itemList[item_Slot_Id];

        buildPage.transform.Find("Icon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(temp.iconPath);
        buildPage.transform.Find("PriceText").GetComponent<Text>().text = GameSettings.language == Language.English ? temp.name + '\n' + "Quality : " + temp.quality.ToString() : temp.cnName + '\n' + "品质 : " + ((ItemQualityCN)temp.quality).ToString();

        {
            int count = 0;

            for (int i = 0; i < buildPage.transform.Find("Attri").childCount; ++i)
                buildPage.transform.Find("Attri").GetChild(i).gameObject.SetActive(false);

            for (int i = 0; i <= 13; ++i)
            {
                if(temp.GetBit(i))
                {
                    int tempInt = count;

                    buildPage.transform.Find("Attri").GetChild(count).gameObject.SetActive(true);
                    buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().color = Color.white;

                    if(GameSettings.language == Language.English)
                    {
                        switch (i)
                        {
                            case 0:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Health Point + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 1:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Mana Point + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 2:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Attack + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 3:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Defense + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 4:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Speed + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 5:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Lucky + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString();
                                break;
                            case 6:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Critical + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 7:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Critical Damage + " + (10.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 8:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Armor Penetration + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 9:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Mana Cost - " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 10:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Reflect Damage + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 11:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "Life Steal + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            default:

                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "生命值 + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 1:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "法力值 + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 2:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "攻击力 + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 3:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "防御力 + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 4:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "速度 + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 5:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "幸运值 + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString();
                                break;
                            case 6:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "暴击几率 + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 7:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "暴击伤害 + " + (10.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 8:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "护甲穿透 + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 9:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "法力消耗 - " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 10:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "伤害反弹 + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            case 11:
                                buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().text = "生命偷取 + " + (5.0f * ItemManager.quality2Entries[temp.quality]).ToString() + "%";
                                break;
                            default:

                                break;
                        }
                    }

                    buildPage.transform.Find("Attri").GetChild(count).GetComponent<Button>().onClick.RemoveAllListeners();
                    buildPage.transform.Find("Attri").GetChild(count).GetComponent<Button>().onClick.AddListener(() =>
                    {
                        if (lastAttri != null)
                            lastAttri.GetComponentInChildren<Text>().color = Color.white;

                        lastAttri = buildPage.transform.Find("Attri").GetChild(tempInt).gameObject;
                        buildPage.transform.Find("Attri").GetChild(tempInt).GetComponentInChildren<Text>().color = Color.red;
                        number = tempInt;
                    });

                    if (i == builtId)
                        buildPage.transform.Find("Attri").GetChild(count).GetComponentInChildren<Text>().color = Color.green;

                    count++;
                }
            }
        }

        buildPage.transform.Find("Build").GetComponent<Button>().onClick.RemoveAllListeners();
        buildPage.transform.Find("Build").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (number != 5)
                RefreshCheckView();
            else
            {
                if (GameSettings.language == Language.English)

                    SingletonObject<HintMedia>.GetInst().SendHint("Choose an entry.", Color.red);
                else
                    SingletonObject<HintMedia>.GetInst().SendHint("请选择一条属性", Color.red);
            }
        });

        buildPage.transform.Find("Cancel").GetComponent<Button>().onClick.RemoveAllListeners();
        buildPage.transform.Find("Cancel").GetComponent<Button>().onClick.AddListener(() =>
        {
            buildPage.SetActive(false);

            Refresh();
        });
    }

    private void RefreshCheckView()
    {
        if(mode == ShopMode.buy||mode == ShopMode.sell)
        {
            buttonBar.SetActive(true);

            upButton.onClick.RemoveAllListeners();
            upButton.GetComponent<Image>().color = Color.gray;
            if (number < 100)
            {
                upButton.GetComponent<Image>().color = Color.white;
                upButton.onClick.AddListener(() =>
                {
                    number++;
                    RefreshCheckView();
                });
            }

            downButton.onClick.RemoveAllListeners();
            downButton.GetComponent<Image>().color = Color.gray;
            if (number > 0)
            {
                downButton.GetComponent<Image>().color = Color.white;
                downButton.onClick.AddListener(() =>
                {
                    number--;
                    RefreshCheckView();
                });
            }

            numberText.text = number.ToString();
        }
        else
        {
            buttonBar.SetActive(false);
        }

        yesButton.GetComponent<Image>().color = Color.gray;
        yesButton.onClick.RemoveAllListeners();

        BaseItemData temp;

        switch (mode)
        {
            case ShopMode.buy:
                {
                    temp = ItemLoader.dataTotal[item_Slot_Id];

                    icon.sprite = AtlasManager.GetInst().GetItemIcon(temp.iconPath);
                    if(GameSettings.language == Language.English)
                    {
                        priceText.text = temp.name + '\n' + "Price : " + temp.priceIn.ToString();
                        checkText.text = "Are you SURE to pay " + (temp.priceIn * number).ToString() + " GOLD to buy the item(s)?";
                    }
                    else
                    {
                        priceText.text = temp.cnName + '\n' + "价格 : " + temp.priceIn.ToString();
                        checkText.text = "你确定要支付 " + (temp.priceIn * number).ToString() + " 金币来购买道具吗？";
                    }

                    if (temp.priceIn * number <= PlayerData.GetInst().gold && number != 0)
                    {
                        yesButton.GetComponent<Image>().color = Color.white;
                        yesButton.onClick.AddListener(Buy);
                    }
                }
                break;
            case ShopMode.sell:
                {
                    temp = ItemManager.GetInst().itemList[item_Slot_Id];

                    icon.sprite = AtlasManager.GetInst().GetItemIcon(temp.iconPath);
                    if(GameSettings.language == Language.English)
                    {
                        priceText.text = temp.name + '\n' + "Price : " + temp.priceOut.ToString();
                        checkText.text = "Are you SURE to sell " + number.ToString() + " " + temp.name + " for " + (number * temp.priceOut).ToString() + " GOLD?";
                    }
                    else
                    {
                        priceText.text = temp.cnName + '\n' + "价格 : " + temp.priceOut.ToString();
                        checkText.text = "你确定要出售 " + number.ToString() + " " + temp.cnName + " 来换取 " + (number * temp.priceOut).ToString() + " 金币吗？";
                    }

                    if (number <= temp.number && number != 0)
                    {
                        yesButton.GetComponent<Image>().color = Color.white;
                        yesButton.onClick.AddListener(Sell);
                    }
                }
                break;
            case ShopMode.build:
                {
                    temp = ItemManager.GetInst().itemList[item_Slot_Id];

                    int price = buildCost[temp.quality];
                    icon.sprite = AtlasManager.GetInst().GetItemIcon(temp.iconPath);
                    if(GameSettings.language == Language.English)
                    {
                        priceText.text = temp.name + '\n' + "Quality : " + temp.quality.ToString();
                        checkText.text = "Are you SURE to pay " + price.ToString() + " GOLD to change the attribute of this gear?";
                    }
                    else
                    {
                        priceText.text = temp.cnName + '\n' + "品质 : " + ((ItemQualityCN)temp.quality).ToString();
                        checkText.text = "你确定要支付 " + price.ToString() + " 金币来重新打造这件装备的属性吗？";
                    }

                    if (price <= PlayerData.GetInst().gold)
                    {
                        yesButton.GetComponent<Image>().color = Color.white;
                        yesButton.onClick.AddListener(Build);
                    }
                }
                break;
            case ShopMode.customize:
                {
                    temp = ItemManager.GetInst().itemList[item_Slot_Id];

                    int price = customizeCost[temp.quality];
                    icon.sprite = AtlasManager.GetInst().GetItemIcon(temp.iconPath);
                    if(GameSettings.language == Language.English)
                    {
                        priceText.text = temp.name + '\n' + "Quality : " + temp.quality.ToString();
                        checkText.text = "Are you SURE to pay " + price.ToString() + " GOLD to change the appearance of this gear?";
                    }
                    else
                    {
                        priceText.text = temp.cnName + '\n' + "品质 : " + ((ItemQualityCN)temp.quality).ToString();
                        checkText.text = "你确定要支付 " + price.ToString() + " 金币来替换这件装备的外形吗？";
                    }

                    if (price <= PlayerData.GetInst().gold)
                    {
                        yesButton.GetComponent<Image>().color = Color.white;
                        yesButton.onClick.AddListener(()=>
                        {
                            if (hasItem2)
                                Customize();
                            else
                            {
                                if(GameSettings.language == Language.English)
                                    SingletonObject<HintMedia>.GetInst().SendHint("Please choose the second item.", Color.red);
                                else
                                    SingletonObject<HintMedia>.GetInst().SendHint("请再选一件装备用于交换", Color.red);
                            }
                        });
                    }
                }
                break;
        }

        checkObj.SetActive(true);
    }

    private void Buy()
    {
        PlayerData.GetInst().GetGold(-ItemLoader.dataTotal[item_Slot_Id].priceIn * number);

        ItemManager.GetInst().GainNewItem(item_Slot_Id, number);
        ItemManager.GetInst().Arrange();

        checkObj.SetActive(false);

        Refresh();
    }

    private void Sell()
    {
        PlayerData.GetInst().GetGold(ItemManager.GetInst().itemList[item_Slot_Id].priceOut * number);

        ItemManager.GetInst().RemoveItem(item_Slot_Id, SlotType.InBag, number);
        ItemManager.GetInst().Arrange();

        checkObj.SetActive(false);

        Refresh();
    }

    private void Build()
    {
        PlayerData.GetInst().GetGold(-buildCost[ItemManager.GetInst().itemList[item_Slot_Id].quality]);

        builtId = ((EquipmentItemData)ItemManager.GetInst().itemList[item_Slot_Id]).Build(number);

        checkObj.SetActive(false);

        OpenBuildPage();
    }

    private void Customize()
    {
        PlayerData.GetInst().GetGold(-customizeCost[ItemManager.GetInst().itemList[item_Slot_Id].quality]);

        EquipmentItemData.Exchange((EquipmentItemData)ItemManager.GetInst().itemList[item_Slot_Id], (EquipmentItemData)ItemManager.GetInst().itemList[item_Slot_Id2]);

        ItemManager.GetInst().Arrange();

        checkObj.SetActive(false);

        Refresh();
    }

    private static Dictionary<ItemQuality, int> buildCost = new Dictionary<ItemQuality, int>()
    {
        {ItemQuality.Normal, 1},
        {ItemQuality.Good, 3},
        {ItemQuality.Superior,  9},
        {ItemQuality.Epic, 27},
    };

    private static Dictionary<ItemQuality, int> customizeCost = new Dictionary<ItemQuality, int>()
    {
        {ItemQuality.Normal, 100},
        {ItemQuality.Good, 100},
        {ItemQuality.Superior,  100},
        {ItemQuality.Epic,  100},
    };
}