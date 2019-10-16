using UnityEngine;
using System;

namespace InputManagment
{
    [Serializable]
    public class _InputManager : MonoBehaviour
    {
        #region Singleton
        public static _InputManager _IM;

        void Awake()
        {
            if (_IM == null)
            {
                DontDestroyOnLoad(gameObject);
                _IM = this;
            }
            else if (_IM != this)
                Destroy(gameObject);

        }
        #endregion

        #region Variables
        [Header("Movement")]
        public string movementXAxis;
        public string movementZAxis;
        public string camXAxis;
        public string camYAxis;
        [Space(5)]
        public KeyCode jumpKey;
        public KeyCode crouchKey;
        public KeyCode layKey;
        public KeyCode sprintKey;
        [Header("Interactions")]
        public string swapWeaponAxis;
        public KeyCode interactKey;
        public KeyCode shootKey;
        public KeyCode aimKey;
        public KeyCode swapWeaponKey;
        public KeyCode weaponWheelKey;
        public KeyCode reloadKey;
        public KeyCode holdBreathKey;
        public KeyCode dropWeaponKey;
        [Header("Menus")]
        public string menuXAxis;
        public string menuYAxis;
        [Space(5)]
        public KeyCode pauseKey;
        public KeyCode mapKey;
        public KeyCode inventoryKey;
        public KeyCode weaponMenuKey;
        public KeyCode skillMenuKey;
        public KeyCode missionsKey;
        public KeyCode takeAllKey;
        public KeyCode takeKey;
        public KeyCode deleteOneKey;
        public KeyCode deleteSeveralKey;
        public KeyCode submitKey;
        public KeyCode cancelKey;
        public KeyCode quickSaveKey;
        public KeyCode quickLoadKey;
        [Space(10)]
        public float holdTime = 15f;
        #endregion

        [Header("Connected Controller")]
        public ConnectedGamepad connectedGamepad;

        public PC_Inputs pc_Inputs = new PC_Inputs();
        public PS4_Inputs ps4_Inputs = new PS4_Inputs();
        public XBOX_Inputs xbox_Inputs = new XBOX_Inputs();

        #region Start/Update
        private int ps4_Gamepad = 0;
        private int xbox_Gamepad = 0;

        private void Start()
        {
            _InputSaveLoad.saveLoad.LoadInputs();
            ChangeInputState();
        }

        void Update()
        {
            string[] names = Input.GetJoystickNames();
            for (int x = 0; x < names.Length; x++)
            {
                if (names[x].Length == 19)
                {
                    ps4_Gamepad = 1;
                    xbox_Gamepad = 0;
                }
                if (names[x].Length == 33 || names[x].Length == 51)
                {
                    ps4_Gamepad = 0;
                    xbox_Gamepad = 1;
                }
            }

            if (ps4_Gamepad == 1 && xbox_Gamepad == 0)
            {
                connectedGamepad = ConnectedGamepad.PS4_Gamepad;
                ps4_Gamepad = 0;
            }
            else if (xbox_Gamepad == 1 && ps4_Gamepad == 0)
            {
                connectedGamepad = ConnectedGamepad.Xbox_Gamepad;
                xbox_Gamepad = 0;
            }
            else if (ps4_Gamepad == 1 && xbox_Gamepad == 1)
            {
                connectedGamepad = ConnectedGamepad.PS4_Gamepad;
                ps4_Gamepad = 0;
                xbox_Gamepad = 0;
            }
            else
                connectedGamepad = ConnectedGamepad.Mouse_Keyboard;

            if (Input.anyKey)
                ChangeInputState();
            if (Input.GetAxisRaw(movementXAxis) != 0 || Input.GetAxisRaw(movementZAxis) != 0 || Input.GetAxisRaw(camXAxis) != 0 || Input.GetAxisRaw(camYAxis) != 0)
                ChangeInputState();
        }
        #endregion

