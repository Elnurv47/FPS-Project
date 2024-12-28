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

    public void UpdateAmmoInfo(Weapon weapon)
    {
        ammoInfoText.text = weapon.CurrentAmmoInMagazine + "/" + weapon.CurrentAmmo;
    }
}
