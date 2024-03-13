using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMedicine : GameUnit
{

    public virtual void OnInit(Transform parent)
    {
        this.TF.position = parent.position;
        this.TF.SetParent(parent);
    }
    public virtual void Update()
    {

    }
    public virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
