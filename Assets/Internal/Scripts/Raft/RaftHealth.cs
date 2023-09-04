using System;
using Internal.Scripts.Enemyes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Internal.Scripts.Raft
{
    public class RaftHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;
        public float Maxhealth { get => maxHealth; }
        private static float health;
        public float Health { get => health; }

        private int sceneIndex;

        private static bool maxHealthLock;

        public event Action OnDie;
        private void Start()
        {
            maxHealthLock = true;
            if (maxHealthLock)
            {
                health = maxHealth;
                maxHealthLock = false;
            }
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        public void ApplyDamage(int damageValue)
        {
            health -= damageValue;
            //animator.ChangeAnimationState(Names.Damage);

            if (health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            OnDie?.Invoke();
        }
    }
}