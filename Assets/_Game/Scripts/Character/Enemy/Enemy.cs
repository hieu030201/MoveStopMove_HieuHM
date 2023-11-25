using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private IState currentState;
    [SerializeField] protected NavMeshAgent agent;

    private Vector3 destinationPos;
    [SerializeField] private float radius;

    public override void OnInit()
    {
        base.OnInit();
        attack.OnInit();
        destinationPos = transform.position;
        ChangeState(new PatrolState());
        RandomColorSkin();
        RandomPantSkin();
        RandomHatSkin();
        SyncWeapon();
        SetLevelCharacter();
        SetName();
    }
    public void SetName()
    {
        characterName = NameUtilities.GetRandomName();
    }
    public override void Update()
    {

        base.Update();
        if (GameManager.IsState(GameState.GamePlay))
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
            EnemyUpdate();
        }
        else if (GameManager.IsState(GameState.GamePause))
        {
            return;
        }
        if (isDead)
        {
            agent.SetDestination(tf.position);
        }

    }
    public void EnemyUpdate()
    {

        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

    }
    public override void SelectNearEnemy()
    {
        if (attackRange.CheckInRangeCharacter() != null)
        {
            Vector3 targetEnemy = attackRange.CheckInRangeCharacter().transform.position;
            DirectAttack(targetEnemy);
            if (anim.GetBool(Constant.ANIM_IDLE))
            {
                attack.SetValueAttack(true);
            }
        }
        else
        {
            return;
        }
    }

    private void RandomDestination(Vector3 startPosition)
    {
        Vector3 dir = Random.insideUnitSphere * radius;
        dir += startPosition;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(dir, out hit, radius, 1))
        {
            destinationPos = hit.position;
        }
    }

    public void Moving()
    {
        anim.SetBool(Constant.ANIM_IDLE, false);
        if (Vector3.Distance(destinationPos, tf.position) <= 1f)
        {
            RandomDestination(tf.position);
        }

        agent.SetDestination(destinationPos);
    }
    public void StopMoving()
    {
        anim.SetBool(Constant.ANIM_IDLE, true);
        agent.SetDestination(tf.position);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }

    }

    public void SetLevelCharacter()
    {
        levelCharacter = Random.Range(0, 25);
    }
    public override void Die()
    {
        base.Die();

    }
    public override void CharacterDie()
    {
        base.CharacterDie();
        ObjectPooler.Despawn(this);
        //Destroy(gameObject);
    }

    public void SyncWeapon()
    {
        weaponHand = (WeaponHandType)Random.Range(0, System.Enum.GetValues(typeof(WeaponHandType)).Length);
        switch (weaponHand)
        {
            case WeaponHandType.None:
                equipedWeapon = WeaponType.Hammer_ID01;
                break;
            case WeaponHandType.Hammer_ID02:
                equipedWeapon = WeaponType.Hammer_ID02;
                break;
            case WeaponHandType.Hammer_ID03:
                equipedWeapon = WeaponType.Hammer_ID03;
                break;
            case WeaponHandType.Candy_ID01:
                equipedWeapon = WeaponType.Candy_ID01;
                break;
            case WeaponHandType.Candy_ID02:
                equipedWeapon = WeaponType.Candy_ID02;
                break;
            case WeaponHandType.Knife_ID01:
                equipedWeapon = WeaponType.Knife_ID01;
                break;
            case WeaponHandType.Knife_ID02:
                equipedWeapon = WeaponType.Knife_ID02;
                break;
            case WeaponHandType.Boomerang_ID01:
                equipedWeapon = WeaponType.Boomerang_ID01;
                break;
            case WeaponHandType.Boomerang_ID02:
                equipedWeapon = WeaponType.Boomerang_ID02;
                break;
        }
        //equipedWeapon = WeaponType.Hammer_ID01;
    }
    public void RandomColorSkin()
    {
        color = (ColorType)Random.Range(1, System.Enum.GetValues(typeof(ColorType)).Length);
    }

    public void RandomPantSkin()
    {
        pantsType = (PantsType)Random.Range(1, System.Enum.GetValues(typeof(PantsType)).Length);
    }
    public void RandomHatSkin()
    {
        hatType = (HatType)Random.Range(1, System.Enum.GetValues(typeof(HatType)).Length);
    }
    public override void ChangeColor(ColorType colorType)
    {
        base.ChangeColor(colorType);
    }
}