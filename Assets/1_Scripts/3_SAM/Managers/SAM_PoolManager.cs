using UnityEngine;
using System;
using System.Collections.Generic;

// 최소 목표 : 단순한 풀링
// 추가 : 여러 오브젝트 대응..어떻게???
// 으으으으으가가각
// 딕셔너리<이름, 풀<겜오브젝트>>
// 음...

public class SAM_PoolManager : MonoBehaviour
{
    public static SAM_PoolManager Instance { get; private set; }

    Dictionary<string, SAM_Pool> pools = new Dictionary<string, SAM_Pool>();

    public int PoolCount { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    // 풀 생성
    public void CreatePool(string poolsKey, GameObject prefab)
    {
        if(!pools.ContainsKey(poolsKey)) // 존재 안하는 풀이면
        {
            SAM_Pool newPool = new SAM_Pool();
            pools.Add(poolsKey, newPool);
            pools[poolsKey].InitializePool(prefab, transform);
        }
    }


    // 내보내기
    public GameObject SpawnObject(string poolsKey)
    {
        return pools[poolsKey].SpawnObject();
    }

    // 들여오기
    public void ReturnObject(string poolsKey, GameObject returnObject)
    {
        pools[poolsKey].ReturnObject(returnObject);
    }
}
