using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WeaponTypeItem
    {
        public GameUnit prefab;
        public Transform prefabParent;
        public int amount;
    }
    public List<WeaponTypeItem> weapons = new List<WeaponTypeItem>();
    public void Awake()
    {
        foreach (WeaponTypeItem item in weapons)
        {
            ObjectPooler.Preload(item.prefab, item.amount, item.prefabParent);
        }
    }

}
