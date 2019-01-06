using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;
using Spine.Unity.Modules.AttachmentTools;

public static class SpineController
{
    private static AtlasAsset atlasAsset;
    private static Atlas atlas;
    private static Skin templateSkin;
    private static Skin equipsSkin;
    private static SkeletonRenderer skeletonRenderer;
    private static SkeletonAnimation skeletonAnimation;

    private static string defaultMeleeWeaponPath = "equip/zhanshi/wuqi01/z_wuqi_1_0/skeleton_Atlas";
    private static string defaultMagicWeaponPath = "equip/fashi/wuqi01/f_wuqi_1_0/skeleton_Atlas";
    private static string defaultRangeWeaponPath = "equip/gongjian/wuqi01/g_wuqi_1_0/skeleton_Atlas";

    public static void RefreshSpine(GameObject obj)
    {
        skeletonAnimation = obj.transform.Find("Spine").GetComponent<SkeletonAnimation>();
        skeletonRenderer = obj.transform.Find("Spine").GetComponent<SkeletonRenderer>();
        EquipSkin();

        for (int i = 0; i < ItemManager.GetInst().equippedData.Length; ++i)
        {
            if (ItemManager.GetInst().equippedData[i] != null)
            {
                RefreshEquip(ItemManager.GetInst().equippedData[i]);
            }
            else if (i == 0)
            {
                ReplaceEquip("wuqi", "equip/zhanshi/wuqi01/z_wuqi_1_0/skeleton_Atlas");
            }
            else if (i == 1)
            {
                ReplaceEquip("dun", "equip/zhanshi/wuqi01/z_fwuqi/skeleton_Atlas");
            }
            else if (i == 2)
            {
                ReplaceEquip("body", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("jianjia01", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("jianjia02", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("weiqun", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("pijian", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("pijian02", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("changpao", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("dabi01", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("dabi02", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("xiaobi01", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("xiaobi02", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("beifen01", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("beifen02", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
                ReplaceEquip("beifen03", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
            }
            else if (i == 3)
            {
                ReplaceEquip("head", "equip/zhanshi/tou/z_head/skeleton_Atlas");
            }
        }
    }

    public static void SetDefaultSpine(GameObject obj, WeaponType weaponType = WeaponType.melee)
    {
        skeletonAnimation = obj.transform.Find("Spine").GetComponent<SkeletonAnimation>();
        skeletonRenderer = obj.transform.Find("Spine").GetComponent<SkeletonRenderer>();

        if (ItemManager.GetInst().equippedData[0] == null || ItemManager.GetInst().equippedData[0].weaponType != weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.melee:
                    ReplaceEquip("wuqi", defaultMeleeWeaponPath);
                    break;
                case WeaponType.magic:
                    ReplaceEquip("wuqi", defaultMagicWeaponPath);
                    break;
                case WeaponType.range:
                    ReplaceEquip("wuqi", defaultRangeWeaponPath);
                    break;
            }
        }
    }

    public static void SetVeallSpine(GameObject obj)
    {
        skeletonAnimation = obj.transform.Find("Spine").GetComponent<SkeletonAnimation>();
        skeletonRenderer = obj.transform.Find("Spine").GetComponent<SkeletonRenderer>();
        EquipSkin();

        ReplaceEquip("wuqi", "equip/gongjian/wuqi01/g_wuqi_1_0/skeleton_Atlas");
        ReplaceEquip("dun", "equip/zhanshi/wuqi01/z_fwuqi_1_0/skeleton_Atlas");
        ReplaceEquip("body", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("jianjia01", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("jianjia02", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("weiqun", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("pijian", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("pijian02", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("changpao", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("dabi01", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("dabi02", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("xiaobi01", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("xiaobi02", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("beifen01", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("beifen02", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("beifen03", "equip/zhanshi/yifu/z_body/skeleton_Atlas");
        ReplaceEquip("head", "equip/zhanshi/tou/z_head/skeleton_Atlas");
    }

    private static void RefreshEquip(EquipmentItemData equipmentData)
    {
        if (equipmentData.part != EquipmentPart.Weapon && equipmentData.part != EquipmentPart.SubWeapon && equipmentData.part != EquipmentPart.Hat && equipmentData.part != EquipmentPart.Armor)
            return;
        string atlasPath = ItemLoader.dataResource[equipmentData.resourceId].atlasPath;
        foreach (var slot in ItemLoader.dataResource[equipmentData.resourceId].spineSlots)
        {
            ReplaceEquip(slot, atlasPath);
        }
    }

    private static void Equip(int slotIndex, string attachmentName, Attachment attachment)
    {
        equipsSkin.AddAttachment(slotIndex, attachmentName, attachment);
        skeletonAnimation.Skeleton.SetSkin(equipsSkin);
        RefreshSkeletonAttachments();
    }

    private static void EquipSkin()
    {
        equipsSkin = new Skin("Equips");
        templateSkin = skeletonAnimation.Skeleton.Data.FindSkin("default");
        if (templateSkin != null)
            equipsSkin.Append(templateSkin);

        skeletonAnimation.Skeleton.Skin = equipsSkin;
        RefreshSkeletonAttachments();
    }

    private static void RefreshSkeletonAttachments()
    {
        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
        skeletonAnimation.AnimationState.Apply(skeletonAnimation.Skeleton); //skeletonAnimation.Update(0);
    }

    private static void ReplaceEquip(string slotName, string atlasPath)
    {
        int slotIndex = skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).FindSlotIndex(slotName);
        atlasAsset = Resources.Load(atlasPath) as AtlasAsset;
        if (atlasAsset == null)
        {
            Debug.LogError("路径错误" + atlasPath);
        }
        List<string> attachNameList = GetAttachmentNameList(slotIndex);
        foreach (var name in attachNameList)
        {
            ReplaceAttachment(slotIndex, name);
        }
    }

    private static void ReplaceAttachment(int slotIndex, string attachName)
    {
        var attachment = GenerateAttachmentFromEquipAsset(slotIndex, attachName);
        if (attachment == null)
        {
            Debug.LogError("not find attachment " + attachName);
            return;
        }
        Equip(slotIndex, attachName, attachment);
    }

    private static List<string> GetAttachmentNameList(int index)
    {
        List<string> result = new List<string>();
        foreach (var a in templateSkin.Attachments.Keys)
        {
            if (a.slotIndex == index)
            {
                result.Add(a.name);
            }
        }
        return result;
    }

    private static Attachment GenerateAttachmentFromEquipAsset(int slotIndex, string templateAttachmentName)
    {
        Attachment attachment = null;
        atlas = atlasAsset.GetAtlas();
        float scale = skeletonRenderer.skeletonDataAsset.scale;
        var skeletonData = skeletonAnimation.skeletonDataAsset.GetSkeletonData(true);
        Attachment templateAttachment = templateSkin.GetAttachment(slotIndex, templateAttachmentName);
        AtlasRegion region = atlas.FindRegion(templateAttachmentName);
        if (region == null)
            return null;
        attachment = templateAttachment.GetRemappedClone(region, true, false, scale);
        //attachment = region.ToRegionAttachment(region.name, scale);
        return attachment;
    }
}