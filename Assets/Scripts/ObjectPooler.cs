using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class ObjectPool
{
    public ObjectPoolItem ItemToPool;
    public int AmountToPool;
    public bool ShouldExpand;
}

[System.Serializable]
public class ObjectPoolItem
{
    private ObjectPooler _objectPooler;
    public GameObject Object;
    public bool IsInUse;

    public ObjectPoolItem(GameObject _object, ObjectPooler objectPooler)
    {
        Object = _object;
        IsInUse = false;
        _objectPooler = objectPooler;
    }

    public void ReturnItemToPool()
    {
        _objectPooler.ReturnItemToPool(this);
    }
}

public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler Instance;
    public List<ObjectPool> itemsToPool;
    public List<ObjectPoolItem> pooledObjects;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        pooledObjects = new List<ObjectPoolItem>();
        foreach (ObjectPool item in itemsToPool)
        {
            GameObject parent = Instantiate(new GameObject("CHIBRE"));
            parent.name = "Pool of " + item.ItemToPool.Object.name;
            parent.transform.parent = gameObject.transform;
            for (int i = 0; i < item.AmountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.ItemToPool.Object);
                obj.transform.parent = parent.transform;
                obj.name = item.ItemToPool.Object.name;
                ObjectPoolItem poolItem = new ObjectPoolItem(obj, Instance);
                pooledObjects.Add(poolItem);
                obj.SetActive(false);
            }
        }
    }

    public ObjectPoolItem GetPooledObjectByTag(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].IsInUse && pooledObjects[i].Object.tag == tag)
            {
                pooledObjects[i].IsInUse = true;
                return pooledObjects[i];
            }
        }
        foreach (ObjectPool item in itemsToPool)
        {
            if (item.ItemToPool.Object.tag == tag)
            {
                if (item.ShouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.ItemToPool.Object);
                    obj.SetActive(false);
                    ObjectPoolItem poolItem = new ObjectPoolItem(obj, Instance);
                    pooledObjects.Add(poolItem);
                    poolItem.IsInUse = true;
                    return poolItem;
                }
            }
        }
        return null;
    }
    public ObjectPoolItem GetPooledObjectByName(string name)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].IsInUse && pooledObjects[i].Object.name == name)
            {
                pooledObjects[i].IsInUse = true;
                return pooledObjects[i];
            }
        }
        foreach (ObjectPool item in itemsToPool)
        {
            if (item.ItemToPool.Object.name == name)
            {
                if (item.ShouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.ItemToPool.Object);
                    ObjectPoolItem poolItem = new ObjectPoolItem(obj, Instance);
                    obj.SetActive(false);
                    pooledObjects.Add(poolItem);
                    poolItem.IsInUse = true;
                    return poolItem;
                }
            }
        }
        return null;
    }

    public void ReturnItemToPool(ObjectPoolItem itemToReturn)
    {
        itemToReturn.IsInUse = false;
        itemToReturn.Object.SetActive(false);
        pooledObjects.Add(itemToReturn);
        Transform poolParent = FindParentInPool(itemToReturn.Object.name);
        if (poolParent != null)
        {
            itemToReturn.Object.transform.parent = poolParent;
        }
    }
    private Transform FindParentInPool(string name)
    {
        foreach (Transform poolParent in transform)
        {
            if (poolParent.name == "Pool of " + name)
            {
                return poolParent.transform;
            }
        }
        return null;
    }

    public static ObjectPooler GetInstance()
    {
        return Instance;
    }
}