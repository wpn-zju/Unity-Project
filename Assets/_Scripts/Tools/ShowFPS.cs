using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public float fpsMeasuringDelta = 2.0f;
    private float lastInterval;
    private int m_FrameCount = 0;
    private float m_FPS = 0.0f;

    private void Start()
    {
#if UNITY_ANDROID
        Application.targetFrameRate = -1;
#endif
        lastInterval = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        m_FrameCount = m_FrameCount + 1;

        float timeNow = Time.realtimeSinceStartup;

        if (timeNow > lastInterval + fpsMeasuringDelta)
        {
            m_FPS = m_FrameCount / (timeNow - lastInterval);
            m_FrameCount = 0;
            lastInterval = timeNow;
        }
    }

    private void OnGUI()
    {
        if(GameSettings.showFPS)
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;    //这是设置背景填充的
            bb.normal.textColor = new Color(1.0f, 1.0f, 1.0f);   //设置字体颜色的
            bb.fontSize = 36;

            GUI.Label(new Rect((Screen.width / 2) - 60, 0, 200, 200), "FPS: " + m_FPS.ToString("F0"), bb);
        }
    }
}
