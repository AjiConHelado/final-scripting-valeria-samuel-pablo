using System.Collections.Generic;
using UnityEngine;

public enum Round
{
    Round1, Round2, Round3
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public EnemyData data1;
    [SerializeField] public EnemyData data2;
    [SerializeField] public EnemyData data3;
    [SerializeField] private GameObject enemy1, enemy2, enemy3;
    [SerializeField] private float spawnTimer;
    [SerializeField] private Vector2 spawnArea;
    
    [SerializeField] private int initialSize, incrementAmount;
     [SerializeField] private GameObject player;
    private Queue<Enemy> pool1, pool2, pool3;
    private Round currentRound = Round.Round1;
    //private Transform player;
    private float timer;
    private uint enemyCount;
    private byte maxEnemies = 10, zero = 0, one = 1;

    
    
    private void Awake()
    {
        pool1 = new Queue<Enemy>();
        pool2 = new Queue<Enemy>();
        pool3 = new Queue<Enemy>();
    }
    
    void Start()
    {
        //player = FindObjectOfType<PlayerController>().transform;
        AddInstances(initialSize, pool1, enemy1);
        AddInstances(initialSize, pool2, enemy2);
        AddInstances(initialSize, pool3, enemy3);
    }
    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < zero)
        {
            PlaceEnemy(SpawnEnemy());
            timer = spawnTimer;
        }
    }

    GameObject SpawnEnemy()
    {
        
        switch (ChangeRound())
        {
            case Round.Round1:
                return Allocate(pool1, enemy1);
            case Round.Round2:
                return Allocate(pool2, enemy2);
            case Round.Round3:
                return Allocate(pool3, enemy3);
        }

        return null;
    }

    Round ChangeRound()
    {
        if (enemyCount >= maxEnemies)
        {
            switch (currentRound)
            {
                case Round.Round1:
                    currentRound = Round.Round2;
                    break;
                case Round.Round2:
                    currentRound = Round.Round3;
                    break;
                case Round.Round3:
                    currentRound = Round.Round1;
                    break;
            }
            
            enemyCount = 0;
        }
        
        return currentRound;
    }
    
    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();

        float f = Random.value > 0.5 ? -one : one;
        if (Random.value > 0.5f)
        {
            position.x = Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }
        position.z = 0;
         
        return position;
    }

    void PlaceEnemy(GameObject enemy)
    {
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;
        
        enemy.transform.position = position;
    }
    
    private void AddInstances(int amount, Queue<Enemy> instances, GameObject prefab)
    {
        for (int i = 0; i < amount; i++)
        {
            Enemy obj = Instantiate(prefab, gameObject.transform).GetComponent<Enemy>();
            obj.SetTarget(player.gameObject);
            obj.enemySpawner = this;

            if(obj.type==EnemyType.three)
            {
                obj.stats = data3;
            }
            else if(obj.type == EnemyType.two)
            {
                obj.stats = data2;
            }
            else {
                obj.stats = data1;
            }

                obj.gameObject.SetActive(false);
            
            instances.Enqueue(obj);
        }
    }

    private GameObject Allocate(Queue<Enemy> instances, GameObject enemy)
    { 
        GameObject instance;
            
        if (instances.Count == 0)
        {
            AddInstances(incrementAmount, instances, enemy);
            enemyCount++;
            return Allocate(instances, enemy);
        }
            
        instance = instances.Dequeue().gameObject;
        instance.SetActive(true);

        enemyCount++;
        
        
        return instance;
    }

    public void Return(Enemy instance, EnemyType type)
    {
        Queue<Enemy> instances = new Queue<Enemy>();
        
        switch (type)
        {
            case EnemyType.One:
                instances = pool1;
                break;
            case EnemyType.two:
                instances = pool2;
                break;
            case EnemyType.three:
                instances = pool3;
                break;
        }
        
        instances.Enqueue(instance);
        instance.gameObject.SetActive(false);
    }
}
