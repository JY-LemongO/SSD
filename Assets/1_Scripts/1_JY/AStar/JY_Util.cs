using TMPro;
using UnityEngine;

public static class JY_Util
{
    public static TextMeshPro CreateNodeCostText(string cost, Vector2 worldPosition, float fontSize = 5f, TextAlignmentOptions alignOption = TextAlignmentOptions.Center, float duration = 2f)
    {
        GameObject go = new GameObject("NodeCostText", typeof(TextMeshPro));
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(1, 1);
        rect.position = worldPosition;
        TextMeshPro text = go.GetComponent<TextMeshPro>();
        text.alignment = alignOption;
        text.fontSize = fontSize;
        text.color = Color.white;
        text.text = cost;
        UnityEngine.Object.Destroy(go, duration);

        return text;
    }
}
