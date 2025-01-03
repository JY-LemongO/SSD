using UnityEngine;
using UnityEngine.UI;

public class SAM_UI_TakeDamage : SAM_UIBase
{
    [SerializeField] Button button;

    void Start()
    {
        button.onClick.AddListener(() => SAM_GameManager.Instance.character.stat.TakeDamage(15));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
