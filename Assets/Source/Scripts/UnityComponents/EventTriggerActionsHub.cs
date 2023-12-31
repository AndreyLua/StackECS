using UnityEngine.EventSystems;
using System;

public class EventTriggerActionsHub : EventTrigger
{
    public event Action<PointerEventData> OnPointerDownEvent;
    public event Action<PointerEventData> OnPointerEnterEvent;
    public event Action<PointerEventData> OnPointerExitEvent;
    public event Action<PointerEventData> OnPointerUpEvent;
    public event Action<PointerEventData> OnDragBeginEvent;
    public event Action<PointerEventData> OnDraggingEvent;
    public event Action<PointerEventData> OnDragEndEvent;

    public override void OnPointerDown(PointerEventData eventData) => OnPointerDownEvent?.Invoke(eventData);
    public override void OnPointerUp(PointerEventData eventData) => OnPointerUpEvent?.Invoke(eventData);

    public override void OnBeginDrag(PointerEventData eventData) => OnDragBeginEvent?.Invoke(eventData);
    public override void OnDrag(PointerEventData eventData) => OnDraggingEvent?.Invoke(eventData);
    public override void OnEndDrag(PointerEventData eventData) => OnDragEndEvent?.Invoke(eventData);

    public override void OnPointerEnter(PointerEventData eventData) => OnPointerEnterEvent?.Invoke(eventData);
    public override void OnPointerExit(PointerEventData eventData) => OnPointerExitEvent?.Invoke(eventData);
}
