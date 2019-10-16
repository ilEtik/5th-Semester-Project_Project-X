using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class _EnemeyStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth = 100;
    public int xpValue = 10;
    public int lvl = 1;
    public Image healthbar;
    public TMPro.TextMeshProUGUI lvlText;
    public List<GameObject> holes;

    private _EnemyShootManager shootManager;
    private GameObject player;
    private Animator anim;
    private _HashIDs hash;
    private _EnemySight sight;

    private void Start()
    {
        shootManager = GetComponent<_EnemyShootManager>();
        player = GameObject.FindGameObjectWithTag(_TagManager.tag_Player);
        anim = GetComponent<Animator>();
        hash = FindObjectOfType<_HashIDs>();
        sight = GetComponent<_EnemySight>();
        lvlText.text = lvl.ToString();
    }

    /// <summary>
    /// Aplies an amount of damage to the enemys health.
    /// </summary>
    /// <param name="damageAmount">The Amount of damage to apply.</param>
    public void GetDamage(int damageAmount)
    {
        curHealth -= damageAmount;
        UpdateHeathBar();
        sight.lastPlayerSight = player.transform.position;
        if (curHealth <= 0)
            Die();
    }

    /// <summary>
    /// Applies a heal amount to the enemy health.
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

    void UpdateHeathBar()
    {
        healthbar.fillAmount = (float) curHealth / maxHealth;
    }

    void Die()
    {

        anim.SetTrigger(hash.e_DyingTrigger);
        anim.SetBool(hash.e_PlayerInSightBool, false);
        anim.SetBool(hash.e_HasGunBool, false);
        player.GetComponent<_PlayerStats>().AddXP(xpValue);
        Destroy(GetComponentInChildren<Canvas>().gameObject);
        Destroy(GetComponent<SphereCollider>());
        Destroy(GetComponentInChildren<CapsuleCollider>());
        shootManager.Die();
        Destroy(shootManager);
        Destroy(GetComponent<_EnemySight>());
        Destroy(GetComponent<_EnemyWalk>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(gameObject, 15f);
        foreach (GameObject hole in holes)
            hole.SetActive(false);
        Destroy(this);
    }
}
