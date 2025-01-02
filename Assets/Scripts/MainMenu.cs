using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Transform mapSelectionMenu;

    public void OnPlayButtonClick()
    {
        playButton.gameObject.SetActive(false);
        mapSelectionMenu.gameObject.SetActive(true);
    }
}
