using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PoolInfo
{
    public string name;
    public GameObject prefab;
    public int count;
}

public class ObjectPool : MonoSingleton<ObjectPool>
{
    [SerializeField]
    private PoolInfo[] poolInfos;

    Dictionary<String, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

    protected override void Init()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolInfos.Length; i++)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            for (int j = 0; j < poolInfos[i].count; j++)
            {
                GameObject gameObj = Instantiate(poolInfos[i].prefab, transform);
                gameObj.SetActive(false);
                gameObj.transform.position = Vector3.zero;
                gameObj.transform.rotation = Quaternion.identity;
                gameObjects.Add(gameObj);
            }
            pools.Add(poolInfos[i].name, gameObjects);
        }
    }

    public GameObject Spawn(string name, Vector3 position, Quaternion rotation)
    {
        GameObject spawnedObject = SpawnCore(name);
        if (!spawnedObject) { return null; }
        spawnedObject.SetActive(true);
        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = rotation;
        foreach (var poolObject in spawnedObject.GetComponentsInChildren<IPoolObject>())
        {
            poolObject.OnSpawn();
        }
        return spawnedObject;
    }

    public GameObject Spawn(string name, Transform parent)
    {
        GameObject spawnedObject = SpawnCore(name);
        if (!spawnedObject) { return null; }
        spawnedObject.SetActive(true);
        spawnedObject.transform.SetParent(parent);
        foreach (var poolObject in spawnedObject.GetComponentsInChildren<IPoolObject>())
        {
            poolObject.OnSpawn();
        }
        return spawnedObject;
    }

    public GameObject SpawnCore(string name)
    {
        List<GameObject> gameObjects;

        if (pools.TryGetValue(name, out gameObjects))
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].activeSelf == false)
                {
                    return gameObjects[i];
                }
            }
            Debug.LogWarning("ObjectPool.cs " + name + " 풀에 더이상 꺼낼 것이 없음.");
            return null;
        }
        else
        {
            Debug.LogWarning("ObjectPool.cs 오브젝트 풀에 " + name + " 이름이 없음.");
            return null;
        }
    }

    public void Despawn(GameObject despawnedObject)
    {
        despawnedObject.SetActive(false);
        if(despawnedObject.transform.parent != transform)
        {
            despawnedObject.transform.SetParent(transform);
        }
        despawnedObject.transform.position = Vector3.zero;
        despawnedObject.transform.rotation = Quaternion.identity;
        foreach (var poolObject in despawnedObject.GetComponentsInChildren<IPoolObject>())
        {
            poolObject.OnDespawn();
        }
    }
}

public interface IPoolObject
{
    public void OnSpawn();
    public void OnDespawn();
}