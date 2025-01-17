using System.Collections.Generic;
using UnityEngine;

namespace JY
{
    public class AddressableSampleScene : MonoBehaviour
    {
        private void Awake()
        {
            List<string> keys = ResourceLoader.Instance.GetLoadedKey();

            foreach (var key in keys)
            {
                GameObject go = ResourceLoader.Instance.Load<GameObject>(key);
                Instantiate(go);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResourceLoader.Instance.UnLoadResources(ResourceLoader.Instance.GetLoadedKey());
                SceneLoaderEx.Instance.LoadSceneAsync("LobbyScene");
            }
        }
    }
}

