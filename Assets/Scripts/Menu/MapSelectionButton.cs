using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectionButton : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void OnClick()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
