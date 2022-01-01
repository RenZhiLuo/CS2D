using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnController : MonoBehaviour
{

    [SerializeField] private Health enemyPrefab;
    [SerializeField] private float interval;
    [SerializeField] private int maxCount;
    [SerializeField] private int currentCount;

    [SerializeField] private Transform[] spawnPoints;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval && currentCount < maxCount)
        {
            int rand = UnityEngine.Random.Range(0, spawnPoints.Length);
            Health health = Instantiate(enemyPrefab, spawnPoints[rand].position, Quaternion.identity);
            health.DeadHandler += EnemyDead;
            currentCount++;
            timer = 0;
        }
    }
    private void EnemyDead()
    {
        currentCount--;
    }
}
