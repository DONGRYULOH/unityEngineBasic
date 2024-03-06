using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action keyAction = null;
    public Action<Defines.MouseEvent> mouseAction = null;

    bool pressedCk = false;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(Input.anyKey && keyAction != null)
        {
            keyAction.Invoke();
        }

        if(mouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                // mouseAction �븮�ڿ� ��ϵ��ִ� �Լ��� ���ڿ� ���� �־ Invoke�� ȣ���Ŵ
                mouseAction.Invoke(Defines.MouseEvent.Press);
                pressedCk = true;
            }
            else
            {
                if (pressedCk)
                {
                    mouseAction.Invoke(Defines.MouseEvent.Click);
                }
                pressedCk = false;
            }
        }
    }

    public void Clear()
    {
        keyAction = null;
        mouseAction = null;
    }
} 
