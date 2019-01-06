using System;
using System.Collections.Generic;

public class SingletonObject<T>
{
    private static T m_Singleton;

    public static T GetInst()
    {
        if (m_Singleton == null)
        {
            m_Singleton = (T)Activator.CreateInstance(typeof(T));
        }
        return m_Singleton;
    }
}