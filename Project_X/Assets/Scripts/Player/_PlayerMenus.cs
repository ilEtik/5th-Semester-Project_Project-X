using UnityEngine;
using InputManagment;

[RequireComponent(typeof(_PlayerMotor))]
public class _PlayerMenus : MonoBehaviour
{
    public bool canPause = true;
    public bool isPaused = false;
    public bool showPause = false;
    public bool showInventory = false;
    public bool showMap = false;
    public bool showWeapon = false;
    public bool showSkill = false;
    private bool showMenus;

    private _PlayerMotor motor;
    private _WeaponManager weaponManager;
    private _InputManager input;
    [SerializeField] private _SkillManager skillManager;
    [SerializeField] private GameObject HUD, menuParrent;
    [SerializeField] private GameObject pauseMenu, inventoryMenu, mapMenu, weaponMenu, skillMenu, gameOverScreen;
    [SerializeField] private GameObject pauseButton, inventoryButton, mapButton, weaponButton, skillButton, gameOverButton;

    private void Start()
    {
        motor = GetComponent<_PlayerMotor>();
        weaponManager = FindObjectOfType<_WeaponManager>();
        input = _InputManager._IM;
    }

    void Update ()
    {
        if (Input.GetKeyDown(input.pauseKey) && canPause)
            PauseMenu(false);
        else if (Input.GetKeyDown(input.inventoryKey) && canPause && !showPause)
            InventoryMenu(false);
        else if (Input.GetKeyDown(input.skillMenuKey) && canPause && !showPause)
            SkillMenu(false);
        // else if (Input.GetKeyDown(input.mapKey) && canPause && !showPause)
        //     MapMenu();
        // else if (Input.GetKeyDown(input.weaponMenuKey) && canPause && !showPause)
        //     WeaponMenu();

        if (Input.GetKeyDown(input.quickSaveKey))
            motor.pSaveAndLoad.Save();
        if (Input.GetKeyDown(input.quickLoadKey))
            motor.pSaveAndLoad.Load();
    }

    /// <summary>
    /// Pause or unpause the Game without open any Menus.
    /// </summary>
    void PauseGame()
    {
        isPaused = !isPaused;
        motor.canMove = !isPaused;
        motor.canRot = !isPaused;
        HUD.SetActive(!isPaused);

        if(!isPaused)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;

        if(weaponManager.curWeaponObj != null)
            weaponManager.curWeaponObj.GetComponent<_WeaponShoot>().shootable = !isPaused;

        _UiManager._UI.CursorDisplayMode(isPaused);

    }

    public void PauseMenu(bool openFromOtherMenu)
    {
        showPause = !showPause;
        if (!openFromOtherMenu)
        {
            showMenus = false;
            if (!isPaused || isPaused && !showPause)
                PauseGame();
            _UiManager._UI.SetNewEvent(pauseButton);
            pauseMenu.SetActive(showPause);
            if (showInventory)
                InventoryMenu(true);
            else if (showMap)
                MapMenu(true);
            else if (showWeapon)
                WeaponMenu(true);
            else if (showSkill)
                SkillMenu(true);
        }
        else
            pauseMenu.SetActive(showPause);
    }

    public void InventoryMenu(bool openFromOtherMenu)
    {
        showInventory = !showInventory;
        if (!openFromOtherMenu)
        {
            if (showInventory)
                showMenus = true;
            else
                showMenus = false;
            if (!isPaused || isPaused && !showInventory)
                PauseGame();
            _UiManager._UI.SetNewEvent(inventoryButton);
            menuParrent.SetActive(showMenus);
            inventoryMenu.SetActive(showInventory);
            if (showPause)
                PauseMenu(true);
            else if (showMap)
                MapMenu(true);
            else if (showWeapon)
                WeaponMenu(true);
            else if (showSkill)
                SkillMenu(true);
        }
        else
        {
            menuParrent.SetActive(showMenus);
            inventoryMenu.SetActive(showInventory);
        }

    }

    public void MapMenu(bool openFromOtherMenu)
    {
        showMap = !showMap;
        if (!openFromOtherMenu)
        {
            if (showMap)
                showMenus = true;
            else
                showMenus = false;
            if (!isPaused || isPaused && !showMap)
                PauseGame();
            _UiManager._UI.SetNewEvent(mapButton);
            menuParrent.SetActive(showMenus);
            mapMenu.SetActive(showMap);
            if (showPause)
                PauseMenu(true);
            else if (showInventory)
                InventoryMenu(true);
            else if (showWeapon)
                WeaponMenu(true);
            else if (showSkill)
                SkillMenu(true);
        }
        else
        {
            menuParrent.SetActive(showMenus);
            mapMenu.SetActive(showMap);
        }
    }

    public void WeaponMenu(bool openFromOtherMenu)
    {
        showWeapon = !showWeapon;
        if (!openFromOtherMenu)
        {
            if (showWeapon)
                showMenus = true;
            else
                showMenus = false;
            if (!isPaused || isPaused && !showWeapon)
                PauseGame();
            _UiManager._UI.SetNewEvent(weaponButton);
            menuParrent.SetActive(showMenus);
            weaponMenu.SetActive(showWeapon);
            if (showPause)
                PauseMenu(true);
            else if (showInventory)
                InventoryMenu(true);
            else if (showMap)
                MapMenu(true);
            else if (showSkill)
                SkillMenu(true);
        }
        else
        {
            menuParrent.SetActive(showMenus);
            weaponMenu.SetActive(showWeapon);
        }
    }

    public void SkillMenu(bool openFromOtherMenu)
    {
        showSkill = !showSkill;
        if (!openFromOtherMenu)
        {
            if (showSkill)
                showMenus = true;
            else
                showMenus = false;
            _UiManager._UI.SetNewEvent(skillButton);
            menuParrent.SetActive(showMenus);
            skillMenu.SetActive(showSkill);
            if (!isPaused || isPaused && !showSkill)
                PauseGame();
            if (showPause)
                PauseMenu(true);
            else if (showInventory)
                InventoryMenu(true);
            else if (showMap)
                MapMenu(true);
            else if (showWeapon)
                WeaponMenu(true);
        }
        else
        {
            menuParrent.SetActive(showMenus);
            skillMenu.SetActive(showSkill);
        }
        skillManager.UpdateUI();
    }

    public void Die()
    {
        _UiManager._UI.SetNewEvent(gameOverButton);
        canPause = false;
        gameOverScreen.SetActive(true);
    }
}
