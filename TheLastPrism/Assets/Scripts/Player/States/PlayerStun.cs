using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : IState
{
    private PlayerController playerController;

    public PlayerStun(PlayerController playerController)
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