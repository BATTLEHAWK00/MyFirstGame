using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    private GameObject obj_Audio = null;
    private AudioSource bgm_source = null;
    private List<AudioSource> sound_source = new List<AudioSource>();
    const int Fade = 5000;
    public float BGM_Volume{
        get
        { 
            return bgm_source.volume*4; 
        } 
        set
        {
            if (value > 1f)
                value = 1f;
            else if (value < 0f)
                value = 0f;
            bgm_source.volume = value * 0.25f; ;
        }
    }
    public float Sound_Volume = 1f;
    public void PlayBGM(string name)
    {
        if (bgm_source == null)
            Debug.LogError("[错误]找不到音频组件!");
        Debug.Log("[消息]播放BGM:" + name);
        ResManager.Getinstance().LoadAsync<AudioClip>("Audio/BGM/" + name, (clip) => {
            bgm_source.clip = clip;
            bgm_source.Play();
        });
    }
    void Init()
    {
        obj_Audio = new GameObject();
        obj_Audio.name = "Audio";
        bgm_source = obj_Audio.AddComponent<AudioSource>();
        bgm_source.volume = 0.05f;
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
    
    public void PlaySound(string name)
    {
        PlaySound(name, Sound_Volume);
    }
    public void PlaySound(string name,float volume)
    {
        ResManager.Getinstance().LoadAsync<AudioClip>("Audio/Sounds/" + name, (clip) => {
            AudioSource audioSource = obj_Audio.AddComponent<AudioSource>();
            sound_source.Add(audioSource);
            audioSource.clip = clip;
            audioSource.volume = volume*Sound_Volume;
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
