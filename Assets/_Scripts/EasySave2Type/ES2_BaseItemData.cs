using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2_BaseItemData : ES2Type
{
    public override void Write(object obj, ES2Writer writer)
    {
        BaseItemData data = (BaseItemData)obj;
        // Add your writer.Write calls here.
        writer.Write(data.id_int);
        writer.Write(data.number);
    }

    public override object Read(ES2Reader reader)
    {
        BaseItemData data = new BaseItemData();
        Read(reader, data);
        return data;
    }

    public override void Read(ES2Reader reader, object c)
    {
        BaseItemData data = (BaseItemData)c;
        // Add your reader.Read calls here to read the data into the object.
        data.id_int = reader.Read<System.Int32>();
        data.number = reader.Read<System.Int32>();
    }

    /* ! Don't modify anything below this line ! */
    public ES2_BaseItemData() : base(typeof(BaseItemData)) { }
}