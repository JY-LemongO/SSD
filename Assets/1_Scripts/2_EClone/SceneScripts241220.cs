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
        newGo.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));

        if (!activeGoDict.ContainsKey(inputField.text))
        {
            activeGoDict[inputField.text] = new Stack<GameObject>();
        }

        activeGoDict[inputField.text].Push(newGo);
        Debug.Log (activeGoDict[inputField.text].Count);
    }

    public void OnMinusClick()
    {
        GameObject go = activeGoDict[inputField.text].Pop();

        if(go == null)
        {
            Debug.Log("반환할 GameObject 없음");
            return;
        }

        StaticData241220.pool.Release(go, inputField.text);
    }
}
