using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class _UiManager : MonoBehaviour
{
    #region Singleton
    public static _UiManager _UI;

    private void Awake()
    {
        if (_UI == null)
        {
            DontDestroyOnLoad(gameObject);
            _UI = this;
        }
        else if (_UI != this)
            Destroy(gameObject);
    }
    #endregion

    public bool chestActive = false;
    public EventSystem eventSystem;
    public GameObject loadingScreen;

    public void Start()
    {
        CursorDisplayMode(false);
    }

    /// <summary>
    /// Set the visibility and lock the Cursor.
    /// </summary>
    /// <param name="showCursor">If the cursor is visible or not.</param>
    public void CursorDisplayMode(bool showCursor)
    {
        Cursor.visible = showCursor;

        if (!showCursor)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Set the new Selected Gameobject(Button) of the Eventsystem.
    /// </summary>
    /// <param name="newEvent">The object to select.</param>
    public void SetNewEvent( GameObject newEvent )
    {
        if (newEvent != null)
        {
            eventSystem.SetSelectedGameObject(newEvent, new BaseEventData(eventSystem));
            newEvent.GetComponent<Button>().OnSelect(new BaseEventData(eventSystem));
        }
        else
            eventSystem.SetSelectedGameObject(null);
    }

    public void SetLoadingScreen(bool isLoading)
    {
        loadingScreen.SetActive(isLoading);
    }
}
