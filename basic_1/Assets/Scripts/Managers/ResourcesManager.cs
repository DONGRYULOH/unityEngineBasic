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
            Debug.Log("경로가 잘못됨");
            return null;
        }

        GameObject go = Object.Instantiate(prefab, parent);

        // Prefab으로 인스턴스 생성시 Clone 이라는 이름이 붙으니까 삭제하는 기능 추가
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
