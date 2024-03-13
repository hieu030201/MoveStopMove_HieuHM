using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamage : EffectMedicine
{
    CounterTime counterTime = new CounterTime();

    public override void OnInit(Transform parent)
    {
        base.OnInit(parent);
        counterTime.Start(OnDespawn, Constant.TIME_ALIVE_EFFECT_DAMAGE);
    }

    public override void Update()
    {
        base.Update();
        counterTime.Execute();
    }
}
