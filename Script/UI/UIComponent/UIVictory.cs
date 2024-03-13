using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIVictory : UICanvas
{
    private int coin;
    [SerializeField] TextMeshProUGUI coinTxt;
    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChanState(GameState.Finish);
    }
    public void NextAreaButton()
    {
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.Home();

    }
    internal void SetCoin(int coin)
    {
        this.coin = coin;
        coinTxt.SetText(coin.ToString());
        DataUser.Ins.coin += coin;
    }
}
