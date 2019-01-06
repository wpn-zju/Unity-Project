using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class ItemLoader
{
    public static string dataPathEquip = "data/txt/gear";
    public static string dataPathOther = "data/txt/other";
    public static string dataPathDefCurve = "data/txt/defcurve";
    public static string dataPathResource = "data/txt/itemresource";
    public static Dictionary<int, EquipmentItemData> dataEquip = new Dictionary<int, EquipmentItemData>();
    public static Dictionary<int, BaseItemData> dataOther = new Dictionary<int, BaseItemData>();
    public static Dictionary<int, float> defCurve = new Dictionary<int, float>();
    public static Dictionary<int, ItemResource> dataResource = new Dictionary<int, ItemResource>();
    public static Dictionary<int, BaseItemData> dataTotal = new Dictionary<int, BaseItemData>();

    public static void LoadData()
    {
        LoadDataEquip();

        LoadDataOther();

        LoadDefCurve();

        LoadItemResource();

        LinkData();
    }

    private static void LoadDataEquip()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPathEquip);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            EquipmentItemData item = new EquipmentItemData();

            int id = Convert.ToInt32(temp1[0]);

            item.type = ItemType.Equipment;
            item.id_int = Convert.ToInt32(temp1[0]);
            item.name = null;
            item.cnName = null;
            item.describe = null;
            item.cnDescribe = null;
            item.priceOut = 0;
            item.priceIn = 10000;

            item.iconPath = null;
            item.quality = ItemQuality.Normal;
            item.part = (EquipmentPart)Convert.ToInt32(temp1[1]);
            item.attriArray = 0;
            item.resourceId = 0;
            item.weaponType = WeaponType.not_a_weapon;

            dataEquip[id] = item;
        }

        Debug.Log("Loading Equipment Successfully!");
    }

    private static void LoadDataOther()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPathOther);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            BaseItemData item = new BaseItemData();

            int id = Convert.ToInt32(temp1[0]);

            item.id_int = Convert.ToInt32(temp1[0]);
            item.name = temp1[1];
            item.cnName = temp1[2];
            item.describe = temp1[3];
            item.cnDescribe = temp1[4];
            item.priceIn = Convert.ToInt32(temp1[5]);
            item.priceOut = Convert.ToInt32(temp1[6]);
            item.iconPath = temp1[7];
            item.quality = (ItemQuality)Convert.ToInt32(temp1[8]);

            switch (temp1[9])
            {
                case "1":
                    item.type = ItemType.CanUse;
                    break;
                case "2":
                    item.type = ItemType.Rune;
                    break;
                case "3":
                    item.type = ItemType.Others;
                    break;
            }

            dataOther[id] = item;
        }

        Debug.Log("Loading Other Item Successfully!");
    }

    private static void LoadDefCurve()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPathDefCurve);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            defCurve[Convert.ToInt32(temp1[0])] = (float)Convert.ToDouble(temp1[1]);
        }

        Debug.Log("Loading DefCurve Successfully!");
    }

    private static void LoadItemResource()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPathResource);

        string dataText = textAsset.text;
        dataText.Replace("\r", "");

        string[] dataTextLines = textAsset.text.Split('\n');

        for (int i = 1; i < dataTextLines.Length; ++i)
        {
            string line = dataTextLines[i];

            string[] temp1 = line.Split('\t');

            ItemResource temp = new ItemResource();

            int id = Convert.ToInt32(temp1[0]);

            temp.id_int = Convert.ToInt32(temp1[0]);
            temp.name = temp1[1];
            temp.cnName = temp1[2];
            temp.describe = temp1[3];
            temp.cnDescribe = temp1[4];
            temp.iconPath = temp1[5];
            temp.atlasPath = temp1[6];

            {
                string[] temp2 = temp1[7].Split('$');
                for (int j = 0; j < temp2.Length; ++j)
                    temp.spineSlots.Add(temp2[j]);
            }

            temp.part = (EquipmentPart)Convert.ToInt32(temp1[8]);
            temp.weaponType = (WeaponType)Convert.ToInt32(temp1[9]);

            dataResource[id] = temp;
        }

        Debug.Log("Loading Item Resource Successfully!");
    }

    private static void LinkData()
    {
        foreach(KeyValuePair<int, EquipmentItemData> kvp in dataEquip)
            dataTotal[kvp.Key] = kvp.Value;
        foreach (KeyValuePair<int, BaseItemData> kvp in dataOther)
            dataTotal[kvp.Key] = kvp.Value;
    }
}