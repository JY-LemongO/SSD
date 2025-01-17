using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SAM_AddressableManager : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject[] cubes;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    void Start()
    {
        StartCoroutine(InitAddressable());
    }

    IEnumerator InitAddressable()
    {
        var init = Addressables.InitializeAsync();
        yield return init;
    }

    public void SpawnObject()
    {
        foreach (var obj in cubes)
        {
            obj.InstantiateAsync().Completed += (x) => spawnedObjects.Add(x.Result);
        }
    }

    public void ReleaseObject()
    {
        if (spawnedObjects.Count <= 0) return;

        int index = spawnedObjects.Count - 1;
        Addressables.ReleaseInstance(spawnedObjects[index]);
        spawnedObjects.RemoveAt(index);
    }
}
