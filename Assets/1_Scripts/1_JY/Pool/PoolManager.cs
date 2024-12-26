using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PoolManager에선 진짜 풀링만 담당
// Pool 에서 전달받은 T 로 Resources 로드 등을 하고 PoolManager에게 반환 하는 식으로 하기.

#region Pool[Generic]
public class Pool<T> where T : Component
{
    // Pool 클래스는 각 오브젝트 별 풀을 담당한다.
    // Origin Object를 들고있고
    // 풀에 객체를 추가 - Create
    // 풀에서 객체를 풀링(가져오기) - Get
    // 객체를 풀로 반환(돌려놓기) - Return
    // 풀의 카운트를 반환하기 - Count

    public GameObject originPrefab;

    private Queue<T> _poolQueue = new();
    private const int POOL_COUNT = 10;

    public void Init(string key)
    {
        // 초기화 작업.
        // 1. 게임오브젝트를 생성한다. [Resources.Load] -> 추후 ResourceManager 등을 통해 생성하는 것으로 확장
        // 2. (아무말) 어드레서블 쓸 때 키가 유용하겠지? 흐흐흐

        originPrefab = Resources.Load<GameObject>($"1_JY/Prefabs/{key}");        
        if (originPrefab == null)
        {
            Debug.LogError($"{key}:: 이름을 가진 오브젝트가 해당 경로에 없습니다.");
            return;
        }

        if(!originPrefab.TryGetComponent(out T type))
        {
            Debug.LogError($"{type.name}:: 타입의 컴포넌트가 없습니다.");
            return;
        }

        for (int i = 0; i < POOL_COUNT; i++)
            Create();
    }

    private void Create()
    {
        GameObject clone = UnityEngine.Object.Instantiate(originPrefab, PoolManager.Instance.transform);
        clone.SetActive(false);

        T objType = clone.GetComponent<T>();
        Debug.Log(typeof(T).Name);
        _poolQueue.Enqueue(objType);        
    }

    public T Get()
    {
        if (_poolQueue.Count <= 0)
            Create();

        T obj = _poolQueue.Dequeue();
        obj.gameObject.SetActive(true);
        obj.name = originPrefab.name;

        return obj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _poolQueue.Enqueue(obj);
    }

    public int GetPoolCount() => _poolQueue.Count;
}
#endregion

public class PoolManager : MonoBehaviour
{
    #region Static
    public static PoolManager Instance => _instance;
    private static PoolManager _instance;
    #endregion

    private Dictionary<string, object> _poolDict = new();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public T Get<T>(T objType, string key) where T : Component
    {
        if (!_poolDict.ContainsKey(key))
            CreatePool<T>(key);

        Pool<T> pool = _poolDict[key] as Pool<T>;

        return pool.Get();
    }

    public void CreatePool<T>(string key) where T : Component
    {
        Pool<T> newPool = new Pool<T>();
        _poolDict[key] = newPool;

        newPool.Init(key);
    }

    public void Return<T>(T objType, string key) where T : Component
    {
        Pool<T> pool = _poolDict[key] as Pool<T>;

        pool.Return(objType);
    }

    public void Dispose()
    {
        _poolDict.Clear();
        _instance = null;
    }
}
