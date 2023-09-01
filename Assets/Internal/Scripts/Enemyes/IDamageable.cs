namespace Internal.Scripts.Enemyes
{
    public interface IDamageable 
    {
        void ApplyDamage(int damageValue);

        void Die();
    }
}