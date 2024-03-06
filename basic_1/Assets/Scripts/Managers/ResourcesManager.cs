using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{

    public T Load<T>(string path) where T : Object {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null) {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null) {
            Debug.Log("��ΰ� �߸���");
            return null;
        }

        GameObject go = Object.Instantiate(prefab, parent);

        // Prefab���� �ν��Ͻ� ������ Clone �̶�� �̸��� �����ϱ� �����ϴ� ��� �߰�
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;
    }

    public void Destroy(GameObject obj) {
        if(obj != null)
        {
            Object.Destroy(obj);
        }        
    }

}
