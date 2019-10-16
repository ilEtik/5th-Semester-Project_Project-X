using UnityEngine;

public class _MaterialsInv : MonoBehaviour
{
    [SerializeField] private ScriptableItem[] materialList;
    [SerializeField] private Transform matParrent;
    [SerializeField] private _ItemSlot[] matSlots;

    public int matSlotSpace = 25;

    private void Start()
    {
        if (matParrent != null)
            matSlots = matParrent.GetComponentsInChildren<_ItemSlot>();
        matSlotSpace = matSlots.Length;
        RefreshUI();
    }


    void RefreshUI()
    {
        int i = 0;
        for (; i < materialList.Length && i < matSlots.Length; i++)
        {
            matSlots[i].item = materialList[i].GetCopy();
            matSlots[i].curStack = 1;
        }
        for (; i < matSlots.Length; i++)
        {
            matSlots[i].item = null;
            matSlots[i].curStack = 0;
        }
    }

    public bool AddMaterial(ScriptableItem material)
    {
        for (int i = 0; i < matSlots.Length; i++)
        {
            if (matSlots[i].item == null || (matSlots[i].item.ID == material.ID && matSlots[i].curStack < material.maxStack))
            {
                matSlots[i].item = material;
                matSlots[i].curStack++;
                return true;
            }
        }
        return false;
    }

    public bool RemoveMaterial(ScriptableItem material)
    {
        for (int i = 0; i < matSlots.Length; i++)
        {
            if (matSlots[i].item == material)
            {
                matSlots[i].curStack--;
                if (matSlots[i].curStack == 0)
                    matSlots[i].item = null;
                matSlots[i].item = null;
                return true;
            }
        }
        return false;
    }

    public ScriptableItem RemoveMaterial(string id)
    {
        for (int i = 0; i < matSlots.Length; i++)
        {
            ScriptableItem collect = matSlots[i].item;
            if (collect != null && collect.ID == id)
            {
                matSlots[i].curStack--;
                if (matSlots[i].curStack == 0)
                    matSlots[i].item = null;
                return collect;
            }
        }
        return null;
    }

    public bool IsFull()
    {
        for (int i = 0; i < matSlots.Length; i++)
        {
            if (matSlots[i].item == null)
                return false;
        }
        return true;
    }

    public int ItemCount(string id)
    {
        int a = 0;
        for (int i = 0; i < matSlots.Length; i++)
        {
            if (matSlots[i].item.ID == id)
                a++;
        }
        return a;
    }
}
