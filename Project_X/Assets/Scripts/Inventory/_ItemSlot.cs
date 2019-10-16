using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using InputManagment;

public class _ItemSlot : MonoBehaviour, ISelectHandler, IDeselectHandler, IUpdateSelectedHandler
{
    private ScriptableItem _item;
    public ScriptableItem item
    {
        get { return _item; }
        set{
            _item = value;
            if (_item == null)
            {
                icon.enabled = false;
                stackCount.enabled = false;
                if(slotButton != null)
                    slotButton.enabled = false;
            }
            else
            {
                icon.sprite = _item.icon;
                icon.enabled = true;
                if(slotButton != null)
                    slotButton.enabled = true;
            }
        }
    }

    private int _curStack;
    public int curStack
    {
        get { return _curStack; }
        set
        {
            _curStack = value;
            stackCount.enabled = _item != null && _item.maxStack > 1 && _curStack > 1;
            if (stackCount.enabled)
                stackCount.text = _curStack.ToString();
        }
    }

    private Button _slotButton;
    public Button slotButton
    {
        get { return _slotButton; }
        set
        {
            _slotButton = value;
            if (invType.activeInHierarchy && _item != null)
                slotButton.enabled = true;
            else
                slotButton.enabled = false;
        }
    }

    public bool isSelected;
    private _InventoryManager manager;
    private Transform player;
    [SerializeField] private Image icon, desIcon;
    [SerializeField] private TextMeshProUGUI stackCount, desText;
    [SerializeField] private GameObject invType;
    private GameObject dropOneObj, dropSeveralObj, useObj;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(_TagManager.tag_Player).transform;
        slotButton = GetComponent<Button>();
        manager = GetComponentInParent<_InventoryManager>();
        dropOneObj = manager.dropOneObj;
        dropSeveralObj = manager.dropSeveralObj;
        useObj = manager.dropSeveralObj;
    }

    public void FillSlot(ScriptableItem _item)
    {
        item = _item;
    }

    public void ClearSlot()
    {
        item = null;
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        desIcon.enabled = true;
        desIcon.sprite = item.icon;
        desText.enabled = true;
        desText.text = item.description;

        if (item != null)
            dropOneObj.SetActive(true);
        if (curStack > 1)
            manager.dropSeveralObj.SetActive(true);
        if (item != null && item is UsableItem || item != null && item is DocumentItem)
            manager.useObj.SetActive(true);
    }

    void IUpdateSelectedHandler.OnUpdateSelected(BaseEventData eventData)
    {
        if(Input.GetKeyDown(_InputManager._IM.interactKey))
        {
            if (item is UsableItem)
            {
                UsableItem usableItem = (UsableItem)item;
                if (usableItem.isUseable)
                {
                    usableItem.Use();
                    DropOneItem();
                }
            }
            else if(item is DocumentItem)
            {
                DocumentItem document = (DocumentItem)item;
                manager.OpenDocument(document);
            }
        }
        else if (Input.GetKeyDown(_InputManager._IM.deleteOneKey))
            DropOneItem();
        else if (Input.GetKeyDown(_InputManager._IM.deleteSeveralKey) && curStack > 1)
        {
            for (int i = -1; i < curStack + 1; i++)
                Drop();

            manager.UpdateSlots();
            item = null;
        }
    }

    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        desIcon.enabled = false;
        desIcon.sprite = null;
        desText.enabled = false;
        desText.text = null;
        dropOneObj.SetActive(false);
        manager.dropSeveralObj.SetActive(false);
        manager.useObj.SetActive(false);
    }

    void DropAll()
    {
        for (int i = 0; i < curStack; i++)
            Drop();

        manager.UpdateSlots();
        item = null;
    }

    void DropOneItem()
    {
        Drop();
        if(curStack < 1)
        {
            manager.UpdateSlots();
            item = null;
        }
    }

    void Drop()
    {
        curStack--;
        var obj = Instantiate(item.dropObj, player.position + Vector3.forward, player.rotation);
        obj.GetComponent<_PickUp>().item = item;
        obj.name = item.itemName;
    }
}
