using System;
using UnityEngine;

public class SAM_Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected bool _dontDestroyOnLoad = false;
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<T>(FindObjectsInactive.Exclude);
            if (_instance == null)
            {
                GameObject @object = new GameObject(typeof(T).Name);
                _instance = @object.AddComponent<T>();
            }

            return _instance;
        }
    }

    protected void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Init();
    }

    protected virtual void Init()
    {
        if(_dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}
