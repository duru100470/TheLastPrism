using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamage
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
    private PlayerController playerController;

    public int Health => health;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void GetDamage(int amount, float stunDuration, float invTime, bool ignoreInvTime)
    {
        health = Mathf.Max(0, health - amount);
        if (health == 0)
        {
            Dead();
            return;
        }
        StartCoroutine(Stun(stunDuration));
        EventManager.Instance.PostNotification(EVENT_TYPE.PlayerHPChanged, this, amount);
    }

    public void Dead()
    {
        playerController.stateMachine.SetState(new PlayerDead(playerController));
        EventManager.Instance.PostNotification(EVENT_TYPE.PlayerDead, this);
    }

    private IEnumerator Stun(float duration)
    {
        playerController.stateMachine.SetState(new PlayerStun(playerController));
        yield return new WaitForSeconds(duration);
        playerController.stateMachine.SetState(new PlayerIdle(playerController));
    }
}
