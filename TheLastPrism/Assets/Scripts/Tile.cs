using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private int health;
    public Coordinate Pos;
    public TileType TileType;
}
