using UnityEngine;
using UnityEngine.UI;

public class SAM_UI_ExpBar : SAM_UIBase
{
    [SerializeField] private Image image;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float exp = SAM_GameManager.Instance.character.stat.CurrentEXP / SAM_GameManager.Instance.character.stat.MaxEXPPerLevel;
        image.fillAmount = exp;
    }
}
