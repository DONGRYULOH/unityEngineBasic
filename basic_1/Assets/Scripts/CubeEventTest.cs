using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeEventTest : MonoBehaviour, InterfaceTest
{
    public void Print()
    {
        throw new System.NotImplementedException();
    }

    // �ִϸ��̼� callback �̺�Ʈ�� ���
    // ĳ���Ͱ� ���� �ֵθ��� �ִϸ��̼��� �ִµ� ���� �������� �ε����� �� ���带 �߻���Ű�� callback �̺�Ʈ�� ȣ���Ų�ٴ���
    // �������� ������ ������ 
    void TestEventCallback()
    {
       // Debug.Log("event recived");
    }
}
