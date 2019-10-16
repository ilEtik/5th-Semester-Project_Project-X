using UnityEngine;
using UnityEngine.SceneManagement;
using InputManagment;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class _MainMenu : MonoBehaviour
{
    public int sceneInd;
    private _UiManager ui;

    private void Start()
    {
        ui = _UiManager._UI;
        ui.SetNewEvent(GetComponentInChildren<Button>().gameObject);

        if (_InputManager._IM.connectedGamepad == ConnectedGamepad.Mouse_Keyboard)
            ui.CursorDisplayMode(true);
        else
            ui.CursorDisplayMode(false);
    }

    public void StartGame()
    {
        ui.SetLoadingScreen(true);
        ui.CursorDisplayMode(false);
        SceneManager.GetSceneByBuildIndex(sceneInd);
        SceneManager.LoadScene(sceneInd, LoadSceneMode.Single);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
