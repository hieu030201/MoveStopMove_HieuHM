using Scriptable;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CanvasSkinShop : UICanvas
{
    public enum ShopType { Hat, Pant, Accessory, Skin, Weapon}

    [SerializeField] ShopData data;
    [SerializeField] ShopItem prefab;
    [SerializeField] Transform content;
    [SerializeField] ShopBar[] shopBars;

    [SerializeField] TextMeshProUGUI playerCoinTxt;

    MiniPool<ShopItem> miniPool = new MiniPool<ShopItem>();
    [SerializeField] ButtonState buttonState;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] Text adsTxt;


    private ShopItem currentItem;
    private ShopBar currentBar;
    private ShopItem itemEquiped;
    public ShopType shopType => currentBar.Type;


    private void Awake()
    {
        miniPool.OnInit(prefab, 10, content);
        for (int i = 0; i < shopBars.Length; i++)
        {
            shopBars[i].SetShop(this);
        }
    }

    public override void Open()
    {
        base.Open();
        SelectBar(shopBars[0]);

        playerCoinTxt.SetText(UserData.Ins.coin.ToString());
    }
    public override void CloseDirectly()
    {
        base.CloseDirectly();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
        LevelManager.Ins.player.OnTakeClothsData();
    }


    internal void SelectBar(ShopBar shopBar)
    {
        if (currentBar != null)
        {
            currentBar.Active(false);
        }

        currentBar = shopBar;
        currentBar.Active(true);
        miniPool.Collect();
        itemEquiped = null;

        switch (currentBar.Type)
        {
            case ShopType.Hat:
                InitShipItems(data.hats.Ts, ref itemEquiped);
                break;
            case ShopType.Pant:
                InitShipItems(data.pants.Ts, ref itemEquiped);
                break;
            case ShopType.Skin:
                InitShipItems(data.skins.Ts, ref itemEquiped);
                    break;
        }
    }

    private void InitShipItems<T>(List<ShopItemData<T>> items, ref ShopItem itemEquiped) where T : Enum
    {
        for(int i= 0; i < items.Count; i++)
        {
            ShopItem.State state = UserData.Ins.GetEnumData(items[i].type.ToString(), ShopItem.State.Buy);
            ShopItem item = miniPool.Spawn();
            item.SetData(i, items[i], this);
            item.SetState(state);

            if(state == ShopItem.State.Equipped)
            {
                SelectItem(item);
                itemEquiped = item;
            }
        }
    }
    public ShopItem.State GetState(Enum t) => UserData.Ins.GetEnumData(t.ToString(), ShopItem.State.Buy);

    internal void SelectItem(ShopItem item)
    {
        if(currentItem != null)
        {
            currentItem.SetState(GetState(currentItem.Type));
        }
        currentItem = item;

        switch (currentItem.state)
        {
            case ShopItem.State.Buy:
                buttonState.SetState(ButtonState.State.Buy);
                break;
            case ShopItem.State.Bought:
                buttonState.SetState(ButtonState.State.Equip);
                break;
            case ShopItem.State.Equipped:
                buttonState.SetState(ButtonState.State.Equiped);
                break;
            case ShopItem.State.Selecting:
                break;
            default: 
                break;
        }
        LevelManager.Ins.player.TryCloth(shopType, currentItem.Type);
        currentItem.SetState(ShopItem.State.Selecting);
        coinTxt.text = item.data.cost.ToString();
    }

    public void BuyButton()
    {
        int coinUser = int.Parse(playerCoinTxt.text);
        int coinBuy = int.Parse(coinTxt.text);
        if(coinUser > coinBuy)
        {
            UserData.Ins.SetEnumData(currentItem.Type.ToString(), ShopItem.State.Bought);
            SelectItem(currentItem);
        }
        UserData.Ins.coin = coinUser - coinBuy;
        playerCoinTxt.SetText(UserData.Ins.coin.ToString());
    }

    public void EquipButton()
    {
        if (currentItem != null)
        {
            UserData.Ins.SetEnumData(currentItem.Type.ToString(), ShopItem.State.Equipped);

            switch (shopType)
            {
                case ShopType.Hat:
                    //reset trang thai do dang deo ve bought
                    UserData.Ins.SetEnumData(UserData.Ins.playerHat.ToString(), ShopItem.State.Bought);
                    Debug.Log(UserData.Ins.playerHat.ToString());
                    //save id do moi vao player
                    UserData.Ins.SetEnumData(UserData.Key_Player_Hat, ref UserData.Ins.playerHat, (HatType)currentItem.Type);
                    break;
                case ShopType.Pant:
                    UserData.Ins.SetEnumData(UserData.Ins.playerPant.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.Key_Player_Pant, ref UserData.Ins.playerPant, (PantsType)currentItem.Type);
                    break;
                case ShopType.Skin:
                    UserData.Ins.SetEnumData(UserData.Ins.playerColor.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.Key_Player_Color, ref UserData.Ins.playerColor, (ColorType)currentItem.Type);
                    break;
                case ShopType.Weapon:
                    break;
                default:
                    break;
            }
        }

        if(itemEquiped != null)
        {
            itemEquiped.SetState(ShopItem.State.Bought);
        }

        currentItem.SetState(ShopItem.State.Equipped);
        SelectItem(currentItem);
    }
}
