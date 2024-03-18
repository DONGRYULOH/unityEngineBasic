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
    float _pressedTime = 0.0f;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Ű����� �Է��� �޴� ���
        // if(Input.anyKey && keyAction != null) keyAction.Invoke();

        // ���콺�� �Է��� �޴� ���
        if(mouseAction != null)
        {
            // ���콺 ��ư�� ������ �ִ��� �ƴ��� ���θ� ��ȯ�ϴµ� ���ڰ����� 0�� ���� ���� ��ư�� ���ؼ� üũ
            if (Input.GetMouseButton(0))
            {
                // ���ʿ� Ŭ���� �ϴ� ���� ���콺 Ŭ���� ��������� �ð��� üũ
                if(!pressedCk)
                {
                    mouseAction.Invoke(Defines.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                mouseAction.Invoke(Defines.MouseEvent.Press); // mouseAction �븮�ڿ� ��ϵ��ִ� �Լ��� ���ڿ� ���� �־ Invoke�� ȣ���Ŵ
                pressedCk = true;
            }
            else
            {
                if (pressedCk)
                {
                    // ���콺�� Ŭ���ϰ� �ٷ� �� ���¸� Ŭ������ �ν�
                    if(Time.time < _pressedTime + 0.2f)
                        mouseAction.Invoke(Defines.MouseEvent.Click);

                    mouseAction.Invoke(Defines.MouseEvent.PointerUp);
                }
                pressedCk = false;
                _pressedTime = 0.0f;
            }
        }
    }

    public void Clear()
    {
        keyAction = null;
        mouseAction = null;
    }
} 
