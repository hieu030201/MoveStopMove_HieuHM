using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCandy : Bullet
{
    CounterTime counterTime = new CounterTime();
    public override void OnEnable()
    {
        base.OnEnable();
        this.damage = 7;
        this.speed = 5.0f;
    }

    public override void OnInit(Character character, Vector3 target)
    {
        base.OnInit(character, target);
        counterTime.Start(OnDespawn, Constant.TIME_ALIVE);
    }

    public override void Update()
    {
        base.Update();
        counterTime.Execute();
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
}
