using UnityEngine;
using UnityEngine.UI;

public class SAM_UI_HpBar : SAM_UIBase
{
    [SerializeField] private Image image;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hp = SAM_GameManager.Instance.character.stat.CurrentHP / SAM_GameManager.Instance.character.stat.MaxHP;
        image.fillAmount = hp;
    }
}
