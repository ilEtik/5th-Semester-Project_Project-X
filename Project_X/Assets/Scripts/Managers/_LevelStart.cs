using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _LevelStart : MonoBehaviour
{
    public static _LevelStart levelStart;

    private void Awake()
    {
        if (levelStart == null)
        {
            levelStart = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (levelStart != this)
            Destroy(gameObject);
    }

    private GameObject player;
    private GameObject cam;
    private Vector3 playPos;
    private Quaternion playRot;
    private Quaternion camRot;
    public bool loading = false;
    private _UiManager ui;

    private void Start()
    {
        ui = _UiManager._UI;
        if (!loading)
            ui.SetLoadingScreen(false);
    }

    private void OnLevelWasLoaded(int level)
    {
        ui = _UiManager._UI;
        if (SceneManager.GetActiveScene().buildIndex == 1)
            Destroy(gameObject);
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Time.timeScale = 1;
            ui.CursorDisplayMode(false);
        }
    }

    public void LoadPlayerdata(Vector3 _playerPos, Quaternion _playerRot, Quaternion _camRot, float delay)
    {
        loading = true;
        playPos = _playerPos;
        playRot = _playerRot;
        camRot = _camRot;
        StartCoroutine(LoadData(delay));
    }

    IEnumerator LoadData(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        player = GameObject.FindGameObjectWithTag(_TagManager.tag_Player);
        cam = GameObject.FindGameObjectWithTag(_TagManager.tag_MainCamera);
        player.transform.position = playPos;
        player.transform.rotation = playRot;
        cam.transform.rotation = camRot;
        ui.CursorDisplayMode(false);
        yield return new WaitForSeconds(_delay);
        ui.SetLoadingScreen(false);
        loading = false;
    }
}
