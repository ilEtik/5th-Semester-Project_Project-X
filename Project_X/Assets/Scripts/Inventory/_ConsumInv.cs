using UnityEngine;

public class _ConsumInv : MonoBehaviour
{
    [SerializeField]private ScriptableItem[] consumableList;
    [SerializeField]private Transform conParent;
    [SerializeField]private _ItemSlot[] conSlots;

    public int conListSpace = 25;

    private void Start()
    {
        if(conParent != null)
            conSlots = conParent.GetComponentsInChildren<_ItemSlot>();
        conListSpace = conSlots.Length;
        RefreshUI();
    }

    void RefreshUI()
    {
        int i = 0;
        for (; i < consumableList.Length && i < conSlots.Length; i++)
        {
            conSlots[i].item = consumableList[i].GetCopy();
            conSlots[i].curStack = 1;
        }
        for (; i < conSlots.Length; i++)
        {
            conSlots[i].item = null;
            conSlots[i].curStack = 0;
        }
    }

    public bool AddConsumable(ScriptableItem consum)
    {
        for(int i = 0; i < conSlots.Length; i++)
        {
            if (conSlots[i].item == null ||(conSlots[i].item.ID == consum.ID && conSlots[i].curStack < consum.maxStack))
            {
                conSlots[i].item = consum;
                conSlots[i].curStack++;
                return true;
            }
        }
        return false;
    }

    public bool RemoveConsumable(ScriptableItem consum)
    {
        for (int i = 0; i < conSlots.Length; i++)
        {
            if (conSlots[i].item == consum)
            {
                conSlots[i].curStack--;
                if (conSlots[i].curStack == 0)
                    conSlots[i].item = null;
                conSlots[i].item = null;
                return true;
            }
        }
        return false;
    }

    public ScriptableItem RemoveConsum(string id)
    {
        for (int i = 0; i < conSlots.Length; i++)
        {
            ScriptableItem consum = conSlots[i].item;
            if (consum != null && consum.ID == id)
            {
                conSlots[i].curStack--;
                if (conSlots[i].curStack == 0)
                    conSlots[i].item = null;
                return consum;
            }
        }
        return null;
    }

    public bool IsFull()
    {
        for (int i = 0; i < conSlots.Length; i++)
        {
            if (conSlots[i].item == null)
                return false;
        }
        return true;
    }

    public int ItemCount(string id)
    {
        int a = 0;

        for(int i = 0; i < conSlots.Length; i++)
        {
            if(conSlots[i].item.ID == id)
                a++;
        }
        return a;
    }

}
