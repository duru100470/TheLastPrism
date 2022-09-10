using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : IState
{
    private Player player;

    public PlayerJump(Player player)
    {
        this.player = player;
    }

    public void OperateEnter()
    {
        if (player.IsThereLand() == false || player.JumpCount == 0)
            return;

        player.rigid2d.AddForce(Vector2.up * player.JumpPower, ForceMode2D.Impulse);
        player.IsJumping = true;
        player.JumpCount--;
    }
    public void OperateExit()
    {
        player.IsJumping = false;
        player.JumpCount = player.JumpMaxCount;
    }
    public void OperateUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
            player.rigid2d.velocity = new Vector2(0, player.rigid2d.velocity.y);
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

        if (player.rigid2d.velocity.y < 0)
        {
            if (player.IsThereLand())
            {
                player.stateMachine.SetState(new PlayerIdle(player));
            }
        }
    }
}