        #region ChangeInputState
        public void ChangeInputState()
        {
            switch (connectedGamepad)
            {
                case ConnectedGamepad.Mouse_Keyboard:
                    movementXAxis = pc_Inputs.movementXAxis;
                    movementZAxis = pc_Inputs.movementZAxis;
                    camXAxis = pc_Inputs.camXAxis;
                    camYAxis = pc_Inputs.camYAxis;
                    menuXAxis = pc_Inputs.menuXAxis;
                    menuYAxis = pc_Inputs.menuYAxis;
                    jumpKey = pc_Inputs.jumpKey;
                    crouchKey = pc_Inputs.crouchKey;
                    layKey = pc_Inputs.layKey;
                    sprintKey = pc_Inputs.sprintKey;
                    swapWeaponAxis = pc_Inputs.swapWeaponAxis;
                    weaponWheelKey = pc_Inputs.weaponWheelKey;
                    interactKey = pc_Inputs.interactKey;
                    shootKey = pc_Inputs.shootKey;
                    aimKey = pc_Inputs.aimKey;
                    reloadKey = pc_Inputs.reloadKey;
                    holdBreathKey = pc_Inputs.holdBreathKey;
                    dropWeaponKey = pc_Inputs.dropWeaponKey;
                    pauseKey = pc_Inputs.pauseKey;
                    mapKey = pc_Inputs.mapKey;
                    inventoryKey = pc_Inputs.inventoryKey;
                    weaponMenuKey = pc_Inputs.weaponMenuKey;
                    skillMenuKey = pc_Inputs.skillMenuKey;
                    missionsKey = pc_Inputs.missionsKey;
                    takeAllKey = pc_Inputs.takeAllKey;
                    takeKey = pc_Inputs.takeKey;
                    deleteOneKey = pc_Inputs.deleteOneKey;
                    deleteSeveralKey = pc_Inputs.deleteSeveralKey;
                    submitKey = pc_Inputs.submitKey;
                    cancelKey = pc_Inputs.cancelKey;
                    quickSaveKey = pc_Inputs.quickSaveKey;
                    quickLoadKey = pc_Inputs.quickLoadKey;
                    break;
                case ConnectedGamepad.PS4_Gamepad:
                    movementXAxis = ps4_Inputs.movementXAxis;
                    movementZAxis = ps4_Inputs.movementZAxis;
                    camXAxis = ps4_Inputs.camXAxis;
                    camYAxis = ps4_Inputs.camYAxis;
                    menuXAxis = ps4_Inputs.menuXAxis;
                    menuYAxis = ps4_Inputs.menuYAxis;
                    jumpKey = ps4_Inputs.jumpKey;
                    crouchKey = ps4_Inputs.crouchKey;
                    sprintKey = ps4_Inputs.sprintKey;
                    swapWeaponAxis = ps4_Inputs.swapWeaponAxis;
                    weaponWheelKey = ps4_Inputs.weaponWheelKey;
                    interactKey = ps4_Inputs.interactKey;
                    shootKey = ps4_Inputs.shootKey;
                    aimKey = ps4_Inputs.aimKey;
                    swapWeaponKey = ps4_Inputs.swapWeaponKey;
                    reloadKey = ps4_Inputs.reloadKey;
                    holdBreathKey = ps4_Inputs.holdBreathKey;
                    dropWeaponKey = ps4_Inputs.dropWeaponKey;
                    pauseKey = ps4_Inputs.pauseKey;
                    mapKey = ps4_Inputs.mapKey;
                    inventoryKey = ps4_Inputs.inventoryKey;
                    weaponMenuKey = ps4_Inputs.weaponMenuKey;
                    skillMenuKey = ps4_Inputs.skillMenuKey;
                    missionsKey = ps4_Inputs.missionsKey;
                    takeAllKey = ps4_Inputs.takeAllKey;
                    takeKey = ps4_Inputs.takeKey;
                    deleteOneKey = ps4_Inputs.deleteOneKey;
                    deleteSeveralKey = ps4_Inputs.deleteSeveralKey;
                    submitKey = ps4_Inputs.submitKey;
                    cancelKey = ps4_Inputs.cancelKey;
                    break;
                case ConnectedGamepad.Xbox_Gamepad:
                    movementXAxis = xbox_Inputs.movementXAxis;
                    movementZAxis = xbox_Inputs.movementZAxis;
                    camXAxis = xbox_Inputs.camXAxis;
                    camYAxis = xbox_Inputs.camYAxis;
                    menuXAxis = xbox_Inputs.menuXAxis;
                    menuYAxis = xbox_Inputs.menuYAxis;
                    jumpKey = xbox_Inputs.jumpKey;
                    crouchKey = xbox_Inputs.crouchKey;
                    sprintKey = xbox_Inputs.sprintKey;
                    swapWeaponAxis = xbox_Inputs.swapWeaponAxis;
                    weaponWheelKey = xbox_Inputs.weaponWheelKey;
                    interactKey = xbox_Inputs.interactKey;
                    shootKey = xbox_Inputs.shootKey;
                    aimKey = xbox_Inputs.aimKey;
                    swapWeaponKey = xbox_Inputs.swapWeaponKey;
                    reloadKey = xbox_Inputs.reloadKey;
                    holdBreathKey = xbox_Inputs.holdBreathKey;
                    dropWeaponKey = xbox_Inputs.dropWeaponKey;
                    pauseKey = xbox_Inputs.pauseKey;
                    mapKey = xbox_Inputs.mapKey;
                    inventoryKey = xbox_Inputs.inventoryKey;
                    weaponMenuKey = xbox_Inputs.weaponMenuKey;
                    skillMenuKey = xbox_Inputs.skillMenuKey;
                    missionsKey = xbox_Inputs.missionsKey;
                    takeAllKey = xbox_Inputs.takeAllKey;
                    takeKey = xbox_Inputs.takeKey;
                    deleteOneKey = xbox_Inputs.deleteOneKey;
                    deleteSeveralKey = xbox_Inputs.deleteSeveralKey;
                    submitKey = xbox_Inputs.submitKey;
                    cancelKey = xbox_Inputs.cancelKey;
                    break;
            }
        }
        #endregion
    }

    public enum ConnectedGamepad
    {
        PS4_Gamepad,
        Xbox_Gamepad,
        Mouse_Keyboard
    }
}


