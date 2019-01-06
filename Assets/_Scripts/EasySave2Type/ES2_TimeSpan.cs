using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ES2_TimeSpan : ES2Type
{
    public override void Write(object obj, ES2Writer writer)
    {
        TimeSpan ts = (TimeSpan)obj;
        // Add your writer.Write calls here.
        writer.Write(ts.Days);
        writer.Write(ts.Hours);
        writer.Write(ts.Minutes);
        writer.Write(ts.Seconds);
    }

    public override object Read(ES2Reader reader)
    {
        int days = reader.Read<System.Int32>();
        int hours = reader.Read<System.Int32>();
        int minutes = reader.Read<System.Int32>();
        int seconds = reader.Read<System.Int32>();
        TimeSpan ts = new TimeSpan(days, hours, minutes, seconds);
        return ts;
    }

    /* ! Don't modify anything below this line ! */
    public ES2_TimeSpan() : base(typeof(TimeSpan)) { }
}
