using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public const string obstacleTag = "Obstacle";
    private int playerScore;
    public delegate void ObstacleReachedEnd(GameObject obstacle, int newScore);
    public static event ObstacleReachedEnd OnObstacleEnd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(obstacleTag))
        {
            playerScore++;
            OnObstacleEnd?.Invoke(other.gameObject, playerScore);
        }
    }
}
