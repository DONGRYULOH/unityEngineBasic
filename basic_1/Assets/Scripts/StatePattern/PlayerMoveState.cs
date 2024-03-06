using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : MonoBehaviour, PlayerState
{
    private PlayerController _playerController;

    void UpdateMoving()
    {
        Vector3 dir = _playerController.destPos - transform.position;

        // ������ ���� �����Ѱ�� (�Ǽ����� �Ǽ��� ���� ���������� �ֱ� ������ �� 0���� ������������ ����)
        if (dir.magnitude < 0.0001f)
        {
            Debug.Log("������ ����!");
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
        anim.SetFloat("Speed", playerSpeed); // ���� ���ӻ��¿� ���� ������ �ִϸ��̼� �Ķ���������� �Ѱ���
    }

    // �ִϸ��̼� �̺�Ʈ 
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
