﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class StartGame : MonoBehaviour
{
    public GameObject BlackPanel;
    private void Awake()
    {
        AudioManager.Get().PlayBGM("MainMenu");
    }
    public void Load()
    {
        transform.Find("Button").gameObject.SetActive(false);
        MonoBase.Get().GetMono().RunDelayTask(() =>
        {
            StartCoroutine(LoadScene("GameMain"));
        },0.25f);
    }
    IEnumerator LoadScene(string name)
    {
        GameObject bar = ResManager.Get().Load<GameObject>("Prefabs/UI/OtherScene/LoadingBar");
        Text text = bar.GetComponentInChildren<Text>();
        Slider slider = bar.GetComponentInChildren<Slider>();
        void setvalue(float value)
        {
            text.text = Mathf.Round(value*100f).ToString()+"%";
            slider.value = value;
        }
        bar.transform.SetParent(GameObject.Find("Canvas").transform,false);
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = false;
        float currentvalue=asyncOperation.progress,targetvalue=asyncOperation.progress;
        while (asyncOperation.progress<=0.9f)
        {
            yield return new WaitForEndOfFrame();
            targetvalue = asyncOperation.progress / 0.9f;
            while(targetvalue>currentvalue && Mathf.Abs(currentvalue-targetvalue)>=0.01f)
            {
                float value= (targetvalue-currentvalue)*0.1f;
                currentvalue += value;
                setvalue(currentvalue);
                yield return new WaitForSeconds(0.025f);
            }
            if (Mathf.Abs(currentvalue - 1f) <= 0.01f)
            { 
                setvalue(1f);
                yield return new WaitForSeconds(0.5f);
                break; 
            }
            yield return new WaitForEndOfFrame();
        }
        BlackPanel.GetComponent<Image>().DOFade(1f, 0.5f).OnComplete(() => {
            asyncOperation.allowSceneActivation = true;
        });
        yield break;
    }
}
