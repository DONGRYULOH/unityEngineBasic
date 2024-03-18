using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �����͸� �����ϴ� ���, ������ ���忡���� �����Ϳ� �ڵ带 �и��ؼ� �۾���
// ex) int hp = 100 --> �̷������� �ϵ��ڵ��ؼ� �־����� �ʰ� ������ ���Ͽ��� �ҷ���
public class DataManager
{
    // ����, NPC, ... ��� ���� ������ ����

    public Dictionary<int, Data.Stat> StatDic { get; private set; } = new Dictionary<int, Data.Stat>();
    // public Dictionary<string, Data.NPC> NpcDic { get; private set; } = new Dictionary<string, Data.NPC>();

    public void Init()
    {
        // "StatData"��� Json ������ �ش� Ÿ��(Loader)�� ���缭 �Ľ�
        Data.StatData loader = LoadJson<Data.StatData, int, Data.Stat>("StatData");
        StatDic = loader.MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : DataLoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text); // json ������ �ش� Loader Ÿ��(StatData)�� ���缭 �Ľ�(��ȯ)        
    }
}
