using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeSelf) return;
            pauseMenu.Pause();
        }
    }
}
