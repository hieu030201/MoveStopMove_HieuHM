//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class ListBot : Singleton<ListBot>
//{
//    [SerializeField] private Enemy enemy;
//    [SerializeField] private Transform poolParent;
//    private int enemyInMap;
//    private int numberEnemyActive;
//    public Transform _player;
//    public List<Enemy> enemyList = new List<Enemy>();
//    [SerializeField] private List<Transform> posSpawn;
//    public int count = 0;

//    public void OnInit()
//    {
//        this.enemyInMap = LevelManager.Ins.enemyInMap;
//        this.numberEnemyActive = LevelManager.Ins.numberEnemyActive;
//        ObjectPooler.Preload(enemy, numberEnemyActive, poolParent);
//        for (int i = 0; i < numberEnemyActive; i++)
//        {
//            SpawnEnemy();
//        }
//    }
//    //private void Update()
//    //{
//    //    GetEnemyInMap();
//    //}
//    //public void GetEnemyInMap()
//    //{
//    //    enemyList.RemoveAll(item => item == null);
//    //    if (enemyList.Count >= numberEnemyActive)
//    //    {
//    //        List<Enemy> subEnemyList = enemyList.GetRange(0, numberEnemyActive);
//    //        for (int i = 0; i < subEnemyList.Count; i++)
//    //        {
//    //            SpawnEnemy();
//    //        }
//    //    }
//    //}

//    private void SpawnEnemy()
//    {
//        if (posSpawn != null && posSpawn.Count > 0)
//        {
//            count = (count == posSpawn.Count - 1) ? 0 : count;
//            Enemy newBot = ObjectPooler.Spawn<Enemy>(enemy, posSpawn[count++].position, Quaternion.identity);
//            enemyList.Add(newBot);
//        }
//    }


//}
