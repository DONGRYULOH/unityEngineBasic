using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveState : MonoBehaviour, PlayerState
{
    private PlayerController _playerController;

    void Update()
    {
        if (_playerController != null)
        {
            if (_playerController.playerState == PlayerController.PlayerStateEnum.Moving)
            {
                UpdateMoving();
            }
        }
    }

    void UpdateMoving()
    {
        // 플레이어 사정거리 안에 몬스터가 있으면 공격(단, 몬스터에 대해서 락온처리를 한 경우만)
        if (_playerController.LockTarget != null)
        {
            float distance = (_playerController.destPos - transform.position).magnitude;
            // 플레이어의 사정거리(TODO: 추후에 사정거리 설정필요)에 몬스터가 들어오면 공격처리
            if (distance <= 2.0f)
            {                
                _playerController.PlayerSkill();
                return;
            }
        }

        Vector3 dir = _playerController.destPos - transform.position;

        // 목적지 까지 도달한경우 (실수에서 실수를 빼면 오차범위가 있기 때문에 딱 0으로 나눠떨어지지 않음)
        if (dir.magnitude < 0.1f)
        {                        
            _playerController.PlayerWait();
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();            
            float moveDist = Mathf.Clamp(_playerController.Stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            nma.Move(dir.normalized * moveDist); // 방향벡터

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.blue);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    _playerController.PlayerWait();
                return;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            MovingAnimationState(GetComponent<Animator>(), _playerController.Stat.MoveSpeed);
        }
    }

    public void MovingAnimationState(Animator anim, float playerSpeed)
    {
        // 플레이어의 상태와 애니메이션 변환은 같이 수행되니까 하나로 묶어서 수행
        _playerController.playerState = PlayerController.PlayerStateEnum.Moving;

        // 애니메이션 툴과 싱크를 맞추는 작업을 하지않고 애니메이션 제어도 코드에서 관리하도록 설정 
        // 애니메이션이 많아질수록 코드에서 싱크를 맞춰주는 작업도 많아지므로 오히려 코드에서만 제어하도록 하는게 좋을 수도 있음
        anim.Play("RUN");
        // anim.CrossFade("RUN", 0.1f);
        // anim.SetFloat("Speed", playerSpeed); // 현재 게임상태에 대한 정보를 애니메이션 파라미터쪽으로 넘겨줌
    }

    // 애니메이션 이벤트 
    public void OnRunEvent()
    {
        // Debug.Log("run event!");
    }

    public void Handle(PlayerController playerController)
    {
        if (playerController == null)
            _playerController = gameObject.GetOrAddComponent<PlayerController>();
        else
            _playerController = playerController;

        MovingAnimationState(GetComponent<Animator>(), _playerController.Stat.MoveSpeed);
    }
}
