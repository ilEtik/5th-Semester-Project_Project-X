using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using InputManagment;

public class _SkillSlot : MonoBehaviour, ISelectHandler, IDeselectHandler, IUpdateSelectedHandler
{
    public ScriptableSkill skill;
    private _PlayerStats stats;
    private _SkillManager manager;
    private Button button;
    [SerializeField] private GameObject activateSkillField, costTextField, skillNeededField;
    [SerializeField] private Image icon, highlightedImage, lockedImage, isActiveIcon;
    [SerializeField] private TextMeshProUGUI skillName, description, cost, activateSkillKey, skillNeededText;

    void Awake()
    {
        skill.isActive = false;
    }

    void Start()
    {
        manager = GetComponentInParent<_SkillManager>();
    }

    public void ActivateSkill()
    {
        skill.ActivateSkill();
        stats.skillPoints -= skill.cost;
        skill.isActive = true;
        manager.UpdateUI();
        costTextField.SetActive(false);
        isActiveIcon.enabled = true;
        activateSkillField.SetActive(false);
    }

    void IUpdateSelectedHandler.OnUpdateSelected(BaseEventData eventData)
    {
        if (Input.GetKeyDown(_InputManager._IM.interactKey) && CanSkill())
            ActivateSkill();
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        skillName.text = skill.skillName;
        description.text = skill.Description;
        cost.text = skill.cost.ToString();

        if (CanSkill())
        {
            activateSkillField.SetActive(true);
            activateSkillKey.text = _InputManager._IM.interactKey.ToString();
        }
        else if(!CanSkill() && !skill.skillNeeded.isActive)
        {
            skillNeededField.SetActive(true);
            skillNeededText.text = skill.skillNeeded.skillName;
        }
        if (skill.isActive)
        {
            cost.text = null;
            costTextField.SetActive(false);
            isActiveIcon.enabled = true;
        }
        else
        {
            cost.text = skill.cost.ToString();
            costTextField.SetActive(true);
        }
    }

    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        skillName.text = null;
        description.text = null;
        cost.text = null;
        isActiveIcon.enabled = false;
        activateSkillField.SetActive(false);
        skillNeededField.SetActive(false);
    }

    public void UpdateIcon()
    {
        stats = GameObject.FindGameObjectWithTag(_TagManager.tag_Player).GetComponent<_PlayerStats>();
        button = GetComponent<Button>();

        icon.sprite = skill.icon;

        if (CanSkill())
            highlightedImage.enabled = true;
        else
            highlightedImage.enabled = false;

        if (AbleToSkill())
        {
            lockedImage.enabled = true;
            button.interactable = false;
        }
        else
        {
            lockedImage.enabled = false;
            button.interactable = true;
        }
    }

    bool AbleToSkill()
    {
        if (skill.lvlNeeded > stats.curLevel && skill.skillNeeded != null && !skill.skillNeeded.isActive)
            return true;
        return false;
    }

    bool CanSkill()
    {
        if(stats.skillPoints >= skill.cost && stats.curLevel >= skill.lvlNeeded && !skill.isActive && skill.skillNeeded.isActive)
            return true;
        return false;
    }
}
