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

    public List<Coordinate> FloorPosList => floorPosList;
    public int Health => health;

    public virtual void Awake()
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
        Destroy(this.gameObject);
        EventManager.Instance.RemoveListener(EVENT_TYPE.TileChange, this);
    }
}
