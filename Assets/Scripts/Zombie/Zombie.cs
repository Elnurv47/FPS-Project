using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour, IDamageable
{
    private const float DYING_ANIMATION_PERIOD = 1f;

    private float attackCooldown = 1.0f;
    private float lastAttackTime = -Mathf.Infinity;

    private Player player;
    private Transform playerTransform;

    [SerializeField] private float range = 20f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ParticleSystem hitEffect;

    public Action<bool> OnMovementStateChanged;
    public Action<bool> OnAttackingStateChanged;
    public Action OnDied;
    public static Action OnZombieKilled;

    private void Awake()
    {
        agent.speed = speed;
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerTransform = player.transform;
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void Update()
    {
        if (PlayerIsInRange())
            HandleMovementAndAttack();
        else
            StopMoving();
    }

    private void HandleMovementAndAttack()
    {
        MoveToPlayer();

        if (CanAttackPlayer() && EnoughTimePassedForAttacking())
        {
            StopMoving();
            Attack();
            lastAttackTime = Time.time;
        }
        else
        {
            StopAttacking();
        }
    }

    private bool EnoughTimePassedForAttacking() =>
        Time.time >= lastAttackTime + attackCooldown;

    private bool PlayerIsInRange() =>
        Vector3.Distance(agent.transform.position, playerTransform.position) < range;

    private bool CanAttackPlayer() =>
        Vector3.Distance(agent.transform.position, playerTransform.position) < attackRange;

    private void MoveToPlayer()
    {
        if (!agent.enabled) return;
        agent.SetDestination(playerTransform.position);
        OnMovementStateChanged?.Invoke(true);
    }

    private void StopMoving()
    {
        if (!agent.enabled) return;
        agent.ResetPath();
        OnMovementStateChanged?.Invoke(false);
    }

    private void Attack()
    {
        player.TakeDamage(damage, default);
        OnAttackingStateChanged?.Invoke(true);
    }

    private void StopAttacking()
    {
        OnAttackingStateChanged?.Invoke(false);
    }

    public void TakeDamage(float damage, RaycastHit hitInfo)
    {
        hitEffect.transform.position = hitInfo.point;
        hitEffect.Play();
        healthSystem.DecreaseHealth(damage);
    }

    private void HealthSystem_OnDied() => Die();

    private void Die()
    {
        OnDied?.Invoke();
        OnZombieKilled?.Invoke();

        DeactivateComponents();
        StartCoroutine(WaitForDeathAnimationCoroutine());
    }

    private IEnumerator WaitForDeathAnimationCoroutine()
    {
        yield return new WaitForSeconds(DYING_ANIMATION_PERIOD);
        DeactivateAnimator();
    }

    private void DeactivateComponents()
    {
        Component[] components = GetComponents<Component>();

        foreach (Component component in components)
        {
            if (component is Transform || component is Renderer || component is Animator)
                continue;

            if (component is MonoBehaviour)
                ((MonoBehaviour)component).enabled = false;
            else
                Destroy(component);
        }
    }

    private void DeactivateAnimator()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

}
