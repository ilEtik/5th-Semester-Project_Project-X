using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _SkillManager : MonoBehaviour
{
    public _SkillSlot[] skills;
    [SerializeField]private Image xpBar;
    [SerializeField]private TextMeshProUGUI curLvlNum, curXpNum, xpNeededNum, pointsNum;
    [SerializeField]private _PlayerStats player;

    private void Start()
    {
        skills = GetComponentsInChildren<_SkillSlot>();
        player = GameObject.FindGameObjectWithTag(_TagManager.tag_Player).GetComponent<_PlayerStats>();
    }

    public void UpdateUI()
    {
        foreach(_SkillSlot _skill in skills)
            _skill.UpdateIcon();

        xpBar.fillAmount = (player.curXp * 100 / player.xpNeeded) / 100f;
        curLvlNum.text = player.curLevel.ToString();
        curXpNum.text = player.curXp.ToString();
        xpNeededNum.text = player.xpNeeded.ToString();
        pointsNum.text = player.skillPoints.ToString();
    }
}
