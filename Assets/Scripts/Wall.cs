using UnityEngine;

public class Wall : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject bulletHolePrefab;

    public void TakeDamage(float damage, RaycastHit hitInfo)
    {
        GameObject spawnedBulletHole = Instantiate(bulletHolePrefab, hitInfo.point, Quaternion.identity);
    }
}
