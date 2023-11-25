using Cinemachine;
using Scriptable;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class Player : Character
{
    public Vector3 desiredPosition = Vector3.zero;
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    private Vector3 _moveVector;
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

    public override void OnInit()
    {
        base.OnInit();
        gameObject.SetActive(true);
        tf.position = Vector3.zero;
        tf.rotation = Quaternion.Euler(Vector3.up * 180);
        levelCharacter = 0;
        OnTakeClothsData();
        attack.OnInit();
        //equipedWeapon = UserData.Ins.playerWeapon;
    }

    public void SetName()
    {
        characterName = PlayerPrefs.GetString("playerName", "Player");
    }
    public override void Update()
    {
        base.Update();
        if (GameManager.IsState(GameState.GamePlay))
        {
            Move();
        }
    }

    public void WeaponEquipeMoment()
    {
        equipedWeapon = UserData.Ins.playerWeapon;
        SyncWeapon();
        ResetHand();
        ChangeWeaponHand(weaponHand);
        ChangeWeaponHand(weaponHand);
    }
 
    public void TryCloth(CanvasSkinShop.ShopType shopType, Enum type)
    {
        switch (shopType)
        {
            case CanvasSkinShop.ShopType.Hat:
                ResetHat();
                ChangeHat((HatType)type);
                break;

            case CanvasSkinShop.ShopType.Pant:
                ChangePant((PantsType)type);
                break;
            case CanvasSkinShop.ShopType.Skin:
                ChangeColor((ColorType)type);
                break;
            
            default:
                break;
        }

    }
    public void SyncWeapon()
    {
        switch (equipedWeapon)
        {
            case WeaponType.Hammer_ID01:
                weaponHand = WeaponHandType.None;
                break;
            case WeaponType.Hammer_ID02:
                weaponHand = WeaponHandType.Hammer_ID02;
                break;
            case WeaponType.Hammer_ID03:
                weaponHand = WeaponHandType.Hammer_ID03;
                break;
            case WeaponType.Candy_ID01:
                weaponHand = WeaponHandType.Candy_ID01;
                break;
            case WeaponType.Candy_ID02:
                weaponHand = WeaponHandType.Candy_ID02;
                break;
            case WeaponType.Knife_ID01:
                weaponHand = WeaponHandType.Knife_ID01;
                break;
            case WeaponType.Knife_ID02:
                weaponHand = WeaponHandType.Knife_ID02;
                break;
            case WeaponType.Boomerang_ID01:
                weaponHand = WeaponHandType.Boomerang_ID01;
                break;
            case WeaponType.Boomerang_ID02:
                weaponHand = WeaponHandType.Boomerang_ID02;
                break;
        }
        //equipedWeapon = WeaponType.Hammer_ID01;
    }

    public virtual void ResetHat()
    {
        foreach (Transform item in hatPos)
        {
            Destroy(item.gameObject);
        }
    }
    public virtual void ResetHand()
    {
        foreach (Transform item in weaponPos)
        {
            Destroy(item.gameObject);
        }
    }



    public override void SelectNearEnemy()
    {
        if (attackRange.CheckInRangeCharacter() != null)
        {
            Vector3 targetEnemy = attackRange.CheckInRangeCharacter().tf.position;
            DirectAttack(targetEnemy);
            if (anim.GetBool(Constant.ANIM_IDLE))
            {
                attack.SetValueAttack(true);
            }
        }
        else if(attackRange.CheckInRangeCharacter() == null)
        {
            return;
        }
    }

    public override void CharacterDie()
    {
        base.CharacterDie();
        gameObject.SetActive(false);
    }

    public void IncreaseLevel(int enemyLevel)
    {
        if (enemyLevel <= 2)
        {
            levelCharacter += 1;
        }
        else if (enemyLevel <= 7)
        {

            levelCharacter += 2;
        }
        else if (enemyLevel <= 16)
        {
            levelCharacter += 3;
        }
        else if (levelCharacter <= 24)
        {
            levelCharacter += 5;
        }
        else if (levelCharacter <= 32)
        {
            levelCharacter += 6;
        }
        else
        {
            levelCharacter += 7;
        }
    }


    private void Move()
    {
        if (!isDead)
        {
            _moveVector = Vector3.zero;
            _moveVector.x = Joystick.Horizontal * moveSpeed * Time.deltaTime;
            _moveVector.z = Joystick.Vertical * moveSpeed * Time.deltaTime;

            if (Joystick.Horizontal != 0 || Joystick.Vertical != 0)
            {
                Vector3 direction = Vector3.RotateTowards(tf.forward, _moveVector, rotateSpeed * Time.deltaTime, 0.0f);
                tf.rotation = Quaternion.LookRotation(direction);
                anim.SetBool(Constant.ANIM_IDLE, false);
            }
            else if (Joystick.Horizontal == 0 && Joystick.Vertical == 0)
            {
                anim.SetBool(Constant.ANIM_IDLE, true);
            }
            this.tf.position += _moveVector;
        }

    }

    internal void OnTakeClothsData()
    {
        // take old cloth data
        color = UserData.Ins.playerColor;
        //weaponType = UserData.Ins.playerWeapon;
        hatType = UserData.Ins.playerHat;
        pantsType = UserData.Ins.playerPant;
    }
}
