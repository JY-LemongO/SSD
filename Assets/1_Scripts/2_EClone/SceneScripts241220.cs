using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScripts241220 : MonoBehaviour
{
    public InputField inputField; // 인스펙터 매핑

    private Dictionary<string, Stack<GameObject>> activeGoDict = new Dictionary<string, Stack<GameObject>>();

    public void OnPlusClick()
    {
        GameObject newGo = StaticData241220.pool.Get(inputField.text);

        activeGoDict[inputField.text].Push(newGo);
    }
    public void OnMinusClick()
    {
        GameObject go = activeGoDict[inputField.text].Pop();
        if(go != null)
        {
            Debug.Log("반환할 GameObject 없음");
            return;
        }
        StaticData241220.pool.Release(go, inputField.text);
    }
}
