using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooting : MonoBehaviour
{
    private const string SHOOTING_INPUT_NAME = "Fire1";

    private float nextTimeToFire = 0f;
    private bool isReloading;

    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private GameObject[] muzzleEffects;
    [SerializeField] private WeaponSelectionSystem selectionSystem;

    public Action<bool> OnPlayerIsShooting;
    public Action<Action> OnPlayerIsReloading;

    private void Start()
    {
        selectionSystem.OnNewWeaponEquipped += SelectionSystem_OnNewWeaponEquipped;
    }

    private void Update()
    {
        if (CanShoot())
        {
            nextTimeToFire = Time.time + currentWeapon.FireRate;
            Shoot();
        }

        if (Input.GetButtonUp(SHOOTING_INPUT_NAME)) StopShooting();
        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }

    private bool CanShoot() => 
        Input.GetButtonDown(SHOOTING_INPUT_NAME) && Time.time >= nextTimeToFire && !isReloading;

    private void Shoot()
    {
        PlayRandomMuzzleAnimation();
        currentWeapon.PlayShootingSound();
        currentWeapon.DecreaseAmmo();

        if (currentWeapon.CurrentAmmoInMagazine == 0) Reload();

        Vector3 rayOrigin = currentWeapon.ShootingPoint;
        Vector3 rayDirection = fpsCamera.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, currentWeapon.ShootingRange))
        {
            if (hitInfo.transform.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log(damageable);
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
            randomMuzzleEffect, 
            currentWeapon.MuzzleEffectSpawnPoint.position, 
            currentWeapon.MuzzleEffectSpawnPoint.rotation, 
            currentWeapon.MuzzleEffectSpawnPoint);
    }

    private void Reload()
    {
        if (currentWeapon.CurrentAmmoInMagazine == currentWeapon.WeaponMagazineCapacity) return;
        isReloading = true;
        currentWeapon.Reload();
        OnPlayerIsReloading?.Invoke(() =>
        {
            isReloading = false;
        });
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