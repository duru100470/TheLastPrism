using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public StateMachine stateMachine;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float maxFallingSpeed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private int jumpMaxCount;
    public int JumpCount { set; get; }
    public bool IsJumping { set; get; }

    public SpriteRenderer spriteRenderer { set; get; }
    public Rigidbody2D rigid2d { set; get; }
    public Collider2D coll { set; get; }

    public float MaxSpeed => maxSpeed;
    public float MaxFallingSpeed => maxFallingSpeed;
    public float JumpPower => jumpPower;
    public int JumpMaxCount => jumpMaxCount;

    // Initialize states
    private void Start()
    {
        stateMachine = new StateMachine(new PlayerIdle(this));

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        stateMachine.DoOperateUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.DoOperateFixedUpdate();
    }

    public bool IsThereLand()
    {
        Vector2 Pos1 = new Vector2(rigid2d.position.x + 0.25f, rigid2d.position.y);
        Vector2 Pos2 = new Vector2(rigid2d.position.x - 0.25f, rigid2d.position.y);

        RaycastHit2D raycastHit2DDown1 = Physics2D.Raycast(Pos1, Vector3.down, 0.6f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DDown2 = Physics2D.Raycast(Pos2, Vector3.down, 0.6f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DDown3 = Physics2D.Raycast(rigid2d.position, Vector3.down, 0.6f, LayerMask.GetMask("Ground"));

        if (raycastHit2DDown1.collider != null || raycastHit2DDown2.collider != null || raycastHit2DDown3.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}