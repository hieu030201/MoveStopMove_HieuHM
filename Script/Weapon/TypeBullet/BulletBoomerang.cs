using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BulletBoomerang : Bullet
{

    public enum State { Forward, Backward, Stop }

    private State state;

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
        this.target = (target - character.TF.position).normalized * (Character.ATT_RANGE + 1) + character.TF.position;
        state = State.Forward;
    }

    public override void Update()
    {
        base.Update();
        switch (state)
        {
            case State.Forward:
                TF.position = Vector3.MoveTowards(TF.position, this.target, speed * Time.deltaTime);
                if (Vector3.Distance(TF.position, target) < 0.1f)
                {
                    state = State.Backward;
                }
                child.Rotate(Vector3.up * -6, Space.Self);
                break;

            case State.Backward:
                TF.position = Vector3.MoveTowards(TF.position, this.character.TF.position, speed * Time.deltaTime);
                if (character.isDead || Vector3.Distance(TF.position, this.character.TF.position) < 0.1f)
                {
                    OnDespawn();
                }
                child.Rotate(Vector3.up * -6, Space.Self);

                break;
        }
    }
    protected override void Stop()
    {
        base.Stop();
        state = State.Stop;
        Invoke(nameof(OnDespawn), 2f);
    }
}
