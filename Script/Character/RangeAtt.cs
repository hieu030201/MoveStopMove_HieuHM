using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeAtt : MonoBehaviour
{

    [SerializeField] Character parent;

    private void OnTriggerEnter(Collider other)
    {
        Character c = Cache.GetCollectCharacter(other);
        if (other.CompareTag(Constant.TAG_CHARACTER) && parent != c)
        {
            parent.AddTarget(c);
            if(parent is Player && c is Enemy)
            {
                c.SetMask(true);
            }

            if (c.isDead)
            {
                parent.RemoveTarget(c);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character c = Cache.GetCollectCharacter(other);
        if (other.CompareTag(Constant.TAG_CHARACTER) && parent != c)
        {
            parent.RemoveTarget(c);
            if (parent is Player && c is Enemy)
            {
                c.SetMask(false);
            }

        }
    }
    //public void GetNearestEnemy()
    //{
    //    enemyList.RemoveAll(enemy => enemy.isDead);
    //    enemyList = enemyList.OrderBy(enemy => Vector3.Distance(parent.TF.position, enemy.TF.position)).ToList();
    //    if (enemyList.Count > 0)
    //    {
    //        float min = Vector3.Distance(parent.TF.position, enemyList[0].TF.position);

    //        if (enemyList[0] is Enemy && parent is Player)
    //        {
    //            enemyList[0].SetMask(true);
    //            for (int i = 1; i < enemyList.Count; i++)
    //            {
    //                enemyList[i].SetMask(false);
    //            }
    //        }
      
    //    }
    //}

        //[SerializeField] private Character parent;
        //public Character enemyTarget;

        //public List<Character> enemyList = new List<Character>();

        //private void OnTriggerEnter(Collider other)
        //{
        //    Character c = Cache.GetCollectCharacter(other);
        //    if (other.CompareTag(Constant.TAG_CHARACTER) && parent != c & !c.isDead)
        //    {
        //        enemyList.Add(c);
        //    }
        //}

        //private void OnTriggerExit(Collider other)
        //{
        //    Character c = Cache.GetCollectCharacter(other);
        //    if (other.CompareTag(Constant.TAG_CHARACTER) && parent != c)
        //    {
        //        enemyList.Remove(c);
        //        c.cicleSelect.SetActive(false);
        //        enemyTarget = null;
        //    }

        //}
        //public Character GetNearestEnemy()
        //{
        //    //if(enemyList.Count > 0)
        //    //{
        //    //    float min = Vector3.Distance(parent.transform.position, enemyList[0].transform.position);

        //    //    for(int i = 0; i < enemyList.Count; i++)
        //    //    {
        //    //        float targetTemp = Vector3.Distance(parent.transform.position, enemyList[i].transform.position);

        //    //        if(targetTemp < min)
        //    //        {
        //    //            this.enemyTarget = enemyList[i];
        //    //        }
        //    //        else
        //    //        {
        //    //            this.enemyTarget = enemyList[0];
        //    //        }
        //    //    }
        //    //    return this.enemyTarget;

        //    //}

        //    // Loại bỏ những kẻ chết khỏi danh sách
        //    enemyList.RemoveAll(enemy => enemy.isDead);

        //    // Sắp xếp danh sách dựa trên khoảng cách tới cha
        //    enemyList = enemyList.OrderBy(enemy => Vector3.Distance(parent.transform.position, enemy.transform.position)).ToList();

        //    // Lấy phần tử đầu tiên (gần nhất)
        //    if (enemyList.Count > 0)
        //    {   
        //        if (enemyList[0] is Enemy && parent is Player)
        //        {
        //            enemyList[0].cicleSelect.SetActive(true);
        //        }

        //        for (int i = 1; i < enemyList.Count; i++)
        //        {
        //            enemyList[i].cicleSelect.SetActive(false);
        //        }

        //        this.enemyTarget = enemyList[0];
        //        return this.enemyTarget;
        //    }

        //    return null; // Trả về null nếu danh sách rỗng
        //}

}
