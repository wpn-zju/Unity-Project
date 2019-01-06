using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Capture
{
    public static Sprite CaptureCamera(int width = -1, int height = -1)
    {
        Rect rect = height == -1 ? new Rect(0, 0, Screen.width, Screen.height) : new Rect(0, 0, width, height);

        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
        
        Camera.main.targetTexture = rt;

        Camera.main.Render();

        RenderTexture.active = rt;

        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();

        Camera.main.targetTexture = null;

        RenderTexture.active = null;
        GameObject.Destroy(rt);

        Sprite n = Sprite.Create(screenShot, new Rect(0, 0, rect.width, rect.height), new Vector2(0.5f, 0.5f));
        return n;
    }
}
