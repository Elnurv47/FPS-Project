using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float health;

    [SerializeField] private float maxHealth;

    public Action OnDied;

    private void Awake()
    {
        health = maxHealth;
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void DecreaseHealth(float amount)
    {
        health -= amount;
        if (health <= 0) OnDied?.Invoke();
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}