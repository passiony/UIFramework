using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
class UGUIEventListener:EventTrigger
{
    public Action<GameObject> onClick;
    public Action<GameObject> onDown;
    public Action<GameObject> onUp;
    public Action<GameObject> onEnter;
    public Action<GameObject> onExit;
    public Action<GameObject> onSelect;
    public Action<GameObject> onUpdateSlect;

    static public UGUIEventListener Get(GameObject go)
    {
        UGUIEventListener listener = go.GetComponent<UGUIEventListener>();
        if (listener == null) listener = go.AddComponent<UGUIEventListener>();
        return listener;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSlect != null) onUpdateSlect(gameObject);
    }
}
