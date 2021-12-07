using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurryEnemy : Enemy
{
    [SerializeField] private Animator anim;
    protected override void Hurt(float damage)
    {
        base.Hurt(damage);
        anim.SetTrigger("isHurting");
    }
}
