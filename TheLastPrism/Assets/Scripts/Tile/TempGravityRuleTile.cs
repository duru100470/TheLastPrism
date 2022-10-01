using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGravityRuleTile : MonoBehaviour
{
    public Tile baseTile { get; set; }
    private Rigidbody2D rigid2d;
    [SerializeField]
    private float maxFallingSpeed;

    private void Awake() {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // Limit Tile's Falling Speed
        if (rigid2d.velocity.y < (-1) * maxFallingSpeed)
        {
            rigid2d.velocity = new Vector2(rigid2d.velocity.x, (-1) * maxFallingSpeed);
        }

        RaycastHit2D raycastHit2D = Physics2D.Raycast(rigid2d.position, Vector3.down, 0.35f, LayerMask.GetMask("Ground"));

        if (raycastHit2D.collider != null)
        {
            TileManager.Instance.PlaceTile(
                new Coordinate(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), 
                baseTile.TileId, 
                baseTile.Health);
            Destroy(this.gameObject);
        }
    }
}
