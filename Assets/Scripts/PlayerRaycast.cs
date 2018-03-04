using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour {

    public float groundedSkin = 0.05f;
    public LayerMask mask;

    public bool grounded;
    private float playerSize;

    void Awake() {
        //Get player and box size for the raycast
        playerSize = GetComponent<CircleCollider2D>().radius * 2;
    }

    void FixedUpdate() {
        //RAYCAST
        //Vector2 rayStart = (Vector2)transform.position + Vector2.down * playerSize * 0.5f;
        //grounded = Physics2D.Raycast(rayStart, Vector2.down, groundedSkin, mask);

        //CIRCLE CAST
        grounded = (Physics2D.OverlapCircle(transform.position * 0.5f, playerSize, mask) != null);
    }

}
