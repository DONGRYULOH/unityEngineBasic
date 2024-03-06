using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : MonoBehaviour, PlayerState
{
    private PlayerController _playerController;

    void UpdateMoving()
    {
        Vector3 dir = _playerController.destPos - transform.position;

        // 목적지 까지 도달한경우 (실수에서 실수를 빼면 오차범위가 있기 때문에 딱 0으로 나눠떨어지지 않음)
        if (dir.magnitude < 0.0001f)
        {
            Debug.Log("목적지 도착!");
            _playerController.PlayerWait();
        }
        else
        {
            float moveDist = Mathf.Clamp(_playerController.speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            MovingAnimationState(GetComponent<Animator>(), _playerController.speed);
        }
    }

    public void MovingAnimationState(Animator anim, float playerSpeed)
    {
        anim.SetFloat("Speed", playerSpeed); // 현재 게임상태에 대한 정보를 애니메이션 파라미터쪽으로 넘겨줌
    }

    // 애니메이션 이벤트 
    public void OnRunEvent()
    {
        Debug.Log("run event!");
    }

    void Update()
    {
        if(_playerController != null)
        {
            if (_playerController.playerState == PlayerController.PlayerStateEnum.Moving)
            {
                UpdateMoving();
            }
        }        
    }

    public void Handle(PlayerController playerController)
    {
        if (playerController != null)
            _playerController = playerController;

        _playerController.playerState = PlayerController.PlayerStateEnum.Moving;        
    }
}
