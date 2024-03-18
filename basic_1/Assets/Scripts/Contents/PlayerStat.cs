using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp { 
        get { return _exp; }
        // ���Ͱ� �׾����� ����ġ�� �ִ� �� �Ӹ� �ƴ϶� ����Ʈ�� �Ϸ��ϰų� ��� �̺�Ʈ�� ������������ ����ġ�� �ֱ� ������ ����ġ�� ����Ǵ� ���� �������� ����
        set
        {
            // 1.����ġ�� ������ ���� ������ ����ġ�� �����ϴ� �� �Ʒ��� �������� �ʵ��� ����            
            _exp = value;
            if (Managers.Data.StatDic.ContainsKey(Level))
            {
                Data.Stat stat = Managers.Data.StatDic[Level];
                if (_exp < stat.totalExp) _exp = stat.totalExp; // ���� ������ �����ϴ� �ּ� EXP ������ ����                
            }

            // 2.������ üũ
            // �ش� ����ġ�� ��� ������ �����ߴ��� üũ �� ������ ������ ����
            int level = Level; 
            while (true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDic.TryGetValue(level + 1, out stat) == false) // �� ���� ������ ���� ��� (����)
                    break;
                if (_exp < stat.totalExp) // �� ���� ������ ������ ���� ����ġ�� ���� ���� ����ġ�� �����ϴ��� üũ
                    break;
                level = stat.level;
            }

            if(Level != level)
            {
                Level = level;
                SetStat(Level);
            }
        }
    }
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 2;
        _defense = 5;
        _moveSpeed = 10.0f;
        _exp = 10;
        _gold = 0;
        SetStat(_level);
    }

    // ������ �Ҷ����� �ش� �÷��̾��� ������ ����
    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> stat = Managers.Data.StatDic;
        _hp = stat[_level].maxHp;
        _maxHp = stat[_level].maxHp;
        _attack = stat[_level].attack;
    }

    protected override void OnDead(Stat attacker)
    {
        if(gameObject != null)
        {
            gameObject.GetComponent<PlayerStat>().Exp -= 10;
        }

        Debug.Log("player ����ġ ����");
    }
}
