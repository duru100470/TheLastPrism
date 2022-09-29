using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IDamage
{
    [Header("Tile")]
    [SerializeField]
    private int health;
    [SerializeField]
    private TILE_TYPE tileType;
    [SerializeReference]
    private List<Item> dropItemList;

    [Header("Damage Multiplier")]
    [SerializeField]
    private float handMultiplier = 1;
    [SerializeField]
    private float pickaxeMultiplier = 5;

    public Coordinate Pos { get; set; }
    public TILE_TYPE TileType => tileType;
    public List<Item> DropItemList => dropItemList;
    public int Health => health;

    public virtual void GetDamage(int amount, DAMAGE_TYPE dmgType, float invTime, bool ignoreInvTime)
    {
        switch (dmgType)
        {
            case DAMAGE_TYPE.Hand:
                health = Mathf.Max(0, health - (int)(amount * handMultiplier));
                break;
            case DAMAGE_TYPE.Pickaxe:
                health = Mathf.Max(0, health - (int)(amount * pickaxeMultiplier));
                break;
        }

        if (health == 0)
            Dead();
    }

    public virtual void Dead()
    {
        TileManager.Instance.DestroyTile(Pos);
    }
}