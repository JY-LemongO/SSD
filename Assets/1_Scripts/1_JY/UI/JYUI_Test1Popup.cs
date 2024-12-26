using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Test1Popup : UI_Popup
{
    [SerializeField] private TMP_Text _myNameText;
    [SerializeField] private Button _closeUIBtn;

    public override void Setup()
    {
        base.Setup();

        _myNameText.text = $"{GetType().Name}";
        _closeUIBtn.onClick.AddListener(() => CloseUI());
    }

    protected override void Dispose()
    {
        _myNameText.text = "";
        _closeUIBtn.onClick.RemoveAllListeners();
        base.Dispose();
    }
}
