
using UnityEngine;

public class Weapon : GameUnit
{
    public Character _owner;
    public float bulletSpeed;
    public float despawnTime = 1;
    public float despawnTimer;

    public void OnInit(Character owner)
    {
        _owner = owner;
        despawnTimer = despawnTime;
    }
    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public virtual void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;

        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            ObjectPooler.Despawn(this);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            IDamage damage = other.transform.GetComponent<IDamage>();
            Character getHit = Cache.GetCollectCharacter(other);
            if (getHit != _owner) 
            {
                if (damage != null)
                {
                    damage.Damage(_owner, getHit);
                }
                //ParticlePool.Play(hitVFX, Transform.position, Quaternion.identity);
                ObjectPooler.Despawn(this);
                _owner.Hit();
            }
        }
    }


}

