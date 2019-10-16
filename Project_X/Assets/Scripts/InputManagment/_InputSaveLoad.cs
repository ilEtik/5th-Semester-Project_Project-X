using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace InputManagment
{
    public class _InputSaveLoad : MonoBehaviour
    {
        public static _InputSaveLoad saveLoad;

        private void Awake()
        {
            if (saveLoad == null)
                saveLoad = this;

            filePath = Application.persistentDataPath + "/InputManagment.save";
        }

        private string filePath;
        private PC_Inputs inputs;

        public void SaveInputs()
        {
            inputs = _InputManager._IM.pc_Inputs;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.OpenOrCreate);
            InputSaver data = new InputSaver();

            if (_InputManager._IM.connectedGamepad == ConnectedGamepad.Mouse_Keyboard)
            {
                data.movementXAxis = inputs.movementXAxis;
                data.movementYAxis = inputs.movementZAxis;
                data.camXAxis = inputs.camXAxis;
                data.camYAxis = inputs.camYAxis;
                data.jumpKey = inputs.jumpKey;
                data.crouchKey = inputs.crouchKey;
                data.layKey = inputs.layKey;
                data.sprintKey = inputs.sprintKey;
                data.swapWeaponAxis = inputs.swapWeaponAxis;
                data.interactKey = inputs.interactKey;
                data.shootKey = inputs.shootKey;
                data.aimKey = inputs.aimKey;
                data.swapWeaponKey = inputs.swapWeaponKey;
                data.weaponWheelKey = inputs.weaponWheelKey;
                data.reloadKey = inputs.reloadKey;
                data.holdBreathKey = inputs.holdBreathKey;
                data.dropWeaponKey = inputs.dropWeaponKey;
                data.menuXAxis = inputs.menuXAxis;
                data.menuYAxis = inputs.menuYAxis;
                data.pauseKey = inputs.pauseKey;
                data.mapKey = inputs.mapKey;
                data.inventoryKey = inputs.inventoryKey;
                data.weaponMenuKey = inputs.weaponMenuKey;
                data.skillMenuKey = inputs.skillMenuKey;
                data.missionsKey = inputs.missionsKey;
                data.takeAllKey = inputs.takeAllKey;
                data.takeKey = inputs.takeKey;
                data.submitKey = inputs.submitKey;
                data.cancelKey = inputs.cancelKey;
                data.quickSave = inputs.quickSaveKey;
                data.quickLoad = inputs.quickLoadKey;
            }
            bf.Serialize(file, data);
            file.Close();
        }

        public void LoadInputs()
        {
            if (File.Exists(filePath))
            {
                inputs = _InputManager._IM.pc_Inputs;
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(filePath, FileMode.Open);
                InputSaver data = (InputSaver)bf.Deserialize(file);

                if (_InputManager._IM.connectedGamepad == ConnectedGamepad.Mouse_Keyboard)
                {
                    inputs.movementXAxis = data.movementXAxis;
                    inputs.movementZAxis = data.movementYAxis;
                    inputs.camXAxis = data.camXAxis;
                    inputs.camYAxis = data.camYAxis;
                    inputs.jumpKey = data.jumpKey;
                    inputs.crouchKey = data.crouchKey;
                    inputs.layKey = data.layKey;
                    inputs.sprintKey = data.sprintKey;
                    inputs.swapWeaponAxis = data.swapWeaponAxis;
                    inputs.interactKey = data.interactKey;
                    inputs.shootKey = data.shootKey;
                    inputs.aimKey = data.aimKey;
                    inputs.swapWeaponKey = data.swapWeaponKey;
                    inputs.weaponWheelKey = data.weaponWheelKey;
                    inputs.reloadKey = data.reloadKey;
                    inputs.holdBreathKey = data.holdBreathKey;
                    inputs.dropWeaponKey = data.dropWeaponKey;
                    inputs.menuXAxis = data.menuXAxis;
                    inputs.menuYAxis = data.menuYAxis;
                    inputs.pauseKey = data.pauseKey;
                    inputs.mapKey = data.mapKey;
                    inputs.inventoryKey = data.inventoryKey;
                    inputs.weaponMenuKey = data.weaponMenuKey;
                    inputs.skillMenuKey = data.skillMenuKey;
                    inputs.missionsKey = data.missionsKey;
                    inputs.takeAllKey = data.takeAllKey;
                    inputs.takeKey = data.takeKey;
                    inputs.submitKey = data.submitKey;
                    inputs.cancelKey = data.cancelKey;
                    inputs.quickSaveKey = data.quickSave;
                    inputs.quickLoadKey = data.quickLoad;
                }
                file.Close();
            }
            else
                Debug.LogWarning("No Saved Inputs Data found!");
        }
    }

    [Serializable]
    public struct InputSaver
    {
        public string movementXAxis;
        public string movementYAxis;
        public string camXAxis;
        public string camYAxis;
        public KeyCode jumpKey;
        public KeyCode crouchKey;
        public KeyCode layKey;
        public KeyCode sprintKey;
        public string swapWeaponAxis;
        public KeyCode interactKey;
        public KeyCode shootKey;
        public KeyCode aimKey;
        public KeyCode swapWeaponKey;
        public KeyCode weaponWheelKey;
        public KeyCode reloadKey;
        public KeyCode holdBreathKey;
        public KeyCode dropWeaponKey;
        public string menuXAxis;
        public string menuYAxis;
        public KeyCode pauseKey;
        public KeyCode mapKey;
        public KeyCode inventoryKey;
        public KeyCode weaponMenuKey;
        public KeyCode skillMenuKey;
        public KeyCode missionsKey;
        public KeyCode takeAllKey;
        public KeyCode takeKey;
        public KeyCode submitKey;
        public KeyCode cancelKey;
        public KeyCode quickSave;
        public KeyCode quickLoad;
    }
}
