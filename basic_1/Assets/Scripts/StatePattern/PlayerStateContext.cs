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

    // 현재 플레이어의 상태
    public void Transition()
    {
        CurrentState.Handle(_playerController);
    }

    // 현재 플레이어 상태를 업데이트 후 그 상태로 변경 
    public void Transition(PlayerState state)
    {
        CurrentState = state;
        Transition();
    }
}
