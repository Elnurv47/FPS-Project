using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameOverMenu gameOverMenu;

    private void Start()
    {
        gameOverMenu.SusbscribeToPlayerDeathEvent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeSelf) return;
            pauseMenu.Pause();
        }
    }
}
