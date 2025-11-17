using UnityEngine;

public class Weapon : Item
{
    public float Damage { get { return damageOutput; } private set { damageOutput = value; } }
    private float damageOutput;

    [SerializeField] private float lightDamage;
    [SerializeField] private float heavyDamage;

    private bool isAttacking;

    public void LightAttack()
    {
        Damage = lightDamage;
    }

    public void HeavyAttack()
    {
        Damage = heavyDamage;
    }

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(isAttacking && col.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(Damage);
        }
    }
}
