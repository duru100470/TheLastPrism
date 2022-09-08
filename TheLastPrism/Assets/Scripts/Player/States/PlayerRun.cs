using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : IState
{
    private PlayerStateManager player;

    public PlayerRun(PlayerStateManager player)
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
    }
    public void OperateFixedUpdate()
    {
    }
}