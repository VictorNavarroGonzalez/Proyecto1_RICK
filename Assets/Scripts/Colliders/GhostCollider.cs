using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollider : MonoBehaviour {

    private bool _overlaped;
    public bool Overlaped {
        get { return _overlaped;  }
        set { _overlaped = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Overlaped = (collision.gameObject.tag == "Platform");
    }

}
