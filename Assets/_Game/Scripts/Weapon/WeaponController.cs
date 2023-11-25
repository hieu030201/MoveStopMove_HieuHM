using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public enum WeaponType
{
    Hammer_ID01 = 0,
    Hammer_ID02 = 1,
    Hammer_ID03 = 2,
    Candy_ID01 = 3,
    Candy_ID02 = 4,
    Knife_ID01 = 5,
    Knife_ID02 = 6,
    Boomerang_ID01 = 7,
    Boomerang_ID02 = 8,
}
public class WeaponController : MonoBehaviour
{
    //[System.Serializable]
    //public class WeaponTypeItem
    //{
    //    public WeaponType weapontype;
    //    public GameObject prefab;
    //}
    //public List<WeaponTypeItem> weapons = new List<WeaponTypeItem>();
    //public Dictionary<WeaponType, GameObject> dicWeapon = new Dictionary<WeaponType, GameObject>();
    //public void Start()
    //{
    //    foreach (WeaponTypeItem item in weapons)
    //    {
    //        dicWeapon[item.weapontype] = item.prefab;
    //    }
    //}
    //void Update()
    //{

    //}
    //public GameObject ChangeWeapon(WeaponType weaponTypeCurrent)
    //{
    //   return dicWeapon[weaponTypeCurrent];
    //}
}
