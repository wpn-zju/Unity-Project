using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2_EquipmentItemData : ES2Type
{
    public override void Write(object obj, ES2Writer writer)
    {
        EquipmentItemData data = (EquipmentItemData)obj;
        // Add your writer.Write calls here.
        writer.Write(data.id_int);
        writer.Write((int)data.quality);
        writer.Write(data.attriArray);
        writer.Write(data.resourceId);
    }

    public override object Read(ES2Reader reader)
    {
        EquipmentItemData data = new EquipmentItemData();
        Read(reader, data);
        return data;
    }

    public override void Read(ES2Reader reader, object c)
    {
        EquipmentItemData data = (EquipmentItemData)c;
        // Add your reader.Read calls here to read the data into the object.
        data.id_int = reader.Read<System.Int32>();
        data.quality = (ItemQuality)reader.Read<System.Int32>();
        data.attriArray = reader.Read<System.UInt32>();
        data.resourceId = reader.Read<System.Int32>();
    }

    /* ! Don't modify anything below this line ! */
    public ES2_EquipmentItemData() : base(typeof(EquipmentItemData)) { }
}