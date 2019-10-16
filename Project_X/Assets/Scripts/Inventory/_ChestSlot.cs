using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _ChestSlot : MonoBehaviour
{
    private ScriptableItem _Item;
    public ScriptableItem Item
    {
        get { return _Item; }
        set
        {
            _Item = value;
            if (_Item == null)
            {
                icon.enabled = false;
                stackCount.enabled = false;
            }
            else
            {
                icon.sprite = _Item.icon;
                icon.enabled = true;
            }
        }
    }

    public int _CurStack;
    public int CurStack
    {
        get { return _CurStack; }
        set
        {
            _CurStack = value;
            stackCount.enabled = _Item != null && _Item.maxStack > 1 && _CurStack > 1;
            if (stackCount.enabled)
                stackCount.text = _CurStack.ToString();
        }
    }

    public Image icon;
    public TextMeshProUGUI stackCount;
    public int slotInd;
    public _Chest chest;

    public void TakeItem()
    {
        chest.TakeOne(this, Item, slotInd);
    }
}
