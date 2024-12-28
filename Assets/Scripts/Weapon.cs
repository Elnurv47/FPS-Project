using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private float shootingRange = 100f;
    [SerializeField] private float damage = 10f;
    [SerializeField, Range(0f, 1f)] private float fireRate = 0.5f;

    public float ShootingRange { get => shootingRange; }
    public float FireRate { get => fireRate; }
    public float Damage { get => damage; }
}
