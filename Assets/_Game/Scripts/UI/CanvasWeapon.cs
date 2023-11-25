using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeapon : UICanvas
{
    public Transform weaponpoint;

    [SerializeField] TextMeshProUGUI playerCoinTxt;
    [SerializeField] WeaponData weaponData;
    [SerializeField] ButtonState buttonState;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] Image imageIcon;

    private WeaponType weaponType;

    public override void Setup()
    {
        base.Setup();
        ChangeWeapon(UserData.Ins.playerWeapon);

        playerCoinTxt.SetText(UserData.Ins.coin.ToString());
    }
    public override void CloseDirectly()
    {
        base.CloseDirectly();
        LevelManager.Ins.player.WeaponEquipeMoment();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
    }

    public void NextButton()
    {
        ChangeWeapon(weaponData.NextType(weaponType));
    }

    public void PrevButton()
    {
        ChangeWeapon(weaponData.PrevType(weaponType));
    }

    public void BuyButton()
    {
        int coinUser = int.Parse(playerCoinTxt.text);
        int coinBuy = int.Parse(coinTxt.text);
        if (coinUser > coinBuy)
        {
            UserData.Ins.SetEnumData(weaponType.ToString(), ShopItem.State.Bought);
            ChangeWeapon(weaponType);
        }
        UserData.Ins.coin = coinUser - coinBuy;
        playerCoinTxt.SetText(UserData.Ins.coin.ToString());
    }
    public void EquipeButton()
    {
        UserData.Ins.SetEnumData(weaponType.ToString(), ShopItem.State.Equipped);
        UserData.Ins.SetEnumData(UserData.Ins.playerWeapon.ToString(), ShopItem.State.Bought);
        UserData.Ins.SetEnumData(UserData.Key_Player_Weapon, ref UserData.Ins.playerWeapon, weaponType);
        ChangeWeapon(weaponType);
        LevelManager.Ins.player.TryCloth(CanvasSkinShop.ShopType.Weapon, weaponType);
    }
    public void ChangeWeapon(WeaponType weaponType)
    {
        this.weaponType = weaponType;

        ButtonState.State state = (ButtonState.State)UserData.Ins.GetDataState(weaponType.ToString(), 0);
        buttonState.SetState(state);

        WeaponItem item = weaponData.GetWeaponItem(weaponType);
        imageIcon.sprite = item.icon;
        nameTxt.SetText(item.name);
        coinTxt.text = item.cost.ToString();
    }
}
