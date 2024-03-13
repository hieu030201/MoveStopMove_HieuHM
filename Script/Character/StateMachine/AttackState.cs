using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.MoveStop();
        t.OnAttack();

        t.Counter.Start(() =>
        {
            t.Throw();

            t.Counter.Start(() => t.ChangeState(Utilities.Chance(70, 100) ? new IdleState() : new PatrolState()), Character.TIME_DELAY_THROW);
        }, Character.TIME_DELAY_THROW);

    }

    public void OnExecute(Enemy t)
    {
        t.Counter.Execute();
    }

    public void OnExit(Enemy t)
    {

    }

}
