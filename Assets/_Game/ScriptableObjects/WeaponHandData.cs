using Scriptable;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    public enum WeaponHandType
    {
        None = 0,
        Hammer_ID02 = 1,
        Hammer_ID03 = 2,
        Candy_ID01 = 3,
        Candy_ID02 = 4,
        Knife_ID01 = 5,
        Knife_ID02 = 6,
        Boomerang_ID01 = 7,
        Boomerang_ID02 = 8,
    }
}

[CreateAssetMenu(fileName = "WeaponDataHand", menuName = "ScriptableObjects/WeaponDataHand", order = 1)]
public class WeaponHandData : ScriptableObject
{

    [SerializeField] GameObject[] weaponHand;

    public GameObject GetWeaponHand(WeaponHandType weaponHandType)
    {
        return weaponHand[(int)weaponHandType];
    }
}
