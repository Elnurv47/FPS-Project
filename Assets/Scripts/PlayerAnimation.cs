using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string SHOOTING_ANIMATION_NAME = "aiming";
    public const string WALKING_ANIMATION_NAME = "isWalking";

    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator playerAnimator;

    private void Start()
    {
        playerShooting.OnPlayerIsShooting += PlayerShooting_OnPlayerIsShooting;
        playerMovement.OnMovementStateChanged += PlayerMovement_OnMovementStateChanged;
    }

    private void PlayerShooting_OnPlayerIsShooting(bool isShooting)
    {
        playerAnimator.SetBool(SHOOTING_ANIMATION_NAME, isShooting);
    }

    private void PlayerMovement_OnMovementStateChanged(bool isWalking)
    {
        playerAnimator.SetBool(WALKING_ANIMATION_NAME, isWalking);
    }
}
