using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform throwPoint;
    [SerializeField] private Animator parentAnimator;
    public bool isThrow = false;
    public WeaponManager weaponManager;
    public Character character;

    public void SetValueAttack(bool canAttack)
    {
        if (!character.isDead)
        {
            this.isThrow = canAttack;
        }
        else
        {
            this.isThrow = false;
        }

    }
    public void OnInit()
    {
        StartCoroutine(ShootWithDelay());
    }

    public virtual void Throwing()
    {

        Vector3 scaleWeapon = character.transform.localScale;
        weaponManager.Fire(character, throwPoint, scaleWeapon);
    }

    private IEnumerator ShootWithDelay()
    {
        while (true)
        {
            if (isThrow)
            {
                StartCoroutine(AnimShootDelay());
                yield return new WaitForSeconds(2f);

                isThrow = false;
            }
            yield return null;
        }
    }
    private IEnumerator AnimShootDelay()
    {
        parentAnimator.SetBool(Constant.ANIM_ATTACK, true);
        Throwing();
        yield return new WaitForSeconds(0.6f);
        parentAnimator.SetBool(Constant.ANIM_ATTACK, false);
    }

}
