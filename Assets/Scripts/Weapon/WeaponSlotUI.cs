using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    private Color defaultColor = Color.white;
    private Color selectedColor = new Color(18 / 255f, 92 / 255f, 157 / 255f);

    [SerializeField] private Weapon linkedWeapon;

    public Weapon LinkedWeapon { get => linkedWeapon; }

    [SerializeField] private Image backgroundImage;

    public void Select()
    {
        backgroundImage.color = selectedColor;
    }

    public void DeSelect()
    {
        backgroundImage.color = defaultColor;
    }
}
