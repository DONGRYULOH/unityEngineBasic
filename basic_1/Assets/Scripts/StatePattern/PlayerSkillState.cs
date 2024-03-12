using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : MonoBehaviour, PlayerState
{
    private PlayerController _playerController;

    void Update()
    {
        if (_playerController != null)
        {
            if (_playerController.playerState == PlayerController.PlayerStateEnum.Skill)
            {                
                // 락온인 경우 해당 해당 대상을 바라보게 처리
                if (_playerController.LockTarget != null)
                {
                    Vector3 dir = _playerController.LockTarget.transform.position - transform.position;
                    Quaternion quat = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
                }

                SkillAnimationState(GetComponent<Animator>());
            }
        }
    }

    // 애니메이션에 추가한 이벤트 발동
    void OnHitEvent()
    {        
        if (_playerController.StopSkill)
        {            
            _playerController.PlayerWait();
        }
        else
        {
            _playerController.playerState = PlayerController.PlayerStateEnum.Skill;
        }
    }

    public void SkillAnimationState(Animator anim)
    {
        _playerController.playerState = PlayerController.PlayerStateEnum.Skill;
        anim.Play("ATTACK");
    }

    public void Handle(PlayerController playerController)
    {
        if (playerController == null)
            _playerController = gameObject.GetOrAddComponent<PlayerController>();
        else
            _playerController = playerController;

        SkillAnimationState(GetComponent<Animator>());
    }
}
