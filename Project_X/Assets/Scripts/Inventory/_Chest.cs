using System.Collections.Generic;
using UnityEngine;
using InputManagment;
using TMPro;

public class _Chest : MonoBehaviour
{
    [SerializeField] private ScriptableItem[] items;
    [SerializeField] private _ChestSlot[] slots;
    [SerializeField] private GameObject chestUI;

    public bool isOpen = false;
    public bool isEmpty = false;
    public GameObject slotPref;
    public Transform slotTransform;

    private _PlayerMotor motor;
    private _ConsumInv conInv;
    private _MaterialsInv matInv;
    private _CollectibleInv colInv;
    [SerializeField] private TextMeshProUGUI takeAllKey, takeOneKey, closeKey;

	void Start()
    {
        motor = FindObjectOfType<_PlayerMotor>();
        conInv = FindObjectOfType<_ConsumInv>();
        matInv = FindObjectOfType<_MaterialsInv>();
        colInv = FindObjectOfType<_CollectibleInv>();
        if (items.Length <= 0)
            isEmpty = true;
    }

    void Update ()
    {
        if (Input.GetKey(_InputManager._IM.pauseKey) && isOpen)
            CloseChest();
        else if (Input.GetKeyUp(_InputManager._IM.takeAllKey) && isOpen)
            TakeAll();
        else if (Input.GetKey(_InputManager._IM.takeKey) && items.Length < 1 && isOpen)
            CloseChest();
        else if (Input.GetKey(_InputManager._IM.cancelKey) && isOpen)
            CloseChest();
      
	}

    public void OpenChest()
    {
        for (int i = 0; i < items.Length; i++)
            AddItem(items[i]);

        motor.canRot = false;
        motor.canMove = false;
        chestUI.SetActive(true);

        if (slots.Length > 0)
            _UiManager._UI.SetNewEvent(slots[0].gameObject);

        takeAllKey.text = _InputManager._IM.takeAllKey.ToString();
        takeOneKey.text = _InputManager._IM.takeKey.ToString();
        closeKey.text = _InputManager._IM.cancelKey.ToString();
        _UiManager._UI.chestActive = true;
        motor.pMenus.canPause = false;
        chestUI.SetActive(true);
        isOpen = true;
    }

    public void CloseChest()
    {
        if (slots.Length > 0)
        {
            foreach (_ChestSlot slot in slots)
                Destroy(slot.gameObject);
            slots = new _ChestSlot[0];
        }

        if (items.Length <= 0)
            isEmpty = true;

        motor.canRot = true;
        motor.canMove = true;
        _UiManager._UI.chestActive = false;
        chestUI.SetActive(false);
        isOpen = false;
        motor.pMenus.canPause = true;
    }

    void TakeAll()
    {
        foreach (ScriptableItem item in items)
        {
             if (item.itemType == ItemType.Consumable)
                 conInv.AddConsumable(item.GetCopy());
             else if (item.itemType == ItemType.Material)
                 matInv.AddMaterial(item.GetCopy());
             else if (item.itemType == ItemType.Collectible)
                 colInv.AddCollectible(item.GetCopy());
        }

        items = new ScriptableItem[0];
        CloseChest();
    }

    public void TakeOne(_ChestSlot slot, ScriptableItem item, int ind)
    {
        if (item.itemType == ItemType.Consumable)
            conInv.AddConsumable(item.GetCopy());
        else if (item.itemType == ItemType.Material)
            matInv.AddMaterial(item.GetCopy());
        else if (item.itemType == ItemType.Collectible)
            colInv.AddCollectible(item.GetCopy());

        if (slot.CurStack > 1)
            slot.CurStack--;
        else
        {
            Destroy(slot.gameObject);
            if (slots.Length > 1)
            {
                Resize(slot, item);

                if (ind < slots.Length)
                    _UiManager._UI.SetNewEvent(slots[ind].gameObject);
                else if (ind == slots.Length || ind > slots.Length)
                    _UiManager._UI.SetNewEvent(slots[ind - 1].gameObject);
            }
            else
            {
                Resize(slot, item);
                CloseChest();
            }
        }
    }

    void Resize(_ChestSlot slot, ScriptableItem item)
    {
        List<_ChestSlot> _slots = new List<_ChestSlot>(slots);
        _slots.Remove(slot);
        slots = _slots.ToArray();
        List<ScriptableItem> _items = new List<ScriptableItem>(items);
        _items.Remove(item);
        items = _items.ToArray();
        for (int i = 0; i < slots.Length; i++)
            slots[i].slotInd = i;
    }

    public bool AddItem(ScriptableItem item)
    {
        int targetInd = 0;
        if (slots.Length >= 1)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if(i > slots.Length)
                {
                    targetInd = 0;
                    return true;
                }

                if (slots[i].Item.ID == item.ID && slots[i].CurStack < item.maxStack)
                {
                    slots[i].CurStack++;
                    targetInd = 0;
                    return true;
                }
                else if (slots[i].Item.ID != item.ID || slots[i].CurStack >= item.maxStack )
                    targetInd = 1;
            }
            if(targetInd > 0)
            {
                AddSlot(item, slots.Length);
                return true;
            }
        }
        else if (slots.Length < 1)
        {
            AddSlot(item, 0);
            return true;
        }
        return false;
    }

    void AddSlot(ScriptableItem item, int i)
    {
        Instantiate(slotPref, slotTransform);
        slots = slotTransform.GetComponentsInChildren<_ChestSlot>();
        slots[i].Item = item;
        slots[i].CurStack++;
        slots[i].chest = this;
        slots[i].slotInd = i;
    }
}
