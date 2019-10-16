using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using InputManagment;

public class _WeaponWheelSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image weaponIcon;
    public Sprite emptyIcon;
    public ScriptableWeapon weapon;

    private Button button;
    private bool isHighlight = false;

    void Start ()
    {
        button = GetComponent<Button>();
        UpdateUI();
	}

    void Update()
    {
        if (Input.GetKeyUp(_InputManager._IM.weaponWheelKey) && isHighlight)
        {
            button.onClick.Invoke();
            isHighlight = false;
        }
    }

    public void UpdateUI()
    {
        if(weapon != null)
            FillSlotUI();
        else
            ClearSlotUI();
    }

    void FillSlotUI()
    {
        weaponIcon.sprite = weapon.weaponIcon;
    }

    void ClearSlotUI()
    {
        weaponIcon.sprite = emptyIcon;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        isHighlight = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        isHighlight = false;
    }
}
