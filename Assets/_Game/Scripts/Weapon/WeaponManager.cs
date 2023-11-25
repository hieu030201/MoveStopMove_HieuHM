using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Hammer hammer01;
    public Hammer hammer02;
    public Hammer hammer03;
    public Candy candy01;
    public Candy candy02;
    public Knife knife01;
    public Knife knife02;
    public Boomerang boomerang01;
    public Boomerang boomerang02;

    public void Fire(Character owner, Transform spawnPoint, Vector3 newScale)
    {
        switch (owner.equipedWeapon)
        {
            case WeaponType.Hammer_ID01:
                ObjectPooler.Spawn<Hammer>(hammer01, spawnPoint.position, spawnPoint.rotation, newScale).OnInit(owner);
                break;
            case WeaponType.Hammer_ID02:
                ObjectPooler.Spawn<Hammer>(hammer02, spawnPoint.position, spawnPoint.rotation, newScale).OnInit(owner);
                break;
            case WeaponType.Hammer_ID03:
                ObjectPooler.Spawn<Hammer>(hammer03, spawnPoint.position, spawnPoint.rotation, newScale).OnInit(owner);
                break;
            case WeaponType.Candy_ID01:
                ObjectPooler.Spawn<Candy>(candy01, spawnPoint.position, spawnPoint.rotation, newScale).OnInit(owner);
                break;
            case WeaponType.Candy_ID02:
                ObjectPooler.Spawn<Candy>(candy02, spawnPoint.position, spawnPoint.rotation, newScale).OnInit(owner);
                break;
            case WeaponType.Knife_ID01:
                ObjectPooler.Spawn<Knife>(knife01, spawnPoint.position, spawnPoint.rotation , newScale).OnInit(owner);
                break;
            case WeaponType.Knife_ID02:
                ObjectPooler.Spawn<Knife>(knife02, spawnPoint.position, spawnPoint.rotation, newScale).OnInit(owner);
                break;
            case WeaponType.Boomerang_ID01:
                ObjectPooler.Spawn<Boomerang>(boomerang01, spawnPoint.position, spawnPoint.rotation, newScale).OnInit(owner);
                break;
            case WeaponType.Boomerang_ID02:
                ObjectPooler.Spawn<Boomerang>(boomerang02, spawnPoint.position, spawnPoint.rotation, newScale).OnInit(owner);
                break;
        }
    }
}
