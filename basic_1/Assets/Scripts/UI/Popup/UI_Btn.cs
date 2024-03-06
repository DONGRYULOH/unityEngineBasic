using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Btn : UI_Popup
{  
    // �ش� ������Ʈ�� ������Ʈ�� ������ �ڵ�ȭ ��ų UI ������Ʈ���� �̸���
    enum Buttons
    {
        PointBtn
    }

    enum Texts
    {
        PointTxt,
        ScoreTxt
    }

    enum GameObjects
    {
        TestObject
    }

    enum Images
    {
        ItemIcon
    }
    // -----------------------------------------------------------------------

    int score;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init(); // �θ��� Init() �޼ҵ� �����ؼ� Canvas Sort �۾� ����

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GameObject pb = Get<Button>((int)Buttons.PointBtn).gameObject;
        BindEvent(pb, BtnOnClicked, Defines.UIEvent.Click);

        // �͸� �޼ҵ� ���·� �̺�Ʈ �ڵ鷯�� ���� OnDragHandler �̺�Ʈ�� �߻��� ȣ���� �̺�Ʈ �ڵ鷯�� ��Ͻ�Ŵ
        GameObject go = Get<Image>((int)Images.ItemIcon).gameObject;
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Defines.UIEvent.Drag);
    }


    public void BtnOnClicked(PointerEventData data)
    {
        score++;
        GetText((int)Texts.ScoreTxt).text = $"���� : {score}";
    }
}
