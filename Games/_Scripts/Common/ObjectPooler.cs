using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //INFO
    public GameObject prefab;
    public bool fixedSize;
    public int poolSize;

    //MANAGER
    List<GameObject> pooledObjects;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject gameObject in pooledObjects)
        {
            if (gameObject.activeInHierarchy == false)
                return gameObject;
        }
        if (fixedSize)
            return null;
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
