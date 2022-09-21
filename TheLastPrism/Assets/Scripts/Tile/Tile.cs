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
    [SerializeField]
    private List<GameObject> dropItemList;
    public Coordinate Pos { get; set; }
    public TILE_TYPE TileType => tileType;
    public int Health { get => health; set => health = value; }
}