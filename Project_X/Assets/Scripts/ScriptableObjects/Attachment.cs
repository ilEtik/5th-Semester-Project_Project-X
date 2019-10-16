using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Attachment", menuName = "SciptiableObject/Items/Attachment", order = 4)]
public class Attachment : ScriptableItem
{
    public AttachmentType attachmentType;
    public List<ScriptableWeapon> compatibleWeapons;
}

public enum AttachmentType
{
    Scope,
    Barrel,
    Magazine,
    Bodies,
    Stock
}
