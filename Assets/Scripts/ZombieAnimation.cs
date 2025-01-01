using System;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private const string MOVEMENT_ANIMAITON_NAME = "isMoving";
    private const string ATTACKING_ANIMATION_NAME = "isAttacking";

    [SerializeField] private Zombie zombie;
    [SerializeField] private Animator animator;

    private void Start()
    {
        zombie.OnMovementStateChanged += Zombie_OnMovementStateChanged;
        zombie.OnAttackingStateChanged += Zombie_OnAttackingStateChanged;
    }

    private void Zombie_OnMovementStateChanged(bool isMoving)
    {
        animator.SetBool(MOVEMENT_ANIMAITON_NAME, isMoving);
    }

    private void Zombie_OnAttackingStateChanged(bool isAttacking)
    {
        animator.SetBool(ATTACKING_ANIMATION_NAME, isAttacking);
    }
}
