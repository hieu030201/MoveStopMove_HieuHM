using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    float delayTime = 1;

    public void OnEnter(Enemy enemy)
    {
        enemy.SelectNearEnemy();
        timer = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if(timer > delayTime + 1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
       
    }
   
}
