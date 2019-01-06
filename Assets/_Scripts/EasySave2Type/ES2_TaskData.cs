using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2_TaskData : ES2Type
{
    public override void Write(object obj, ES2Writer writer)
    {
        TaskData data = (TaskData)obj;
        // Add your writer.Write calls here.
        writer.Write(data.id_int);
        writer.Write((int)data.state);
        writer.Write(data.killEnemies);
        writer.Write(data.currentKill);
        writer.Write(data.itemsCollect);
        writer.Write(data.controlConditions);
    }

    public override object Read(ES2Reader reader)
    {
        Read(reader, null);
        return null;
    }

    public override void Read(ES2Reader reader, object c)
    {
        // Add your reader.Read calls here to read the data into the object.
        int id_int = reader.Read<System.Int32>();
        TaskData temp = TaskLoader.data[id_int];
        temp.state = (TaskState)reader.Read<System.Int32>();
        temp.killEnemies = reader.ReadDictionary<int, int>();
        temp.currentKill = reader.ReadDictionary<int, int>();
        temp.itemsCollect = reader.ReadDictionary<int, int>();
        temp.controlConditions = reader.ReadDictionary<int, bool>();
    }

    /* ! Don't modify anything below this line ! */
    public ES2_TaskData() : base(typeof(TaskData)) { }
}