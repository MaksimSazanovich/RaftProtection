using Internal.Scripts.Manager_Controller;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Internal.Scripts.Enemyes
{
    public abstract class Enemy : MonoBehaviour, IDamageable, IEnemy
    {
        [SerializeField] private int maxHealth;
        public float Maxhealth { get => maxHealth; }
        private int health;
        public int Health { get => health; }

        private bool isLive = true;

        private SpriteRenderer sprite;

        [SerializeField] private UnityEvent OnApplyDamage;


        [SerializeField] protected float startSpeed;

        [SerializeField] protected AI.AI ai;

        [SerializeField] private UnityEvent OnHealthEnd;

        private EnemyController _enemyController;

        [Inject]
        private void Construct(EnemyController enemyController)
        {
            _enemyController = enemyController;
        }


        protected virtual void Start()
        {
            health = maxHealth;
            //animator.SetBool(Names.IsLive, true);
            sprite = GetComponent<SpriteRenderer>();
            Color color = sprite.material.color;
            color.a = 1f;
            sprite.material.color = color;
        }
        //protected void Update()
        //{
        //    if (health > 0)
        //    {
        //        //if (dazedTime <= 0)
        //        //    _speed = startSpeed;
        //        //else
        //        //{
        //        //    _speed = 0;
        //        //    dazedTime -= Time.deltaTime;
        //        //}
        //    }
        //}

        public virtual void ApplyDamage(int _damage)
        {
            isLive = health > 0;
            OnApplyDamage.Invoke();
            if (isLive)
            {
                //animator.SetTrigger(Names.Damage);
                health -= _damage;
            }

            if (health <= 0)
            {
                Die();
            }
        }

        protected abstract void PlayerApplyDamage();
        public virtual void Die()
        {
            ai.Speed = 0;
            GetComponent<BoxCollider2D>().enabled = false;
            OnHealthEnd.Invoke();
            _enemyController.RemoveEnemy();
            Destroy(gameObject, 0.1f);
            //StartCoroutine(CoroutineDie());
        }
    }
}