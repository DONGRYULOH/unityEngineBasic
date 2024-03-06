using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateContext
{
    public PlayerState CurrentState
    {
        get; set;
    }

    private readonly PlayerController _playerController;

    public PlayerStateContext(PlayerController playerController)
    {
        _playerController = playerController;
    }

    // ���� �÷��̾��� ����
    public void Transition()
    {
        CurrentState.Handle(_playerController);
    }

    // ���� �÷��̾� ���¸� ������Ʈ �� �� ���·� ���� 
    public void Transition(PlayerState state)
    {
        CurrentState = state;
        Transition();
    }
}
