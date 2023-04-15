using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//敌人生成管理器
public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] GameObject[] EnemyPrefabs;//包括巡逻的敌人、直往一个方向移动的敌人
    [SerializeField] GameObject Mine;//地雷

    [SerializeField, Range(0, 100)] float probability;//决定了敌人生成在左或右的几率
    [SerializeField] float minOffset = 5f;
    [SerializeField] float maxOffset = 5f;

    [SerializeField] float EnemyReGenerate = 3f;
    [SerializeField] float MineReGenerate = 4f;
    
    
    WaitForSeconds waitforEnemyReGenerate;//敌人产生的等待时间
    WaitForSeconds waitforMineReGenerate;//敌人产生的等待时间

    List<GameObject> enemyList;//敌人列表
    
    protected override void Awake()
    {
        base.Awake();
        waitforEnemyReGenerate = new WaitForSeconds(EnemyReGenerate);
        waitforMineReGenerate = new WaitForSeconds(MineReGenerate);
        enemyList = new List<GameObject>();
    }

    void Start()
    {
        StartCoroutine(nameof(GenerateEnemyContinusly));
        //StartCoroutine(nameof(GenerateMineContinusly));
    }
    
    
    IEnumerator GenerateEnemyContinusly()
    {
        while (gameObject.activeSelf)
        {
            yield return waitforEnemyReGenerate;
            Release();
        }
    }
    
    IEnumerator GenerateMineContinusly()
    {
        while (gameObject.activeSelf)
        {
            yield return waitforMineReGenerate;
            GenerateMine(GetSuitablePosForMine());
        }
    }

    public void Release()//生成一个敌人并将其置于地图的合适位置
    {
        var enemy = GenerateEnemy();
        enemy.transform.position = GetSuitablePosForEnemy();
        enemy.SetActive(true);
        enemyList.Add(enemy);
    }
    
    public Vector3 GetSuitablePosForMine()
    {
        float x = Random.Range(0,100) > probability
            ?Random.Range(Camera.main.transform.position.x-maxOffset,Camera.main.transform.position.x-minOffset)
            :Random.Range(Camera.main.transform.position.x+minOffset,Camera.main.transform.position.x+maxOffset);
        float y = -3;
        return new Vector3(x, y,0);
    }
    
    public Vector3 GetSuitablePosForEnemy()
    {
        float x = Random.Range(0,100) > probability
            ?Random.Range(Camera.main.transform.position.x-maxOffset,Camera.main.transform.position.x-minOffset)
            :Random.Range(Camera.main.transform.position.x+minOffset,Camera.main.transform.position.x+maxOffset);
        
        float y = Random.Range(0,3);
        
        if (y < 1) y = -1.7f;
        else if (y < 2) y = 2.2f;
        else y = 7.2f;
        
        //y = -3 、 2 、 7可以生成敌人
        return new Vector3(x, y,0);
    }
    
    private GameObject GenerateEnemy()//生成敌人方法
    {
        foreach (var enemyNotActive in enemyList)
        {
            if (!enemyNotActive.activeSelf)
            {
                return enemyNotActive;
            }
        }
        return Instantiate(EnemyPrefabs[Random.Range(0,3)]);
    }
    
    private GameObject GenerateMine()//生成地雷的方法
    {
        return Instantiate(Mine);
    }
    
    private GameObject GenerateEnemy(Vector3 targetPos)//生成敌人方法
    {
        return Instantiate(EnemyPrefabs[Random.Range(0,3)],targetPos,Quaternion.identity);
    }
    
    private GameObject GenerateMine(Vector3 targetPos)//生成地雷的方法
    {
        return Instantiate(Mine,targetPos,Quaternion.identity);
    }
    
}
