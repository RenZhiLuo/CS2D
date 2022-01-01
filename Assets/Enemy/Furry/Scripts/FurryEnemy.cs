using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurryEnemy : Enemy
{
    [SerializeField] private Animator anim;
    protected override void OnHurt(float damage)
    {
        base.OnHurt(damage);
        anim.SetTrigger("isHurting");
    }
}
