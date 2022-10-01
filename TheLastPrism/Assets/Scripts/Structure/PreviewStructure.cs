using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewStructure : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int width = 1;

    public int Width { get { return width; } set { width = value; } }

    private void Start()
    {
        player = GameManager.Instance.CurPlayer.gameObject;
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
    }
}
