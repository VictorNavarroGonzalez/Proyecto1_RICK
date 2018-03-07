using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour {

    public bool bounce;

    Vector2 playerSize;
    Vector2 playerPos;

    private Rigidbody2D rb;
    public float vBounce;
    private bool rebote;
    private int e;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        playerSize = new Vector2(GetComponent<CircleCollider2D>().radius*2, GetComponent<CircleCollider2D>().radius * 2);
        vBounce = 0f;
        e = 10000;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        playerPos = new Vector2(rb.transform.position.x, rb.transform.position.y);
        RaycastHit2D downHit = Physics2D.Raycast(playerPos+playerSize/2, Vector2.down);
        if (rb.velocity.y < 0 && downHit.collider != null) {
            bounce = true;
            if (downHit.distance <= 1.0f && vBounce == 0f)
            {
                vBounce = -rb.velocity.y*e;  
            }
                
            //rb.AddForce(Vector2.up * -rb.velocity.y * Time.deltaTime, ForceMode2D.Impulse);
            //rb.velocity = new Vector2(0, rb.velocity.y);
            //rb.AddForce(Vector2.up * rb.velocity.y * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (GetComponent<PlayerJump>().grounded && bounce)
        {
            Debug.Log(vBounce);
            //rb.velocity = new Vector2(rb.velocity.x, InputManager.MainVertical() * vBounce * Time.deltaTime);
            rb.AddForce(Vector2.up * vBounce * Time.deltaTime, ForceMode2D.Impulse);
            Debug.Log(Vector2.up * vBounce * Time.deltaTime);
            //vBounce = 0;
        }
            
        
    }
}
