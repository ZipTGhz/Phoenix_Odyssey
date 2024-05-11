using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeDetector : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneController.Instance.ChangeScene(scene.buildIndex);
    }
}
