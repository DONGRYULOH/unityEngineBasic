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
                // mouseAction 대리자에 등록되있는 함수의 인자에 값을 넣어서 Invoke를 호출시킴
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
