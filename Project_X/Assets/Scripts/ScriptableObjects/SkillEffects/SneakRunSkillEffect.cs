using UnityEngine;

[CreateAssetMenu(fileName = "new Sneak Run Skill Effect", menuName = "SciptiableObject/Attribute Effects/Skill Effects/Sneak Run Effect", order = 2)]
public class SneakRunSkillEffect : AttributeEffect
{
    public float crouchSpeedMultiplicator;

    public override void ExecuteEffect()
    {
        FindObjectOfType<_PlayerMotor>().crouchSpeed *= crouchSpeedMultiplicator;
    }
}
