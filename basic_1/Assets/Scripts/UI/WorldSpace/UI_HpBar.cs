using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HpBar : UI_Base
{
    enum GameObjects
    {
        HpBar // HpBar��� �̸��� ������ �ִ� ���ӿ�����Ʈ�� �ش� ������Ʈ(UI_HpBar)�� �ٿ��ִ°��� �ڵ�ȭ ��Ű�� ���Ѱ�
    }

    Stat _stat;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {        
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = gameObject.transform.parent; // "UI_HpBar" ������Ʈ�� �پ��ִ� ������Ʈ�� �θ��� transform.parent; ������ �ǹ�
        transform.position = parent.position + (Vector3.up * (parent.GetComponent<Collider>().bounds.size.y));

        // ü�� UI�� ī�޶�� ���� ������ ������
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.Hp / (float)_stat.MaxHp; // �ش� ĳ������ �ִ�HP�� ���� ���� hp�� ����
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetGameObject((int)GameObjects.HpBar).GetComponent<Slider>().value = ratio;
    }
}
