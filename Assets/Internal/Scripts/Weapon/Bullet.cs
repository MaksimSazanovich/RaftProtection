using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] protected float speed;
	[SerializeField] protected Rigidbody2D rb;

    public int Damage;

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnBecameInvisible()
    {
        Discativate();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(Damage);
            //Destroy(gameObject);
            Discativate();
        }
    }

    private void Discativate()
    {
        gameObject.SetActive(false);
    }
}