using System;
using UnityEngine;

namespace InputManagment
{
    [Serializable]
    public class PS4_Inputs
    {
        [Header(" PS4 Movement")]
        public string movementXAxis = "Movement X Axis PS4";
        public string movementZAxis = "Movement Y Axis PS4";
        public string camXAxis = "Camera X Axis PS4";
        public string camYAxis = "Camera Y Axis PS4";
        [Space(5)]
        public KeyCode jumpKey = KeyCode.JoystickButton1;
        public KeyCode crouchKey = KeyCode.JoystickButton11;
        public KeyCode sprintKey = KeyCode.JoystickButton10;
        [Header(" PS4 Interactions")]
        public string swapWeaponAxis = null;
        public KeyCode interactKey = KeyCode.JoystickButton2;
        public KeyCode shootKey = KeyCode.JoystickButton5;
        public KeyCode aimKey = KeyCode.JoystickButton4;
        public KeyCode swapWeaponKey = KeyCode.JoystickButton3;
        public KeyCode weaponWheelKey = KeyCode.JoystickButton6;
        public KeyCode reloadKey = KeyCode.JoystickButton0;
        public KeyCode holdBreathKey = KeyCode.JoystickButton10;
        public KeyCode dropWeaponKey = KeyCode.JoystickButton3;
        [Header(" PS4 Menus")]
        public string menuXAxis = "Menu X Axis PS4";
        public string menuYAxis = "Menu Y Axis PS4";
        [Space(5)]
        public KeyCode pauseKey = KeyCode.JoystickButton9;
        public KeyCode mapKey = KeyCode.JoystickButton13;
        public KeyCode inventoryKey;
        public KeyCode weaponMenuKey;
        public KeyCode skillMenuKey;
        public KeyCode missionsKey;
        public KeyCode takeAllKey = KeyCode.JoystickButton1;
        public KeyCode takeKey = KeyCode.JoystickButton2;
        public KeyCode deleteOneKey = KeyCode.JoystickButton3;
        public KeyCode deleteSeveralKey = KeyCode.JoystickButton2;
        public KeyCode submitKey = KeyCode.JoystickButton1;
        public KeyCode cancelKey = KeyCode.JoystickButton2;
    }
}
