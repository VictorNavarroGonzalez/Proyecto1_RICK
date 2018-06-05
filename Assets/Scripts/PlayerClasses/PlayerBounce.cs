using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBounce : MonoBehaviour {

    const int MINIMUM_HEIGHT = 8;

    private PlayerGround pg;        // PlayerGround.
    private Rigidbody2D rb;         // RigidBody2D.
    public RaycastHit2D downHit;    // Raycast under the player.
    public LayerMask mask;

    Vector2 playerHeight;
    Vector2 playerPos;
    Vector2 wallForce = new Vector2(14, 12);
    Vector2 wallDashForce = new Vector2(18, 14);

    private bool _canBounce;
    public bool CanBounce { get { return _canBounce; } set { _canBounce = value; } }
    private bool _canWBounce;
    public bool CanWBounce { get { return _canWBounce; } set { _canWBounce = value; } }
    private bool reading;       // Checks if need save the height
    private bool running;       // Checks if player is WallBouncing
    private bool isOnPlatform;
    private float startY;       // Max height
    private float height;       
    private float timeHit;      // Time since Player hit the wall
    private float k;            // Elsatiicity constant
    public float K {
        set { k = value; }
        get { return k; }
    }
    private float t;            // Time falling


    void Awake() {
        pg = GetComponent<PlayerGround>();
        rb = GetComponent<Rigidbody2D>();
        reading = false;
        isOnPlatform = false;
        k = 1f;
    }

    void Update() {
        //Reads the height of the fall
        CheckHeight();      
    }

    #region Checkers

    #region Height
    public float CheckHeight() {
        //Read the Y value at the highest point
        if (!reading && rb.velocity.y < 0) {
            reading = true;
            startY = rb.position.y;
            t = Time.fixedTime;
        }
        else if (rb.velocity.y > 0 && DistGround() > 1f) reading = false;
        return startY;
    }
    #endregion

    #region Check Normal Bounce
    public bool CheckBounce() {
        // Detect if Player is falling from enough heigh (MINIMUM_HEIGHT)
        if (pg.Grounded) {
            if (Mathf.Abs(startY - rb.transform.position.y) != height) {
                height = Mathf.Abs(startY - rb.transform.position.y);
                if (height > MINIMUM_HEIGHT) CanBounce = true;
                else CanBounce = false;
            }
        }
        //if (DistGround() < MINIMUM_HEIGHT + 20 && DistGround() > MINIMUM_HEIGHT+5 && rb.velocity.y <= -2) {
        //    Debug.Log(DistGround());          
        //    GetComponent<Animator>().SetBool("Bouncing", true);
        //}
        return CanBounce;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Platform") isOnPlatform = true;
    }
    #endregion

    #region Check Wall Bounce
    public bool CheckWallBounce() {
       
        // Detect if Player is hitting the wall and isn't at the ground
        if (DistGround() > 2f && ((GetComponent<PlayerGround>().LeftHit) || (GetComponent<PlayerGround>().RightHit))) {
            CanWBounce = true;
            if (Time.time - timeHit > 1f) timeHit = Time.time;
            Debug.Log("Entra");
        }
        else CanWBounce = false;
        return CanWBounce;
    }
    #endregion

    #region Dist to Ground
    //Distance to the ground
    public float DistGround() {
        playerPos = new Vector2(rb.transform.position.x, rb.transform.position.y);
        //Ray under the player
        downHit = Physics2D.Raycast(playerPos - playerHeight / 2, Vector2.down, Mathf.Infinity, mask);            
        return downHit.distance;            
    }
    #endregion

    #endregion

    #region Bounces

    #region VerticalBounce
    public IEnumerator NormalBounce() {
        if (CanBounce) {
            CanBounce = false;      //Present overload
            yield return new WaitUntil(() => pg.Grounded);
            //WalledBounce if:
            
            //ButtonA is pressed and hasn't bounced yet
            if (InputManager.ButtonDownA() && PlayerState.State != PlayerState.MyState.Bouncing && PlayerState.LastState != PlayerState.MyState.Bouncing) {
                if (rb.velocity.y <= 0) {
                    t = Time.fixedTime - t;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(Vector2.up * k * height / t, ForceMode2D.Impulse);
                }
            }
                //ButtonA is pressed and has bounced at least one time
            else if (InputManager.ButtonDownA()) {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * k * 0.6f * height, ForceMode2D.Impulse);
            }
                //Button A isn't pressed (soft bounce for attenuate the fall)
            else if(!isOnPlatform && height > 2f) {    //not isOnPlatform prevent blinking in platforms
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * k * 0.4f * height, ForceMode2D.Impulse);
            }
        }
    }
    #endregion

    #region LateralBounce
    public IEnumerator WalledBounce() {
        if (CanWBounce && !running) {
            running = true;     //Prevent overload
            //Wait unitil Player Hit the wall and:
                //Change joystic direction  OR  wait 0.15second
            yield return new WaitUntil(() => (pg.LeftHit || pg.RightHit));
            StartCoroutine(GetComponent<PlayerState>().Stopping(0.5f));
            //Left Bounce
            if (pg.LeftHit && InputManager.ButtonDownA()) {
                rb.velocity = new Vector2(0, 0);
                //If Player is Dashing
                if (PlayerState.State == PlayerState.MyState.Dashing) {
                    rb.AddForce(Vector2.up * wallDashForce.y, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.right * wallDashForce.x, ForceMode2D.Impulse);
                }
                //If not dashing
                else {
                    rb.AddForce(Vector2.up * wallForce.y, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.right * wallForce.x, ForceMode2D.Impulse);
                }
                running = false;
            }
            //Right Bounce
            else if (pg.RightHit && InputManager.ButtonDownA()) {
                rb.velocity = new Vector2(0, 0);
                //If Player is Dashing
                if (PlayerState.State == PlayerState.MyState.Dashing) {
                    rb.AddForce(Vector2.up * wallDashForce.y, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.left * wallDashForce.x, ForceMode2D.Impulse);
                }
                //If not dashing
                else {
                    rb.AddForce(Vector2.up * wallForce.y, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.left * wallForce.x, ForceMode2D.Impulse);
                }
                running = false;
            }
            running = false;
        }
    }
    #endregion

    #endregion

}