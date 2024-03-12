using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �����͸� �����ϴ� ���, ������ ���忡���� �����Ϳ� �ڵ带 �и��ؼ� �۾���
// XML, JSON ���� ����


// ��ȯ�� �����͸� Dictionary ���·� ��� ����
public interface ILoader<Key, value>
{
    Dictionary<Key, value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDic { get; private set; } = new Dictionary<int, Data.Stat>();

    public void Init()
    {
        StatDic = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        // json ���� �ش� Ÿ������ �Ľ�(��ȯ)�ϴ� ���
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
