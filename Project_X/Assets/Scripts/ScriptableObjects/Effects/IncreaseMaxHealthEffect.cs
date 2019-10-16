using UnityEngine;

[CreateAssetMenu(fileName = "new Heal Effect", menuName = "SciptiableObject/Attribute Effects/Increase Maximum Health Effect")]
public class IncreaseMaxHealthEffect : AttributeEffect
{
    public int healthAmount;

    public override void ExecuteEffect()
    {
        FindObjectOfType<_PlayerStats>().IncreaseMaxHealth(healthAmount);
    }
}
