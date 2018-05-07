using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour {

    public float depth = 0.05f;
    public LayerMask mask;

    private bool _grounded;
    private bool _leftHit;
    private bool _rightHit;

    public bool Grounded {      //True if Player is in the floor
        get {return _grounded;}
        set {_grounded = value;}
    }
    public bool LeftHit {       //True if player is touching a left wall
        get {return _leftHit;}
        set {_leftHit = value;}
    }
    public bool RightHit {      //True if player is touching a right wall
        get {return _rightHit;}
        set {_rightHit = value;}
    }

    private Vector2 playerSize;

    private Vector2 boxSize;        
    private Vector2 invertedBox;

    private Vector2 boxCenter;
    private Vector2 rigthCenter;
    private Vector2 leftCenter;

    void Awake() {
        // Get player size and scale it.
        playerSize = GetComponent<Transform>().localScale;
        playerSize.x *= GetComponent<BoxCollider2D>().size.x;
        playerSize.y *= GetComponent<BoxCollider2D>().size.y;

        // Box collision under the player.
        boxSize = GetComponent<Transform>().localScale;
        boxSize.x *= GetComponent<BoxCollider2D>().size.x;
        boxSize.y *= depth;

        // Box collision on the sides of the player.
        invertedBox = GetComponent<Transform>().localScale;
        invertedBox.x *= depth;
        invertedBox.y *= GetComponent<BoxCollider2D>().size.y * 0.9f;

    }

    void FixedUpdate() {
        //Down side of the player
        boxCenter = (Vector2) transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;

        // Left and Right side of player
        rigthCenter = (Vector2) transform.position + Vector2.right * (playerSize.x + invertedBox.x) * 0.5f;
        leftCenter = (Vector2) transform.position + Vector2.left * (playerSize.x + invertedBox.x) * 0.5f;

        _grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0, mask) != null);                   //Box collider to the left of the player (detect if grounding)
        _leftHit = (Physics2D.OverlapBox(leftCenter, invertedBox, 0, mask) != null);               //Box collider to the left of the player (dtect left hit)
        _rightHit = (Physics2D.OverlapBox(rigthCenter, invertedBox, 0, mask) != null);             //Box collider to the left of the player (detect right hit)
    }

    //Chech if playr is grounding
    public bool CheckGround() {     
        return Grounded;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(boxCenter, boxSize);
        Gizmos.DrawCube(rigthCenter, invertedBox);
        Gizmos.DrawCube(leftCenter, invertedBox);
    }
}
