using UnityEngine;

public class Wall : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        Debug.Log("Wall taking damage");
    }
}
