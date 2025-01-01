using System;
using UnityEngine;

public class WeaponSelectionSystem : MonoBehaviour
{
    private const string MOUSE_SCROLL_WHEEL_INPUT = "Mouse ScrollWheel";

    private int selectedWeaponSlotIndex = 0;
    private Weapon currentWeapon;

    [SerializeField] private WeaponSlotUI[] weaponSelectionUIs;
    [SerializeField] private Transform playerWeaponHolder;

    public Action<Weapon> OnNewWeaponEquipped;
    public Action OnTakingDownWeapon;

    public Weapon CurrentWeapon { get => currentWeapon; }

    private void Awake()
    {
        EquipInitialWeapon();
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
        {
            EquipWeapon(selectedWeaponSlotIndex, previousWeaponSlotIndex);
        }
    }

    private void EquipInitialWeapon()
    {
        WeaponSlotUI firstWeaponSlot = weaponSelectionUIs[0];
        currentWeapon = firstWeaponSlot.LinkedWeapon;
        firstWeaponSlot.Select();
        currentWeapon.gameObject.SetActive(true);
    }

    private bool IfNewWeaponSelected(int lastWeaponSlotUIIndex)
    {
        return lastWeaponSlotUIIndex != selectedWeaponSlotIndex;
    }

    private void EquipWeapon(int selectedWeaponSlotIndex, int previousWeaponSlotIndex)
    {
        TakeDownPreviousWeapon();

        DeselectPreviousWeaponSlot(previousWeaponSlotIndex);
        WeaponSlotUI selectedWeaponSlotUI = SelectNewWeaponSlot(selectedWeaponSlotIndex);

        currentWeapon = selectedWeaponSlotUI.LinkedWeapon;
        currentWeapon.gameObject.SetActive(true);

        OnNewWeaponEquipped?.Invoke(currentWeapon);
    }

    private WeaponSlotUI SelectNewWeaponSlot(int selectedWeaponSlotIndex)
    {
        WeaponSlotUI selectedWeaponSlotUI = weaponSelectionUIs[selectedWeaponSlotIndex];
        selectedWeaponSlotUI.Select();
        return selectedWeaponSlotUI;
    }

    private void DeselectPreviousWeaponSlot(int previousWeaponSlotIndex)
    {
        WeaponSlotUI previouslySelectedWeaponSlot = weaponSelectionUIs[previousWeaponSlotIndex];
        previouslySelectedWeaponSlot.DeSelect();
    }

    private void TakeDownPreviousWeapon()
    {
        OnTakingDownWeapon?.Invoke();

        foreach (Transform weapon in playerWeaponHolder)
        {
            weapon.gameObject.SetActive(false);
        }
    }
}
