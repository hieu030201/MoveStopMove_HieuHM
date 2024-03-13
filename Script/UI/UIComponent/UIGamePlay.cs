using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    public TextMeshProUGUI characterAlive;
    public Slider timeDamageSlider;
    private float sliderDamageTime;

    public Slider timeSpeedSlider;
    private float sliderSpeedTime;

    public bool activeDamage = false;
    public bool activeSpeed = false;

    public override void Setup()
    {
        base.Setup();
        TotalCharacterAlive();
    }
    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChanState(GameState.GamePlay);
    
    }
    public override void CloseDirectly()
    {
        base.CloseDirectly();

    }

    public void TotalCharacterAlive()
    {
        characterAlive.text = LevelManager.Ins.TotalCharater.ToString();
    }

    public void Setting()
    {
        UIManager.Ins.OpenUI<UISetting>();
        UIManager.Ins.CloseUI<UIGamePlay>();
        GameManager.Ins.ChanState(GameState.GamePause);
    }

    public void StartTimeDamageSlider()
    {
        activeDamage = true;
        sliderDamageTime = Constant.TIME_ALIVE_EFFECT_DAMAGE;
        timeDamageSlider.gameObject.SetActive(true);
        timeDamageSlider.maxValue = sliderDamageTime;
        timeDamageSlider.value = sliderDamageTime;
        StartTime();
    }

    public void StartTime()
    {
        StartCoroutine(StartTheDamageTimeSticker());
    }

    IEnumerator StartTheDamageTimeSticker()
    {
        while (activeDamage)
        {
            sliderDamageTime -= Time.deltaTime;
            yield return new WaitForSeconds(0.0001f);

            timeDamageSlider.value = sliderDamageTime;
            if(sliderDamageTime < 0.01f)
            {
                activeDamage = false;
                timeDamageSlider.gameObject.SetActive(false);
                sliderDamageTime = Constant.TIME_ALIVE_EFFECT_DAMAGE;
            }
        }
    }

    public void StartTimeSpeedSlider()
    {
        activeSpeed = true;
        sliderSpeedTime = Constant.TIME_ALIVE_EFFECT_DAMAGE;
        timeSpeedSlider.gameObject.SetActive(true);
        timeSpeedSlider.maxValue = sliderSpeedTime;
        timeSpeedSlider.value = sliderSpeedTime;
        StartSpeedTime();
    }

    public void StartSpeedTime()
    {
        StartCoroutine(StartTheSpeedTimeSticker());
    }

    IEnumerator StartTheSpeedTimeSticker()
    {
        while (activeSpeed)
        {
            sliderSpeedTime -= Time.deltaTime;
            yield return new WaitForSeconds(0.0001f);

            timeSpeedSlider.value = sliderSpeedTime;
            if (sliderSpeedTime < 0.01f)
            {
                activeSpeed= false;
                timeSpeedSlider.gameObject.SetActive(false);
                sliderSpeedTime = Constant.TIME_ALIVE_EFFECT_DAMAGE;
            }
        }
    }
}
