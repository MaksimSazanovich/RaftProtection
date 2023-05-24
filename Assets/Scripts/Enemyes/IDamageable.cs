using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    void ApplyDamage(int damageValue);

    void Die();
}