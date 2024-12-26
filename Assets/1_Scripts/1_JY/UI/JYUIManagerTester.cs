using UnityEngine;
using UnityEngine.UI;

public class UIManagerTester : MonoBehaviour
{
    public Button test1Popup;
    public Button test2Popup;
    public Button test3Popup;

    public Button test1Item;
    public Button test2Item;

    public Button closeFrontUI;
    public Button closeAllUI;

    private void Start()
    {
        test1Popup.onClick.AddListener(() => UIManager.Instance.OpenPopupUI<UI_Test1Popup>());
        test2Popup.onClick.AddListener(() => UIManager.Instance.OpenPopupUI<UI_Test2Popup>());
        test3Popup.onClick.AddListener(() => UIManager.Instance.OpenPopupUI<UI_Test3Popup>());

        test1Item.onClick.AddListener(() => UIManager.Instance.CreateItemUI<UI_Test1Item>(test3Popup.transform));
        test2Item.onClick.AddListener(() => UIManager.Instance.CreateItemUI<UI_Test2Item>(test3Popup.transform));

        closeFrontUI.onClick.AddListener(() => UIManager.Instance.CloseFrontUI());
        closeAllUI.onClick.AddListener(() => UIManager.Instance.CloseAllUI());
    }
}
