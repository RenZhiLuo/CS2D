using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Health enemyPrefab;
    [SerializeField] private float interval;
    [SerializeField] private int maxCount;
    [SerializeField] private int currentCount;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval && currentCount < maxCount)
        {
            Vector3 pos = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0);
            Health health = Instantiate(enemyPrefab, pos, Quaternion.identity);
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
