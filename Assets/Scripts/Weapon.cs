using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int currentAmmoInMagazine;

    [Header("Ammo")]
    [SerializeField] private int currentAmmo = 90;
    [SerializeField] private int weaponMagazineCapacity = 10;

    [Header("Gun Settings")]
    [SerializeField] private float shootingRange = 100f;
    [SerializeField] private float damage = 10f;
    [SerializeField, Range(0f, 1f)] private float fireRate = 0.5f;

    [Header("Sound Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootingSound;
    
    public int CurrentAmmoInMagazine { get => currentAmmoInMagazine; }
    public int WeaponMagazineCapacity { get => weaponMagazineCapacity; }
    public int CurrentAmmo { get => currentAmmo; }
    public float ShootingRange { get => shootingRange; }
    public float FireRate { get => fireRate; }
    public float Damage { get => damage; }

    private void Awake()
    {
        currentAmmoInMagazine = currentAmmo > weaponMagazineCapacity ? weaponMagazineCapacity : currentAmmo;
    }

    public void PlayShootingSound()
    {
        audioSource.PlayOneShot(shootingSound);
    }

    public void DecreaseAmmo()
    {
        currentAmmoInMagazine--;
        AmmoInfoPanel.Instance.UpdateAmmoInfo(this);
    }

    public void Reload()
    {
        int emptyBulletSlotAmount = weaponMagazineCapacity - currentAmmoInMagazine;
        currentAmmoInMagazine = currentAmmo > weaponMagazineCapacity ?
            emptyBulletSlotAmount + currentAmmoInMagazine : currentAmmo;
        currentAmmo -= emptyBulletSlotAmount;
        AmmoInfoPanel.Instance.UpdateAmmoInfo(this);
    }
}
