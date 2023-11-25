using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    
    public void StartGamePlay()
    {
        GameManager.ChangeState(GameState.GamePlay);
        UIManager.Ins.OpenUI<CanvasGamePlay>();
        CloseDirectly();
    }

    public void ShopWeapon()
    {
        UIManager.Ins.OpenUI<CanvasWeapon>();
        UIManager.Ins.CloseUI<CanvasMainMenu>();
    }

    public void ShopSkin()
    {
        UIManager.Ins.OpenUI<CanvasSkinShop>();
        UIManager.Ins.CloseUI<CanvasMainMenu>();
    }
}
