using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{   
    [SerializeField]
    public float speed = 10.0f;
    
    public Vector3 destPos;
    public float wait_run_ratio = 0.0f;

    private PlayerStateContext _playerStateContext;
    private PlayerState dieState, moveState, waitState;

    public enum PlayerStateEnum
    {
        Die,
        Moving,
        Wait,
    }
    public PlayerStateEnum playerState = PlayerStateEnum.Wait;

    void Start()
    {       
        // Managers.Resource.Instantiate("UI/Popup/UI_Btn");

        Managers.Input.mouseAction -= OnMouseClicked;
        Managers.Input.mouseAction += OnMouseClicked;

        // state 패턴 호출 
        _playerStateContext = new PlayerStateContext(this);

        // PlayerController 컴포넌트가 붙어있는 오브젝트에 PlayerMoveState 클래스도 컴포넌트로 붙임
        moveState = gameObject.AddComponent<PlayerMoveState>();
        waitState = gameObject.AddComponent<PlayerWaitState>();
        dieState = gameObject.AddComponent<PlayerDieState>();        
    }

    public void PlayerMove()
    {
        _playerStateContext.Transition(moveState);
    }

    public void PlayerWait()
    {
        _playerStateContext.Transition(waitState);
    }

    public void PlayerDie()
    {
        _playerStateContext.Transition(dieState);
    }

    void OnMouseClicked(Defines.MouseEvent evt)
    {
        if (playerState == PlayerStateEnum.Die)
            return;
                
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 2.0f);       

        RaycastHit hit; 
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            destPos = hit.point;
            PlayerMove();
            // Debug.Log($"Raycast Camera @{hit.collider.gameObject.name}");
        }
    }
}
