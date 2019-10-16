using UnityEngine;

[CreateAssetMenu(fileName = "new Sliding Skill", menuName = "SciptiableObject/Attribute Effects/Skill Effects/Sliding Skill", order = 1)]
public class SlidingSkillEffect : AttributeEffect
{
    public override void ExecuteEffect()
    {
        FindObjectOfType<_PlayerController>().canSlide = true;
    }
}
