using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector3 direction;
    public delegate void GameEvent(eGameEvent gameEvent);
    public static event GameEvent OnGameEvent;
    public bool isGameRunning;


    void Start()
    {
        isGameRunning = true;
    }

    void Update()
    {
        if (isGameRunning)
        {
            direction = Vector3.zero;

            //direction.z = Input.acceleration.y;

            direction.x = Input.acceleration.x;

            if (direction.sqrMagnitude > 1)
                direction.Normalize();

            transform.Translate(direction * speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ScoreCounter.obstacleTag))
        {
            isGameRunning = false;
            OnGameEvent?.Invoke(eGameEvent.GAME_OVER);
        }
    }


}
public enum eGameEvent
{
    GAME_START,
    GAME_PAUSE,
    GAME_OVER,
}