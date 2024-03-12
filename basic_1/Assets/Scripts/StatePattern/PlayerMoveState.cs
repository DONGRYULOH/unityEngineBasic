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
        // �÷��̾� �����Ÿ� �ȿ� ���Ͱ� ������ ����(��, ���Ϳ� ���ؼ� ����ó���� �� ��츸)
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

        Vector3 dir = _playerController.destPos - transform.position;

        // ������ ���� �����Ѱ�� (�Ǽ����� �Ǽ��� ���� ���������� �ֱ� ������ �� 0���� ������������ ����)
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
            MovingAnimationState(GetComponent<Animator>(), _playerController.Stat.MoveSpeed);
        }
    }

    public void MovingAnimationState(Animator anim, float playerSpeed)
    {
        // �÷��̾��� ���¿� �ִϸ��̼� ��ȯ�� ���� ����Ǵϱ� �ϳ��� ��� ����
        _playerController.playerState = PlayerController.PlayerStateEnum.Moving;

        // �ִϸ��̼� ���� ��ũ�� ���ߴ� �۾��� �����ʰ� �ִϸ��̼� ��� �ڵ忡�� �����ϵ��� ���� 
        // �ִϸ��̼��� ���������� �ڵ忡�� ��ũ�� �����ִ� �۾��� �������Ƿ� ������ �ڵ忡���� �����ϵ��� �ϴ°� ���� ���� ����
        anim.Play("RUN");
        // anim.CrossFade("RUN", 0.1f);
        // anim.SetFloat("Speed", playerSpeed); // ���� ���ӻ��¿� ���� ������ �ִϸ��̼� �Ķ���������� �Ѱ���
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

        MovingAnimationState(GetComponent<Animator>(), _playerController.Stat.MoveSpeed);
    }
}
