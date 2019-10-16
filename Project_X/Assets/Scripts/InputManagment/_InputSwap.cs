using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace InputManagment
{
    public class _InputSwap : MonoBehaviour
    {
        public TextMeshProUGUI buttonText;
        public string key;
        public GameObject waitForNewKeyPanel;
        private KeyCode currentKeyCode;
        private bool isSwaping;
        System.Reflection.FieldInfo fi;
        Type t = typeof(PC_Inputs);

        private void Start()
        {
            GetKeyName();
        }

        public void GetKeyName()
        {
            fi = t.GetField(key);
            var code = fi.GetValue(_InputManager._IM.pc_Inputs);
            currentKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), code.ToString());
            buttonText.text = currentKeyCode.ToString();
        }

        public void ActivateKeySwap()
        {
            isSwaping = true;
        }

        private void Update()
        {
            if (isSwaping)
                StartCoroutine(SwapKey());
            else
                StopCoroutine(SwapKey());
        }

        IEnumerator SwapKey()
        {
            _UiManager._UI.SetNewEvent(null);
            waitForNewKeyPanel.SetActive(true);
            yield return new WaitForSeconds(.001f);
            foreach (KeyCode newKeycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(newKeycode))
                {
                    fi = t.GetField(key);
                    fi.SetValue(_InputManager._IM.pc_Inputs, newKeycode);
                    currentKeyCode = newKeycode;
                    GetKeyName();
                    isSwaping = false;
                    _InputSaveLoad.saveLoad.SaveInputs();
                    _UiManager._UI.SetNewEvent(GetComponentInChildren<Button>().gameObject);
                    waitForNewKeyPanel.SetActive(false);
                }
            }
        }
    }
}
