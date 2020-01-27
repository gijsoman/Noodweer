using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
    }

    #region Singleton!
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Start()
    {
        SpawnPoolObjects();
    }

    private void SpawnPoolObjects()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string _tag, Vector3 _position, Quaternion _rotation)
    {
        if (!PoolDictionary.ContainsKey(_tag))
        {
            Debug.LogWarning("Pool with tag: " + _tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = PoolDictionary[_tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = _position;
        objectToSpawn.transform.rotation = _rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawned();
        }

        PoolDictionary[_tag].Enqueue(objectToSpawn);
        StartCoroutine(DeactivateBullet(objectToSpawn));

        return objectToSpawn;
    }

    IEnumerator DeactivateBullet(GameObject Bullet)
    {
        yield return new WaitForSeconds(2);
        Bullet.SetActive(false);
    }

    private void OnLevelWasLoaded(int level)
    {
        SpawnPoolObjects();
    }
}
