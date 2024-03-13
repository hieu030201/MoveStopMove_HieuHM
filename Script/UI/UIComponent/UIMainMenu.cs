using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    [SerializeField] TextMeshProUGUI playerCoinTxt;
    public override void Open()
    {
        base.Open();
        playerCoinTxt.SetText(DataUser.Ins.coin.ToString());
    }
    public void StarGamePlay()
    {
        GameManager.Ins.ChanState(GameState.GamePlay);
        UIManager.Ins.OpenUI<UIGamePlay>();
        SoundManager.Ins.PlayOnShot(SoundType.SoundClick);
        CloseDirectly();
    }

    public void ShopWeapon()
    {
        UIManager.Ins.OpenUI<UIWeapon>();
        SoundManager.Ins.PlayOnShot(SoundType.SoundClick);
        UIManager.Ins.CloseUI<UIMainMenu>();
    }

    public void ShopSkin()
    {
        UIManager.Ins.OpenUI<UIShop>();
        SoundManager.Ins.PlayOnShot(SoundType.SoundClick);
        UIManager.Ins.CloseUI<UIMainMenu>();
    }
}
