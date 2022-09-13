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
    
    [Header("Debug")]
    [SerializeField]
    private GameObject tile;
    private GameObject tileParent;

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

        tileParent = new GameObject("TileParant");
    }

    public void PlaceTile(Coordinate coor)
    {
        GameObject newTile = Instantiate(tile);
        newTile.transform.parent = tileParent.transform;
        newTile.transform.position = new Vector2(coor.X + 0.5f, coor.Y + 0.5f);
        TileArray[coor.X, coor.Y] = newTile.GetComponent<RuleTile>();
        newTile.GetComponent<RuleTile>().Pos = coor;
        newTile.GetComponent<RuleTile>().TileType = TileType.Debug;

        // Notify Tile has changed
        EventManager.Instance.PostNotification(EVENT_TYPE.AdjacentTileChange, null, coor);
        
        // Update adjacent rule tiles
        UpdateAdjacentRuleTile(coor);
    }

    public void DestroyTile(Coordinate coor)
    {
        Destroy(TileArray[coor.X, coor.Y].gameObject);
        TileArray[coor.X, coor.Y] = null;

        // Notify Tile has changed
        EventManager.Instance.PostNotification(EVENT_TYPE.AdjacentTileChange, null, coor);

        // Update adjacent rule tiles
        UpdateAdjacentRuleTile(coor);
    }

    public void UpdateAdjacentRuleTile(Coordinate coor)
    {
        if (coor.X != 0 && coor.Y != 0)
            (TileArray[coor.X - 1, coor.Y - 1] as RuleTile)?.UpdateRuleTile();
        if (coor.Y != 0)
            (TileArray[coor.X, coor.Y - 1] as RuleTile)?.UpdateRuleTile();
        if (coor.X != worldXSize - 1 && coor.Y != 0)
            (TileArray[coor.X + 1, coor.Y - 1] as RuleTile)?.UpdateRuleTile();
        if (coor.X != 0)
            (TileArray[coor.X - 1, coor.Y] as RuleTile)?.UpdateRuleTile();
        if (coor.X != worldXSize - 1)
            (TileArray[coor.X + 1, coor.Y] as RuleTile)?.UpdateRuleTile();
        if (coor.X != 0 && coor.Y != worldYSize - 1)
            (TileArray[coor.X - 1, coor.Y + 1] as RuleTile)?.UpdateRuleTile();
        if (coor.Y != worldYSize - 1)
            (TileArray[coor.X, coor.Y + 1] as RuleTile)?.UpdateRuleTile();
        if (coor.X != worldXSize - 1 && coor.Y != worldYSize - 1)
            (TileArray[coor.X + 1, coor.Y + 1] as RuleTile)?.UpdateRuleTile();
    }
}