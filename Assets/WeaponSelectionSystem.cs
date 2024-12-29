using UnityEngine;

public class WeaponSelectionSystem : MonoBehaviour
{
    private WeaponSelectionUI currentWeapon;

    [SerializeField] private WeaponSelectionUI[] weaponSelectionUIs;

    private void Awake()
    {
        currentWeapon = weaponSelectionUIs[0];
    }
}
