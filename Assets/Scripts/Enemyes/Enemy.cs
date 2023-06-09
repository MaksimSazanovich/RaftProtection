using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IDamageable, IEnemy
{
    [SerializeField] private int maxHealth;
    public float Maxhealth { get => maxHealth; }
    private int health;
    public int Health { get => health; }

    private bool isLive = true;

    private SpriteRenderer sprite;

    [SerializeField] private UnityEvent OnApplyDamage;


    //[SerializeField] protected float startSpeed;

    [SerializeField] protected AI ai;

    [SerializeField] private UnityEvent OnHealthEnd;
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
    //        //    speed = startSpeed;
    //        //else
    //        //{
    //        //    speed = 0;
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
        Destroy(gameObject, 0.1f);
        //StartCoroutine(CoroutineDie());
    }

    //IEnumerator CoroutineDie()
    //{
    //    //animator.SetBool(Names.IsLive, false);
    //    yield return new WaitForSeconds(.2f);
    //    //StartCoroutine(InvisibleSprite());
    //}
    //IEnumerator InvisibleSprite()
    //{
    //    for (float f = 1f; f >= -0.05f; f -= 0.05f)
    //    {
    //        Color color = sprite.material.color;
    //        color.a = f;
    //        sprite.material.color = color;
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //}



}