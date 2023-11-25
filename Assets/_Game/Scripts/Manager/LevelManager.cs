using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public Enemy enemy;
    [SerializeField] Level[] levels;
    public Level currentLevel;
    private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private Transform poolParent;
    [SerializeField] private List<Transform> posSpawn;
    private int countPosPawn;
    private int totalBot;

    private int levelIndex;
    public int TotalCharater => totalBot + enemies.Count + 1;

    public void Start()
    {
        levelIndex = 0;
        OnLoadLevel(levelIndex);
        OnInit();
    }

    //khoi tao trang thai bat dau game
    public void OnInit()
    {
        player.OnInit();
        ObjectPooler.Preload(enemy, currentLevel.numberEnemyActive, poolParent);

        for (int i = 0; i < currentLevel.numberEnemyActive; i++)
        {
            NewEnemy(null);
        }

        totalBot = currentLevel.enemyInMap - currentLevel.numberEnemyActive - 1;
    }

    //reset trang thai khi ket thuc game
    public void OnReset()
    {
        //player.OnDespawn();
        //for (int i = 0; i < bots.Count; i++)
        //{
        //    bots[i].OnDespawn();
        //}
        ObjectPooler.ReleaseAll();
        enemies.Clear();
        ObjectPooler.CollectAll();
    }
    //tao prefab level moi
    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
    }


    public void Home()
    {
        UIManager.Ins.CloseAll();
        OnReset();
        OnLoadLevel(levelIndex);
        OnInit();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
        GameManager.ChangeState(GameState.MainMenu);
        player.anim.SetBool(Constant.ANIM_WIN, false);
    }
    //public Vector3 RandomPoint()
    //{
    //    Vector3 randPoint = Vector3.zero;

    //    for (int t = 0; t < 50; t++)
    //    {

    //        randPoint = currentLevel.RandomPoint();
    //        if (Vector3.Distance(randPoint, player.tf.position) < 6f)
    //        {
    //            continue;
    //        }

    //        for (int j = 0; j < 20; j++)
    //        {
    //            for (int i = 0; i < enemies.Count; i++)
    //            {
    //                if (Vector3.Distance(randPoint, enemies[i].tf.position) < 10f)
    //                {
    //                    break;
    //                }
    //            }

    //            if (j == 19)
    //            {
    //                return randPoint;
    //            }
    //        }


    //    }

    //    return randPoint;
    //}
    private void NewEnemy(IState state)
    {
        if (posSpawn != null && posSpawn.Count > 0)
        {
            countPosPawn = (countPosPawn == posSpawn.Count - 1) ? 0 : countPosPawn;
            Enemy newEnemy = ObjectPooler.Spawn<Enemy>(enemy, posSpawn[countPosPawn++].position, Quaternion.identity);
            newEnemy.OnInit();
            //newEnemy.ChangeState(state);
            enemies.Add(newEnemy);
        }
            
            
    }
    public void CharacterDeath(Character c)
    {
        if(c is Player)
        {
            Lose();
        }
        else if(c is Enemy)
        {
            enemies.Remove(c as Enemy);
            if(totalBot > 0)
            {
                totalBot--;
                NewEnemy(null);
            }
            if (enemies.Count == 0)
            {
                Victory();
            }
        }
        UIManager.Ins.GetUI<CanvasGamePlay>().SetTextAlive();
    }
    public void NextLevel()
    {
        levelIndex++;
    }
    private void Victory()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<CanvasVictory>().SetCoin(player.Coin);
        player.anim.SetBool(Constant.ANIM_WIN, true);
    
        //Invoke(player)
    }
    public void Lose()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<CanvasFail>().SetCoin(player.Coin);
    }
}
