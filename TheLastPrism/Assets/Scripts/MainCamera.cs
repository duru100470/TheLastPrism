using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private void Update() {
        Vector3 move = new Vector3(0, 0, 0);

        move.x += Time.deltaTime * Input.GetAxisRaw("Horizontal") * speed;
        move.y += Time.deltaTime * Input.GetAxisRaw("Vertical") * speed;

        transform.position += move;
    }
}
