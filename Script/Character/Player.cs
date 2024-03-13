using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;


public class Player : Character
{
    [Space]
    [Header("Properties for Player")]
    #region MOVING
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float initSpeed;
    [SerializeField] private float rotateSpeed;

    private float elapsedTimeSpeed = 0;
    private Vector3 _moveVector;
    #endregion
    private CounterTime counter = new CounterTime();
    public CounterTime counterOnAttack = new CounterTime();
    public WeaponType weaponType = WeaponType.W_Hammer_1;

    public int Coin => levelCharacter * 10;
    private FloatingJoystick Joystick
    {
        get
        {
            if (floatingJoystick == null)
            {
                floatingJoystick = FindObjectOfType<FloatingJoystick>();
            }
            return floatingJoystick;
        }
    }
    public void OnEnable()
    {
        OnTakeClothsData();
        EquippeHat();
    }
    public override void OnInit()
    {
        base.OnInit();
        TF.position = Vector3.zero;
        this.experience = 0;
        this.levelCharacter = 1;
        this.health = levelCharacter * 10;
        this.currentHealth = health;
        this.initSpeed = this.moveSpeed;
        targetIndicator.SetHealth(currentHealth, health);
        ChangeAnim(Constant.ANIM_DANCE_SKIN);
        TF.rotation = Quaternion.Euler(0, 180, 0);

        ChangeWeapon(weaponType);

    }
    public override void Update()
    {
        base.Update();
        if (GameManager.Ins.IsState(GameState.GamePlay))
        {
            Move();
        }
        if (IncrementSpeed)
        {
            float detaTime = Time.deltaTime;
            elapsedTimeSpeed += detaTime;

            if (elapsedTimeSpeed > Constant.TIME_ALIVE_EFFECT_SPEED)
            {
                ResetMSpeed();
                elapsedTimeSpeed = 0;
            }
        }
    }

    private void Move()
    {

        if (!isDead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                counter.Cancel();
                counterOnAttack.Cancel();
            }
            _moveVector = Vector3.zero;
            _moveVector.x = Joystick.Horizontal * moveSpeed * Time.deltaTime;
            _moveVector.z = Joystick.Vertical * moveSpeed * Time.deltaTime;

            if (Input.GetMouseButton(0) && Joystick.Horizontal != 0 || Joystick.Vertical != 0)
            {
                isMove = true;
                Vector3 direction = Vector3.RotateTowards(TF.forward, _moveVector, rotateSpeed * Time.deltaTime, 0.0f);
                TF.rotation = Quaternion.LookRotation(direction);
                ChangeAnim(Constant.ANIM_RUN);
            }

            else
            {
                counter.Execute();
                counterOnAttack.Execute();
            }
            if (Input.GetMouseButtonUp(0))
            {
                isMove = false;
                MoveStop();
                OnAttack();
            }
            this.TF.position += _moveVector;
        }

    }

    public override void MoveStop()
    {
        base.MoveStop();
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public override void AddTarget(Character target)
    {
        base.AddTarget(target);

        if (!target.isDead && !isDead)
        {
            if (!counter.IsRunning && !isMove)
            {
                OnAttack();
            }
        }
    }

    public override void OnAttack()
    {
        base.OnAttack();
        if (target != null && weapon.IsCanAttack)
        {
            counter.Start(Throw, TIME_DELAY_THROW);
            ResetAnim();
            counterOnAttack.Start(OnAttack, 2f);
        }
    }

    public override void RemoveTarget(Character target)
    {
        base.RemoveTarget(target);
        target.SetMask(false);
    }

    public override void MSpeedEffect()
    {
        base.MSpeedEffect();
        this.moveSpeed += (moveSpeed / 3);
        UIManager.Ins.GetUI<UIGamePlay>().StartTimeSpeedSlider();

    }

    private void ResetMSpeed()
    {
        IncrementSpeed = false;
        this.moveSpeed = initSpeed;
    }

    //public override void SelectNearEnemy()
    //{
    //    base.SelectNearEnemy();
    //    counter.Start(Throw, TIME_DELAY_THROW);

    //}

    public override void MDameEffect()
    {
        base.MDameEffect();
        UIManager.Ins.GetUI<UIGamePlay>().StartTimeDamageSlider();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public void TryCloth(UIShop.ShopType shopType, Enum type)
    {
        switch (shopType)
        {
            case UIShop.ShopType.Hat:
                RemoveHat();
                ChangeHat((HatType)type);
                break;

            //case UIShop.ShopType.Pant:
            //    ChangePant((PantType)type);
            //    break;

            //case UIShop.ShopType.Skin:
            //    TakeOffClothes();
            //    skinType = (SkinType)type;
            //    WearClothes();
            //    break;
            case UIShop.ShopType.Weapon:
                if (currentWeapon) SimplePool.Despawn(currentWeapon);
                ChangeWeapon((WeaponType)type);
                break;
            default:
                break;
        }

    }

    public void ChangeHat(HatType hatType)
    {
        if (hatType != HatType.none)
        {
            currentHat = SimplePool.Spawn<Hat>((PoolType)hatType, hatPos);
        }
    }
    public void RemoveHat()
    {
        if (currentHat) SimplePool.Despawn(currentHat);
    }

    public void OnTakeClothsData()
    {
        // take old cloth data
        //color = DataUser.Ins.playerColor;
        //weaponType = UserData.Ins.playerWeapon;
        hatType = DataUser.Ins.playerHat;
        //pantsType = DataUser.Ins.playerPant;
    }
    public void WearClothes()
    {
        ChangeHat(hatType);
        //ChangeColor();
        //ChangePant();
    }

}
