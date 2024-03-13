using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletHammer : Bullet
{
    CounterTime counterTime = new CounterTime();
    [SerializeField] Transform child;
    public override void OnEnable()
    {
        base.OnEnable();
        this.damage = 7;
        this.speed = 5.0f;
    }

    public override void OnInit(Character character, Vector3 target)
    {
        base.OnInit(character, target);
        counterTime.Start(OnDespawn, Constant.TIME_ALIVE_HAMMER);
    }

    public override void Update()
    {
        base.Update();
        counterTime.Execute();
        TF.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        child.Rotate(Vector3.up * -6, Space.Self);
    }
}
