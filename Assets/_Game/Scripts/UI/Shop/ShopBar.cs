using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBar : MonoBehaviour
{
    [SerializeField] GameObject bg;
    [SerializeField] CanvasSkinShop.ShopType type;
    public CanvasSkinShop.ShopType Type => type;

    CanvasSkinShop shop;

    public void SetShop(CanvasSkinShop shop)
    {
        this.shop = shop;
    }

    public void Select()
    {
        shop.SelectBar(this);
    }

    public void Active(bool active)
    {
        bg.SetActive(!active);
    }
}
