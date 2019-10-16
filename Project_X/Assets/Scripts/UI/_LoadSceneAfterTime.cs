using UnityEngine;
using UnityEngine.SceneManagement;

public class _LoadSceneAfterTime : MonoBehaviour
{
    public float loadTime = 3.0f;
    public int sceneIndexToLoad;

	void Start ()
    {
        Invoke("LoadNewScene", loadTime);
	}
	
    public void LoadNewScene()
    {
        SceneManager.LoadScene(sceneIndexToLoad, LoadSceneMode.Single);
    }
}
