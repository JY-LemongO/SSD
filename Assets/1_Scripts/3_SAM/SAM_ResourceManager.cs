using System.Xml.Linq;
using UnityEngine;

public class SAM_ResourceManager : SAM_Singleton<SAM_ResourceManager>
{
    protected override void Init()
    {
        _dontDestroyOnLoad = true;
        base.Init();
    }

    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"3_SAM/Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Path can't be null !!");
            return null;
        }

        return Instantiate(prefab, parent);
    }
}
