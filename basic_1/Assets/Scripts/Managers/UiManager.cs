using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*        
    �˾������� UI�� �˾��� �ƴ� �׳� UI�� �����ϴ� �Ŵ���
*/
public class UiManager 
{
    int _order = 10;

    Stack<UI_Popup> _popStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }        
    }

    // Popup ������ UI Canvas���� �켱������ ����
    public void SetCanvas(GameObject go, bool sort = true)
    {
        // ��� UI ��Ҵ� Canvas �ȿ� ��ġ�� 
        // ��� UI ��Ҵ� �ݵ�� � Canvas�� �ڽ��̾�� �Ѵ�.
        // Images, Text .. �� �̷��� UI�� ���鶧 � Canvas�� �ڽ����� ������ ���Եȴ�.
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        // sort = true ��� popup canvas ���� false��� �Ϲ� UI canvas
        if (sort)
        {
            canvas.sortingOrder = _order++;
        }            
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    // name : ������ �̸�
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popStack.Push(popup);       

        // ���� ������ ��� �˾��� �ϳ��� �� ���ӿ�����Ʈ ������ ������ ���� 
        // �˾��� ������ �����Ǵ� ��� ���� ������Ʈ���� �ʹ� ���Ƽ� ������ ���̴ϱ� ��� �����ִ� �뵵
        go.transform.SetParent(Root.transform);
         
        return popup;
    }

    // ���� �˾��� ���� ���ڷ� �޾Ƽ� ����ϴ� ��� 
    // �߰��� �ִ� �˾��� ������ �����Ϸ��� ��� �̷��� ����� ���� ������ �� ���� �ִ� �˾����� �����ǹǷ� �߰� ������ ������ �� ����
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popStack.Count == 0)
            return;

        // ���ÿ� �� �����ִ� ���Ҹ� ���ڷ� ���� �˾��� ��
        // ������ �� �����ִ� ���Һ��� pop�ϱ� ������ ���ڷ� ���� �˾��� �� ���� �ִ°��� �ƴ϶�� pop�� �������� ����
        if(_popStack.Peek() != popup)
        {
            Debug.Log("Close popup Failed");
            return;
        }

        CloseAllPopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popStack.Count == 0)
            return;

        UI_Popup popup = _popStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while(_popStack.Count > 0)
        {
            ClosePopupUI();
        }           
    }


    // ------------- Popup�� �ƴ� Scene UI --------------------------

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI; 

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }


    // ------------- Popup�� �ƴϰ� Scene �ƴ� sub UI --------------------------

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);        
    }

    // ���� ����Ǹ� ������ �ִ� �����͸� ���ֹ���
    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }

}
