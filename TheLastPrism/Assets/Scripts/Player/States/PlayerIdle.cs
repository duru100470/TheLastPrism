using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : IState
{
    private Player player;

    public PlayerIdle(Player player)
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
        // Transition
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            player.stateMachine.SetState(new PlayerRun(player));
        }
        else
        {
            player.rigid2d.velocity = new Vector2(0, player.rigid2d.velocity.y);
        }

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

    }
}
