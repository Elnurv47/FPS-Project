using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    private const int RELOADING_ANIMATION_TIME = 3;

    private const string SHOOTING_ANIMATION_NAME = "isAiming";
    private const string WALKING_ANIMATION_NAME = "isWalking";
    private const string RELOADING_ANIMATION_NAME = "isReloading";

    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator playerAnimator;

    private void Start()
    {
        playerShooting.OnPlayerIsShooting += PlayerShooting_OnPlayerIsShooting;
        playerShooting.OnPlayerIsReloading += PlayerShooting_OnPlayerIsReloading;
        playerMovement.OnMovementStateChanged += PlayerMovement_OnMovementStateChanged;
    }

    private void PlayerShooting_OnPlayerIsShooting(bool isShooting)
    {
        playerAnimator.SetBool(SHOOTING_ANIMATION_NAME, isShooting);
    }

    private void PlayerShooting_OnPlayerIsReloading(bool isReloading)
    {
        playerAnimator.SetBool(RELOADING_ANIMATION_NAME, isReloading);
        StartCoroutine(StopReloadingAnimationCoroutine());
    }

    private IEnumerator StopReloadingAnimationCoroutine()
    {
        yield return new WaitForSeconds(RELOADING_ANIMATION_TIME);

        playerAnimator.SetBool(RELOADING_ANIMATION_NAME, false);
    }

    private void PlayerMovement_OnMovementStateChanged(bool isWalking)
    {
        playerAnimator.SetBool(WALKING_ANIMATION_NAME, isWalking);
    }
}
