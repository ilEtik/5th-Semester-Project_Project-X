using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(_PlayerMotor))]
public class _PlayerStats : MonoBehaviour
{
    #region Variables
    [Header("Main Player Stats")]
    public int maxHealth = 100;
    public int curHealth = 100;
    public int curLevel = 1;
    public int curXp = 0;
    public int xpNeeded = 100;
    public int skillPoints = 0;
    public float speed = 1f;
    public float breathTime = 3f;
    public bool isDead = false;
    public Image healthbar;
    public GameObject gameOverScreen;

    private _HashIDs hash;
    private _PlayerMotor motor;
    private _WeaponManager manager;
    #endregion

    void Start ()
    {
        hash = FindObjectOfType<_HashIDs>();
        motor = GetComponent<_PlayerMotor>();
        manager = FindObjectOfType<_WeaponManager>();
        UpdateHeathBar();
    }

    /// <summary>
    /// Applies a damage amount to the players health.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to apply.</param>
    public void GetDamage(int damageAmount)
    {
        curHealth -= damageAmount;
        UpdateHeathBar();
        if (curHealth <= 0)
            Die();
    }

    /// <summary>
    /// Applies a heal amount to the players health.
    /// </summary>
    /// <param name="healAmount">The amount of heal to apply.</param>
    public void AddHeal(int healAmount)
    {
        if (maxHealth <= curHealth)
            return;
        else if (maxHealth - curHealth > healAmount)
            curHealth += healAmount;
        else if (maxHealth - curHealth < healAmount)
            curHealth += (maxHealth - curHealth);
        UpdateHeathBar();
    }

    /// <summary>
    /// Increases the maximum health of the player.
    /// </summary>
    /// <param name="healthAmount">The amount of health to increase.</param>
    public void IncreaseMaxHealth(int healthAmount)
    {
        maxHealth += healthAmount;
        curHealth += healthAmount;
        UpdateHeathBar();
    }

    /// <summary>
    /// Applies an amount of experience to the players xp.
    /// </summary>
    /// <param name="xpAmount">The amount of experience to apply.</param>
    public void AddXP(int xpAmount)
    {
        curXp += xpAmount;
        if (curXp >= xpNeeded)
        {
            int overlappedXp;
            overlappedXp = 0 + (curXp - xpNeeded);
            LevelUp(overlappedXp);
        }
    }

    void LevelUp(int _overlappedXp)
    {
        curLevel++;
        curXp = _overlappedXp;
        xpNeeded = Mathf.FloorToInt(xpNeeded * 1.75f);
        skillPoints++;
    }
    
    void UpdateHeathBar ()
    {
        healthbar.fillAmount = (float) curHealth /maxHealth;
    }
    
    void Die()
    {
        GetComponent<Animator>().SetTrigger(hash.p_DyingTrigger);
        motor.canMove = false;
        motor.canRot = false;
        manager.DropWeapon();
        Destroy(manager);
        Destroy(GetComponentInChildren<_PlayerPickUp>());
        isDead = true;
        motor.pMenus.Die();
    }
}
