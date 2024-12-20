using System;
using UnityEngine;

public class CommonInitializerEClone241220 : MonoBehaviour
{
    public GameObject opPrefab; // Prefab
    [NonSerialized] public ObjectPoolingEClone241220 op;

    private void Awake()
    {
        op = Instantiate(opPrefab).GetComponent<ObjectPoolingEClone241220>();
    }
}

public static class StaticData241220
{
    public static ObjectPoolingEClone241220 pool;
}