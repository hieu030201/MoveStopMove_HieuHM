using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    private int coin;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] RectTransform x3Point;
    [SerializeField] RectTransform mainMenuPoint;

    public override void Open()
    {
        base.Open();
        GameManager.ChangeState(GameState.Finish);
    }

    public void x3Button()
    {
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.Home();
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
        UserData.Ins.coin += coin;
    }
}
