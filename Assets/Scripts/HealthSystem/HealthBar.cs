using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void HealthSystem_OnHealthChanged(float health, float maxHealth)
    {
        healthBarImage.fillAmount = health / maxHealth;
    }

    private void HealthSystem_OnDied()
    {
        gameObject.SetActive(false);
    }
}
