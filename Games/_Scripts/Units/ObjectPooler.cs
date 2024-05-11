using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerBase : CustomMonoBehaviour
{
    //INFO
    [SerializeField]
    GameObject _prefab;

    [SerializeField]
    bool _fixedSize;

    [SerializeField]
    int _poolSize;

    //MANAGER
    [SerializeReference]
    List<GameObject> pooledObjects;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        SetupObjectPooler();
    }

    private void SetupObjectPooler()
    {
        if (pooledObjects != null)
        {
            Debug.LogWarning("Please reset" + transform.name, gameObject);
            return;
        }
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < _poolSize; ++i)
        {
            GameObject obj = Instantiate(_prefab);
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
        if (_fixedSize)
            return null;
        GameObject obj = Instantiate(_prefab);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
