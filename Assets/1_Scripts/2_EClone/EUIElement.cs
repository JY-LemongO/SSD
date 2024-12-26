using System;
using UnityEngine;

//[CreateAssetMenu(fileName = "EUI_Type1", menuName ="EClone/SO/UI/Type1")] // 상속받는 클래스에 추가
public class EUIElementMetadata : ScriptableObject // 사전 정의
{
    [SerializeField] public int typeId;
    [SerializeField] public GameObject baseGo;
}
public class EUIElementParameter // 매개 변수
{
    [SerializeField] Action OnShow;
    [SerializeField] Action OnClose;
}
public class EUIElement : MonoBehaviour // 본체. CloneUIElementData의 baseGo에 붙일 컴포넌트
{
    // 역참조를 피하고 싶은데
    // CloneUIElementMetadata_Type1 의 baseGo 의 CloneUIElement_Type1 에서,
    // CloneUIElementMetadata_Type1 의 default_image 와 default_message 를 얻을 방법이 생각나지 않는다
    [SerializeField]
    public EUIElementMetadata metadata;

    public Action OnShow;
    public Action OnClose;

    public virtual void Init(EUIElementParameter p)
    {
        //var parameter = p as T;
    }

    public EUIElement Show()
    {
        gameObject.SetActive(true);
        OnShow?.Invoke();
        return this;
    }

    public EUIElement Close()
    {
        gameObject.SetActive(false);
        OnClose?.Invoke();
        return this;
    }
}
