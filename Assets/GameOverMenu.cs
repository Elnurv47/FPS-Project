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

    private void Start()
    {
        player.OnDied += Player_OnDied;
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(currentMapScene.name);
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene(mainMenuScene.name);
    }

    public void Player_OnDied()
    {
        gameObject.SetActive(true);
        yourScoreText.text = scoreSystem.Score.ToString();
    }
}
