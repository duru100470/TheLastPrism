using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
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
    public int Health { get => health; set => health = value; }
}