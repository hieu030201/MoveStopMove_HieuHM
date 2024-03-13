using Scriptable;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Enemy : Character
{
    private Vector3 destination;
    [SerializeField] protected NavMeshAgent agent;
    private float initSpeed;
    protected IState<Enemy> currentState;

    private CounterTime counter = new CounterTime();
    public CounterTime Counter => counter;

    private CounterTime couterAttack = new CounterTime();
    public CounterTime CounterAttack => couterAttack;

    public bool IsDestination => (Mathf.Abs(destination.x - transform.position.x) + Mathf.Abs(destination.z - transform.position.z)) < 0.05f;

    WeaponType weaponType;

    public void ChangeState(IState<Enemy> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void Awake()
    {
        RandomPantSkin();
        RandomColorSkin();
        RandomWeapon();
    }
    public void OnEnable()
    {
    
    }
    public override void OnInit()
    {
    
        base.OnInit();
        this.health = levelCharacter * 10;
        this.currentHealth = health;
        this.initSpeed = agent.speed;
        targetIndicator.SetLevel(levelCharacter);
        targetIndicator.SetHealth(currentHealth,health);
        targetIndicator.SetName(Utilities.SetNameRandom());
        RandomHat();
        ChangeWeapon(weaponType);
        EquippeHat();
        ChangeState(new IdleState());
        destination = TF.position;
    }
    public override void Update()
    {
        base.Update();
        if (GameManager.Ins.IsState(GameState.GamePlay))
        {
            if (currentState != null && !isDead)
            {
                currentState.OnExecute(this);
            }
        }

    }

    public void SetDestination(Vector3 point)
    {
        isMove = true;
        destination = point;
        agent.enabled = true;
        agent.SetDestination(destination);
        ChangeAnim(Constant.ANIM_RUN);
    }

    public override void MoveStop()
    {
        base.MoveStop();
        agent.enabled = false;
        ChangeAnim(Constant.ANIM_IDLE);
    }


    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        this.levelCharacter = level;
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
    }
    public override void MSpeedEffect()
    {
        base.MSpeedEffect();
        ResetMSpeed();
        agent.speed += (agent.speed / 3);
    }

    private void ResetMSpeed()
    {
        agent.speed = this.initSpeed;
    }

    public void RandomColorSkin()
    {
        colorType = (ColorType)Random.Range(1, System.Enum.GetValues(typeof(ColorType)).Length);
    }

    public void RandomPantSkin()
    {
        pantType = (PantType)Random.Range(1, System.Enum.GetValues(typeof(PantType)).Length);
    }

    public void RandomWeapon()
    {
        var values = System.Enum.GetValues(typeof(WeaponType));
        weaponType = (WeaponType)values.GetValue(Random.Range(0, values.Length));
    }

    public void RandomHat()
    {
        DestroyHat();
        var values = System.Enum.GetValues(typeof(HatType));
        hatType = (HatType)values.GetValue(Random.Range(0, values.Length));
    }

    public void DestroyHat()
    {
        if(currentHat) SimplePool.Despawn(currentHat);
    }
}
