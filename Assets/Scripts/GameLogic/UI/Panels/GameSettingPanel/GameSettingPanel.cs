using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSettingPanel : PanelBase
{
    #region 音频组件
    [Header("Audio")]
    public Slider BGMVolumeSlider;
    public Slider SoundVolumeSlider;
    public Slider MasterVolumeSlider;
    #endregion
    #region 显示组件
    [Header("Graphics")]
    public Slider GraphicsQualitySlider;
    public Toggle FullScreenToggle;
    private UnityAction OnAwake;
    #endregion

    #region 音频设置事件
    private void OnAwake_Audio()
    {
        BGMVolumeSlider.value = AudioManager.Getinstance().BGM_Volume;
        SoundVolumeSlider.value = AudioManager.Getinstance().Sounds_Volume;
        MasterVolumeSlider.value = AudioManager.Getinstance().Master_Volume;
    }
    public void OnSoundVolumeChanged()
    {
        AudioManager.Getinstance().Sounds_Volume = SoundVolumeSlider.value;
    }
    public void OnBGMVolumeChanged()
    {
        AudioManager.Getinstance().BGM_Volume = BGMVolumeSlider.value;
    }
    public void OnMasterVolumeChanged()
    {
        AudioManager.Getinstance().Master_Volume = MasterVolumeSlider.value;
    }
    #endregion
    #region 显示设置事件
    private Resolution windowedResolution;
    private void OnAwake_Graphics()
    {
        GraphicsQualitySlider.value = QualitySettings.GetQualityLevel();
        FullScreenToggle.isOn = Screen.fullScreen;
    }
    public void OnGraphicsQualityChanged()
    {
        QualitySettings.SetQualityLevel((int)GraphicsQualitySlider.value, true);
    }
    public void OnFullScreenToggleChanged()
    {/*
        if (FullScreenToggle.isOn)
        {
            windowedResolution = Screen.currentResolution;
            Resolution[] resolutions = Screen.resolutions;
            int width = resolutions[resolutions.Length - 1].width;
            int height = resolutions[resolutions.Length - 1].height;
            Screen.SetResolution(width, height, true);
        }
        else
        {
            Screen.SetResolution(windowedResolution.width, windowedResolution.height, false);
        }*/
        
    }
    #endregion
    #region 其它事件
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ToMainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("GameMenu");
    }
    #endregion
    #region MonoBehavior事件
    public override void Awake()
    {
        base.Awake();
        if (OnAwake != null)
            OnAwake();
    }
    #endregion

    public GameSettingPanel()   //初始化
    {
        base.Layer = UILayers.Top;
        OnAwake += OnAwake_Audio;
        OnAwake += OnAwake_Graphics;
    }
}
