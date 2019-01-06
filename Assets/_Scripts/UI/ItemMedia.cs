using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine;
using Spine.Unity;

public class ItemMedia : BaseMedia
{
    private Transform itemTransform;
    private GameObject slotPrefab;
    private List<Transform> runeSlots = new List<Transform>();
    private List<Transform> equippedSlots = new List<Transform>();

    private string slotPath = "MyResource/Panel/Slot";

    private Button exitButton;
    private Button arrangeButton;
    private Button attributesButton;
    private Transform attributes;

    private GameObject modelPrefab;
    private GameObject modelObj;

    private int mode = 1; // 1 = overall, 2 = equipment, 3 = rune, 4 = others

    public override void Init()
    {
        base.Init();

        slotPrefab = Resources.Load<GameObject>(slotPath);
        itemTransform = panel.Find("ItemSlot/ViewPoint/Content").transform;
        runeSlots.Add(panel.Find("RuneSlots/Slot1/Rune1").GetComponent<Transform>());
        runeSlots.Add(panel.Find("RuneSlots/Slot2/Rune2").GetComponent<Transform>());
        runeSlots.Add(panel.Find("RuneSlots/Slot3/Rune3").GetComponent<Transform>());
        runeSlots.Add(panel.Find("RuneSlots/Slot4/Rune4").GetComponent<Transform>());
        Transform equippedSlotTransform = panel.Find("EquippedSlots").transform;
        equippedSlots.Add(equippedSlotTransform.Find("Weapon/Slot").GetComponent<Transform>());
        equippedSlots.Add(equippedSlotTransform.Find("SubWeapon/Slot").GetComponent<Transform>());
        equippedSlots.Add(equippedSlotTransform.Find("Armor/Slot").GetComponent<Transform>());
        equippedSlots.Add(equippedSlotTransform.Find("Hat/Slot").GetComponent<Transform>());
        equippedSlots.Add(equippedSlotTransform.Find("Necklace/Slot").GetComponent<Transform>());
        equippedSlots.Add(equippedSlotTransform.Find("Ring/Slot").GetComponent<Transform>());
        attributesButton = panel.Find("AttributesButton").GetComponent<Button>();
        attributes = panel.Find("PlayerAttributes");

        arrangeButton = panel.Find("ArrangeLabel").GetComponent<Button>();
        exitButton = panel.Find("Exit").GetComponent<Button>();

        modelPrefab = Resources.Load<GameObject>("MyResource/Model");

        arrangeButton.onClick.RemoveAllListeners();
        arrangeButton.onClick.AddListener(() =>
        {
            ItemManager.GetInst().Arrange();
        });

        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(Close);

        attributesButton.onClick.RemoveAllListeners();
        attributesButton.onClick.AddListener(() =>
        {
            if (attributes.gameObject.activeSelf)
            {
                attributes.gameObject.SetActive(false);
                modelObj.SetActive(true);
            }
            else
            {
                modelObj.SetActive(false);
                attributes.gameObject.SetActive(true);
            }
        });
    }

    public override void Open()
    {
        base.Open();

        panel.Find("RuneSlots").gameObject.SetActive(true);
        panel.Find("EquippedSlots").gameObject.SetActive(true);
        attributes.gameObject.SetActive(false);

        mode = 1;

        modelObj = GameObject.Instantiate(modelPrefab);

        SpineController.RefreshSpine(modelObj);

        ItemManager.GetInst().UpdateItem();

        time = Time.time;
    }

    public override void Close()
    {
        base.Close();

        CommonHandle.RefreshSpine();
        CommonHandle.PlayerSetIdle(MyPlayer.instance.gameObject);

        GameObject.Destroy(modelObj);

        ItemManager.GetInst().dirtyBit = false; SingletonObject<FightMedia>.GetInst().RefreshHint();
    }

