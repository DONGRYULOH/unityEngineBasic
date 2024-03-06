using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
    UI�� ��� �̺�Ʈ�� �߻����� �� ��� ������ ������ �ڵ鷯 �̺�Ʈ
*/
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler ,IDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {        
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData); // ��ϵ� �̺�Ʈ �ڵ鷯 ȣ��
    }

    public void OnDrag(PointerEventData eventData)
    {
        // transform.position = eventData.position;
        Debug.Log("�巡�� ��..");

        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData); // ��ϵ� �̺�Ʈ �ڵ鷯 ȣ��
    }

}
