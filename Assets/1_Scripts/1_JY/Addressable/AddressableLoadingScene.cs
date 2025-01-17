using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JY
{
    public class AddressableLoadingScene : MonoBehaviour
    {
        [SerializeField] TMP_Text _loadingCountText;
        [SerializeField] Slider _loadingProgressSlider;

        private void Awake()
        {
            ResourceLoader.Instance.OnLoadProgressChanged += HandleLoadingProgressChange;
        }

        private void HandleLoadingProgressChange(int current, int total)
        {
            _loadingCountText.text = $"{current}/{total}";
            _loadingProgressSlider.value = (float)(current / total);
        }

        private void OnDestroy()
        {
            ResourceLoader.Instance.OnLoadProgressChanged -= HandleLoadingProgressChange;
        }
    }
}

