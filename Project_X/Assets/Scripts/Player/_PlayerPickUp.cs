using UnityEngine;
using TMPro;
using InputManagment;
using UnityEngine.UI;

public class _PlayerPickUp : MonoBehaviour
{
    public float radius = 40f;
    public float maxDis = 5f;
    public int targetSlotInd;
    public GameObject pickUpUI, holdTimeUI;
    public TextMeshProUGUI text;
    public LayerMask layerMask;
    public Image holdTimeImage;

    private GameObject hitObj;
    private _WeaponManager manager;
    private _ConsumInv conInv;
    private _MaterialsInv matInv;
    private _CollectibleInv colInv;
    private _InputManager input;

	void Start ()
    {
        manager = FindObjectOfType<_WeaponManager>();
        conInv = FindObjectOfType<_ConsumInv>();
        matInv = FindObjectOfType<_MaterialsInv>();
        colInv = FindObjectOfType<_CollectibleInv>();
        input = _InputManager._IM;
	}
	
	void Update ()
    {
        if(Time.timeScale != 0)
            CanPickUp();
	}

    void CanPickUp()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, radius, transform.forward, maxDis, layerMask, QueryTriggerInteraction.UseGlobal);

        if (hit.Length > 0)
        {
            hitObj = hit[0].collider.gameObject;
            pickUpUI.SetActive(true);
            text.text = hitObj.name;

            if (Input.GetKeyDown(input.interactKey) && input.connectedGamepad == ConnectedGamepad.Mouse_Keyboard)
            {
                if (hitObj.tag == _TagManager.tag_Weapon)     //Weapon
                {
                    GetSlotInd();
                    manager.pickedWeapons[targetSlotInd] = hitObj.GetComponent<_PickUp>().weapon;
                    Destroy(hitObj);
                    manager.UpdateWeaponWheel();
                    manager.ChangeWeapon();
                }
                else if (hitObj.tag == _TagManager.tag_Chest && !hitObj.GetComponent<_Chest>().isOpen && !hitObj.GetComponent<_Chest>().isEmpty)
                    hitObj.GetComponent<_Chest>().OpenChest();
                else if (hitObj.tag == _TagManager.tag_Item)
                {
                    ScriptableItem item = hitObj.GetComponent<_PickUp>().item;
                    Destroy(hitObj);
                    if (item.itemType == ItemType.Consumable)
                        conInv.AddConsumable(item.GetCopy());
                    else if (item.itemType == ItemType.Material)
                        matInv.AddMaterial(item.GetCopy());
                    else if (item.itemType == ItemType.Collectible)
                        colInv.AddCollectible(item.GetCopy());
                }
            }
            else if(Input.GetKeyDown(input.interactKey) && input.connectedGamepad == ConnectedGamepad.PS4_Gamepad || input.connectedGamepad == ConnectedGamepad.Xbox_Gamepad)
            {
                holdTimeUI.SetActive(true);
                float holdTime =+ Time.deltaTime;
                holdTimeImage.fillAmount = holdTime / input.holdTime;

                if(holdTime > input.holdTime)
                {
                    if (hitObj.tag == _TagManager.tag_Weapon)     //Weapon
                    {
                        GetSlotInd();
                        manager.pickedWeapons[targetSlotInd] = hitObj.GetComponent<_PickUp>().weapon;
                        manager.UpdateWeaponWheel();
                        manager.ChangeWeapon();
                    }
                    else if (hitObj.tag == _TagManager.tag_Chest && !hitObj.GetComponent<_Chest>().isOpen && !hitObj.GetComponent<_Chest>().isEmpty)
                        hitObj.GetComponent<_Chest>().OpenChest();

                    holdTimeUI.SetActive(false);
                }
            }
        }
        else
        {
            hitObj = null;
            text.text = null;
            pickUpUI.SetActive(false);
        }
    }

    void GetSlotInd()
    {
        if(manager.pickedWeapons[0] == null)
            targetSlotInd = 0;
        else if(manager.pickedWeapons[1] == null)
            targetSlotInd = 1;
        else if (manager.pickedWeapons[2] == null)
            targetSlotInd = 2;
        else if (manager.pickedWeapons[3] == null)
            targetSlotInd = 3;
        else
            targetSlotInd = manager.curSlotInd;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + transform.forward * maxDis);
        Gizmos.DrawWireSphere(transform.position + transform.forward * maxDis, radius);
    }
}
