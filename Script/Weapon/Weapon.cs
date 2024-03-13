using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class Weapon : GameUnit
{
    [SerializeField] GameObject child;
    [SerializeField] BulletType bulletType;
    public bool IsCanAttack => child.activeSelf;

    public void SetActive(bool active)
    {
        child.SetActive(active);
    }

    public virtual void OnEnable()
    {
        SetActive(true);
    }
    public void Throw(Character character, Vector3 targetPoint, Action<Character, Character, float> onHit)
    {
        Vector3 spawnPoint = new Vector3(character.TF.position.x, character.TF.position.y + 1f, character.TF.position.z);
        Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, spawnPoint, Quaternion.identity);
        bullet.OnInit(character, targetPoint);
        bullet.OnHit(character, onHit);
        SetActive(false);
        Invoke(nameof(OnEnable), Constant.TIME_WEAPON_RELOAD);
    }
}
