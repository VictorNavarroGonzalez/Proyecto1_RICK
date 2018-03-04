using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour {

    public float skinDepth = 0.05f;
    public LayerMask mask;

    public bool grounded; //Touching the ground
    public bool walled; //Touching the wall

    private float playerSize;

    Vector2 boxSize;
    Vector2 invertedBox;

    void Awake() {
        playerSize = GetComponent<CircleCollider2D>().radius * 2;
        boxSize = new Vector2(playerSize * 0.8f, skinDepth);
        invertedBox = new Vector2(skinDepth, playerSize * 0.8f);
    }

    void FixedUpdate() {
        //RAYCAST
        //Vector2 rayStart = (Vector2)transform.position + Vector2.down * playerSize * 0.5f;
        //grounded = Physics2D.Raycast(rayStart, Vector2.down, groundedSkin, mask);
        
        
        //BOXCAST
        //Grounded Box
        Vector2 downCenter = (Vector2)transform.position + Vector2.down * (playerSize + boxSize.y) * 0.5f;
        grounded = (Physics2D.OverlapBox(downCenter, boxSize, 0, mask) != null);


        //Walled Boxes
        Vector2 rigthCenter = (Vector2)(transform.position) + Vector2.right * ((playerSize + boxSize.y) * 0.5f);
        bool rightWalled = (Physics2D.OverlapBox(rigthCenter, invertedBox, 0, mask) != null);

        Vector2 leftCenter = (Vector2)transform.position + Vector2.left * (playerSize + boxSize.y * 0.5f);
        bool leftWalled = (Physics2D.OverlapBox(leftCenter, invertedBox, 0, mask) != null);

        walled = rightWalled || leftWalled;

    }

}
