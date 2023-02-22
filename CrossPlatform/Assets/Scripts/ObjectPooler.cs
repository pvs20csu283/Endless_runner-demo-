using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject obstacleClone;
    public int poolCount;
    public float spawnRate;
    public float speed;
    private List<GameObject> obstaclePool;
    private List<Transform> obstacles;


    private float lastSpawnTime;

    void Start()
    {
        obstaclePool = new List<GameObject>(poolCount);
        obstacles = new List<Transform>();
        for (int i = 0; i < poolCount; i++)
        {
            obstaclePool.Add(Instantiate(obstacleClone, transform));
           
            obstaclePool[i].SetActive(false);
            obstaclePool[i].name = "0" + i;
        }
    }

   
    private void OnEnable()
    {
        ScoreCounter.OnObstacleEnd += ReturnToPool;
        Player.OnGameEvent += OnGameEvent;
    }

    private void OnGameEvent(eGameEvent gameEvent)
    {
        obstacles.Clear();
    }

    private void OnDisable()
    {
        ScoreCounter.OnObstacleEnd -= ReturnToPool;
        Player.OnGameEvent -= OnGameEvent;
    }

    private void ReturnToPool(GameObject obstacle, int newScore)
    {
        obstacles.Remove(obstacle.transform);
        obstacle.SetActive(false);
    }

    private GameObject GetFromPool()
    {
        foreach (GameObject poolObject in obstaclePool)
        {

            if (!poolObject.activeInHierarchy)
            {
                poolObject.SetActive(true);
                poolObject.transform.position = GetSpawnPosition();
                return poolObject;
            }

        }
        return null;
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range((int)-10, 10), 0.5f, transform.position.z);
    }

    void Update()
    {
        lastSpawnTime += Time.deltaTime;
        if (lastSpawnTime > spawnRate)
        {
            SpawnNextObstacle();
        }
        if (obstacles.Count > 0)
        {
            MoveObstacles();
        }
    }

    private void MoveObstacles()
    {
        foreach(Transform obstacle in obstacles)
        {
            obstacle.Translate(Vector3.forward * -speed *  Time.deltaTime);
        }
    }

    private void SpawnNextObstacle()
    {
        GameObject obstacle = GetFromPool();
        if (obstacle != null)
        {
            obstacles.Add(obstacle.transform);
        }
    }
}