using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI yourScoreText;
    [SerializeField] private SceneAsset currentMapScene;
    [SerializeField] private SceneAsset mainMenuScene;
    [SerializeField] private Player player;
    [SerializeField] private ScoreSystem scoreSystem;

    public void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentMapScene.name);
    }

    public void OnMainMenuButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuScene.name);
    }
    public void SusbscribeToPlayerDeathEvent()
    {
        player.OnDied += Player_OnDied;
    }

    public void Player_OnDied()
    {
        gameObject.SetActive(true);
        yourScoreText.text = scoreSystem.Score.ToString();
        UnlockCursor();
        Time.timeScale = 0;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
