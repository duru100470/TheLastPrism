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

        player.HorizontalMove(h);

        // Transition
        if (h == 0)
        {
            player.stateMachine.SetState(new PlayerIdle(player));
        }
    }
}