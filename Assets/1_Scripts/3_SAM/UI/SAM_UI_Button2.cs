using UnityEngine;
using UnityEngine.UI;

public class SAM_UI_Button2 : SAM_UIBase
{
    [SerializeField] Button button;

    void Start()
    {
        button.onClick.AddListener(() => SAM_UIManager.Instance.ShowUI("SAM_UI_Button3"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
