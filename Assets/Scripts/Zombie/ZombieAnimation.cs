using UnityEngine;
using System.Collections;

public class ZombieAnimation : MonoBehaviour
{
    private const string MOVEMENT_ANIMAITON_NAME = "isMoving";
    private const string ATTACKING_ANIMATION_NAME = "isAttacking";
    private const string DYING_ANIMATION_NAME = "isDying";
    private const int DYING_ANIMATION_PERIOD = 1;

    [SerializeField] private Zombie zombie;
    [SerializeField] private Animator animator;

    private void Start()
    {
        zombie.OnMovementStateChanged += Zombie_OnMovementStateChanged;
        zombie.OnAttackingStateChanged += Zombie_OnAttackingStateChanged;
        zombie.OnDied += Zombie_OnDied;
    }

    private void Zombie_OnMovementStateChanged(bool isMoving)
    {
        animator.SetBool(MOVEMENT_ANIMAITON_NAME, isMoving);
    }

    private void Zombie_OnAttackingStateChanged(bool isAttacking)
    {
        animator.SetBool(ATTACKING_ANIMATION_NAME, isAttacking);
    }

    private void Zombie_OnDied()
    {
        animator.SetTrigger(DYING_ANIMATION_NAME);
    }
}
