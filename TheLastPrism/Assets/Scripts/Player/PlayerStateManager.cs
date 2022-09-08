using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStateManager : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Stun,
        Dead,
    }

    private StateMachine stateMachine;
    // 상태를 저장할 딕셔너리 생성
    private Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();
    public SpriteRenderer spriteRenderer { set; get; }
    public Rigidbody2D rigid2d { set; get; }
    public Collider2D coll { set; get; }

    // Initialize states
    private void Start()
    {
        IState idle = new PlayerIdle(this);
        IState run = new PlayerRun(this);
        IState jump = new PlayerJump(this);
        IState stun = new PlayerStun(this);
        IState dead = new PlayerDead(this);

        dicState.Add(PlayerState.Idle, idle);
        dicState.Add(PlayerState.Run, run);
        dicState.Add(PlayerState.Jump, jump);
        dicState.Add(PlayerState.Stun, stun);
        dicState.Add(PlayerState.Dead, dead);

        // 시작 상태를 Idle로 설정
        stateMachine = new StateMachine(dicState[PlayerState.Idle]);

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // 키보드 입력 받기 및 State 갱신
    private void Update()
    {
        KeyboardInput();
        stateMachine.DoOperateUpdate();
    }

    private void FixedUpdate() {
        stateMachine.DoOperateFixedUpdate();
    }

    // 키보드 입력 제어
    private void KeyboardInput()
    {
    }
}