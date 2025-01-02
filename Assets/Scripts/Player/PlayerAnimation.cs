using System;
using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    private const float RELOADING_ANIMATION_TIME = 3f;
    private const float TAKING_DOWN_WEAPON_ANIMATION_TIME = 1f;

    private const string WALKING_ANIMATION_NAME = "isWalking";
    private const string RELOADING_ANIMATION_NAME = "reloadTrigger";
    private const string SHOOTING_ANIMATION_NAME = "shootingTrigger";
    private const string TAKING_DOWN_WEAPON_ANIMATION_NAME = "takingDownWeapon";

    private Animator currentWeaponAnimator;

    [SerializeField] private WeaponSelectionSystem weaponSelectionSystem;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        weaponSelectionSystem.OnNewWeaponEquipped += WeaponSelectionSystem_OnNewWeaponSelected;
        playerShooting.OnPlayerIsShooting += PlayerShooting_OnPlayerIsShooting;
        playerShooting.OnPlayerIsReloading += PlayerShooting_OnPlayerIsReloading;
        playerMovement.OnMovementStateChanged += PlayerMovement_OnMovementStateChanged;
        weaponSelectionSystem.OnTakingDownWeapon += WeaponSelectionSystem_OnTakingDownWeapon;

        currentWeaponAnimator = weaponSelectionSystem.CurrentWeapon.Animator;
    }

    private void WeaponSelectionSystem_OnNewWeaponSelected(Weapon weapon)
    {
        currentWeaponAnimator = weapon.Animator;
    }

    private void PlayerShooting_OnPlayerIsShooting(bool isShooting)
    {
        currentWeaponAnimator.SetTrigger(SHOOTING_ANIMATION_NAME);
    }

    private void PlayerShooting_OnPlayerIsReloading(Action onFinished)
    {
        currentWeaponAnimator.SetTrigger(RELOADING_ANIMATION_NAME);
        StartCoroutine(FinishReloadingAnimationCoroutine(onFinished));
    }

    private IEnumerator FinishReloadingAnimationCoroutine(Action onFinished)
    {
        yield return new WaitForSeconds(RELOADING_ANIMATION_TIME);
        onFinished?.Invoke();
    }

    private void PlayerMovement_OnMovementStateChanged(bool isWalking)
    {
        currentWeaponAnimator.SetBool(WALKING_ANIMATION_NAME, isWalking);
    }

    private void WeaponSelectionSystem_OnTakingDownWeapon()
    {
        currentWeaponAnimator.SetBool(TAKING_DOWN_WEAPON_ANIMATION_NAME, true);
    }
}
