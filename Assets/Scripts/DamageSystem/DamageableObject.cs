using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject bulletHolePrefab;

    public void TakeDamage(float damage, RaycastHit hitInfo)
    {
        GameObject spawnedBulletHole = Instantiate(bulletHolePrefab, hitInfo.point, Quaternion.identity);
        spawnedBulletHole.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
    }
}
