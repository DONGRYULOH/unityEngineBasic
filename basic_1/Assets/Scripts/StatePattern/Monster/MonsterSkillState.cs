using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillState : MonoBehaviour, MonsterState
{
    private MonsterController _monsterController;

    public void Handle(MonsterController controller)
    {
        if (_monsterController == null)
            _monsterController = gameObject.GetOrAddComponent<MonsterController>();
        else
            _monsterController = controller;
        
        RockOnOrDeFaultEvent();
        SkillAnimationState(GetComponent<Animator>());
    }

    public void RockOnOrDeFaultEvent()
    {
        // ������ ��� �ش� �ش� ����� �ٶ󺸰� ó��
        if (_monsterController.LockTarget != null)
        {
            Vector3 dir = _monsterController.LockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }        
    }

    public void SkillAnimationState(Animator anim)
    {
        _monsterController.State = Defines.State.Skill;
        anim.Play("ATTACK");
    }

    void OnHitEvent()
    {
        if(_monsterController.LockTarget != null)
        {
            Stat targetStat = _monsterController.LockTarget.GetComponent<Stat>();
            Stat myStat = _monsterController.GetComponent<Stat>();
            int damage = Mathf.Max(0, myStat.Attack - targetStat.Defense);
            targetStat.Hp -= damage;

            if(targetStat.Hp > 0)
            {
                // �����Ÿ� üũ
                float distance = (_monsterController.LockTarget.transform.position - transform.position).magnitude;
                if (distance <= _monsterController.AttackRange)
                    _monsterController.State = Defines.State.Skill;
                else
                    _monsterController.State = Defines.State.Moving;
            }
            else
            {
                _monsterController.State = Defines.State.Wait;
                _monsterController.RangeCheck = false;
            }
        }
        else
        {
            _monsterController.State = Defines.State.Wait;
        }
    }
}
