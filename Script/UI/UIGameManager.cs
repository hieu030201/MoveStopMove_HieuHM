using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : Singleton<UIGameManager>
{
    public void OnInit()
    {
        GameManager.Ins.ChanState(GameState.MainMenu);
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

}
