using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager _instance = null;
    public static TileManager Instance => _instance;
    public Tile[,] TileArray { get; set; }
    public int worldXSize { get; set; }
    public int worldYSize { get; set; }
    public bool IsGenerating { get; set; } = true;

    [SerializeField]
    private List<GameObject> tilePrefabList;

    [Header("Debug")]
    [SerializeField]
    private GameObject tile;
    private GameObject tileParent;
    [SerializeField]
    private bool isDebugMod;

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

    private void Update()
    {
        if (!isDebugMod) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Coordinate coor = new Coordinate(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y));
            if (TileArray[coor.X, coor.Y] == null)
                PlaceTile(coor, TileType.Debug);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Coordinate coor = new Coordinate(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y));
            if (TileArray[coor.X, coor.Y] != null)
                DestroyTile(coor);
        }
    }

    public void PlaceTile(Coordinate coor, TileType tileType)
    {
        GameObject newTile = Instantiate(tilePrefabList[(int)tileType]);
        newTile.transform.parent = tileParent.transform;
        newTile.transform.position = new Vector2(coor.X + 0.5f, coor.Y + 0.5f);

        // Initialize Tile Class
        var tmp = newTile.GetComponent<Tile>();
        TileArray[coor.X, coor.Y] = tmp;
        tmp.Pos = coor;

        // Notify Tile has changed        
        EventManager.Instance.PostNotification(EVENT_TYPE.TileChange, null, coor);

        // Update rule tiles
        if (IsGenerating) return;
        (tmp as RuleTile)?.UpdateRuleTile();
        UpdateAdjacentRuleTile(coor);
    }

    public void DestroyTile(Coordinate coor)
    {
        Destroy(TileArray[coor.X, coor.Y].gameObject);
        TileArray[coor.X, coor.Y] = null;

        // Notify Tile has changed
        EventManager.Instance.PostNotification(EVENT_TYPE.TileChange, null, coor);

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