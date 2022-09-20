using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int health;
    private PlayerController playerController;

    public int Health => health;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void GetDamage(int amount, bool ignoreInvTime)
    {
        health -= amount;
        playerController.GetStunned(1f);
        EventManager.Instance.PostNotification(EVENT_TYPE.PlayerHPChanged, this, amount);
    }
}
