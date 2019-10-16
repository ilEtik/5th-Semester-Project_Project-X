using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(_PlayerMotor))]
public class _PlayerSaveLoad : MonoBehaviour
{
    private string filePath;
    private Vector3 position;
    private Vector3 rotation;
    private GameObject cam;
    private GameObject start;
    private _LevelStart lvlStart;
    public float delay = 1f;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/Player.dat";
    }

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag(_TagManager.tag_MainCamera);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.OpenOrCreate);
        PlayerData data = new PlayerData();
        data.savedScene = SceneManager.GetActiveScene().buildIndex;
        data.posX = transform.position.x;
        data.posY = transform.position.y;
        data.posZ = transform.position.z;
        data.protX = transform.rotation.x;
        data.protY = transform.rotation.y;
        data.protZ = transform.rotation.z;
        data.protW = transform.rotation.w;
        data.rotX = cam.transform.rotation.x;
        data.rotY = cam.transform.rotation.y;
        data.rotZ = cam.transform.rotation.z;
        data.rotW = cam.transform.rotation.w;
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            _UiManager._UI.SetLoadingScreen(true);
            Time.timeScale = 1;
            SceneManager.LoadScene(data.savedScene, LoadSceneMode.Single);
            Vector3 playerPos = new Vector3(data.posX, data.posY, data.posZ);
            Quaternion playerRot = new Quaternion(data.protX, data.protY, data.protZ, data.protW);
            Quaternion camRot = new Quaternion(data.rotX, data.rotY, data.rotZ, data.rotW);
            _LevelStart.levelStart.LoadPlayerdata(playerPos, playerRot, camRot, delay);
            file.Close();
        }
    }
}

[Serializable]
struct PlayerData
{
    public float posX, posY, posZ;
    public float protX, protY, protZ, protW;
    public float rotX, rotY, rotZ, rotW;
    public int savedScene;
}
