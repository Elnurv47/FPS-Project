using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private Image crosshairImage;

    private void Update()
    {
        Ray ray = fpsCamera.ScreenPointToRay(transform.position);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.TryGetComponent(out IDamageable damageable))
            {
                if (damageable is Zombie)
                {
                    crosshairImage.color = Color.red;
                }
                else
                {
                    crosshairImage.color = Color.black;
                }
            }
            else
            {
                crosshairImage.color = Color.black;
            }
        }
        else
        {
            crosshairImage.color = Color.black;
        }
    }
}
