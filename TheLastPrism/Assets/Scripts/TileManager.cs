using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager _instance = null;
    public static TileManager Instance => _instance;
    public Tile[,] TileArray {get; set;}
    public int worldXSize {get; set;}
    public int worldYSize {get; set;}

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Tile GetLeftTile(int x, int y)
    {
        if (x == 0)
            return null;

        return TileArray[x-1, y];
    }

    public Tile GetRightTile(int x, int y)
    {
        if (x == worldXSize)
            return null;

        return TileArray[x+1, y];
    }

    public Tile GetUpTile(int x, int y)
    {
        if (y == worldYSize)
            return null;

        return TileArray[x, y + 1];
    }
    
    public Tile GetDownTile(int x, int y)
    {
        if (y == 0)
            return null;

        return TileArray[x, y - 1];
    }
}