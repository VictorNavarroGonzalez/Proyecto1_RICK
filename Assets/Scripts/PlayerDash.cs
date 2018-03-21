using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {
    private float dashForce = 1000f;
    private Rigidbody2D rb;
    public bool canDash;
    private bool grounded;
    	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        canDash = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        grounded = GetComponent<PlayerGround>().Grounded;
        if (grounded)
        {
            canDash = true;
        }

        if (InputManager.ButtonRT() && canDash)
        {
            rb.velocity =new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.right * dashForce * Time.deltaTime, ForceMode2D.Impulse);
            StartCoroutine(SlowDownRT());
            canDash = false;
            
        }
        if (InputManager.ButtonLT() && canDash)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.left * dashForce * Time.deltaTime, ForceMode2D.Impulse);
            StartCoroutine(SlowDownLT());
            canDash = false;
        }
    }
    IEnumerator SlowDownRT()
    {
        yield return new WaitForSeconds(0.3f);
        rb.AddForce(Vector2.left * dashForce * Time.deltaTime, ForceMode2D.Impulse);
    }

    IEnumerator SlowDownLT()
    {
        yield return new WaitForSeconds(0.3f);
        rb.AddForce(Vector2.right * dashForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
