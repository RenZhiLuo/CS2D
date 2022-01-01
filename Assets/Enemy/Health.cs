using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected float currentHealth = 100;

    [SerializeField] protected Sound hurtSound;
    [SerializeField] protected Sound deadSound;

    public float MaxHealth { get { return maxHealth; } }
    public float CurrentHealth { get { return currentHealth; } }

    public Action DeadHandler;
    public Action<float> HurtHandler;
    public virtual void TakeDamage(float damage)
    {

        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            SoundManager.instance.PlayAudio(deadSound);
            DeadHandler?.Invoke();
        }
        else
        {
            SoundManager.instance.PlayAudio(hurtSound);
            HurtHandler?.Invoke(damage);
        }
    }
}
