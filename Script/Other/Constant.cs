using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : MonoBehaviour
{
    public const string TAG_CHARACTER = "Character";
    public const string ANIM_IDLE = "idle";
    public const string ANIM_WIN = "win";
    public const string ANIM_RUN = "run";
    public const string ANIM_ATTACK = "attack";
    public const string ANIM_DEAD = "die";
    public const string ANIM_DANCE = "dance";
    public const string ANIM_DANCE_SKIN = "dance-skin";
    public const string ANIM_ULTI = "ulti";
    public const float TIME_WEAPON_RELOAD = 0.5f;

    public const string TAG_OBSTACLE = "Obstacle";
    public const float TIME_ALIVE = 1f;
    public const float TIME_ALIVE_HAMMER = 1.3f;

    public const float TIME_ALIVE_EFFECT_HEAL = 5.0f;
    public const float TIME_ALIVE_EFFECT_DAMAGE = 25.0f;
    public const float TIME_ALIVE_EFFECT_SPEED = 25.0f;

}
public enum BulletType
{
    B_Hammer_1 = PoolType.B_Hammer_1,
    B_Hammer_2 = PoolType.B_Hammer_2,
    B_Hammer_3 = PoolType.B_Hammer_3,
    B_Candy_1 = PoolType.B_Candy_1,
    B_Candy_2 = PoolType.B_Candy_2,
    B_Candy_3 = PoolType.B_Candy_3,
    B_Boomerang_1 = PoolType.B_Boomerang_1,
    B_Boomerang_2 = PoolType.B_Boomerang_2,
}

public enum WeaponType
{
    W_Hammer_1 = PoolType.W_Hammer_1,
    W_Hammer_2 = PoolType.W_Hammer_2,
    W_Hammer_3 = PoolType.W_Hammer_3,
    W_Candy_1 = PoolType.W_Candy_1,
    W_Candy_2 = PoolType.W_Candy_2,
    W_Candy_3 = PoolType.W_Candy_3,
    W_Boomerang_1 = PoolType.W_Boomerang_1,
    W_Boomerang_2 = PoolType.W_Boomerang_2,
}

public enum HatType
{
    none = 0,
    Arrow = PoolType.HAT_Arrow,
    Cap = PoolType.HAT_Cap,
    Cowboy = PoolType.HAT_Cowboy,
    Crown = PoolType.HAT_Crown,
    Ear = PoolType.HAT_Ear,
    StrawHat = PoolType.HAT_StrawHat,
    Headpone = PoolType.HAT_Headphone,
    Horn = PoolType.HAT_Horn,
    Police = PoolType.HAT_Police,
}

public enum MedicineType
{
    MC_Heal = PoolType.MC_Heal,
    MC_Speed = PoolType.MC_Speed,
    MC_Dame = PoolType.MC_Dame,
}
