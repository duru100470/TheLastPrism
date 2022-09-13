using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private int health;
    protected int x;
    protected int y;
    public TileType tileType {get; set;}
}
