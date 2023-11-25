using UnityEngine;
using Scriptable;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;

public abstract class Character : GameUnit, IDamage
{
    public const float ATTACK_RANGE = 5f;
    public Animator anim;
    public Player player;

    [SerializeField] public SpriteRenderer selectEnemy;
    #region
    public WeaponType equipedWeapon;
    public const float TIME_DELAY_THROW = 0.4f;
    //protected GameObject currentWeaponInHand;
    //protected GameObject lastWeaponInHand;
    #endregion

    [SerializeField] private Character target;
    public void SetTarget(Character target)
    {
        this.target = target;
    }


    #region ATTACK COMPONENT
    [SerializeField] protected RangeAtt attackRange;
    [SerializeField] protected Attack attack;

    [HideInInspector] public Character currentAttacker;
    public int levelCharacter;

    [HideInInspector] public bool isDead;
    public string characterName;
    #endregion

    [Space(20)]
    [Header("Skin Character")]
    #region SKILL COMPONENT
    [SerializeField] public ColorData colorData;
    public ColorType color;
    [SerializeField] public SkinnedMeshRenderer colorMeshRenderer;
    [Space(10)]
    [SerializeField] public WeaponHandData weaponHandData;
    public WeaponHandType weaponHand;
    public Transform weaponPos;
    [Space(10)]
    [SerializeField] public PantsData pantsData;
    public PantsType pantsType;
    public SkinnedMeshRenderer pantsMeshRenderer;
    [Space(10)]
    [SerializeField] public HatData hatData;
    public HatType hatType;
    public Transform hatPos;

    #endregion

    public void Start()
    {
        ChangeWeaponHand(weaponHand);
        ChangePant(pantsType);
        ChangeColor(color);
        ChangeHat(hatType);
    }
    public virtual void OnInit()
    {
        isDead = false;

    }
    public virtual void Update()
    {
        SelectNearEnemy();
        IncreaseSizeByLv();
    }
    public abstract void SelectNearEnemy();

    public void DirectAttack(Vector3 enemyPos)
    {
        if (enemyPos == null) return;
        if (!anim.GetBool(Constant.ANIM_IDLE)) return;
        Vector3 directionToTarget = enemyPos - tf.position;
        directionToTarget.y = 0;
        Quaternion rotation = Quaternion.LookRotation(directionToTarget);
        Quaternion begin = Quaternion.Euler(0, tf.eulerAngles.y, 0);
        Quaternion target = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        tf.rotation = Quaternion.Slerp(begin, target, 1f);
    }
    public bool IsTargetInRange()
    {
        if (attackRange.CheckInRangeCharacter() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void Damage(Character attacker, Character GetHit)
    {
        currentAttacker = attacker;
        Character getHit = GetHit;
        if (getHit != null)
        {
            getHit.Die();
            if (attacker == player && player.levelCharacter < Constant.MAX_LV_CHARACTER)
            {
                player.IncreaseLevel(getHit.levelCharacter);
            }
        }
    }

    public void IncreaseSizeByLv()
    {
        if (levelCharacter < 2)
        {
            tf.localScale = new Vector3(1, 1, 1);
        }
        else if (levelCharacter <= 5)
        {
            tf.localScale = new Vector3(Constant.SCALE_1ST, Constant.SCALE_1ST, Constant.SCALE_1ST);
        }
        else if (levelCharacter <= 15)
        {
            tf.localScale = new Vector3(Constant.SCALE_2ND, Constant.SCALE_2ND, Constant.SCALE_2ND);
        }
        else if (levelCharacter <= 25)
        {
            tf.localScale = new Vector3(Constant.SCALE_3RD, Constant.SCALE_3RD, Constant.SCALE_3RD);
        }
        else if (levelCharacter <= 35)
        {
            tf.localScale = new Vector3(Constant.SCALE_4TH, Constant.SCALE_4TH, Constant.SCALE_4TH);
        }
    }
    public virtual void CheckDie()
    {
        if (!isDead)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        isDead = true;
        selectEnemy.enabled = false;
        anim.SetBool(Constant.ANIM_DEAD, true);
        LevelManager.Ins.CharacterDeath(this);
        StartCoroutine(BeforeDestroyObj());
    }

    private IEnumerator BeforeDestroyObj()
    {
        yield return new WaitForSeconds(1.5f);
        CharacterDie();
    }
    public virtual void CharacterDie() { }
    public virtual void Hit()
    {

    }

    #region SKIN

    public virtual void ChangeColor(ColorType colorType)
    {
        color = colorType;
        colorMeshRenderer.material = colorData.GetMat(colorType);
    }

    public virtual void ChangeWeaponHand(WeaponHandType weaponHandType)
    {
        this.weaponHand = weaponHandType;
        GameObject weaponHandGameObject = weaponHandData.GetWeaponHand(weaponHandType);
        Instantiate(weaponHandGameObject, weaponPos);
    }

    public virtual void ChangeHat(HatType hatDataType)
    {
        this.hatType = hatDataType;
        GameObject hatGameObject = hatData.GetHat(hatDataType);
        Instantiate(hatGameObject, hatPos);
    }

    public virtual void ChangePant(PantsType pantsType)
    {
        this.pantsType = pantsType;
        pantsMeshRenderer.material = pantsData.GetPants(pantsType);
    }
    #endregion

}
