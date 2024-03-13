using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : UICanvas
{
    public Transform weaponPoint;
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI coinTxt;

    [SerializeField] TextMeshProUGUI playerCoinTxt;

    [SerializeField] Button buy;
    [SerializeField] Button equipe;
    [SerializeField] Button equiped;

    private Weapon currentWeapon;
    private WeaponType weaponType;
    private WeaponType weaponEquipe;


    public override void Setup()
    {
        base.Setup();
        weaponEquipe = WeaponType.W_Hammer_1;
        PlayerPrefs.SetInt("W_Hammer_1", 3);
        ChangeWeapon(WeaponType.W_Hammer_1);
        playerCoinTxt.SetText(DataUser.Ins.coin.ToString());
    }

    public override void CloseDirectly()
    {
        base.CloseDirectly();
        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
            currentWeapon = null;
        }
        UIManager.Ins.OpenUI<UIMainMenu>();
    }
    public void NextButton()
    {
        ChangeWeapon(weaponSO.NextType(weaponType));
    }

    public void PrevButton()
    {
        ChangeWeapon(weaponSO.PrevType(weaponType));
    }
    public void BuyButton()
    {
        int coinUser = int.Parse(playerCoinTxt.text);
        int coinBuy = int.Parse(coinTxt.text);
        if (coinUser >= coinBuy)
        {
            PlayerPrefs.SetInt(weaponType.ToString(), 2);
            ChangeWeapon(weaponType);
            DataUser.Ins.coin = coinUser - coinBuy;
        }
  
        playerCoinTxt.SetText(DataUser.Ins.coin.ToString());
    }

    public void EquipButton()
    {

        weaponEquipe = weaponType;
        PlayerPrefs.SetInt(weaponType.ToString(), 3);
        ChangeWeapon(weaponType);
        LevelManager.Ins.player.TryCloth(UIShop.ShopType.Weapon, weaponType);
    }


    public void ChangeWeapon(WeaponType weaponType)
    {
        if (weaponType != weaponEquipe)
        {
            if (PlayerPrefs.GetInt(weaponType.ToString()) != 1)
            {
                PlayerPrefs.SetInt(weaponType.ToString(), 2);
            }
        }
 
        this.weaponType = weaponType;


        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
        }

        //Button state start

        if (2 == PlayerPrefs.GetInt(weaponType.ToString(), 2))
        {
            equipe.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
            equiped.gameObject.SetActive(false);
        }
        else if(1 == PlayerPrefs.GetInt(weaponType.ToString(), 1))
        {
            equipe.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            equiped.gameObject.SetActive(false);
        }
        else if(3 == PlayerPrefs.GetInt(weaponType.ToString(), 3))
        {
            equipe.gameObject.SetActive(false);
            buy.gameObject.SetActive(false);
            equiped.gameObject.SetActive(true);
        }
        // button state end

        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, Vector3.zero, Quaternion.identity, weaponPoint);
        WeaponItem item = weaponSO.GetWeaponItem(weaponType);
        nameTxt.SetText(item.name);
        coinTxt.text = item.cost.ToString();
    }

    public void DeleteDataWeapon()
    {
       
        for(int i = 1 ; i< Enum.GetValues(typeof(WeaponType)).Length; i++)
        {
            PlayerPrefs.SetInt(((WeaponType)i).ToString(), 1);

        }
        Setup();
    }
}
