using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;
    public Vector3 destPos;

    private PlayerStateContext _playerStateContext;
    private PlayerState dieState, moveState, waitState, skillState;
    
    int _mask = (1 << (int)Defines.Layer.Ground) | (1 << (int)Defines.Layer.Monster1); // Layer 마스킹 처리    
    GameObject _lockTarget; // 몬스터를 클릭시 타켓팅 설정
    public GameObject LockTarget { get { return _lockTarget; } }

    public enum PlayerStateEnum
    {
        Die,
        Moving,
        Wait,
        Skill,
    }
    public PlayerStateEnum playerState = PlayerStateEnum.Wait;

    public PlayerStateEnum PlayerState { get { return playerState; } set { playerState = value; } }
    public PlayerStat Stat { get { return _stat; }}


    bool _stopSkill = false;
    public bool StopSkill { get { return _stopSkill; } }

    void Start()
    {
        // 플레이어 스탯(체력, 방어력 ..등) 적용
        _stat = gameObject.GetOrAddComponent<PlayerStat>();

        // 플레이어 UpBar 생성
        Managers.UI.MakeWorldSpaceUI<UI_HpBar>(transform);

        // 입력(마우스) 발생시 플레이어에게 발생할 이벤트
        Managers.Input.mouseAction -= OnMouseEvent;
        Managers.Input.mouseAction += OnMouseEvent;

        // state 패턴 호출
        StatePattern();         
    }

    void Update()
    {
        
    }
   
    // 플레이어가 스킬을 시전중인 상태일때 마우스를 클릭하는 경우 다른 메소드가 실행되면 안되기 때문에 분기처리를 해줬음
    void OnMouseEvent(Defines.MouseEvent evt)
    {
        switch (playerState)
        {
            case PlayerStateEnum.Wait:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerStateEnum.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerStateEnum.Skill: // 공격을 하고 있는 상태라면 이동모션 불가능
                {
                    if (evt == Defines.MouseEvent.PointerUp)
                        _stopSkill = true; // 마우스 클릭이 끝났을때 다른 모션 가능
                }                
                break;
        }
    }
    
    void OnMouseEvent_IdleRun(Defines.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        // Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 2.0f);

        switch (evt)
        {            
            case Defines.MouseEvent.PointerDown: // 마우스 클릭을 놓은 상태에서 최초로 해당 지점을 마우스로 클릭했을 때
                {
                    if (raycastHit)
                    {
                        _stopSkill = false;

                        // 몬스터 클릭시 락온 설정
                        if (hit.collider.gameObject.layer == (int)Defines.Layer.Monster1)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;

                        destPos = hit.point;                        
                        PlayerMove();
                    }
                }
                break;
            case Defines.MouseEvent.Press: // 마우스를 계속 누르고 있는 상태
                {
                    if (_lockTarget == null && raycastHit)
                        destPos = hit.point;
                }
                break;
            case Defines.MouseEvent.PointerUp: // 클릭하고 바로땐경우 공격스킬을 한번만 실행
                _stopSkill = true;
                break;
        }
    }

    // -------------- 플레이어 state 패턴 --------------------
    public void StatePattern()
    {        
        _playerStateContext = new PlayerStateContext(this);

        // PlayerController 컴포넌트가 붙어있는 오브젝트에 PlayerMoveState 클래스도 컴포넌트로 붙임
        moveState = gameObject.AddComponent<PlayerMoveState>();
        waitState = gameObject.AddComponent<PlayerWaitState>();
        dieState = gameObject.AddComponent<PlayerDieState>();
        skillState = gameObject.AddComponent<PlayerSkillState>();
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

    public void PlayerSkill()
    {
        _playerStateContext.Transition(skillState);
    }

}
