using UnityEngine;

namespace Internal.Scripts.Enemyes
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange = 0.5f;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] protected int attackDamage = 1;
        private float nextAttackTime;
        [SerializeField] private float timeBetweenAttacks;

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                //animator.SetTrigger(Names.Attack);
                ai.Speed = 0;
                damageable.ApplyDamage(attackDamage);
                //PlayerApplyDamage();
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            nextAttackTime += Time.deltaTime;
            if (nextAttackTime >= timeBetweenAttacks)
            {
                if (collision.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(attackDamage);
                    nextAttackTime = 0f;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                nextAttackTime = 0f;
            }
        }

        //private void OnDrawGizmosSelected()
        //{
        //    if (attackPoint == null)
        //        return;
        //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        //}

        protected override void PlayerApplyDamage()
        {
            //Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
            //foreach (Collider2D damageableObject in hitObjects)
            //{
            //    StartCoroutine(Wait());
            //    if (damageableObject != null) damageableObject.GetComponent<RaftHealth>().ApplyDamage(attackDamage);
            //}
        }
    }
}