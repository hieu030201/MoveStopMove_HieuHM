using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
 
        t.MoveStop();
        if(t.targets != null)
        {
            t.CounterAttack.Start(() => t.ChangeState(Utilities.Chance(70,100) ? new AttackState() : new PatrolState() ) , 1.5f);
        }
        else
        {
            t.Counter.Start(() => t.ChangeState(new PatrolState()), Random.Range(0f, 2f));
        }
    }

    public void OnExecute(Enemy t)
    {
        t.Counter.Execute();
        t.CounterAttack.Execute();
    }

    public void OnExit(Enemy t)
    {

    }


}
