using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
