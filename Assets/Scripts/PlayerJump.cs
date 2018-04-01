using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    private Rigidbody2D rb;
    public AnimationCurve jumpCurve;

    private float _jumpForce = 500f;
    public float JumpForce {
        get {
            return _jumpForce;
        }
        set {
            _jumpForce = value;
        }
    }

    public float lowGravity = 4f;
    public float highGravity = 6f;


    void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	void FixedUpdate () { 
        //Better jump feel modifying gravityScale
        if(rb.velocity.y < 0) {
            //state = PlayerState.State.IsFalling;
            rb.gravityScale = highGravity;
        }
        else if(rb.velocity.y > 0 && !InputManager.PressedButtonA()) {
            rb.gravityScale = lowGravity;
        }
        else {
            rb.gravityScale = 2f;
        }
	}

    public void Jump() {
        PlayerState.State = PlayerState.MyState.Jumping;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * 700f * Time.deltaTime, ForceMode2D.Impulse);
    }


    public void DoubleJump() {
        PlayerState.State = PlayerState.MyState.DoubleJumping;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * _jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
