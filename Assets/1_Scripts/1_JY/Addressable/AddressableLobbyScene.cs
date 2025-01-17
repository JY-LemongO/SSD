//using QFSW.QC;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace JY
{
    public class AddressableLobbyScene : MonoBehaviour
    {
        [SerializeField] private AssetLabelReference _sample1Reference;
        [SerializeField] private AssetLabelReference _sample2Reference;
        [SerializeField] private string _loadingSceneName;
        [SerializeField] private string _nextSceneName1;
        [SerializeField] private string _nextSceneName2;

        //[Command]
        public void OnLoadSceneAsyncSample1()
        {
            SceneLoaderEx.Instance.LoadSceneAsync(_loadingSceneName, () =>
            {
                ResourceLoader.Instance.LoadAsyncByLabel(_sample1Reference.labelString, () =>
                {
                    SceneLoaderEx.Instance.LoadSceneAsync(_nextSceneName1);
                });
            });
        }

        //[Command]
        public void OnLoadSceneAsyncSample2()
        {
            SceneLoaderEx.Instance.LoadSceneAsync(_loadingSceneName, () =>
            {
                ResourceLoader.Instance.LoadAsyncByLabel(_sample2Reference.labelString, () =>
                {
                    SceneLoaderEx.Instance.LoadSceneAsync(_nextSceneName2);
                });
            });
        }
    }
}

