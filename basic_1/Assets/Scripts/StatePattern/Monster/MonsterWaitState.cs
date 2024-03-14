 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaitState : MonoBehaviour, MonsterState
{
    private MonsterController _monsterController;

    public void Handle(MonsterController controller)
    {
        if (_monsterController == null)
            _monsterController = gameObject.GetOrAddComponent<MonsterController>();
        else
            _monsterController = controller;

                
        WaitAnimationState(GetComponent<Animator>());

        if(_monsterController.RangeCheck) RangeCheck();
    }

    public void RangeCheck()
    {        
        // 플레이어가 몬스터가 인지하는 반경안에 들어왔을때
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            return;

        float distance = (player.transform.position - _monsterController.transform.position).magnitude;
        if (_monsterController.ScanRange >= distance)
        {
            _monsterController.LockTarget = player;
            _monsterController.State = Defines.State.Moving;
            return;
        }           
    }

    public void WaitAnimationState(Animator anim)
    {
        _monsterController.State = Defines.State.Wait;
        anim.Play("WAIT");
    }

}
