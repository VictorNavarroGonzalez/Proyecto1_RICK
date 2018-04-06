using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour {

    public float skinDepth = 0.05f;
    public LayerMask mask;

    private PlayerState.MyState state;


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
    private Vector2 invertedBox;

    void Awake() {
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x * 0.9f, skinDepth);
        invertedBox = new Vector2(skinDepth, playerSize.x * 0.5f);
    }

    void FixedUpdate() {
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
        Vector2 rigthCenter = (Vector2)(transform.position) + Vector2.right * ((playerSize.x*0.7f) * 0.5f);
        Vector2 leftCenter = (Vector2)(transform.position) + Vector2.left * ((playerSize.x * 0.7f) * 0.5f);
        _grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0, mask) != null);
        _leftHit = (Physics2D.OverlapBox(leftCenter, invertedBox, 0, mask) != null);
        _rightHit = (Physics2D.OverlapBox(rigthCenter, invertedBox, 0, mask) != null);
    }

    public IEnumerator CheckGround() {
        if (_grounded) {
            PlayerState.State = PlayerState.MyState.Grounding;
        }

        yield return new WaitForSeconds(0.5f);
    }

}
