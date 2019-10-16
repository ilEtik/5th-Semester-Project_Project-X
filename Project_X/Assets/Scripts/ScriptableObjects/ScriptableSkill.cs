using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Skill", menuName = "SciptiableObject/Skill", order = 4)]
public class ScriptableSkill : ScriptableObject
{
    public string skillName = "new Skill";
    [TextArea]public string Description;
    public int lvlNeeded = 1;
    public int cost = 1;
    public bool isActive = false;
    public Sprite icon;
    public ScriptableSkill skillNeeded;
    public List<AttributeEffect> effects;

    public virtual void ActivateSkill()
    {
        foreach(AttributeEffect _effect in effects)
            _effect.ExecuteEffect();
    }
}
