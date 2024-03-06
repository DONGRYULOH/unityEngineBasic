using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);        
        foreach (Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        // �κ��� ������ �߰�
        for(int i = 0; i < 8; i++)
        {           
            // parent : --> [��Ʈ ǥ��] 
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent : gridPanel.transform).gameObject;

            // UI_Inven_Item �̶�� ���ӿ�����Ʈ�� �پ��ִ� ������Ʈ(UI_Inven_Item ��ũ��Ʈ)�� ������
            UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item);
            invenItem.SetInfo("���ε� �׽�Ʈ");
        }
    }
}
