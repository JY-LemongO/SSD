using UnityEngine;
using System;
using System.Collections.Generic;
using NUnit.Framework;

public class SAM_Pool : MonoBehaviour
{
    Queue<GameObject> pool = new Queue<GameObject>();
    private GameObject _prefab;
    private Transform _poolManager;

    public int poolCount {  get { return pool.Count; } }

    private void Start()
    {
        CreateNewObject(10);

    }

    public void InitializePool(GameObject prefab, Transform poolManager)
    {
        _prefab = prefab;
        _poolManager = poolManager;
    }

    private void CreateNewObject(int reps = 1)
    {
        if (_prefab == null)
            Assert.Fail("프리팹 비어있음.");

        for (int i = 0; i < reps; ++i)
        {
            GameObject newObj = Instantiate(_prefab, _poolManager);
            newObj.name = _prefab.name;
            newObj.SetActive(false);
            pool.Enqueue(newObj);
        }
    }

    public GameObject SpawnObject()
    {
        // 풀 빈 경우
        if (pool.Count <= 0)
        {
            CreateNewObject();
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);

        return obj;
    }

    public void ReturnObject(GameObject returnObject)
    {
        returnObject.SetActive(false);
        pool.Enqueue(returnObject);
    }
}
