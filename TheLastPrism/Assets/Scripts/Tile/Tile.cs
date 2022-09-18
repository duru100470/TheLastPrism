using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private TileType tileType;
    public Coordinate Pos { get; set; }
    public TileType TileType => tileType;
}
