using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 사진, 메시지, Close버튼, OK버튼이 있는 UI
/// </summary>

[CreateAssetMenu(fileName = "EUI_Type1", menuName ="EClone/SO/UI/Type1")]
public class EUIElementMetadata_Type1 : EUIElementMetadata
{
    public Sprite default_image;
    public string default_message;
}

public class EUIElementParameter_Type1 : EUIElementParameter
{
    public Sprite image;
    public string message;
    public Action ButtonAction_Close;
    public Action ButtonAction_Ok;
}

public class EUIElement_Type1 : EUIElement
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] Button[] ButtonAction_Close;
    [SerializeField] Button ButtonAction_Ok;

    public override void Init(EUIElementParameter p)  // T 는 CloneUIElementParameter_Type1
    {
        base.Init(p);

        var parameter = p as EUIElementParameter_Type1;

        // 매개변수 점검
        if (parameter == null)
        {
            Debug.Log("UI의 매개변수의 타입이 기대와 다릅니다");
            return;
        }

        // 데이터 매핑
        var metadataT = metadata as EUIElementMetadata_Type1;
        image.sprite = (parameter.image != null) ? parameter.image : metadataT.default_image;
        message.text = (parameter.message != null) ? parameter.message : metadataT.default_message;
        foreach ( var button in ButtonAction_Close )
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => parameter.ButtonAction_Close?.Invoke());
        }
        ButtonAction_Ok.onClick.RemoveAllListeners();
        ButtonAction_Ok.onClick.AddListener(() => parameter.ButtonAction_Ok?.Invoke());

        // 보여주기
        Show();
    }
}
