using Scriptable;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Character : GameUnit
{
    public const float ATT_RANGE = 5f;
    public const float TIME_DELAY_THROW = 0.4f;
    public Animator anim;
    private string animName;
    [SerializeField] public Transform rightHand;

    [SerializeField] GameObject mask;

    protected bool isMove;
    [HideInInspector] public bool isDead;
    [SerializeField] protected float health;
    public float Health => health;

    protected float currentHealth;
    protected float currentDamage;
    protected float buffDamage;

    protected int experience;
    [SerializeField] protected int levelCharacter;
    public int LevelCharacter => levelCharacter;

    public List<Character> targets = new List<Character>();
    public Character target;
    private Vector3 targetPoint;

    public Weapon currentWeapon;
    public Weapon weapon => currentWeapon;
    public bool IsCanAttack => weapon.IsCanAttack;
   
    private int point = 0;
    float elapsedTimeDamage = 0;
    private CounterTime couterMDamage;
    [SerializeField] Transform indicatorPoint;
    protected TargetIndicator targetIndicator;

    #region Skin
    [Space(10)]
    [Header("Skin Character")]

    [SerializeField] public ColorSO colorSo;
    public ColorType colorType;
    public SkinnedMeshRenderer colorMeshRenderer;
    [Space(5)]

    [SerializeField] public PantSO pantSO;
    public PantType pantType;
    public SkinnedMeshRenderer pantMeshRenderer;

    public HatType hatType;
    public Transform hatPos;
    [HideInInspector] public Hat currentHat;

    #endregion
    public void Start()
    {

      
    }
    public virtual void OnInit()
    {
        ChangePant(pantType);
        ChangeColor(colorType);
        isDead = false;
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.SetTarget(indicatorPoint);
    }
    public virtual void Update()
    {
        targets.RemoveAll(character => character.isDead);
        if(IncreaseDamage)
        {
            float deltaTime = Time.deltaTime;

            elapsedTimeDamage += deltaTime;
            if (elapsedTimeDamage > Constant.TIME_ALIVE_EFFECT_DAMAGE)
            {
                IncreaseDamage = false;
                elapsedTimeDamage = 0;
            }
        }
    }
    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }
    public void ResetAnim()
    {
        animName = "";
    }

    public virtual void AddTarget(Character target)
    {
        targets.Add(target);
    }

    public virtual void RemoveTarget(Character target)
    {
        targets.Remove(target);
        this.target = null;
    }

    protected virtual void OnHitVictim(Character attacker, Character victim, float damage)
    {
        if (IncreaseDamage)
        {
            currentDamage = damage * 2;
        }else if (!IncreaseDamage)
        {
            currentDamage = damage;
        }

        float onDamage = victim.currentHealth -= currentDamage;
        victim.SetHealthIndication(victim.currentHealth > 0 ? victim.currentHealth : 0, victim.health);
        if (onDamage < 0.1f)
        {
            victim.OnDeath();
            attacker.target = null;
            attacker.plusExp();
            attacker.PlusLevel();
        }
    }


    public void ChangeWeapon(WeaponType weaponType)
    {
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, rightHand);
    }


    #region STATE CHARACTER
    public virtual void MoveStop()
    {

    }
    public virtual Character GetTargetInRange()
    {
        if (targets.Count > 0)
        {
            float disFist = Vector3.Distance(TF.position, targets[0].TF.position);
            for (int i = 0; i < targets.Count; i++)
            {
                float distance = Vector3.Distance(TF.position, targets[i].TF.position);
                if (disFist > distance)
                {
                    disFist = distance;
                    target = targets[i];
                }
                else
                {
                    target = targets[0];
                }
            }
        }
        return target;
    }

    public virtual void OnAttack()
    {
        target = GetTargetInRange();
        if ( target != null && !target.isDead)
        {
            targetPoint = target.TF.position;
            TF.LookAt(targetPoint + (TF.position.y - targetPoint.y) * Vector3.up);
            ChangeAnim(Constant.ANIM_ATTACK);
        }
    }
    public void Throw()
    {
        if (target != null)
        {
            weapon.Throw(this, target.TF.position, OnHitVictim);
        }   
    }
    public void PlusLevel()
    {
        if (this.experience == 105)
        {
            levelCharacter++;
            targetIndicator.SetLevel(levelCharacter);
            experience = 0;
            this.health = this.levelCharacter * 10;
            if (currentHealth < health)
            {
                this.currentHealth = this.currentHealth + 10;
            }
            else
            {
                this.currentHealth = this.health;

            }

        }
        SetHealthIndication(currentHealth, health);

    }

    public virtual void InfluenceMedicine(int id)
    {
        switch (id)
        {
            case 1:
                MHealEffect();
                break;
            case 2:
                MSpeedEffect();
                break;
            case 3:
                MDameEffect();
                break;
            default: break;
        }
    }

    private bool isHealEf = false;

    public void SetHealthIndication(float current, float full)
    {
        targetIndicator.SetHealth(current, full);
    }
    public void MHealEffect()
    {
        this.currentHealth = health;
        SetHealthIndication(currentHealth, health);
        EffectMedicine effect = SimplePool.Spawn<EffectMedicine>(PoolType.EF_Heal);
        effect.OnInit(this.TF);
    }
    protected bool IncrementSpeed = false;
    public virtual void MSpeedEffect()
    {
        IncrementSpeed = true;
        EffectMedicine effect = SimplePool.Spawn<EffectMedicine>(PoolType.EF_Speed);
        effect.OnInit(this.TF);
    }

    private bool IncreaseDamage = false;
    public virtual void MDameEffect()
    {
        IncreaseDamage = true;
        EffectMedicine effect = SimplePool.Spawn<EffectMedicine>(PoolType.EF_Dame);
        effect.OnInit(this.TF);
    }

    public void WearOffMDamage()
    {
        Debug.Log("WearOff");
        IncreaseDamage = false;
    }

    public void plusExp()
    {
        this.experience += 35;
    }
    public void SetMask(bool active)
    {
        mask.SetActive(active);
    }
    #endregion STATE CHARACTER END

    public virtual void SetLevel(int level)
    {
        
    }

    public virtual void OnDeath()
    {
        isDead = true;
        MoveStop();
        ChangeAnim(Constant.ANIM_DEAD);
        LevelManager.Ins.CharacterDeath(this);
   
        Invoke(nameof(OnDespawn), 1.5f);
    }
    public virtual void OnDespawn()
    {
        targets.Clear();
        target = null;
        SetMask(false);
        SimplePool.Despawn(targetIndicator);

    }

    public virtual void ChangePant(PantType pantType)
    {
        this.pantType = pantType;
        pantMeshRenderer.material = pantSO.GetPant(pantType);
    }

    public virtual void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        colorMeshRenderer.material = colorSo.GetColor(colorType);
    }

    public void EquippeHat()
    {

        if (hatType != HatType.none)
        {
            currentHat = SimplePool.Spawn<Hat>((PoolType)hatType, hatPos);
        }
    }

}
