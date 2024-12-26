using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup : UIBase, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public override void Setup() { }    

    public virtual void OnDrag(PointerEventData eventData)
    {
        Debug.Log("드래깅");
        transform.position = eventData.position;
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("포인터 다운");
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("포인터 업");
    }

    public void CloseUI() => UIManager.Instance.CloseUI(this);

    protected override void Dispose()
    {

    }
}
