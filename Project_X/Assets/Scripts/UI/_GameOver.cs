using UnityEngine;
using UnityEngine.SceneManagement;

public class _GameOver : MonoBehaviour
{
    public void RestLevel()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
