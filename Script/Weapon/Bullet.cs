
using System;
using UnityEngine;

public class Bullet : GameUnit
{
    protected Vector3 target;
    [SerializeField] protected Character character;
    [SerializeField] protected float speed;
    public float damage;
    protected Action<Character, Character, float> onHit;

    public GameObject player;
    public float maxDistance = 25f;
    public float minVolume = 0.1f;
    public float maxVolume = 1f;

    public virtual void OnEnable()
    {
        player = GameObject.Find("Player");
    }

    public float VolumDistancePlayer()
    {
        float distancePlayer = Vector3.Distance(character.TF.position, player.transform.position);
        float volume = Mathf.Lerp(maxVolume, minVolume, distancePlayer / maxDistance);
        return volume;
    }
    public virtual void OnInit(Character character, Vector3 target)
    {
        Vector3 posTarget = new Vector3 (target.x, target.y + 1f, target.z);
        TF.forward = (posTarget - TF.position).normalized;

        this.character = character; 
        this.target = target;
   
        SoundManager.Ins.PlayOnShot(SoundType.SoundAttack, VolumDistancePlayer());


    }

    public virtual void OnHit(Character attacker, Action<Character, Character, float> onHit)
    {
        this.character = attacker;
        this.onHit = onHit;
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public virtual void Update()
    {

    }
    protected virtual void Stop()
    {
        TF.position = Vector3.zero;
        Debug.Log(TF.position);
    }

    public virtual void IncreaseDamage()
    {
        this.damage *= 2;
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(Constant.TAG_CHARACTER) && other.gameObject != this.character.gameObject)
        {
            Character victim = Cache.GetCollectCharacter(other);
            onHit?.Invoke(character,victim, damage);
            ParticlePool.Play(ParticleType.Hit_1, TF.position);
            SoundManager.Ins.PlayOnShot(SoundType.SoundOnHit, VolumDistancePlayer());
            OnDespawn();
        }

        if (other.CompareTag(Constant.TAG_OBSTACLE))
        {
            OnDespawn();
        }

    }
}
