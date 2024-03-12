using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이름을 Json 파일에 있는 이름과 똑같이 맞춰줘야지 파싱이 됨

namespace Data
{
    #region Stat
    [Serializable]
    public class Stat
    {
        public int level;
        public int hp;
        public int attack;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);

            return dict;
        }
    }
    #endregion
}