using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour {

    public float skinDepth = 0.05f;
    public LayerMask mask;

    private bool _grounded;
    public bool Grounded {
        get {
            return _grounded;
        }
        set {
            _grounded = value;
        }
    }

    private Vector2 playerSize;
    private Vector2 boxSize;

    void Awake() {
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x * 0.9f, skinDepth);
    }

    void FixedUpdate() {
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
        _grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0, mask) != null);
    }

}
