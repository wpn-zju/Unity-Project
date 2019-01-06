using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonObject<AudioManager>
{
    private AudioSource bgmAudio;
    private List<AudioSource> effectAudio = new List<AudioSource>();
    private Dictionary<int, AudioClip> audioDic = new Dictionary<int, AudioClip>();
    private int maxEffectAudioNum = 5;

    public void Init()
    {
        bgmAudio = GameObject.Find("AudioBackground").GetComponent<AudioSource>();

        GameObject effectAudioObj = GameObject.Find("AudioEffectSound");

        for (int i = 0; i < maxEffectAudioNum; ++i)
        {
            AudioSource audio = effectAudioObj.AddComponent<AudioSource>();
            audio.loop = false;
            effectAudio.Add(audio);
        }
    }

    public void LoadAudioClip()
    {
        foreach (KeyValuePair<int, Audio> kvp in AudioLoader.data)
        {
            AudioClip clip = Resources.Load<AudioClip>(kvp.Value.path);
            audioDic[kvp.Key] = clip;
        }
    }

    public void PlayBGM(int id)
    {
        if (bgmAudio.clip == audioDic[id])
            return;
        else if (bgmAudio.clip != null)
            bgmAudio.Stop();

        bgmAudio.clip = audioDic[id];
        bgmAudio.volume = AudioLoader.data[id].volume;
        bgmAudio.pitch = AudioLoader.data[id].pitch;
        bgmAudio.priority = AudioLoader.data[id].priority;
        bgmAudio.Play();
    }

    public void PlayEffect(int id)
    {
        int currentEffects = 0;
        int idleAudioIndex = -1;

        for (int i = 0; i < effectAudio.Count; ++i)
            if (effectAudio[i].isPlaying)
                currentEffects++;
            else
                if (idleAudioIndex == -1)
                idleAudioIndex = i;

        if (idleAudioIndex == -1)
            return;
        else
        {
            effectAudio[idleAudioIndex].clip = audioDic[id];
            effectAudio[idleAudioIndex].volume = AudioLoader.data[id].volume;
            effectAudio[idleAudioIndex].pitch = AudioLoader.data[id].pitch;
            effectAudio[idleAudioIndex].priority = AudioLoader.data[id].priority;
            effectAudio[idleAudioIndex].Play();
        }
    }

    public void ClearEffects()
    {
        foreach (AudioSource v in effectAudio)
        {
            v.Stop();
            v.clip = null;
        }
    }
}