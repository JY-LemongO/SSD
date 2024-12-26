using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup : UIBase, IDragableUI_JY
{
    private Vector2 _pointerDownOffset;

    public override void Setup()
    {
        Debug.Log($"{GetType().Name}:: Setup");
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        // Drag 시 포인터 위치 정중앙이 아닌 찍은 그 지점부터 움직이도록 Offset 설정.
        // 현재 포인터 다운 한 UI를 FrontUI로 바꿈.

        _pointerDownOffset = (Vector2)transform.position - eventData.position;
        UIManager.Instance.SetFrontUI(this);
    }
    public virtual void OnDrag(PointerEventData eventData)
    {        
        // 움직임만 담당.
        // 포인터를 클릭 지점부터 움직임.
        transform.position = eventData.position + _pointerDownOffset;
    }    
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        // 딱히 뭐 없어도 될 것 같긴 한데 화면 밖으로 나갈 것 같으면 안으로 집어 넣어 준다?
    }

    public void CloseUI() => UIManager.Instance.CloseUI(this);

    protected override void Dispose()
    {

    }
}
