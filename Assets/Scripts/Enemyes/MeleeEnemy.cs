using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] protected int attackDamage = 1;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            //animator.SetTrigger(Names.Attack);
            ai.Speed = 0;
            PlayerApplyDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    protected override void PlayerApplyDamage()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D damageableObject in hitObjects)
        {
            //StartCoroutine(Wait());
            if(damageableObject != null) damageableObject.GetComponent<Raft>().ApplyDamage(attackDamage);
        }
    }
}