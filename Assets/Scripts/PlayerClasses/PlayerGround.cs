using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour {

    public float skinDepth = 0.05f;
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

    void Awake() {
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x * 0.6f, skinDepth);                  //Box collision under the player
        invertedBox = new Vector2(skinDepth, playerSize.x * 0.5f);              //Box collision on the sides of the player
    }

    void FixedUpdate() {
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;             //Down side of the player
        Vector2 rigthCenter = (Vector2)(transform.position) + Vector2.right * ((playerSize.x * 0.7f) * 0.5f);           //Right side of player
        Vector2 leftCenter = (Vector2)(transform.position) + Vector2.left * ((playerSize.x * 0.7f) * 0.5f);             //Left side of player

        _grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0, mask) != null);                   //Box collider to the left of the player (detect if grounding)
        _leftHit = (Physics2D.OverlapBox(leftCenter, invertedBox, 0, mask) != null);               //Box collider to the left of the player (dtect left hit)
        _rightHit = (Physics2D.OverlapBox(rigthCenter, invertedBox, 0, mask) != null);             //Box collider to the left of the player (detect right hit)
    }

    //Chech if playr is grounding
    public bool CheckGround() {     
        return Grounded;
    }

}
