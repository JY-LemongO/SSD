using UnityEngine;
using UnityEngine.UI;

public class SAM_UI_Heal : SAM_UIBase
{
    [SerializeField] Button button;

    void Start()
    {
        button.onClick.AddListener(() => SAM_GameManager.Instance.character.stat.Heal(13));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
