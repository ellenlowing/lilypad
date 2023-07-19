using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 direction = Vector2.zero;

    public void Move()
    {

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        transform.localPosition = new Vector3(
            transform.localPosition.x + direction.x * Conductor.instance.noteDistance,
            transform.localPosition.y + direction.y * Conductor.instance.noteDistance,
            transform.localPosition.z
        );
    }
}
