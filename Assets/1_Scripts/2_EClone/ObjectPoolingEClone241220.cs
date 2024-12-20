using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolItemPack
{
    public string name;
    public GameObject goPrefab;
    public int n;
    Stack<GameObject> remainStack; // 잔량 관리용
    public PoolItemPack()
    {
        remainStack = new Stack<GameObject>();
    }
    public void Push(GameObject go)
    {
        remainStack.Push(go);
    }
    public GameObject Pop()
    {
        return remainStack.Pop();
    }
    public bool CheckStackRemain()
    {
        if (remainStack.Count <= 0) return false;
        else return true;
    }
}

public class ObjectPoolingEClone241220 : MonoBehaviour
{
    // 시나리오
    // 1. 생성큐 없이
    // 2. 빌릴 때, 무엇을 키로 오브젝트를 불러오는지?.... 일단 했던대로 String 가자

    [SerializeField]
    List<PoolItemPack> poolItemsList = new List<PoolItemPack>(); // Inspecter에서 이미 초기화 되어있는 상태

    Dictionary<string, PoolItemPack> poolItemsDict; // 이름(키)을 통해 아이템을 찾기 위함

    //public ObjectPoolingEClone241220() // 이거웨안뒒???? Awake로 하니까 됨
    //{
    //    poolItemsDict = new Dictionary<string, PoolItemPack>();
    //    Init();
    //}

    private void Awake()
    {
        StaticData241220.pool = this;
        poolItemsDict = new Dictionary<string, PoolItemPack>();
        Init();
    }

    // 초기화. 리스트로부터 생성까지
    void Init()
    {
        // Dict 생성
        Debug.Log(poolItemsList.Count);
        foreach (var poolItem in poolItemsList)
        {
            Debug.Log(poolItem);
            poolItemsDict.Add(poolItem.name, poolItem);
        }

        // 씬에 오브젝트 생성
        foreach(var dictItem in poolItemsDict)
        {
            var item = dictItem.Value;
            for(int i = 0; i< item.n; i++)
            {
                CreateGameObject(item);
            }
        }
    }

    // 주 메서드

    // 1. GameObject 생성(private)
    GameObject CreateGameObject(PoolItemPack item)
    {
        GameObject newGo = Instantiate(item.goPrefab, transform);

        newGo.SetActive(false);
        newGo.name = item.goPrefab.name + "(pool)";

        item.Push(newGo);

        return newGo;
    }

    // 2. 대여
    public GameObject Get(string name, Transform t = null, Transform parent = null) // 이름, 트랜스폼(위치 및 각도만 사용), 부모
    {
        var item = poolItemsDict[name];
        
        if(item == null)
        {
            Debug.Log("Key(name) 잘못됨");
            return null;
        }

        // Capacity 초과인지 확인
        if (!item.CheckStackRemain())
        {
            CreateGameObject(item);
        }

        // Stack에서 꺼내기
        GameObject popItem = item.Pop();

        // 꺼낸 오브젝트 세팅
        popItem.SetActive(true);
        if (t != null)
        {
            popItem.transform.position = t.position;
            popItem.transform.rotation = t.rotation;
        }
        if(parent != null)
            popItem.transform.parent = parent;

        return popItem;
    }

    // 3. 반납
    public void Release(GameObject go, string name) // 일단 이름까지 받음. 추후 개선
    {
        var item = poolItemsDict[name];

        if (item == null)
        {
            Debug.Log("Key(name) 잘못됨");
            return;
        }

        item.Push(go);

        // 이후, Transform, 물리, Trail 등 초기화
        go.SetActive(false);
        go.transform.SetParent(transform);
    }
}
