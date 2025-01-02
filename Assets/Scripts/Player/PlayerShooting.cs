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
    [SerializeField] private LayerMask nonShootableLayerMask;
    [SerializeField] private PauseMenu pauseMenu;

    public Action<bool> OnPlayerIsShooting;
    public Action<Action> OnPlayerIsReloading;

    private void Start()
    {
        selectionSystem.OnNewWeaponEquipped += SelectionSystem_OnNewWeaponEquipped;
        pauseMenu.OnPauseStateChanged += PauseMenu_OnPauseStateChanged;
    }

    private void Update()
    {
        if (currentWeapon.IsAutomated)
        {
            HandleAutomatedWeaponShooting();
        }
        else
        {
            HandleNonAutomatedWeaponShooting();
        }

        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }

    private void HandleAutomatedWeaponShooting()
    {
        if (Input.GetButton(SHOOTING_INPUT_NAME) && Time.time >= nextTimeToFire && !isReloading)
        {
            nextTimeToFire = Time.time + currentWeapon.FireRate;
            Shoot();
        }
    }

    private void HandleNonAutomatedWeaponShooting()
    {
        if (Input.GetButtonDown(SHOOTING_INPUT_NAME) && Time.time >= nextTimeToFire && !isReloading)
        {
            nextTimeToFire = Time.time + currentWeapon.FireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayRandomMuzzleAnimation();
        currentWeapon.PlayShootingSound();
        currentWeapon.DecreaseAmmo();

        if (currentWeapon.CurrentAmmoInMagazine == 0) Reload();

        Vector3 rayOrigin = fpsCamera.transform.position;
        Vector3 rayDirection = fpsCamera.transform.forward;

        if (Physics.Raycast(
            rayOrigin,
            rayDirection,
            out RaycastHit hitInfo,
            currentWeapon.ShootingRange,
            ~nonShootableLayerMask))
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

    private void SelectionSystem_OnNewWeaponEquipped(Weapon newlySelectedWeapon)
    {
        currentWeapon = newlySelectedWeapon;
    }

    private void PauseMenu_OnPauseStateChanged(bool isPaused)
    {
        enabled = !isPaused;
    }
}
