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
        // 1.������ �Ǿ��ִ� ������Ʈ�� ���� �̵� (�÷��̾� �����Ÿ� �ȿ� ���Ͱ� ������ ���ݻ��·� ����)
        if (_playerController.LockTarget != null)
        {
            float distance = (_playerController.destPos - transform.position).magnitude;
            // �÷��̾��� �����Ÿ�(TODO: ���Ŀ� �����Ÿ� �����ʿ�)�� ���Ͱ� ������ ����ó��
            if (distance <= 2.0f)
            {                
                _playerController.PlayerSkill();
                return;
            }
        }

        // 2.�ܼ��� �̵����� ���������� �����̴� ���
        Vector3 dir = _playerController.destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {                        
            _playerController.PlayerWait();
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();            
            float moveDist = Mathf.Clamp(_playerController.Stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            nma.Move(dir.normalized * moveDist); // ���⺤��

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.blue);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    _playerController.PlayerWait();
                return;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            MovingAnimationState(GetComponent<Animator>());
        }
    }

    public void MovingAnimationState(Animator anim)
    {
        // �÷��̾��� ���¿� �ִϸ��̼� ��ȯ�� ���� ����Ǵϱ� �ϳ��� ��� ����
        _playerController.playerState = PlayerController.PlayerStateEnum.Moving;

        // �ִϸ��̼� ���� ��ũ�� ���ߴ� �۾��� �����ʰ� �ִϸ��̼� ��� �ڵ忡�� �����ϵ��� ���� 
        // �ִϸ��̼��� ���������� �ڵ忡�� ��ũ�� �����ִ� �۾��� �������Ƿ� ������ �ڵ忡���� �����ϵ��� �ϴ°� ���� ���� ����
        anim.Play("RUN");        
    }

    // �ִϸ��̼� �̺�Ʈ 
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

        MovingAnimationState(GetComponent<Animator>());
    }
}
