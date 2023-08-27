using UnityEngine;
using UnityEngine.SceneManagement;

public class RaftHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    public float Maxhealth { get => maxHealth; }
    private static float health;
    public float Health { get => health; }

    private int sceneIndex;

    private static bool maxHealthLock;
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
        ReloadScene();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}