using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[AddComponentMenu("MyButton")]
[System.Serializable]
public class MyButton : Button
{
    public int clickAudioId = -1;

    private void PlayButtonAudio()
    {

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        Debug.Log("Testing");
    }
}
