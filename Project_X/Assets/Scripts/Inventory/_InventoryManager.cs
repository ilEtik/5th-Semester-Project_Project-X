using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InputManagment;
using TMPro;

public class _InventoryManager : MonoBehaviour
{
    public GameObject consumInv, materialInv, collectibleInv, invParrent;
    public List <Button> activeSlots;
    public Button[] allSlots;
    public Button consumButton, materialButton, collectibleButton, documentButton;
    public TextMeshProUGUI dropOneKeyText, dropSeveralKeyText, useKeyText, documentText;
    public GameObject dropOneObj, dropSeveralObj, useObj, documentObj;
    private int selectedSlot;
    private _UiManager ui;
    private _InputManager input;

    private void Start()
    {
        ui = _UiManager._UI;
        input = _InputManager._IM;
        UpdateKeyUI();
    }

    public void UpdateSlots()
    {
        allSlots = invParrent.GetComponentsInChildren<Button>();
        activeSlots.Clear();

        for (int i = 0; i < allSlots.Length; i++)
        {
            if (allSlots[i].enabled)
                activeSlots.Add(allSlots[i]);
        }

        for(int a = 0; a < activeSlots.Count; a++)
        {
            if (activeSlots[a].gameObject.GetComponent<_ItemSlot>().isSelected)
                selectedSlot = a;
        }

        if (selectedSlot < activeSlots.Count-1 && activeSlots.Count > 1)
            ui.SetNewEvent(activeSlots[++selectedSlot].gameObject);
        else if (selectedSlot == activeSlots.Count - 1 && activeSlots.Count > 1)
            ui.SetNewEvent(activeSlots[--selectedSlot].gameObject);
        else if (activeSlots.Count <= 1)
        {
            if (consumInv.activeSelf)
                ui.SetNewEvent(consumButton.gameObject);
            else if (materialInv.activeSelf)
                ui.SetNewEvent(materialButton.gameObject);
            else if (collectibleInv.activeSelf)
                ui.SetNewEvent(collectibleButton.gameObject);
        }
    }

    void UpdateKeyUI()
    {
        dropOneKeyText.text = input.deleteOneKey.ToString();
        dropSeveralKeyText.text = input.deleteSeveralKey.ToString();
        useKeyText.text = input.interactKey.ToString();
    }

    public void OpenDocument(DocumentItem document)
    {
        documentObj.SetActive(true);
        ui.SetNewEvent(documentButton.gameObject);
        documentText.text = document.Text;
    }

    public void CloseDocument()
    {
        documentObj.SetActive(false);
        ui.SetNewEvent(consumButton.gameObject);
        documentText.text = null;
    }
}
