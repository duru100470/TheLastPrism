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
    public Coordinate Pos { get; set; }
    public TILE_TYPE TileType => tileType;
    public List<Item> DropItemList => dropItemList;
    public int Health => health;

    public virtual void GetDamage(int amount, float invTime, bool ignoreInvTime)
    {
        health = Mathf.Max(0, health - amount);
        if (health == 0)
            Dead();
    }

    public virtual void Dead()
    {
        TileManager.Instance.DestroyTile(Pos, true);
    }
}