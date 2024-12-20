using UnityEngine;

public class SAM_PoolGameManager : MonoBehaviour
{
    private SAM_PoolManager _poolManager;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private GameObject _spherePrefab;

    private void Awake()
    {
        _poolManager = SAM_PoolManager.Instance;
    }


    void Start()
    {
        _poolManager.CreatePool(_cubePrefab.name, _cubePrefab);
        _poolManager.CreatePool(_spherePrefab.name, _spherePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        _poolManager.SpawnObject(_cubePrefab.name);
        _poolManager.SpawnObject(_spherePrefab.name);
        Debug.Log(_poolManager.PoolCount);
    }
}
