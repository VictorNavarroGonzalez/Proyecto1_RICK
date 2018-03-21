using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour {

    public float skinDepth = 0.05f;
    public LayerMask mask;

    private bool _grounded;
    private bool _leftHit;
    private bool _rightHit;

    public bool Grounded {
        get {
            return _grounded;
        }
        set {
            _grounded = value;
        }
    }

    public bool LeftHit
    {
        get
        {
            return _leftHit;
        }
        set
        {
            _leftHit = value;
        }
    }

    public bool RightHit {
        get {
            return _rightHit;
        }
        set {
            _rightHit = value;
        }
    }

    private Vector2 playerSize;
    private Vector2 boxSize;
    private Vector2 boxSize_L;

    void Awake() {
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x * 0.9f, skinDepth);
        boxSize_L = new Vector2(playerSize.y * 0.7f, skinDepth);
    }

    void FixedUpdate() {
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
        Vector2 boxCenter_L = (Vector2)transform.position + Vector2.left * (playerSize.y + boxSize.y) * 0.5f;
        Vector2 boxCenter_R = (Vector2)transform.position + Vector2.right * (playerSize.y + boxSize.y) * 0.5f;
        _grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0, mask) != null);
        _leftHit = (Physics2D.OverlapBox(boxCenter_L, boxSize_L, 0, mask) != null);
        _rightHit = (Physics2D.OverlapBox(boxCenter_R, boxSize_L, 0, mask) != null);
    }

}
