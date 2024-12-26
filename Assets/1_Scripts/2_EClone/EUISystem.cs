using System;
using System.Collections.Generic;
using UnityEngine;

public class EUISystem : MonoBehaviour
{
    public static EUISystem Instance { get; private set; }

    // UI 타입 관리
    public List<EUIElement> elements;
    public Dictionary<int, EUIElement> elementsDict = new Dictionary<int, EUIElement>();

    // 현재 사용중인 UI관리 필요, 미구현

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            foreach(var element in elements)
            {
                elementsDict[element.metadata.typeId] = element;
            }
        }
    }
    
    // UI Show
    public EUIElement Show(int id, EUIElementParameter parameter)
    {
        GameObject newUIGo = Instantiate(elementsDict[id].gameObject);
        EUIElement newEUIElement = newUIGo.GetComponent<EUIElement>();
        newEUIElement.Init(parameter);

        return newEUIElement;
    }

    // UI Close (Instance ID)

    // UI Close All (Close 가능한 UI만)

}
