using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{ MainMenu, GamePlay, GamePause, Finish, Setting}
public class GameManager : Singleton<GameManager>
{
    private GameState gameState;

    public void ChanState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState) => this.gameState == gameState;
    void Start()
    {
        UIGameManager.Ins.OnInit();
        ChanState(GameState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
