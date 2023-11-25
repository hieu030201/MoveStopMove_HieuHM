using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Scriptable;

[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/ShopData")]
public class ShopData : ScriptableObject
{
    public ShopItemDatas<HatType> hats;
    public ShopItemDatas<PantsType> pants;
    public ShopItemDatas<ColorType> skins;
}

[System.Serializable]
public class ShopItemDatas<T> where T : System.Enum
{
    [SerializeField] List<ShopItemData<T>> ts;
    public List<ShopItemData<T>> Ts => ts;

    public ShopItemData<T> GetHat(T t)
    {
        return ts.Single(q => q.type.Equals(t));
    }

}

[System.Serializable]
public class ShopItemData<T> : ShopItemData where T : System.Enum
{
    public T type;
}

public class ShopItemData
{
    public Sprite icon;
    public int cost;
    public int ads;
}