//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Attack : MonoBehaviour
//{
//    [SerializeField] private Transform throwPoint;

//    public bool isThrow = false;
//    public Character parent;

//    public void SetValueAttack(bool canAttack)
//    {
//        this.isThrow = canAttack;
//        //if (!parent.isDead)
//        //{
//        //    this.isThrow = canAttack;
//        //}
//        //else
//        //{
//        //    this.isThrow = false;
//        //}

//    }
//    public void Start()
//    {
//        StartCoroutine(ShootWithDelay());
//    }
//    public virtual void Throwing()
//    {
//        parent.weapon.Throw(parent, throwPoint, parent.size);
//    }
//    private IEnumerator ShootWithDelay()
//    {
//        while (true)
//        {
//            if (isThrow)
//            {
//                //StartCoroutine(AnimShootDelay());
//                Throwing();
//                yield return new WaitForSeconds(2.0f);

//                isThrow = false;
//            }
//            yield return null;
//        }
//    }
//    private IEnumerator AnimShootDelay()
//    {
//        parent.ChangeAnim(Constant.ANIM_ATTACK);
//        Throwing();
//        yield return new WaitForSeconds(0.6f);

//        parent.ChangeAnim(Constant.ANIM_IDLE);
//    }
//}
