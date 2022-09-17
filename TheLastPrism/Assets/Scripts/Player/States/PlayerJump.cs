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
        player.anim.SetBool("isJumping", true);
        player.anim.speed = 0.7f;

        if (player.IsThereLand() == false || player.JumpCount == 0)
        {
            return;
        }

        player.rigid2d.velocity = new Vector2(player.rigid2d.velocity.x, 0);
        player.IsCoyoteTimeEnable = false;
        player.StartCoroutine(ControlJump());

        player.IsJumping = true;
        player.JumpCount--;
    }
    public void OperateExit()
    {
        player.IsJumping = false;
        player.JumpCount = player.JumpMaxCount;
        player.IsCoyoteTimeEnable = true;

        player.anim.SetBool("isJumping", false);
        player.anim.speed = 0.3f;
    }
    public void OperateUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
            player.rigid2d.velocity = new Vector2(0, player.rigid2d.velocity.y);

        player.anim.SetFloat("ySpeed", player.rigid2d.velocity.y);
    }
    public void OperateFixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        player.HorizontalMove(h);

        // Transition
        if (player.rigid2d.velocity.y < 0)
        {
            if (player.IsThereLand())
            {
                if (h == 0)
                    player.stateMachine.SetState(new PlayerIdle(player));
                else
                    player.stateMachine.SetState(new PlayerRun(player));
            }
        }
    }

    private IEnumerator ControlJump()
    {
        for (player.JumpTime = 0; player.JumpTime <= player.JumpMaxTime; player.JumpTime += 0.05f)
        {
            player.rigid2d.AddForce(Vector2.up * player.JumpPower * (player.JumpMaxTime - player.JumpTime), ForceMode2D.Impulse);
            
            if (Input.GetKey(KeyCode.Space))
                yield return new WaitForSeconds(0.05f);
            else
                break;
        }
        yield return null;
    }
}