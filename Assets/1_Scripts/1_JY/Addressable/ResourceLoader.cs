using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace JY
{
    public class ResourceLoader : MonoBehaviour
    {
        // currentLoad, TotalLoad
        public event Action<int, int> OnLoadProgressChanged;

        public static ResourceLoader Instance => _instance;
        private static ResourceLoader _instance;

        // Key - PrimaryKey, Value - GameObject
        private Dictionary<string, UnityEngine.Object> _resourcesDict = new();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public T Load<T>(string key) where T : UnityEngine.Object
        {
            if (_resourcesDict.TryGetValue(key, out UnityEngine.Object value))
                return value as T;
            return null;
        }

        public void LoadAsyncByLabel(string key, Action callback = null)
            => StartCoroutine(Co_LoadAsyncByLabel(key, callback));

        public List<string> GetLoadedKey()
        {
            List<string> loadedKeys = new();
            foreach (string key in _resourcesDict.Keys)
                loadedKeys.Add(key);

            return loadedKeys;
        }

        public void UnLoadResources(List<string> keys)
        {
            foreach (string key in keys)
            {
                if (_resourcesDict.TryGetValue(key, out UnityEngine.Object resource))
                {
                    Addressables.Release(resource);
                    _resourcesDict.Remove(key);
                    Debug.Log($"The resource memory has been released. Key - [{key}] / Resource Name - [{resource.name}]");
                }
            }
        }

        private IEnumerator Co_LoadAsyncByLabel(string key, Action callback = null)
        {
            var locationHandle = Addressables.LoadResourceLocationsAsync(key);

            while (!locationHandle.IsDone)
            {
                Debug.Log($"Location Handle Progress : {locationHandle.PercentComplete * 100:F2}%");
                yield return null;
            }

            var locations = locationHandle.Result;
            int totalCount = locations.Count;
            int currentCount = 0;
            foreach (var location in locations)
            {
                currentCount++;
                Debug.Log($"PrimaryKey - {location.PrimaryKey} Load Start.");
                yield return StartCoroutine(LoadAsyncByPrimaryKey(location.PrimaryKey));
                OnLoadProgressChanged?.Invoke(currentCount, totalCount);
            }

            Debug.Log("All Assets Load Complete.");
            callback?.Invoke();
        }

        private IEnumerator LoadAsyncByPrimaryKey(string primaryKey)
        {
            var op = Addressables.LoadAssetAsync<GameObject>(primaryKey);
            while (!op.IsDone)
            {
                yield return null;
            }

            _resourcesDict.Add(primaryKey, op.Result);
            Debug.Log("Asset Load Complete.");
        }
    }
}

