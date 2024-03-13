using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UICanvas
{
    public enum ShopType { Hat, Pant, Accessory, Skin, Weapon }

    [SerializeField] ShopSO data;
    [SerializeField] ShopItem prefab;
    [SerializeField] Transform content;
    [SerializeField] ShopBar[] shopBars;


    [SerializeField] TextMeshProUGUI playerCoinTxt;

    [SerializeField] Button btnBuy;
    [SerializeField] Button btnEquip;
    [SerializeField] Button btnEquiped;
    [SerializeField] TextMeshProUGUI coinTxt;

    MiniPool<ShopItem> miniPool = new MiniPool<ShopItem>();

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

        playerCoinTxt.SetText(DataUser.Ins.coin.ToString());
    }
    public override void CloseDirectly()
    {
        base.CloseDirectly();
        UIManager.Ins.OpenUI<UIMainMenu>();

        LevelManager.Ins.player.RemoveHat();
        //LevelManager.Ins.player.RemovePant();
        //LevelManager.Ins.player.RemoveColor();
        LevelManager.Ins.player.OnTakeClothsData();
        LevelManager.Ins.player.WearClothes();
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
                InitShipItems(data.colors.Ts, ref itemEquiped);
                break;
            default:
                break;
        }

    }
    public ShopItem.State GetState(Enum t)
    {
        return DataUser.Ins.GetEnumData(t.ToString(), ShopItem.State.Buy);
    }
    internal void SelectItem(ShopItem item)
    {
        if (currentItem != null)
        {
            currentItem.SetState(GetState(currentItem.Type));
        }
        currentItem = item;

        switch (currentItem.state)
        {
            case ShopItem.State.Buy:
                btnBuy.gameObject.SetActive(true);
                btnEquip.gameObject.SetActive(false);
                btnEquiped.gameObject.SetActive(false);
                break;
            case ShopItem.State.Bought:
                btnBuy.gameObject.SetActive(false);
                btnEquip.gameObject.SetActive(true);
                btnEquiped.gameObject.SetActive(false);
                break;
            case ShopItem.State.Equipped:
                btnBuy.gameObject.SetActive(false);
                btnEquip.gameObject.SetActive(false);
                btnEquiped.gameObject.SetActive(true);
                break;
            case ShopItem.State.Selecting:
                break;
            default:
                break;
        }
        LevelManager.Ins.player.TryCloth(shopType, currentItem.Type);
        currentItem.SetState(ShopItem.State.Selecting);

        //check data
        coinTxt.text = item.data.cost.ToString();
    }
    private void InitShipItems<T>(List<ShopItemData<T>> items, ref ShopItem itemEquiped) where T : Enum
    {
        for (int i = 0; i < items.Count; i++)
        {
            ShopItem.State state = DataUser.Ins.GetEnumData(items[i].type.ToString(), ShopItem.State.Buy);
            ShopItem item = miniPool.Spawn();
            item.SetData(i, items[i], this);
            item.SetState(state);
            if (state == ShopItem.State.Equipped)
            {
                SelectItem(item);

                itemEquiped = item;
            }
        }
    }

    //private void ResetData(ShopItem item)
    //{

    //    switch (item.state)
    //    {
    //        case ShopItem.State.Buy:
    //            btnBuy.gameObject.SetActive(true);
    //            btnEquip.gameObject.SetActive(false);
    //            btnEquiped.gameObject.SetActive(false);
    //            break;
    //        case ShopItem.State.Bought:
    //            btnBuy.gameObject.SetActive(false);
    //            btnEquip.gameObject.SetActive(true);
    //            btnEquiped.gameObject.SetActive(false);
    //            break;
    //        case ShopItem.State.Equipped:
    //            btnBuy.gameObject.SetActive(false);
    //            btnEquip.gameObject.SetActive(false);
    //            btnEquiped.gameObject.SetActive(true);
    //            break;
    //        case ShopItem.State.Selecting:
    //            break;
    //        default:
    //            break;
    //    }

    //}

    public void BuyButton()
    {
        int coinUser = int.Parse(playerCoinTxt.text);
        int coinBuy = int.Parse(coinTxt.text);
        if (coinUser >= coinBuy)
        {
            DataUser.Ins.SetEnumData(currentItem.Type.ToString(), ShopItem.State.Bought);

            SelectItem(currentItem);
            DataUser.Ins.coin = coinUser - coinBuy;
        }
       
        playerCoinTxt.SetText(DataUser.Ins.coin.ToString());
    }
    public void Update()
    {

    }
    public void EquipButton()
    {
        if (currentItem != null)
        {
            DataUser.Ins.SetEnumData(currentItem.Type.ToString(), ShopItem.State.Equipped);

            switch (shopType)
            {
                case ShopType.Hat:

                    DataUser.Ins.SetEnumData(DataUser.Ins.playerHat.ToString(), ShopItem.State.Bought);
                
                    DataUser.Ins.SetEnumData(DataUser.Key_Player_Hat, ref DataUser.Ins.playerHat, (HatType)currentItem.Type);
                    break;
                case ShopType.Pant:
                    DataUser.Ins.SetEnumData(DataUser.Ins.playerPant.ToString(), ShopItem.State.Bought);
                    DataUser.Ins.SetEnumData(DataUser.Key_Player_Pant, ref DataUser.Ins.playerPant, (PantType)currentItem.Type);
                    break;

                //case ShopType.Skin:
                //    DataUser.Ins.SetEnumData(DataUser.Ins.playerSkin.ToString(), ShopItem.State.Bought);
                //    DataUser.Ins.SetEnumData(DataUser.Key_Player_Skin, ref DataUser.Ins.playerSkin, (SkinType)currentItem.Type);
                //    break;
                case ShopType.Weapon:
                    break;
                default:
                    break;
            }

        }

        if (itemEquiped != null)
        {
            itemEquiped.SetState(ShopItem.State.Bought);
        }

        currentItem.SetState(ShopItem.State.Equipped);
        SelectItem(currentItem);


    }


    //public void ResetDataItem<T>(List<ShopItemData<T>> items) where T : Enum
    //{
    //    for (int i = 0; i < items.Count; i++)
    //    {
    //        DataUser.Ins.SetEnumData(items[i].type.ToString(), ShopItem.State.Buy);
    //        ShopItem item = miniPool.Spawn();
    //        item.SetData(i, items[i], this);
    //        item.SetState(state);
    //        if (state == ShopItem.State.Equipped)
    //        {
    //            SelectItem(item);

    //            itemEquiped = item;
    //        }
    //    }
    //}


}

