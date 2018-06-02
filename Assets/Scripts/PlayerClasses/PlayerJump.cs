using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    private Rigidbody2D rb;

    private float _jumpForce;
    public float JumpForce {
        get { return _jumpForce; }
        set {
            if(value == -1) {
                _jumpForce = DefaultValues.Circle.JumpForce;
            }
            else {
                _jumpForce = value;
            }
        }
    }

    public float lowGravity = 4f;
    public float highGravity = 6f;


    void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	void FixedUpdate () { 
        //Better jump feel modifying gravityScale depending on player's velocity in Y axis
        if(rb.velocity.y < 0) {
            rb.gravityScale = highGravity;
        }
        else if(rb.velocity.y > 0 && !InputManager.ButtonDownA()) {
            rb.gravityScale = lowGravity;
        }
        else {
            rb.gravityScale = 2f;
        }
	}

    public void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * _jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void DoubleJump() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * _jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
