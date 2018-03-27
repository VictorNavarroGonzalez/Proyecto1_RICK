using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    private Rigidbody2D rb;
    private PlayerBounce pb;

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
        pb = GetComponent<PlayerBounce>();
    }
	
	void FixedUpdate () {


        ////Jump boosted by bounce
        //if (pb.boostBounce && PlayerState.State == PlayerState.MyState.Grounding)
        //{
        //    StartCoroutine(Boost());
        //}
        ////Player jumps when ButtonA is pressed
        //else if (InputManager.ButtonA()) {
        //    if (PlayerState.State == PlayerState.MyState.Grounding && !pb.boostBounce)                //Normal Jump
        //    {
        //        PlayerState.State = PlayerState.MyState.Jumping;
        //    }
        //    else if (PlayerState.State == PlayerState.MyState.Jumping && !pb.boostBounce)          //Double Jump
        //    {
        //        PlayerState.State = PlayerState.MyState.DoubleJumping;
        //        rb.velocity = new Vector2(rb.velocity.x, 0);
        //        rb.AddForce(Vector2.up * _jumpForce * Time.deltaTime, ForceMode2D.Impulse);
        //    }
        //}


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
        rb.AddForce(Vector2.up * _jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void DoubleJump() {
        PlayerState.State = PlayerState.MyState.DoubleJumping;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * _jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    }

    //public IEnumerator Jump() {
    //    PlayerState.State = PlayerState.MyState.Jumping;
    //    rb.velocity = new Vector2(rb.velocity.x, 0);
    //    rb.AddForce(Vector2.up * _jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    //    //yield return new WaitForSeconds(2);
    //}

    //public IEnumerator DoubleJump() {
    //    PlayerState.State = PlayerState.MyState.DoubleJumping;
    //    rb.velocity = new Vector2(rb.velocity.x, 0);
    //    rb.AddForce(Vector2.up * _jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    //    yield return null;
    //}

    ////Boosted jump
    //public IEnumerator Boost() {
    //    yield return null;
    //    rb.velocity = new Vector2(rb.velocity.x, 0);
    //    rb.AddForce(Vector2.up * _jumpForce * 2f * Time.deltaTime, ForceMode2D.Impulse);
    //    canDoubleJump = true;
    //    pb.bounce = false;
    //    pb.boostBounce = false;
    //    //Debug.Log("BOOST");
    //}
}
