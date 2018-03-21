using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    private Rigidbody2D rb;
    private PlayerBounce pb;

    private bool grounded = false;
    private bool canDoubleJump = false;

    private float _jumpForce = 500f;
    public float JumpForce {
        get {
            return _jumpForce;
        }
        set {
            _jumpForce = value;
        }
    }

    private float lowGravity = 4f;
    private float highGravity = 6f;


    void Start () {
        rb = GetComponent<Rigidbody2D>();
        pb = GetComponent<PlayerBounce>();
    }
	
	void FixedUpdate () {
        //Check if the player is grounded
        grounded = GetComponent<PlayerGround>().Grounded;

        //Jump boosted by bounce
        if (pb.boostBounce && grounded)
        {
            StartCoroutine(Boost());
        }
        //Player jumps when ButtonA is pressed
        else if (InputManager.ButtonA()) {
            if (grounded && !pb.boostBounce)                //Normal Jump
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * _jumpForce * Time.deltaTime, ForceMode2D.Impulse);
                canDoubleJump = true;
                //Debug.Log("Jump");
            }
            else if (canDoubleJump && !pb.boostBounce)          //Double Jump
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * _jumpForce * Time.deltaTime, ForceMode2D.Impulse);
                canDoubleJump = false;
                //Debug.Log("DoubleJump");
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
            rb.gravityScale = 2f;
        }
	}

    //Boosted jump
    IEnumerator Boost()
    {
        yield return null;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * _jumpForce * 2f * Time.deltaTime, ForceMode2D.Impulse);
        canDoubleJump = true;
        pb.bounce = false;
        pb.boostBounce = false;
        //Debug.Log("BOOST");
    }
}
