using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MapSelectionButton : MonoBehaviour
{
    [SerializeField] private SceneAsset scene;

    public void OnClick()
    {
        SceneManager.LoadScene(scene.name);
    }
}
