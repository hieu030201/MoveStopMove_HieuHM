using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private float randomTime;
    private float timer;

    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(5f, 7f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(enemy.IsTargetInRange() && timer > 2f)
        {
            enemy.ChangeState(new IdleState());
        }
        else if(timer < randomTime)
        {
            enemy.Moving();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }
    public void OnExit(Enemy enemy) { }
}
