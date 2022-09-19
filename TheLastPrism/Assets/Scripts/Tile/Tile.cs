using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private TileType tileType;
    [SerializeField]
    private List<GameObject> dropItemList;
    public Coordinate Pos { get; set; }
    public TileType TileType => tileType;
}
