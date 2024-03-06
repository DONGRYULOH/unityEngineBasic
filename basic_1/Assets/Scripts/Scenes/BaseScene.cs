using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Scene �Ŵ����� ���� ������ִ� ����? 
 * ��ũ�ҿ� ������ ���÷� ���ڸ� 
 * ���� �÷��̾ ������ ��ġ�Ǿ� �ִ°� �ƴ϶� Ŭ���̾�Ʈ�� ���ӿ� �����ϰ� �� �÷��̾�� ������ �����ؾ��� �������� �÷��̾ ���� ��ġ��Ŵ
*/

public abstract class BaseScene : MonoBehaviour
{    
    public Defines.Scene SceneType
    {
        get;
        protected set;
    } = Defines.Scene.Unknown;

    // Awake�� ���� ������Ʈ�� ��Ȱ���� ���¿����� ����� 
    // Start() ���� ��Ȱ���� ���¿��� ������ �ȵ� 
    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        // ���� EventSystem�� ������ ���� ���� �ٿ��� (EventSystem�� ������ UI �̺�Ʈ ���õ� �͵��� �ȸ����� ����)
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear();
}
