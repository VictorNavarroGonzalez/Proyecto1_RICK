using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{

    public bool canBounce;

    Vector2 playerHeight;
    Vector2 playerPos;

    private Rigidbody2D rb;
    private PlayerJump pj;
    public RaycastHit2D downHit;
    public bool boostBounce;
    private bool rebote;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerHeight = new Vector2(0, GetComponent<CircleCollider2D>().radius * 2);
        boostBounce = false;
        canBounce = false;
    }

    // Update is called once per frame




    public bool CheckBounce() {
        //Raycast under the player
        playerPos = new Vector2(rb.transform.position.x, rb.transform.position.y);
        downHit = Physics2D.Raycast(playerPos - playerHeight / 2, Vector2.down);
        //Detect if Player is falling
        return (rb.velocity.y < 0 && downHit.distance < 2f);
 
        //if (canBounce == true && GetComponent<PlayerGround>().Grounded == true)
        //{
        //    StartCoroutine(disable());
        //}

    }

    public IEnumerator Bounce()
    {
        yield return new WaitUntil(() => (GetComponent<PlayerGround>().Grounded));
        PlayerState.State = PlayerState.MyState.Bouncing;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * 2000 * Time.deltaTime, ForceMode2D.Impulse);
    }

    IEnumerator disable()
    {
        if (GetComponent<PlayerGround>().Grounded)
        {
            yield return new WaitForSeconds(1f);
            canBounce = false;
        }
    }
}
