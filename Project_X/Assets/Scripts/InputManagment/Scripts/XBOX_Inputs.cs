using System;
using UnityEngine;

namespace InputManagment
{
    [Serializable]
    public class XBOX_Inputs
    {
        [Header(" XBOX Movement")]
        public string movementXAxis = "Movement X Axis XBOX";
        public string movementZAxis = "Movement Y Axis XBOX";
        public string camXAxis = "Camera X Axis XBOX";
        public string camYAxis = "Camera Y Axis XBOX";
        [Space(5)]
        public KeyCode jumpKey = KeyCode.JoystickButton0;
        public KeyCode crouchKey = KeyCode.JoystickButton9;
        public KeyCode sprintKey = KeyCode.JoystickButton8;
        [Header(" XBOX Interactions")]
        public string swapWeaponAxis = null;
        public KeyCode interactKey = KeyCode.JoystickButton1;
        public KeyCode shootKey = KeyCode.JoystickButton5;
        public KeyCode aimKey = KeyCode.JoystickButton4;
        public KeyCode swapWeaponKey = KeyCode.JoystickButton3;
        public KeyCode weaponWheelKey = KeyCode.JoystickButton13;
        public KeyCode reloadKey = KeyCode.JoystickButton2;
        public KeyCode holdBreathKey = KeyCode.JoystickButton8;
        public KeyCode dropWeaponKey = KeyCode.JoystickButton3;
        [Header(" XBOX Menus")]
        public string menuXAxis = "Menu X Axis XBOX";
        public string menuYAxis = "Menu Y Axis XBOX";
        [Space(5)]
        public KeyCode pauseKey = KeyCode.Joystick1Button7;
        public KeyCode mapKey = KeyCode.Joystick1Button6;
        public KeyCode inventoryKey;
        public KeyCode weaponMenuKey;
        public KeyCode skillMenuKey;
        public KeyCode missionsKey;
        public KeyCode takeAllKey = KeyCode.JoystickButton0;
        public KeyCode takeKey = KeyCode.JoystickButton1;
        public KeyCode deleteOneKey = KeyCode.JoystickButton3;
        public KeyCode deleteSeveralKey = KeyCode.JoystickButton2;
        public KeyCode submitKey = KeyCode.JoystickButton0;
        public KeyCode cancelKey = KeyCode.JoystickButton1;
    }
}