    public void Refresh(List<BaseItemData> itemList, EquipmentItemData[] equipmentItemData, BaseItemData[] runeItemData)
    {
        for (int i = 0; i < itemTransform.childCount; ++i)
            GameObject.Destroy(itemTransform.GetChild(i).gameObject);

        for (int i = 0; i < itemList.Count; ++i)
        {
            switch(mode)
            {
                case 1:
                    SetSlot(GameObject.Instantiate(slotPrefab, itemTransform), itemList[i], i);
                    break;
                case 2:
                    if (itemList[i].type == ItemType.Equipment)
                        SetSlot(GameObject.Instantiate(slotPrefab, itemTransform), itemList[i], i);
                    break;
                case 3:
                    if (itemList[i].type == ItemType.Rune)
                        SetSlot(GameObject.Instantiate(slotPrefab, itemTransform), itemList[i], i);
                    break;
                case 4:
                    if (itemList[i].type == ItemType.Others || itemList[i].type == ItemType.CanUse)
                        SetSlot(GameObject.Instantiate(slotPrefab, itemTransform), itemList[i], i);
                    break;
            }
        }

        for (int i = 0; i < equipmentItemData.Length; ++i)
        {
            equippedSlots[i].Find("NumberText").gameObject.SetActive(false);

            if (equipmentItemData[i] == null)
            {
                equippedSlots[i].GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(ItemQuality.No);
                equippedSlots[i].Find("ItemIcon").gameObject.SetActive(false);
                equippedSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else
            {
                int temp = i;

                equippedSlots[i].GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(equipmentItemData[i].quality);
                equippedSlots[i].Find("ItemIcon").gameObject.SetActive(true);
                equippedSlots[i].Find("ItemIcon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(equipmentItemData[i].iconPath);
                equippedSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
                equippedSlots[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    // open item info page
                    SingletonObject<ItemPropertyMedia>.GetInst().SetMode(SlotType.Equipment, temp);
                    SingletonObject<ItemPropertyMedia>.GetInst().Open();
                });
            }
        }

        for (int i = 0; i < runeItemData.Length; ++i)
        {
            if (runeItemData[i] == null)
            {
                runeSlots[i].gameObject.SetActive(false);
                runeSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else
            {
                int temp = i;

                runeSlots[i].gameObject.SetActive(true);
                runeSlots[i].GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(runeItemData[i].iconPath);
                runeSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
                runeSlots[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    // open item info page
                    SingletonObject<ItemPropertyMedia>.GetInst().SetMode(SlotType.Rune, temp);
                    SingletonObject<ItemPropertyMedia>.GetInst().Open();
                });
            }
        }

        // Refresh attributes text
        if (PlayerData.GetInst().charB != null)
            attributes.GetComponentInChildren<Text>().text = PlayerData.GetInst().GetAttributesString();

        if (modelObj != null)
        {
            if (ItemManager.GetInst().equippedData[0] == null || ItemManager.GetInst().equippedData[0].weaponType == WeaponType.melee)
                modelObj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "jian_idle", true);
            else if(ItemManager.GetInst().equippedData[0].weaponType == WeaponType.magic)
                modelObj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "fazhang_idle", true);
            else if(ItemManager.GetInst().equippedData[0].weaponType == WeaponType.range)
                modelObj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "gongjian_idle", true);

            SpineController.RefreshSpine(modelObj);
        }

        // reset content position
        panel.Find("ItemSlot").GetComponent<ScrollRect>().StopMovement();
        itemTransform.localPosition = new Vector2(itemTransform.localPosition.x, 0.0f);
    }

    float time;
    public override void Update()
    {
        base.Update();

        if (Time.time - time < 0.1f)
        {
            // reset content position
            panel.Find("ItemSlot").GetComponent<ScrollRect>().StopMovement();
            itemTransform.localPosition = new Vector2(itemTransform.localPosition.x, 0.0f);
        }
    }

    private void SetSlot(GameObject slot, BaseItemData v, int index)
    {
        if (v.type == ItemType.Equipment)
            slot.transform.Find("NumberText").gameObject.SetActive(false);
        else
            slot.transform.Find("NumberText").GetComponent<Text>().text = v.number.ToString();

        slot.GetComponent<Image>().sprite = AtlasManager.GetInst().GetLevelSlotIcon(v.quality);

        slot.transform.Find("ItemIcon").GetComponent<Image>().sprite = AtlasManager.GetInst().GetItemIcon(v.iconPath);

        slot.GetComponent<Button>().onClick.RemoveAllListeners();
        slot.GetComponent<Button>().onClick.AddListener(() =>
        {
            // open item info page
            SingletonObject<ItemPropertyMedia>.GetInst().SetMode(SlotType.InBag, index);
            SingletonObject<ItemPropertyMedia>.GetInst().Open();
        });
    }
}