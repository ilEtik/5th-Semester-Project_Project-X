using UnityEngine;

[CreateAssetMenu(fileName = "new Document", menuName = "SciptiableObject/Items/Document Item", order = 3)]
public class DocumentItem : ScriptableItem
{
    [TextArea]public string Text;

    public override ScriptableItem GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }
}
