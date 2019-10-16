using UnityEngine;
using InputManagment;

public class _InputUI : MonoBehaviour
{
    public GameObject pcInputSettings, pS4InputSettings, xboxInputSettings;

    public void UpdateInputUI()
    {
        switch (_InputManager._IM.connectedGamepad)
        {
            case ConnectedGamepad.Mouse_Keyboard:
                pcInputSettings.SetActive(true);
                pS4InputSettings.SetActive(false);
                xboxInputSettings.SetActive(false);
                break;
            case ConnectedGamepad.PS4_Gamepad:
                pS4InputSettings.SetActive(true);
                pcInputSettings.SetActive(false);
                xboxInputSettings.SetActive(false);
                break;
            case ConnectedGamepad.Xbox_Gamepad:
                xboxInputSettings.SetActive(true);
                pcInputSettings.SetActive(false);
                pS4InputSettings.SetActive(false);
                break;
        }
    }
}

