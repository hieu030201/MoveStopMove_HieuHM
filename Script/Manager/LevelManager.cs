using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    [SerializeField] Level[] levels;
    public Level currentLevel;
    private int levelIndex;

    private int totalEnemy;
    public int TotalCharater => totalEnemy + listEnemy.Count + 1;

    private Medicine medicine = new Medicine();
    List<PoolType> listMedicines = new List<PoolType>();
    List<Character> listEnemy = new List<Character>();

    CounterTime counter = new CounterTime();

    public void Start()
    {
        levelIndex = 0;
        OnLoadLevel(levelIndex);
        OnInit();
        NewMedicine();
    }
    public void OnInit()
    {
        player.OnInit();
        for (int i = 0; i < currentLevel.numberEnemyActive; i++)
        {
            NewEnemy();
        }

        totalEnemy = currentLevel.enemyInMap - currentLevel.numberEnemyActive;
        
        listMedicines.Add(PoolType.MC_Heal);
        listMedicines.Add(PoolType.MC_Speed);
        listMedicines.Add((PoolType.MC_Dame));

        if (GameManager.Ins.IsState(GameState.GamePlay))
        {
            UIManager.Ins.GetUI<UIGamePlay>().TotalCharacterAlive();
        }

    }

    private void Update()
    {
        counter.Execute();
    }

    public void OnReset()
    {
        player.OnDespawn();
        for(int i = 0; i < listEnemy.Count; i++)
        {
            listEnemy[i].OnDespawn();
        }
        listEnemy.Clear();
   
    }

    public void OnLoadLevel(int level)
    {
        if(currentLevel != null)
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
        UIManager.Ins.OpenUI<UIMainMenu>();
        GameManager.Ins.ChanState(GameState.MainMenu);
    }

    public Vector3 RandomPoint()
    {
        Vector3 randomPoint = Vector3.zero;

        float size = 8.0f;

        for(int t =0; t < 40; t++)
        {
            randomPoint = currentLevel.RandomPoint();
            if(Vector3.Distance(randomPoint, player.TF.position) < size)
            {
                continue;
            }
            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < listEnemy.Count; i++)
                {
                    if (Vector3.Distance(randomPoint, listEnemy[i].TF.position) < size)
                    {
                        break;
                    }
                }

                if (j == 19)
                {
                    return randomPoint;
                }
            }

        }
        return randomPoint;

    }

    private Vector3 RandomPointMedia()
    {
        Vector3 randomPoint = currentLevel.RandomPoint();
        return randomPoint;
    }
    private void NewMedicine()
    {
        medicine.OnInit(Utilities.Chance(50, 100) ? listMedicines[0] : Utilities.Chance(50, 100) ? listMedicines[1] : listMedicines[2], RandomPointMedia());
    }

    public void NewEnemy()
    {
        Vector3 spawnPos = RandomPoint();
        Vector3 spawnParticle = new Vector3(spawnPos.x, spawnPos.y + 0.1f, spawnPos.z);
        ParticlePool.Play((ParticleType.Spawn_1), spawnParticle);
        StartCoroutine(SpawnEnemyWithDelay(spawnPos));
    }
    public void CreateEnemy(Vector3 spawnPos)
    {
        Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Bot, spawnPos, Quaternion.identity);
        enemy.SetLevel(Utilities.Chance(50, 100) ? player.LevelCharacter : player.LevelCharacter + Random.Range(1, 3));
        enemy.OnInit();
        listEnemy.Add(enemy);
    }
    private IEnumerator SpawnEnemyWithDelay(Vector3 spawnPos)
    {
        yield return new WaitForSeconds(2.0f); // Đợi 2 giây

        CreateEnemy(spawnPos);
    }
    public void CharacterDeath(Character c)
    {
        if (Utilities.Chance(65, 100))
        {
            Invoke(nameof(NewMedicine), Random.Range(2.0f, 8.0f));
        }

        if (c is Player)
        {
            Lose();
        }
        else
        {
            if(c is Enemy)
            {
                listEnemy.Remove(c);
                if(totalEnemy > 0)
                {
                    totalEnemy--;
                    NewEnemy();
                }
                if(listEnemy.Count == 0)
                {
                    Victory();
                }
            }
        }
        UIManager.Ins.GetUI<UIGamePlay>().TotalCharacterAlive();
    }

    public void Lose()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIFail>().SetCoin(player.Coin);
    }

    public void Victory()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIVictory>().SetCoin(player.Coin);
        player.ChangeAnim(Constant.ANIM_WIN);
    }
    public void NextLevel()
    {
        levelIndex++;
    }

}
