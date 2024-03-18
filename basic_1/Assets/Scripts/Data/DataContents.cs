using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̸��� Json ���Ͽ� �ִ� �̸��� �Ȱ��� ��������� �Ľ��� ��
/*
    "StatData.Json"
{
    "stats" : [
        {
            "level" : "1",
            "maxHp" : "100",
            "attack" : "20",
            "totalExp" : "0"
        },
        {
            "level" : "2",
            "maxHp" : "150",
            "attack" : "30",
            "totalExp" : "10"
        },
        {
            "level" : "3",
            "maxHp" : "200",
            "attack" : "60",
            "totalExp" : "20"
        }
    ]
}
*/
namespace Data
{
    #region Stat
    [Serializable] // [Serializable] Ű���带 �ٿ����� Json ������ �ش� ���Ŀ� ���缭 �����͸� �־���
    public class Stat
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : DataLoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            // TODO : �ش� ������Ʈ�� �����ϰ� �ĺ��� �� �ִ� ���� key�� ����
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);

            return dict;
        }
    }
    #endregion
}