using System;
using UnityEngine;

public class WeaponSelectionSystem : MonoBehaviour
{
    private const string MOUSE_SCROLL_WHEEL_INPUT = "Mouse ScrollWheel";

    private int selectedWeaponSlotIndex = 0;

    [SerializeField] private WeaponSlotUI[] weaponSelectionUIs;
    [SerializeField] private Transform playerWeaponHolder;

    public Action<Weapon> OnNewWeaponEquipped;

    private void Awake()
    {
        EquipWeapon(selectedWeaponSlotIndex, 0);
    }

    private void Update()
    {
        float scrollWheelInput = Input.GetAxis(MOUSE_SCROLL_WHEEL_INPUT);
        int previousWeaponSlotIndex = selectedWeaponSlotIndex;

        if (scrollWheelInput < 0)
        {
            selectedWeaponSlotIndex++;
        }
        else if (scrollWheelInput > 0)
        {
            selectedWeaponSlotIndex--;
        }

        if (selectedWeaponSlotIndex < 0) selectedWeaponSlotIndex = weaponSelectionUIs.Length - 1;
        if (selectedWeaponSlotIndex > weaponSelectionUIs.Length - 1) selectedWeaponSlotIndex = 0;

        if (IfNewWeaponSelected(previousWeaponSlotIndex)) 
            EquipWeapon(selectedWeaponSlotIndex, previousWeaponSlotIndex);
    }

    private bool IfNewWeaponSelected(int lastWeaponSlotUIIndex)
    {
        return lastWeaponSlotUIIndex != selectedWeaponSlotIndex;
    }

    private void EquipWeapon(int selectedWeaponSlotIndex, int previousWeaponSlotIndex)
    {
        DeactivatePreviousWeapon();
        DeselectPreviousWeaponSlot(previousWeaponSlotIndex);

        WeaponSlotUI selectedWeaponSlotUI = weaponSelectionUIs[selectedWeaponSlotIndex];
        selectedWeaponSlotUI.Select();

        Weapon selectedWeapon = selectedWeaponSlotUI.LinkedWeapon;
        selectedWeapon.gameObject.SetActive(true);
        OnNewWeaponEquipped?.Invoke(selectedWeapon);
    }

    private void DeselectPreviousWeaponSlot(int previousWeaponSlotIndex)
    {
        WeaponSlotUI previouslySelectedWeaponSlot = weaponSelectionUIs[previousWeaponSlotIndex];
        previouslySelectedWeaponSlot.DeSelect();
    }

    private void DeactivatePreviousWeapon()
    {
        foreach (Transform weapon in playerWeaponHolder)
        {
            weapon.gameObject.SetActive(false);
        }
    }
}
