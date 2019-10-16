using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new Consumable", menuName = "SciptiableObject/Items/Usable Item", order = 2)]
public class UsableItem : ScriptableItem
{
    public bool isUseable = true;

    public List<AttributeEffect> effects;

    public virtual void Use()
    {
        foreach(AttributeEffect _effect in effects)
            _effect.ExecuteEffect();
    }

    public override ScriptableItem GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }
}

