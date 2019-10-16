using UnityEngine;

public class _CollectibleInv : MonoBehaviour
{
    #region Singleton
    public static _CollectibleInv colInv;

    private void Awake()
    {
        if (colInv == null)
            colInv = this;
    }
    #endregion

    [SerializeField] private ScriptableItem[] collectibleList;
    [SerializeField] private Transform colParrent;
    [SerializeField] private _ItemSlot[] colSlots;
    public int colListSpace = 25;

    private void Start()
    {
        if (colParrent != null)
            colSlots = colParrent.GetComponentsInChildren<_ItemSlot>();
        colListSpace = colSlots.Length;
        RefreshUI();
    }

    void RefreshUI()
    {
        int i = 0;
        for (; i < collectibleList.Length && i < colSlots.Length; i++)
        {
            colSlots[i].item = collectibleList[i].GetCopy();
            colSlots[i].curStack = 1;
        }

        for (; i < colSlots.Length; i++)
        {
            colSlots[i].item = null;
            colSlots[i].curStack = 0;
        }
    }

    public bool AddCollectible(ScriptableItem collect)
    {
        for (int i = 0; i < colSlots.Length; i++)
        {
            if (colSlots[i].item == null || (colSlots[i].item.ID == collect.ID && colSlots[i].curStack < collect.maxStack))
            {
                colSlots[i].item = collect;
                colSlots[i].curStack++;
                return true;
            }
        }
        return false;
    }

    public bool RemoveCollectible(ScriptableItem collect)
    {
        for (int i = 0; i < colSlots.Length; i++)
        {
            if (colSlots[i].item == collect)
            {
                colSlots[i].curStack--;
                if (colSlots[i].curStack == 0)
                    colSlots[i].item = null;
                colSlots[i].item = null;
                return true;
            }
        }
        return false;
    }

    public ScriptableItem RemoveCollectible(string id)
    {
        for (int i = 0; i < colSlots.Length; i++)
        {
            ScriptableItem collect = colSlots[i].item;
            if (collect != null && collect.ID == id)
            {
                colSlots[i].curStack--;
                if (colSlots[i].curStack == 0)
                    colSlots[i].item = null;
                return collect;
            }
        }
        return null;
    }

    public bool IsFull()
    {
        for (int i = 0; i < colSlots.Length; i++)
        {
            if (colSlots[i].item == null)
                return false;
        }
        return true;
    }

    public int ItemCount(string id)
    {
        int a = 0;
        for (int i = 0; i < colSlots.Length; i++)
        {
            if (colSlots[i].item.ID == id)
                a++;
        }
        return a;
    }
}
