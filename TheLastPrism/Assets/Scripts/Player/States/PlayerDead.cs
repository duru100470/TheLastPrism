using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : IState
{
    private PlayerController playerController;

    public PlayerDead(PlayerController playerController)
    {
        this.playerController = playerController;
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