using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected Vector3 destPos;

    [SerializeField]
    protected GameObject _lockTarget; // �ش� ������Ʈ Ÿ���� ����

    public Vector3 DestPos { get { return destPos; } set { destPos = value; } }
    public GameObject LockTarget { get { return _lockTarget; } set { _lockTarget = value; } }

    private void Start()
    {
        Init(); // �ڽĿ��� �������̵�� Init()�� ������ �ڽ��� Init() �����
    }
    
    public abstract void Init();
}
