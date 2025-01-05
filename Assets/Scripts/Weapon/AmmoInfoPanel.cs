using System;
using TMPro;
using UnityEngine;

public class AmmoInfoPanel : MonoBehaviour
{
    public static AmmoInfoPanel Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private TextMeshProUGUI ammoInfoText;
    [SerializeField] private WeaponSelectionSystem weaponSelectionSystem;

    private void Start()
    {
        weaponSelectionSystem.OnNewWeaponEquipped += WeaponSelectionSystem_OnNewWeaponEquipped;
    }

    private void WeaponSelectionSystem_OnNewWeaponEquipped(Weapon weapon)
    {
        UpdateAmmoInfo(weapon);
    }

    public void UpdateAmmoInfo(Weapon weapon)
    {
        ammoInfoText.text = weapon.CurrentAmmoInMagazine + "/" + weapon.CurrentAmmo;
    }
}
