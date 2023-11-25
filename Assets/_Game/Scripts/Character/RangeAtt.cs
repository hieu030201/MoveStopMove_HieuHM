using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeAtt : MonoBehaviour
{
    [SerializeField] private Character parent;
    public Character enemyTarget;
    public void SetTarget(Character enemyTarget)
    {
        this.enemyTarget = enemyTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character c = Cache.GetCollectCharacter(other);
        if (other.CompareTag(Constant.TAG_CHARACTER) && parent != c && !c.isDead)
        {
            SetTarget(c);
 
            if (c is Enemy && parent is Player)
            {
                c.selectEnemy.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character c = Cache.GetCollectCharacter(other);
        if (other.CompareTag(Constant.TAG_CHARACTER) && parent != c)
        {
            SetTarget(null);
            c.selectEnemy.enabled = false;
        }
    }

    public Character CheckInRangeCharacter()
    {
        if (enemyTarget == null) return null;
        if (enemyTarget.isDead)
        {
            enemyTarget = null;
        }
        return enemyTarget;
    }
}
