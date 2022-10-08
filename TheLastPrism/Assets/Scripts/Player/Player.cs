using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamage
{
    [Header("Health Properties")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;

    [Header("Attack Properties")]
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRange;
    private PlayerController playerController;

    [Header("Guitar")]
    [SerializeField]
    private GameObject previewStructure;

    public int Health => health;
    public GameObject PreviewStructure => previewStructure;

    private void OnDrawGizmos()
    {
        if (attackPoint is null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

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

        if (Input.GetMouseButtonDown(0))
        {
            ItemLeftClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ItemRightClick();
        }

        // For debug
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject test = Instantiate(GameManager.Instance.TestPrefab);

            test.transform.position = transform.position.Floor();
            test.GetComponent<Structure>().SetPosition(Coordinate.WorldPointToCoordinate(transform.position));
        }
    }

    private void ItemLeftClick()
    {
        Item usedItem = UIManager.Instance.Inventory.GetItemInfo(UIManager.Instance.Inventory.SelectedSlot);
        if (usedItem == null)
        {
            // Hand is empty;
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Coordinate coor = Coordinate.WorldPointToCoordinate(point);

            if (Coordinate.Distance(Coordinate.WorldPointToCoordinate(transform.position), coor) > 2) return;

            TileManager.Instance.TileArray[coor.X, coor.Y]?.GetDamage(1, DAMAGE_TYPE.Hand, 0, true);
            return;
        }

        usedItem.OnLeftClick();

        UIManager.Instance.Inventory.FreshInventory();
        if (usedItem.Amount == 0) UIManager.Instance.Inventory.RemoveItem(UIManager.Instance.Inventory.SelectedSlot);
    }

    private void ItemRightClick()
    {
        Item usedItem = UIManager.Instance.Inventory.GetItemInfo(UIManager.Instance.Inventory.SelectedSlot);
        if (usedItem == null) return;

        usedItem.OnRightClick();

        UIManager.Instance.Inventory.FreshInventory();
        if (usedItem.Amount == 0) UIManager.Instance.Inventory.RemoveItem(UIManager.Instance.Inventory.SelectedSlot);
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

        Item curItem = UIManager.Instance.Inventory.GetItemInfo(UIManager.Instance.Inventory.SelectedSlot);
        UpdatePreviewStructure(curItem);
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
        ic.item = item.DeepCopy();
        ic.item.Amount = 1;
        ic.UpdateSprite();
        ic.SetAcquirable(3f);

        // Clear selected slot
        UIManager.Instance.Inventory.RemoveItem(selected, 1);
    }

    public void GetDamage(int amount, DAMAGE_TYPE dmgType, float invTime, bool ignoreInvTime)
    {
        health = Mathf.Max(0, health - amount);
        if (health == 0)
        {
            Dead();
            return;
        }
        StartCoroutine(Stun(1f));
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

    public Collider2D[] GetAttackedCollider2D(LayerMask attackMask)
    {
        return Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackMask);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Item")) return;

        ItemController iController = other.GetComponent<ItemController>();
        if (!iController.IsAcquirable)
            return;

        UIManager.Instance.Inventory.AcquireItem(ref iController.item);
        if (iController.item.Amount == 0)
            Destroy(other.gameObject);
    }

    private void UpdatePreviewStructure(Item item)
    {
        if (item is ItemStructure)
        {
            previewStructure.SetActive(true);
            previewStructure.GetComponent<SpriteRenderer>().sprite = (item.ItemInfo as StructureInfo).structureSprite;
            previewStructure.GetComponent<PolygonCollider2D>().TryUpdateShapeToAttachedSprite();
            previewStructure.GetComponent<PreviewStructure>().Width = (item.ItemInfo as StructureInfo).width;
        }
        else
        {
            previewStructure.SetActive(false);
        }
    }
}
