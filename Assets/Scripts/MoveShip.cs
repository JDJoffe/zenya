using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;
public class MoveShip : NetworkBehaviour
{

    [SerializeField]
    private float speed = 3f;

    void FixedUpdate()
    {
        if (this.isLocalPlayer)
        {
            float movement = Input.GetAxis("Horizontal");
            //move player
            GetComponent<Rigidbody2D>().velocity = new Vector2(movement * speed, 0.0f);
        }
    }
}
