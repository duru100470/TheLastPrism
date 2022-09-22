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

    private void Update()
    {   
        if (playerController.stateMachine.CurruentState.GetType() == typeof(PlayerStun))
            return;
        if (playerController.stateMachine.CurruentState.GetType() == typeof(PlayerDead))
            return;

        SelectSlot();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowItem();
        }
    }

    private void SelectSlot()
    {
        // Select Inventory slots
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UIManager.Instance.Inventory.SelectedSlot = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UIManager.Instance.Inventory.SelectedSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            UIManager.Instance.Inventory.SelectedSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            UIManager.Instance.Inventory.SelectedSlot = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            UIManager.Instance.Inventory.SelectedSlot = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            UIManager.Instance.Inventory.SelectedSlot = 5;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            UIManager.Instance.Inventory.SelectedSlot = 6;
        if (Input.GetKeyDown(KeyCode.Alpha8))
            UIManager.Instance.Inventory.SelectedSlot = 7;
        if (Input.GetKeyDown(KeyCode.Alpha9))
            UIManager.Instance.Inventory.SelectedSlot = 8;
    }

    private void ThrowItem()
    {
        int selected = UIManager.Instance.Inventory.SelectedSlot;
        Item item = UIManager.Instance.Inventory.GetItemInfo(selected);

        if (item == null)
            return;

        // Create item prefab
        GameObject itemPrefab = Instantiate(GameManager.Instance.ItemPrefab);
        itemPrefab.transform.position = this.transform.position;

        // CreateItemCounter(itemPrefab);

        ItemController ic = itemPrefab.GetComponent<ItemController>();
        ic.item = new Item(item);
        ic.UpdateSprite();
        ic.SetAcquirable(3f);

        // Clear selected slot
        UIManager.Instance.Inventory.RemoveItem(selected);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Item")) return;

        ItemController iController = other.GetComponent<ItemController>();
        if (!iController.IsAcquirable)
            return;

        UIManager.Instance.Inventory.AcquireItem(ref iController.item);
        if (iController.item.Amount == 0)
            Destroy(other.gameObject);
    }
}
