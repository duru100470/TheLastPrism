using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewStructure : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int width = 1;
    [SerializeField]
    private bool isThereOtherStructure = false;
    [SerializeField]
    private bool isThereOtherTile = false;
    private SpriteRenderer spriteRenderer;

    public int Width { get { return width; } set { width = value; } }
    public bool IsPositionValid => CheckTileLocationIsValid() && !isThereOtherStructure && !isThereOtherTile;

    private void Start()
    {
        player = GameManager.Instance.CurPlayer.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        // width is an even number
        if (width % 2 == 0)
        {
            if (player.transform.position.x - (int)player.transform.position.x < .5f)
            {
                transform.position = new Vector3(Mathf.FloorToInt(player.transform.position.x) - (int)(width * .5f), Mathf.FloorToInt(player.transform.position.y), 0);
            }
            else
            {
                transform.position = new Vector3(Mathf.FloorToInt(player.transform.position.x) - (int)(width * .5f) + 1, Mathf.FloorToInt(player.transform.position.y), 0);
            }
        }
        // width is an odd number
        else
        {
            transform.position = new Vector3(Mathf.FloorToInt(player.transform.position.x) - (int)(width * .5f), Mathf.FloorToInt(player.transform.position.y), 0);
        }

        if (IsPositionValid)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
        }
        else
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, .5f);
        }
    }

    private bool CheckTileLocationIsValid()
    {
        for (int i = 0; i < width; i++)
        {
            Coordinate coor = Coordinate.WorldPointToCoordinate(transform.position) + new Coordinate(i, -1);
            if (TileManager.Instance.TileArray[coor.X, coor.Y] == null)
            {
                return false;
            }
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Structure")) return;

        isThereOtherStructure = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Structure")) return;

        isThereOtherStructure = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Tile")) return;
        if (other.gameObject.GetComponent<Tile>().Pos.Y < transform.position.y) return;

        isThereOtherTile = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Tile")) return;

        isThereOtherTile = false;
    }
}
