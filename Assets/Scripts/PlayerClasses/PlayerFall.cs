using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour {
    private Rigidbody2D rb;

    private float _fallForce;
    public float FallForce {
        get { return _fallForce; }
        set { _fallForce = value; }
    }

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
	}
	
    public void Fall() {
        rb.AddForce(Vector2.down * FallForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
