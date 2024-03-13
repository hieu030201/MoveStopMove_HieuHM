using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UICanvas
{
    public Button musicOff;
    public Button musicOn;
    void Start()
    {
        musicOn.gameObject.SetActive(true);
        musicOff.gameObject.SetActive(false);
    }
    public void ClickBtnOffMusic()
    {
        musicOn.gameObject.SetActive(true);
        musicOff.gameObject.SetActive(false);
        SoundManager.Ins.PlayOnShot(SoundType.SoundClick);
        SoundManager.Ins.PlayMusic();
    }

    public void ClickBtnOnMusic()
    {
        musicOn.gameObject.SetActive(false);
        musicOff.gameObject.SetActive(true);
        SoundManager.Ins.StopMusic();
    }

    public void ClickHome()
    {

    }

    public void ClickContinue()
    {
        UIManager.Ins.OpenUI<UIGamePlay>();
        UIManager.Ins.CloseUI<UISetting>();
        GameManager.Ins.ChanState(GameState.GamePlay);
    }
}
