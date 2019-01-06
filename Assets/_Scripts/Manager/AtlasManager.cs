using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.U2D;

public class AtlasManager : SingletonObject<AtlasManager>
{
    private Dictionary<string, SpriteAtlas> atlasDic = new Dictionary<string, SpriteAtlas>();
    private SpriteAtlas itemAtlas;
    private SpriteAtlas slotAtlas;
    private SpriteAtlas otherAtlas;
    private SpriteAtlas lihuiAtlas;
    private SpriteAtlas eaAtlas;
    private SpriteAtlas flywordAtlas;

    public void Init()
    {
        SpriteAtlas[] atlas = Resources.LoadAll<SpriteAtlas>("UI/Atlas");
        foreach (var item in atlas)
            atlasDic.Add(item.name, item);

        itemAtlas = GetAtlas("IconAtlas");
        slotAtlas = GetAtlas("SlotAtlas");
        otherAtlas = GetAtlas("ItemAtlas");
        lihuiAtlas = GetAtlas("LiHuiAtlas");
        eaAtlas = GetAtlas("EAAtlas");
        flywordAtlas = GetAtlas("flywordAtlas");
    }

    public SpriteAtlas GetAtlas(string atlasName)
    {
        if (!atlasDic.ContainsKey(atlasName))
            return null;
        return atlasDic[atlasName];
    }

    public Sprite GetLevelSlotIcon(ItemQuality quality)
    {
        switch (quality)
        {
            case ItemQuality.No:
                return slotAtlas.GetSprite("none_equip");
            case ItemQuality.Normal:
                return slotAtlas.GetSprite("green_equip");
            case ItemQuality.Good:
                return slotAtlas.GetSprite("blue_equip");
            case ItemQuality.Superior:
                return slotAtlas.GetSprite("purple_equip");
            case ItemQuality.Epic:
                return slotAtlas.GetSprite("orange_equip");
            default:
                return null;
        }
    }

    public Sprite GetSkillBtnIcon(string s)
    {
        return eaAtlas.GetSprite(s);
    }

    public Sprite GetItemIcon(string path)
    {
        try
        {
            Sprite sprite = itemAtlas.GetSprite(path);
            return sprite;
        }
        catch (System.Exception)
        {
            return null;
        }
    }

    public Sprite GetOtherItemIcon(string path)
    {
        return otherAtlas.GetSprite(path);
    }

    public Sprite GetLihuiItemIcon(string name)
    {
        return lihuiAtlas.GetSprite(name);
    }

    public Sprite GetFlyWordAtlas(string name)
    {
        return flywordAtlas.GetSprite(name);
    }
}