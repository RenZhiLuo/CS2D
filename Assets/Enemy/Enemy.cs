using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected Health health;
    [SerializeField] private AIMovement aiMovement;

    [SerializeField] private float attackInterval = 1;
    [SerializeField] private float damage;
    private float timer;

    private int walkParam = Animator.StringToHash("isWalking");
    private int attackParam = Animator.StringToHash("attack");

    private void Start()
    {
        health.DeadHandler += OnDead;
    }
    private void OnDestroy()
    {
        health.DeadHandler -= OnDead;
    }

    private void FixedUpdate()
    {
        anim.SetBool(walkParam, aiMovement.IsMoving);
    }



    private void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (timer >= attackInterval)
            {
                timer = 0;
                anim.SetTrigger(attackParam);
                if (collision.transform.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }

    protected virtual void OnHurt(float damage)
    {

    }
    protected virtual void OnDead()
    {
        Debug.Log("Enemy dead!!");
        Destroy(gameObject);
    }
}
