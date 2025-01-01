using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    //[SerializeField] private DamageEffect damageEffect;

    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    public void TakeDamage(float damage, RaycastHit hitInfo)
    {
        //damageEffect.ShowDamageEffect();
        healthSystem.DecreaseHealth(damage);
    }

    private void HealthSystem_OnDied()
    {
        Debug.Log("GameOver");
    }
}
