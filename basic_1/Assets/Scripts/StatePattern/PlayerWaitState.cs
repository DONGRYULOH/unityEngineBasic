using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaitState : MonoBehaviour, PlayerState
{
    private PlayerController _playerController;

    public void WaitAnimationState(Animator anim)
    {
        _playerController.playerState = PlayerController.PlayerStateEnum.Wait;
        anim.SetFloat("Speed", 0.0f);
    }

    public void Handle(PlayerController playerController)
    {
        if (playerController != null)
            _playerController = playerController;

        WaitAnimationState(GetComponent<Animator>());
    }
}