using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : Singleton<UIGameManager>
{
    // Start is called before the first frame update
    public void OnInit()
    {
        //TODO:
        UIManager.Ins.OpenUI<CanvasMainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
