using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour {

    private bool _active;
    public bool Active {
        get { return _active; }
        set { _active = value; }
    }
    private bool _restart;
    public bool Restart {
        get { return _restart; }
        set { _restart = value; }
    }

    //Activate the objects when player is in the scope
    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            _restart = false;
            Active = true;
        }
    }

    //Desactivate the objects when player left the scope
    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Active = false;
            _restart = true;
        }
    }
}
