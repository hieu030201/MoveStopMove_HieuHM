using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : GameUnit
{
    public int id;
    CounterTime counterTime = new CounterTime();

    public void OnInit(PoolType poolType,Vector3 randomPont)
    {
        SimplePool.Spawn<Medicine>(poolType, randomPont, Quaternion.identity);
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    protected void OnTriggerEnter(Collider other)
    {
        Character c = Cache.GetCollectCharacter(other);
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            OnDespawn();
            c.InfluenceMedicine(id);
        }
    }
}
