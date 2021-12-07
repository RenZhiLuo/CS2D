using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHealth : Health
{
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        SoundManager.instance.PlayAudio(ClipType.PlayerHurt);
    }
}
