using UnityEngine;

[CreateAssetMenu(fileName = "new Heal Effect", menuName = "SciptiableObject/Attribute Effects/Heal Effect")]
public class HealAttributeEffect : AttributeEffect
{
    public int healAmount;
    
    public override void ExecuteEffect()
    {
        FindObjectOfType<_PlayerStats>().AddHeal(healAmount);
    }
}
