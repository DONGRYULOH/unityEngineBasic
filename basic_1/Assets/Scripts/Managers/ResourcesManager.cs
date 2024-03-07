using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{

    public T Load<T>(string path) where T : Object {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if(index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null) 
    {
        // ���������� ������Ʈ�� �ش� ��ġ�� ������ �ִ��� Ȯ��
        // �ش� ������Ʈ�� Ǯ�� ����̶�� Ǯ�ȿ� �������� �����ϱ� ������ ������ �ʰ� Ǯ��ü �����߿��� �ش� ������Ʈ�� �ִ��� Ȯ��
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null) {
            Debug.Log("��ΰ� �߸���");
            return null;
        }

        // Ǯ�� ����� ��ü�� ���ؼ��� Ǯ���� �ش� ��ü�� ������        
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        // ex) �÷��̾� ���� ����â UI ���� �ѹ� �ν��Ͻ�ȭ �ϰ� ��� ���Ǵ� ���� Ǯ���� �� �ʿ� �����Ƿ� �׳� �� ������Ʈ�� �ν��Ͻ�ȭ ����
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    public void Destroy(GameObject obj) {
        if (obj == null)
            return;

        // ���࿡ Ǯ���� �ʿ��� ����̶�� �ش� ������Ʈ�� �޸𸮿��� �������� �ʰ� Ǯ(����)�� �̵���Ŵ
        if (obj.GetComponent<Poolable>() != null)
            Managers.Pool.Push(obj.GetComponent<Poolable>());
        else        
            Object.Destroy(obj);
    }

}
