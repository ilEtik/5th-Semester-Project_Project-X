using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(fileName = "new Item", menuName = "SciptiableObject/Items/Item", order = 1)]
public class ScriptableItem : ScriptableObject
{
    [SerializeField] private string id;
    public string ID { get { return id; } }
    public string itemName = "new Item";
    public Sprite icon;
    [TextArea] public string description;

    public Rarity rarity;
    public ItemType itemType;
    [Range(1, 500)]
    public int maxStack = 1;
    public GameObject dropObj;

#if UNITY_EDITOR
    private void Awake()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif

    public virtual ScriptableItem GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {
    }
}

public enum ItemType
{
    Consumable,
    Ammunition,
    Attachment,
    Material,
    Collectible,
    Other
}

public enum Rarity
{
    Normal,
    Rare,
    Legendary,
    Unique
}
