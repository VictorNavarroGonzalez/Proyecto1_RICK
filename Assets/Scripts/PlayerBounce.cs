using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{

    public bool bounce;

    Vector2 playerHeight;
    Vector2 playerPos;

    private Rigidbody2D rb;
    private PlayerJump pj;
    public RaycastHit2D downHit;
    public bool boostBounce;
    private bool rebote;
    private float e;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHeight = new Vector2(0, GetComponent<CircleCollider2D>().radius * 2);
        e = 100f;
        boostBounce = false;
        bounce = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = new Vector2(rb.transform.position.x, rb.transform.position.y);
        downHit = Physics2D.Raycast(playerPos - playerHeight / 2, Vector2.down);
        if (rb.velocity.y < 0 && downHit.collider != null)
        {
            bounce = true;
            if (downHit.distance <= 1.6f && downHit.distance >= 0.05f && InputManager.ButtonA())
            {
                boostBounce = true;
                //Debug.Log(downHit.distance);
                //Debug.Log("activo");

            }
            if (GetComponent<PlayerGround>().Grounded && bounce && !boostBounce)
            {
                //rb.AddForce(Vector2.up * e * Time.deltaTime, ForceMode2D.Impulse);
            }

            //Debug.Log(downHit.collider);

        }
    }
}
