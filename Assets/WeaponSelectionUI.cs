using UnityEngine;

public class WeaponSelectionUI : MonoBehaviour
{
    [SerializeField] private Weapon linkedWeapon;

    public Weapon LinkedWeapon { get => linkedWeapon; }
}
