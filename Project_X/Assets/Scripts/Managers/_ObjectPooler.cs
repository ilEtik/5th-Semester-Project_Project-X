using System.Collections.Generic;
using UnityEngine;
using System;

public class _ObjectPooler : MonoBehaviour
{
    public static _ObjectPooler pooler;

    private void Awake()
    {
        pooler = this;
    }

    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i< pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, int? range = 0, int? damage = 0)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't excist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        if(objectToSpawn.tag == _TagManager.tag_Bullet)
        {
            objectToSpawn.GetComponent<_Bullet_762mm>().fireRange = range.Value;
            objectToSpawn.GetComponent<_Bullet_762mm>().dmg = damage.Value;
        }
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
            pooledObj.OnObjectSpawn();

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
