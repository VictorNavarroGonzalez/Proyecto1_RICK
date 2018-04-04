using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {
    private float dashForce = 1000f;

    private Rigidbody2D rb;
    private bool grounded;
    	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    public bool CheckDash() {
        // Check player state etc
        return true;
    }

    public void RightDash() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.right * dashForce * Time.deltaTime, ForceMode2D.Impulse);
        StartCoroutine(SlowDownRT());
    }

    public void LeftDash() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.left * dashForce * Time.deltaTime, ForceMode2D.Impulse);
        StartCoroutine(SlowDownRT());
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
