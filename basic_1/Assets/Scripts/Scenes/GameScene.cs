using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;

    protected override void Init()
    {
        base.Init();
        SceneType = Defines.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inven>();

        // �÷��̾ �ش� ��ų�� ���� 4�� �ڿ� ��ų�� �����ٰ� ������

        // �ڷ�ƾ���� �� ��ų�� �����ϰ� 4�� �ڿ� �ٽ� �ڷ�ƾ���� ���ƿͼ� ��ų�� �����Ŵ
        // �ڷ�ƾ�� ������� �ʴ´ٸ� 4�ʰ� �������� ��� �˻��ϴٰ� ��ų�� �����Ű�� ����� �ʿ��ѵ� 4�ʰ� �������� �˻��ϴ� ������ �ٸ� �۾��� ������ �� ����.
        // 4�ʶ�� �ð��� ����ǰ� �����Ƿ� �ڷ�ƾ�� ����ؼ� ��Ƽ �½�ŷ �۾��� �� �� �ִ�.

        // �ڷ�ƾ ���ø� ��� 
        // �ѿ��� ������� �������� ��Ÿ���� 1���̶�� ������ ���� �ٽ� ���� ���ؼ� 1���� ��ٷ��� �Ǵµ� 
        // 1���� �������� ���� �ٽ� Ȱ��ȭ ���·� ����� ���� �ڷ�ƾ���� ���� ���ʿ� ���� ������ �ڷ�ƾ�� ȣ���ϰ� 
        // ������ 1���� �ٸ� �۾��� ������ �� �ֵ��� �ϴٰ� �÷��̾ �߰����� �ź��� ȭ��� �� ������ ���߽�Ű�� 
        // �ٽ� �ڷ�ƾ�� ȣ���ؼ� ���� ��Ÿ���� ���ҽ�Ű�� ���࿡ ���� ��Ÿ���� �������� �ڷ�ƾ�� ������ 

        co = StartCoroutine("ExplodeAfter4Seconds", 4.0f);
        Debug.Log("�÷��̾� �̵�");
        StartCoroutine("CoStopExplode", 2.0f);        
    }

    IEnumerator CoStopExplode(float seconds)
    {
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Excute");
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }            
    }
    
    IEnumerator ExplodeAfter4Seconds(float seconds)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(seconds); // seconds ���� �ش� �ڷ�ƾ�� ����(���� �ϴ� ���� �ٸ� �۾��� �����ϴٰ� ������ �ʰ� ������ �ٽ� �ش� �ڷ�ƾ�� ȣ��)
        Debug.Log("Explode Excute");
        co = null; // Explode ���������ϱ� �ش� �ڷ�ƾ ��� X
    }

    public override void Clear()
    {

    }



}
