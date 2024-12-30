using UnityEngine;
using UnityEngine.UI;

public class SAM_UI_Button1 : SAM_UIBase
{
    // 여기서 인스펙터로 버튼
    // 버튼에 이벤트 할당
    [SerializeField] Button button;

    void Start()
    {
        button.onClick.AddListener(() => SAM_UIManager.Instance.HideUI("SAM_UI_Button3"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
