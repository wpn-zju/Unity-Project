using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// open & close
public class DynamicAnimation : MonoBehaviour {
    private Animation _animation = null;
    private AnimationClip _clipIn = null;
    private AnimationClip _clipOut = null;

    public Transform controlObj = null;
    public List<AnimationAction> actionsIn;
    public List<AnimationAction> actionOut;
    [HideInInspector]
    public bool hasIn = false;
    [HideInInspector]
    public bool hasOut = false;

    public Animation GetAnimation()
    {
        return _animation;
    }

    public void Open()
    {
        _animation.Play("In");
    }

    public void Close()
    {
        _animation.Play("Out");
    }

    public void Init()
    {
        _animation = controlObj.gameObject.AddComponent<Animation>();

        _clipIn = new AnimationClip()
        {
            name = "AnimationIn",
            legacy = true,
            wrapMode = WrapMode.Once,
        };

        _clipOut = new AnimationClip()
        {
            name = "AnimationOut",
            legacy = true,
            wrapMode = WrapMode.Once,
        };

        foreach (AnimationAction act in actionsIn)
        {
            hasIn = true;
            AddActionClip(_clipIn, act.type, act.time, act.value);
        }

        foreach (AnimationAction act in actionOut)
        {
            hasOut = true;
            AddActionClip(_clipOut, act.type, act.time, act.value);
        }

        _animation.AddClip(_clipIn, "In");
        _animation.AddClip(_clipOut, "Out");
    }

    private void AddActionClip(AnimationClip clip, ActionType type, List<float> time, List<float> value)
    {
        AnimationCurve curve = new AnimationCurve();

        for (int i = 0; i < time.Count; ++i)
            curve.AddKey(new Keyframe(time[i], value[i]));

        switch (type)
        {
            case ActionType.xScale:
                clip.SetCurve("", typeof(RectTransform), "m_LocalScale.x", curve);
                break;
            case ActionType.yScale:
                clip.SetCurve("", typeof(RectTransform), "m_LocalScale.y", curve);
                break;
            case ActionType.xPosition:
                clip.SetCurve("", typeof(RectTransform), "m_AnchoredPosition.x", curve);
                break;
            case ActionType.yPosition:
                clip.SetCurve("", typeof(RectTransform), "m_AnchoredPosition.y", curve);
                break;
            case ActionType.alpha:
                clip.SetCurve("", typeof(Graphic), "m_Color.a", curve);
                break;
        }
    }

    public string GetFullPath(Transform obj)
    {
        if (obj.gameObject == gameObject)
            return "";

        string path = obj.name;

        while (obj.parent.gameObject != gameObject)
        {
            path = obj.parent.name + "/" + path;
            obj = obj.parent;
        }

        return path;
    }
}

[System.Serializable]
public struct AnimationAction
{
    public ActionType type;
    public List<float> time;
    public List<float> value;
}