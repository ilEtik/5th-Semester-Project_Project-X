using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _HUD : MonoBehaviour
{
    public static _HUD hud;

    private void Awake()
    {
        if (hud == null)
            hud = this;
        else if (hud != this)
            Destroy(gameObject);
    }

    public Image primaryWeapon;
    public Image secondaryWeapon;
    public TextMeshProUGUI magazinAmmo;

    public void UpdateAmmo(int _currentAmmo)
    {
        magazinAmmo.text = _currentAmmo.ToString();
    }

    public void UpdateWeapon(Sprite _primaryWeapon, Sprite _secondaryWeapon)
    {
        primaryWeapon.sprite = _primaryWeapon;
        secondaryWeapon.sprite = _secondaryWeapon;
    }
}
