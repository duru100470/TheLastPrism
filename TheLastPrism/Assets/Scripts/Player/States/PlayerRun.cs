using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : IState
{
    private Player player;

    public PlayerRun(Player player)
    {
        this.player = player;
    }

    public void OperateEnter()
    {
    }

    public void OperateExit()
    {
    }

    public void OperateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.stateMachine.SetState(new PlayerJump(player));
        }
        
        if (player.IsThereLand() == false)
        {
            player.stateMachine.SetState(new PlayerJump(player));
        }
    }

    public void OperateFixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        // Check Side
        RaycastHit2D raycastHit2DRight = Physics2D.Raycast(player.rigid2d.position, Vector3.right, 0.35f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(player.rigid2d.position, Vector3.left, 0.35f, LayerMask.GetMask("Ground"));

        if (raycastHit2DRight.collider == null && h > 0)
            player.rigid2d.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (raycastHit2DLeft.collider == null && h < 0)
            player.rigid2d.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (player.rigid2d.velocity.x > player.MaxSpeed)
            player.rigid2d.velocity = new Vector2(player.MaxSpeed, player.rigid2d.velocity.y);

        if (player.rigid2d.velocity.x < player.MaxSpeed * (-1))
            player.rigid2d.velocity = new Vector2(player.MaxSpeed * (-1), player.rigid2d.velocity.y);

        if (h == 0)
        {
            player.stateMachine.SetState(new PlayerIdle(player));
        }
    }
}