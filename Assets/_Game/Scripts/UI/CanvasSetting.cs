using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    public void BtnHome()
    {

    }

    public void BtnContinue()
    {
        UIManager.Ins.CloseUI<CanvasSetting>();
        UIManager.Ins.OpenUI<CanvasGamePlay>();
        GameManager.ChangeState(GameState.GamePlay);
    }
}
