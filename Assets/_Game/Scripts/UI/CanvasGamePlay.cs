using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI aliveTxt;

    public override void Setup()
    {
        base.Setup();
        SetTextAlive();
    }
    public void SetTextAlive()
    {
        this.aliveTxt.text = LevelManager.Ins.TotalCharater.ToString();
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close(float delayTime)
    {
        base.Close(delayTime);
    }

    public override void CloseDirectly()
    {
        base.CloseDirectly();
    }
    public void Setting()
    {
        UIManager.Ins.OpenUI<CanvasSetting>();
        UIManager.Ins.CloseUI<CanvasGamePlay>();
        GameManager.ChangeState(GameState.GamePause);
    }
}
