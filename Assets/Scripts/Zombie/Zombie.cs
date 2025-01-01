using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour, IDamageable
{
    private Player player;
    private Transform playerTransform;

    [SerializeField] private float range = 20f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private HealthSystem healthSystem;

    public Action<bool> OnMovementStateChanged;
    public Action<bool> OnAttackingStateChanged;
    public Action OnDied;

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
        {
            MoveToPlayer();

            if (CanAttackPlayer())
            {
                StopMoving();
                Attack();
            }
            else
            {
                StopAttacking();
                MoveToPlayer();
            }
        }
        else
        {
            StopMoving();
        }
    }

    private bool PlayerIsInRange()
    {
        return Vector3.Distance(agent.transform.position, playerTransform.position) < range;
    }

    private bool CanAttackPlayer()
    {
        return Vector3.Distance(agent.transform.position, playerTransform.position) < attackRange;
    }

    private void MoveToPlayer()
    {
        agent.SetDestination(playerTransform.position);
        OnMovementStateChanged?.Invoke(true);
    }

    private void StopMoving()
    {
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
        healthSystem.DecreaseHealth(damage);
    }

    private void HealthSystem_OnDied()
    {
        Die();
    }

    private void Die()
    {
        //Destroy(gameObject);
    }
}
