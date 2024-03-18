using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���������� ���Ǵ� �Ŵ����� �ƴ϶� �ش� ������������ ���Ǵ� Ư���� �Ŵ���
// ex) �÷��̾ ���͸� �������� ��� ó�����ٰ���

public class GameManagerEx
{
    GameObject player; // �÷��̾ �Ѹ�ۿ� �����Ƿ� 

    // TODO : ���߿� ������ �����Ҷ��� ī�װ����� ����� �� ������Ʈ�� ID(����Ű)�� ������
    // Dictionary<int, GameObject> _player = new Dictionary<int, GameObject>();
    // Dictionary<int, GameObject> _monsters = new Dictionary<int, GameObject>();
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    public Action<int> OnSpawnEvent;

    public GameObject GetPlayer() { return player; }

    public GameObject Spawn(Defines.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Defines.WorldObject.Monster:
                _monsters.Add(go);
                if(OnSpawnEvent != null)                
                    OnSpawnEvent.Invoke(1);                
                break;
            case Defines.WorldObject.Player:
                player = go;
                break;
        }        
        return go;
    }

    public Defines.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Defines.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void DeSpawn(GameObject go)
    {
        Defines.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Defines.WorldObject.Monster:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }                                           
                }
                break;
            case Defines.WorldObject.Player:
                {
                    if (player == go)
                        player = null;
                }                
                break;
        }

        Managers.Resource.Destroy(go);
    }

}
