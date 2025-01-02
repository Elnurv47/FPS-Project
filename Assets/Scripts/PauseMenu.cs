using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private SceneAsset mainMenuScene;

    public Action<bool> OnPauseStateChanged;

    public void OnResumeButtonClick()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        LockCursor();
        OnPauseStateChanged?.Invoke(false);
    }

    public void OnMainMenuButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuScene.name);
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        UnlockCursor();
        OnPauseStateChanged?.Invoke(true);
        Time.timeScale = 0;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
