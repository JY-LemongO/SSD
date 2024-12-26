using UnityEngine;

public class SAM_UIScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SAM_UIManager.Instance.RegisterUI("SAM_UI_Button1");
        SAM_UIManager.Instance.ShowAllUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
