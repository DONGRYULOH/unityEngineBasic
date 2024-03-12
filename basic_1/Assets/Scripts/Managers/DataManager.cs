using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임의 데이터를 관리하는 방법, 실제로 현장에서는 데이터와 코드를 분리해서 작업함
// XML, JSON 으로 관리


// 변환한 데이터를 Dictionary 형태로 들고 있음
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
        // json 파일 해당 타입으로 파싱(변환)하는 방법
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
