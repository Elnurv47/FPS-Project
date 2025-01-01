using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private DamageEffect damageEffect;

    public void TakeDamage(float damage, RaycastHit hitInfo)
    {
        damageEffect.ShowDamageEffect();
    }
}
