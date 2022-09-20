using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : IState
{
    private PlayerController playerController;

    public PlayerJump(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OperateEnter()
    {
        playerController.anim.SetBool("isJumping", true);
        playerController.anim.speed = 0.7f;

        if (playerController.IsThereLand() == false || playerController.JumpCount == 0)
        {
            return;
        }

        playerController.rigid2d.velocity = new Vector2(playerController.rigid2d.velocity.x, 0);
        playerController.IsCoyoteTimeEnable = false;
        playerController.StartCoroutine(ControlJump());

        playerController.IsJumping = true;
        playerController.JumpCount--;
    }
    public void OperateExit()
    {
        playerController.IsJumping = false;
        playerController.JumpCount = playerController.JumpMaxCount;
        playerController.IsCoyoteTimeEnable = true;

        // Check Falling Damage

        playerController.anim.SetBool("isJumping", false);
        playerController.anim.speed = 0.3f;
    }
    public void OperateUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
            playerController.rigid2d.velocity = new Vector2(0, playerController.rigid2d.velocity.y);

        playerController.anim.SetFloat("ySpeed", playerController.rigid2d.velocity.y);
    }
    public void OperateFixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        playerController.HorizontalMove(h);

        // Transition
        if (playerController.rigid2d.velocity.y < 0)
        {
            if (playerController.IsThereLand())
            {
                if (h == 0)
                    playerController.stateMachine.SetState(new PlayerIdle(playerController));
                else
                    playerController.stateMachine.SetState(new PlayerRun(playerController));
            }
        }
    }

    private IEnumerator ControlJump()
    {
        for (playerController.JumpTime = 0; playerController.JumpTime <= playerController.JumpMaxTime; playerController.JumpTime += 0.05f)
        {
            playerController.rigid2d.AddForce(Vector2.up * playerController.JumpPower * (playerController.JumpMaxTime - playerController.JumpTime), ForceMode2D.Impulse);
            
            if (Input.GetKey(KeyCode.Space))
                yield return new WaitForSeconds(0.05f);
            else
                break;
        }
        yield return null;
    }
}