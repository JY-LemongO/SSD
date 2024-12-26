using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneScriptsEUISystem : MonoBehaviour
{
    public TextMeshProUGUI inputText;
    EUIElement e;

    [Header("SampleData")]
    public Sprite SampleParameterImage;
    public bool isThrowImage;

    public void OnClickGenerateButton()
    {
        EUIElementParameter_Type1 p = new EUIElementParameter_Type1()
        {
            image = isThrowImage ? SampleParameterImage : null,
            message = inputText.text,
            ButtonAction_Close = () => { Debug.Log("Close"); e.Close(); },
            ButtonAction_Ok = () => { Debug.Log("Ok"); e.Close(); }
        };
        e = EUISystem.Instance.Show(1, p);
    }
}
