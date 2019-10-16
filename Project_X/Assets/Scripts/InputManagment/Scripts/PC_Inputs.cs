using System;
using UnityEngine;

namespace InputManagment
{
    [Serializable]
    public class PC_Inputs
    {
        [Header(" PC Movement")]
        public string movementXAxis = "Movement X Axis PC";
        public string movementZAxis = "Movement Y Axis PC";
        public string camXAxis = "Camera X Axis PC";
        public string camYAxis = "Camera Y Axis PC";
        [Space(5)]
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode crouchKey = KeyCode.C;
        public KeyCode layKey = KeyCode.LeftControl;
        public KeyCode sprintKey = KeyCode.LeftShift;
        [Header(" PC Interactions")]
        public string swapWeaponAxis = "Mouse ScrollWheel";
        public KeyCode interactKey = KeyCode.E;
        public KeyCode shootKey = KeyCode.Mouse0;
        public KeyCode aimKey = KeyCode.Mouse1;
        public KeyCode swapWeaponKey = KeyCode.X;
        public KeyCode weaponWheelKey = KeyCode.Tab;
        public KeyCode reloadKey = KeyCode.R;
        public KeyCode holdBreathKey = KeyCode.LeftShift;
        public KeyCode dropWeaponKey = KeyCode.Q;
        [Header(" PC Menus")]
        public string menuXAxis = "Menu X Axis PC";
        public string menuYAxis = "Menu Y Axis PC";
        [Space(5)]
        public KeyCode pauseKey = KeyCode.Escape;
        public KeyCode mapKey = KeyCode.M;
        public KeyCode inventoryKey = KeyCode.I;
        public KeyCode weaponMenuKey = KeyCode.K;
        public KeyCode skillMenuKey = KeyCode.L;
        public KeyCode missionsKey = KeyCode.J;
        public KeyCode takeAllKey = KeyCode.Space;
        public KeyCode takeKey = KeyCode.E;
        public KeyCode deleteOneKey = KeyCode.X;
        public KeyCode deleteSeveralKey = KeyCode.Y;
        public KeyCode submitKey = KeyCode.Return;
        public KeyCode cancelKey = KeyCode.Backslash;
        public KeyCode quickSaveKey = KeyCode.F5;
        public KeyCode quickLoadKey = KeyCode.F9;
    }
}
