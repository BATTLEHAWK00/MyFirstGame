using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : BaseManager<AudioManager>
{
    private GameObject obj_Audio = null;
    private AudioSource bgm_source = null;
    private AudioMixer Mixer;
    #region 音频轨道
    private AudioMixerGroup BGM_MixerGroup;
    private AudioMixerGroup Sounds_MixerGroup;
    private AudioMixerGroup Master_MixerGroup;
    #endregion
    private List<AudioSource> sound_source = new List<AudioSource>();
    const int Fade = 5000;
    #region 音量
    private float bgm_Volume = 0.5f;
    private float sounds_Volume = 0.5f;
    private float master_Volume = 1f;
    #region 音量访问器
    public float BGM_Volume
    {
        get
        {
            return bgm_Volume;
        }
        set
        {
            if (value > 1f)
                value = 1f;
            else if (value < 0f)
                value = 0f;
            bgm_Volume = value;
            Mixer.SetFloat("BGM_Vol", Mathf.Lerp(-30f, 0f, value));
        }
    }
    public float Sounds_Volume
    {
        get
        {
            return sounds_Volume;
        }
        set
        {
            if (value > 1f)
                value = 1f;
            else if (value < 0f)
                value = 0f;
            sounds_Volume = value;
            Mixer.SetFloat("Sounds_Vol", Mathf.Lerp(-30f, 0f, value));
        }
    }
    public float Master_Volume
    {
        get
        {
            return master_Volume;
        }
        set
        {
            if (value > 1f)
                value = 1f;
            else if (value < 0f)
                value = 0f;
            master_Volume = value;
            Mixer.SetFloat("Master_Vol", Mathf.Lerp(-30f, 0f, value));
        }
    }
    #endregion
    #endregion
    public void PlayBGM(string name)
    {
        if (bgm_source == null)
            Debug.LogError("[错误]找不到音频组件!");
        Debug.Log("[消息]播放BGM:" + name);
        ResManager.Getinstance().LoadAsync<AudioClip>("Audio/BGM/" + name, (clip) => {
            bgm_source.clip = clip;
            bgm_source.volume = 0.25f;
            bgm_source.Play();
        });
    }
    void Init()
    {
        Mixer = ResManager.Getinstance().Load<AudioMixer>("Audio/AudioMaster");
        BGM_MixerGroup = Mixer.FindMatchingGroups("Master/BGM")[0];
        Sounds_MixerGroup = Mixer.FindMatchingGroups("Master/Sounds")[0];
        obj_Audio = new GameObject();
        obj_Audio.name = "Audio";
        bgm_source = obj_Audio.AddComponent<AudioSource>();
        bgm_source.outputAudioMixerGroup = BGM_MixerGroup;
        GameObject.DontDestroyOnLoad(obj_Audio);
    }
    public void SwitchBGM()
    {
        if (bgm_source.isPlaying)
            bgm_source.Pause();
        else
            bgm_source.UnPause();
    }
    public void StopBGM()
    {
        bgm_source.Pause();
    }
    private float currentMasterVolume;
    public void Mute()
    {
        currentMasterVolume = Master_Volume;
        Master_Volume = 0f;
    }
    public void Resume()
    {
        Master_Volume = currentMasterVolume;
    }
    public void PlaySound(string name)
    {
        PlaySound(name);
    }
    public void PlaySound(string name,float volume)
    {
        ResManager.Getinstance().LoadAsync<AudioClip>("Audio/Sounds/" + name, (clip) => {
            AudioSource audioSource = obj_Audio.AddComponent<AudioSource>();
            sound_source.Add(audioSource);
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.outputAudioMixerGroup = Sounds_MixerGroup;
            audioSource.Play();
            MonoBase.Getinstance().GetMono().StartCoroutine(AudioTimeToLive(audioSource));
        });
    }
    IEnumerator AudioTimeToLive(AudioSource audioSource)
    {
        while (audioSource.isPlaying)
            yield return new WaitForSeconds(0.5f);
        sound_source.Remove(audioSource);
        GameObject.Destroy(audioSource);
    }
    /*  因代码优化而弃用
    void CleanAudioSource()
    {
        for (int i = 0; i < sound_source.Count; i++)
        {
            if (!sound_source[i].isPlaying)
            {
                GameObject.Destroy(sound_source[i]);
                sound_source.RemoveAt(i);
            }
        }
    }
    */
    public AudioManager()
    {
        Init();
        //TheGame.Getinstance().GameMain.MonoManager.AddUpdateListener(CleanAudioSource);
    }
}
