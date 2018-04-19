using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{

    private Rigidbody2D rb;
    public RaycastHit2D downHit;

    public bool canBounce;
    public bool canWBounce;
    public bool reading;
    public bool canChange;

    Vector2 playerHeight;
    Vector2 playerPos;

    private float tempY;
    private float _bounceForce;
    public float BounceForce
    {
        get { return _bounceForce; }
        set { _bounceForce = value; }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerHeight = new Vector2(0, GetComponent<CircleCollider2D>().radius * 2);
        canBounce = false;
        canWBounce = true;
        reading = false;

    }

    private void FixedUpdate()
    {
        if (rb.velocity.y > 0) reading = false;
        else if (rb.velocity.y < 0 && !reading)
        {
            tempY = rb.transform.position.y;
            reading = true;

        }
        CheckBounce();
    }

    public float DistGround()
    {
        playerPos = new Vector2(rb.transform.position.x, rb.transform.position.y);
        downHit = Physics2D.Raycast(playerPos - playerHeight / 2, Vector2.down);

        return downHit.distance;
    }

    public bool CheckBounce()
    {
        // Detect if Player is falling from enough heigh
        if (GetComponent<PlayerGround>().Grounded && reading)
        {
            if (Mathf.Abs(tempY - rb.transform.position.y) > 9f) canBounce = true;
            else canBounce = false;
            reading = false;
        }

        return canBounce;
    }

    public IEnumerator Bounce()
    {
        float multiplier;
        if (PlayerState.State != PlayerState.MyState.Bouncing)
            multiplier = Mathf.Abs(tempY - rb.transform.position.y);
        else multiplier = Mathf.Abs(tempY - rb.transform.position.y)/2;
        if (multiplier > 15) multiplier = 15.5f;
        yield return new WaitUntil(() => (GetComponent<PlayerGround>().Grounded));
        if (InputManager.ButtonDownA())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * BounceForce * multiplier * Time.deltaTime, ForceMode2D.Impulse);
        }
        canBounce = false;
    }

    public IEnumerator LeftBounce()
    {
        yield return new WaitUntil(() => (GetComponent<PlayerGround>().LeftHit));
        if (InputManager.ButtonDownA())
        {
            StartCoroutine(GetComponent<PlayerState>().Stopping(0.5f));
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.right * BounceForce * 7f * Time.deltaTime, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * GetComponent<PlayerJump>().JumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    public IEnumerator RightBounce()
    {
        yield return new WaitUntil(() => (GetComponent<PlayerGround>().RightHit));
        if (InputManager.ButtonDownA())
        {
            StartCoroutine(GetComponent<PlayerState>().Stopping(0.5f));
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.left * BounceForce* 7f * Time.deltaTime, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * GetComponent<PlayerJump>().JumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    public IEnumerator CheckWallBounce()
    {
        if (GetComponent<PlayerGround>().LeftHit || GetComponent<PlayerGround>().RightHit)
        {
            canWBounce = false;
            yield return new WaitForSeconds(0.6f);
            canWBounce = true;
        }
      
    }

}