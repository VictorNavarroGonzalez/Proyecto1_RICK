using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    private Rigidbody2D rb;

    public bool grounded = false;
    private bool canDoubleJump = false;

    public float jumpForce = 5f;

    public float lowGravity = 1.5f;
    public float highGravity = 2.5f;


    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	void FixedUpdate () {
        //Check if the player is grounded
        grounded = GetComponent<PlayerRaycast>().grounded;

        //Player jumps when ButtonA is pressed
        if (InputManager.ButtonA()) {
            if (grounded) {
                //rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
                canDoubleJump = true;
            }
            else if (canDoubleJump) {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
                canDoubleJump = false;
            }
        }

        //Better jump feel modifying gravityScale
        if(rb.velocity.y < 0) {
            rb.gravityScale = highGravity;
        }
        else if(rb.velocity.y > 0 && !InputManager.PressedButtonA()) {
            rb.gravityScale = lowGravity;
        }
        else {
            rb.gravityScale = 1f;
        }
	}
}
