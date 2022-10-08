using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour, IListener, IDamage
{
    [SerializeField]
    private Coordinate pos;
    [SerializeField]
    private int width;
    [SerializeField]
    private List<Coordinate> floorPosList;
    [SerializeField]
    protected int health;
    [SerializeReference]
    private List<Item> dropItemList;

    public List<Coordinate> FloorPosList => floorPosList;
    public int Health => health;
    public List<Item> DropItemList => dropItemList;

    protected virtual void Awake()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.TileChange, this);
    }

    public void SetPosition(Coordinate coor)
    {
        pos = coor;
        for (int i = 0; i < width; i++)
        {
            floorPosList.Add(new Coordinate(i, -1) + pos);
        }
    }

    public virtual void OnEvent(EVENT_TYPE eType, Component sender, object param = null)
    {
        switch (eType)
        {
            case EVENT_TYPE.TileChange:
                foreach (var pos in floorPosList)
                {
                    if (TileManager.Instance.TileArray[pos.X, pos.Y] is null)
                        Dead();
                }
                break;
        }
    }

    public abstract void GetDamage(int amount, DAMAGE_TYPE dmgType, float invTime, bool ignoreInvTime);
    public virtual void Dead()
    {
        foreach (var item in dropItemList)
        {
            GameObject itemPrefab = Instantiate(GameManager.Instance.ItemPrefab);
            itemPrefab.transform.position = transform.position + new Vector3(Random.Range(-0.2f, 0.2f) + width * .5f, Random.Range(-0.2f, 0.2f) + .5f);

            // CreateItemCounter(itemPrefab);

            ItemController ic = itemPrefab.GetComponent<ItemController>();
            ic.item = item.DeepCopy();
            ic.UpdateSprite();
            ic.SetAcquirable(0f);
        }
        Destroy(this.gameObject);
        EventManager.Instance.RemoveListener(EVENT_TYPE.TileChange, this);
    }
}
