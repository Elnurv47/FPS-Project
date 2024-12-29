using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooting : MonoBehaviour
{
    private const string SHOOTING_INPUT_NAME = "Fire1";
    private float nextTimeToFire = 0f;

    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private Transform muzzleEffectSpawnPoint;
    [SerializeField] private GameObject[] muzzleEffects;
    [SerializeField] private WeaponSelectionSystem weaponSelectionSystem;

    public Action<bool> OnPlayerIsShooting;
    public Action<bool> OnPlayerIsReloading;

    private void Start()
    {
        weaponSelectionSystem.OnNewWeaponEquipped += SelectionSystem_OnNewWeaponEquipped;
    }

    private void Update()
    {
        if (Input.GetButtonDown(SHOOTING_INPUT_NAME) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + currentWeapon.FireRate;
            Shoot();
        }

        if (Input.GetButtonUp(SHOOTING_INPUT_NAME)) StopShooting();
        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }

    private void Shoot()
    {
        PlayRandomMuzzleAnimation();
        currentWeapon.PlayShootingSound();
        currentWeapon.DecreaseAmmo();

        if (currentWeapon.CurrentAmmoInMagazine == 0) Reload();

        Vector3 cameraPosition = fpsCamera.transform.position;
        Vector3 cameraForward = fpsCamera.transform.forward;

        if (Physics.Raycast(cameraPosition, cameraForward, out RaycastHit hitInfo, currentWeapon.ShootingRange))
        {
            if (hitInfo.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(currentWeapon.Damage, hitInfo);
            }
        }

        OnPlayerIsShooting?.Invoke(true);
    }


    private void PlayRandomMuzzleAnimation()
    {
        int randomIndex = Random.Range(0, muzzleEffects.Length);
        GameObject randomMuzzleEffect = muzzleEffects[randomIndex];
        Instantiate(
            randomMuzzleEffect, muzzleEffectSpawnPoint.position, muzzleEffectSpawnPoint.rotation, muzzleEffectSpawnPoint);
    }

    private void Reload()
    {
        if (currentWeapon.CurrentAmmoInMagazine == currentWeapon.WeaponMagazineCapacity) return;
        currentWeapon.Reload();
        OnPlayerIsReloading?.Invoke(true);
    }

    private void StopShooting()
    {
        OnPlayerIsShooting?.Invoke(false);
    }

    private void SelectionSystem_OnNewWeaponEquipped(Weapon newlySelectedWeapon)
    {
        currentWeapon = newlySelectedWeapon;
    }
}
