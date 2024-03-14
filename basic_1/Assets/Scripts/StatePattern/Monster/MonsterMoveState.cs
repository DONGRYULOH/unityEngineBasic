using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMoveState : MonoBehaviour, MonsterState
{
    private MonsterController _monsterController;

    public void MovingAnimationState(Animator anim)
    {        
        _monsterController.State = Defines.State.Moving;
        anim.Play("RUN");
    }

    void UpdateMoving()
    {
        // ������ �����Ÿ��� ���ݴ���� �ִ��� üũ
        if (_monsterController.LockTarget != null)
        {
            _monsterController.DestPos = _monsterController.LockTarget.transform.position;
            float distance = (_monsterController.DestPos - transform.position).magnitude;
            // �÷��̾ ���� ���� �����Ÿ� ���� ������ ����
            if (distance <= _monsterController.AttackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                _monsterController.State = Defines.State.Skill;                
                return;
            }
        }

        // �÷��̾ ���� �����̴� ���
        Vector3 dir = _monsterController.DestPos - transform.position;
        Debug.Log(dir.magnitude);
        if (dir.magnitude < 1.0f)
        {
            _monsterController.RangeCheck = false;
            _monsterController.State = Defines.State.Wait;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.speed = _monsterController.Stat.MoveSpeed;
            nma.SetDestination(_monsterController.DestPos);            

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            MovingAnimationState(GetComponent<Animator>());
        }
    }

    public void Handle(MonsterController controller)
    {
        if (_monsterController == null)
            _monsterController = gameObject.GetOrAddComponent<MonsterController>();
        else
            _monsterController = controller;

        MovingAnimationState(GetComponent<Animator>());
        UpdateMoving();
    }
}
