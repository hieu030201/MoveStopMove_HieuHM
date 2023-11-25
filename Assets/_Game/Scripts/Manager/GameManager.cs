using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState 
{ 
    MainMenu = 0, 
    GamePlay = 1,
    GamePause = 2, 
    Finish = 3, 
    Revive = 4, 
    Setting = 5 
}
public class GameManager : Singleton<GameManager>
{
    public static GameState gameState;
    public static void ChangeState(GameState state)
    {
        gameState = state;
    }
    public static bool IsState(GameState state) => gameState == state;

    private void Awake()
    {
        // tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;

        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //Xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

    }
    public void Start()
    {
        UIGameManager.Ins.OnInit();
    }


}
