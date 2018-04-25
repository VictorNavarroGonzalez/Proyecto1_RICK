using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollider : MonoBehaviour {

    private bool _overlaped;
    public bool Overlaped {
        get { return _overlaped;  }
        set { _overlaped = value; }
    }

    // If the ghost is colliding with some platform, this function
    // will return false. This is intended to prevent clipping teleports
    // to the player's ghost.
    private void OnTriggerEnter2D(Collider2D collision) {
        Overlaped = (collision.gameObject.tag == "Platform");
    }

}
