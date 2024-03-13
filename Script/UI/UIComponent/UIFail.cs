using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFail : UICanvas
{
    private int coin;
    [SerializeField] TextMeshProUGUI coinTxt;
    //[SerializeField] RectTransform x3Point;
    //[SerializeField] RectTransform mainMenuPoint;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChanState(GameState.Finish);
    }
    public void MainMenuButton()
    {
        LevelManager.Ins.Home();
    }
    public void SetCoin(int coin)
    {
        this.coin = coin;
        coinTxt.SetText(coin.ToString());
        //UserData.Ins.coin += (coin / 2);
    }
}
