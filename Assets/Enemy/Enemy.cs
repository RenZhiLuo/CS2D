using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Health health;
    [SerializeField] protected float moveSpeed;
    private void Start()
    {
        health.HurtHandler += Hurt;
        health.DeadHandler += Dead;
    }
    private void Update()
    {
        Move(Time.deltaTime);
    }

    private void Move(float time)
    {
        if (PlayerController2D.instance != null)
        {
            Vector3 direction = PlayerController2D.instance.transform.position - transform.position;
            transform.position += moveSpeed * time * direction;
            transform.up = direction;
        }
    }
    protected virtual void Hurt(float damage)
    {
        SoundManager.instance.PlayAudio(ClipType.EnemyHurt);
    }
    protected void Dead()
    {
        Debug.LogError("Dead!!");
        Destroy(gameObject);
    }

    [SerializeField] private float damageInterval;
    [SerializeField] private float damage;
    private float timer;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            timer += Time.fixedDeltaTime;
            if (timer >= damageInterval)
            {
                timer = 0;
                if (collision.transform.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }
}
