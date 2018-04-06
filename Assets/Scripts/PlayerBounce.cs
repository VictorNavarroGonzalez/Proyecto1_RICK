using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{

    private Rigidbody2D rb;
    public RaycastHit2D downHit;

    public bool canBounce;
    public bool boostBounce;

    Vector2 playerHeight;
    Vector2 playerPos;

    private float _bounceForce;
    public float BounceForce {
        get { return _bounceForce; }
        set { _bounceForce = value; }
    }


    void Awake() {
        rb = GetComponent<Rigidbody2D>();

        playerHeight = new Vector2(0, GetComponent<CircleCollider2D>().radius * 2);
        boostBounce = false;
        canBounce = false;
    }


    public bool CheckBounce() {
        // Raycast under the player
        playerPos = new Vector2(rb.transform.position.x, rb.transform.position.y);
        downHit = Physics2D.Raycast(playerPos - playerHeight / 2, Vector2.down);
        
        // Detect if Player is falling
        return (rb.velocity.y < 0 && downHit.distance < 2f);
    }

    public IEnumerator Bounce() {
        yield return new WaitUntil(() => (GetComponent<PlayerGround>().Grounded));
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * BounceForce * Time.deltaTime, ForceMode2D.Impulse);
    }

    public IEnumerator Atenuation(float value) {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitUntil(() => (GetComponent<PlayerGround>().Grounded));
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * value * Time.deltaTime, ForceMode2D.Impulse);
            value = value / 2;
            Debug.Log("bote");
        }
    }

    public IEnumerator LeftBounce() {
        yield return new WaitUntil(() => (GetComponent<PlayerGround>().LeftHit));
        StartCoroutine(GetComponent<PlayerState>().Stopping(0.5f));
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(Vector2.right * 600 * Time.deltaTime, ForceMode2D.Impulse);
    }

    public IEnumerator RightBounce()
    {
        yield return new WaitUntil(() => (GetComponent<PlayerGround>().RightHit));
        StartCoroutine(GetComponent<PlayerState>().Stopping(0.5f));
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(Vector2.left * 600 * Time.deltaTime, ForceMode2D.Impulse);
    }
}
