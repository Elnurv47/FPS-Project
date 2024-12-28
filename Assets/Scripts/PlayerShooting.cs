using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private const string SHOOTING_INPUT_NAME = "Fire1";
    private float nextTimeToFire = 0f;

    [SerializeField] private Weapon currentGun;
    [SerializeField] private Camera fpsCamera;

    public Action<bool> OnPlayerIsShooting;

    private void Update()
    {
        if (Input.GetButtonDown(SHOOTING_INPUT_NAME) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + currentGun.FireRate;
            Shoot();
        }

        if (Input.GetButtonUp(SHOOTING_INPUT_NAME))
        {
            StopShooting();
        }
    }

    private void Shoot()
    {
        Vector3 cameraPosition = fpsCamera.transform.position;
        Vector3 cameraForward = fpsCamera.transform.forward;

        if (Physics.Raycast(cameraPosition, cameraForward, out RaycastHit hit, currentGun.ShootingRange))
        {
            if (hit.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(currentGun.Damage);
            }
        }

        OnPlayerIsShooting?.Invoke(true);
    }

    private void StopShooting()
    {
        OnPlayerIsShooting?.Invoke(false);
    }
}
