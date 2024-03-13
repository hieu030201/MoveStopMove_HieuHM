using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        SeekTarget(t);
        t.ChangeAnim(Constant.ANIM_RUN);
    }

    public void OnExecute(Enemy t)
    {
        if (t.IsDestination)
        {
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy t)
    {

    }

    private void SeekTarget(Enemy t)
    {
        Vector3 newPos = t.RandomNavSphere(t.TF.position, 20.0f, 1);

        t.SetDestination(newPos);
    }
}
