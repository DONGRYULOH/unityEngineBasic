using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene
    {
        get
        {
            // BaseScene Ÿ�԰� ��ġ�ϴ� ù ��°�� �ε�� ��ü(Ȱ��ȭ ���¸�)�� ���� 
            // BaseScene�� �θ�� ������ �ִ� ��ü�� ��ȯ 
            // ex) ù��°�� �ε�� ���� Login�̶�� LoginScene ��ü�� ��ȯ
            return GameObject.FindObjectOfType<BaseScene>();
        }
    }

    public void LoadScene(Defines.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Defines.Scene type)
    {
        string name = System.Enum.GetName(typeof(Defines.Scene), type);     
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
