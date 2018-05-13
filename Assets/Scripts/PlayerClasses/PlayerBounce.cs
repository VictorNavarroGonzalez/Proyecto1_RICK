using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBounce : MonoBehaviour {

    const int MINIMUM_HEIGHT = 8;

    private PlayerGround pg;
    private Rigidbody2D rb;
    public RaycastHit2D downHit;
    public AudioClip bounceSound;
    public AudioSource source;
    public LayerMask mask;

    Vector2 playerHeight;
    Vector2 playerPos;
    Vector2 wallForce = new Vector2(600, 600);
    Vector2 wallDashForce = new Vector2(900, 900);

    private bool _canBounce;
    public bool CanBounce { get { return _canBounce; } set { _canBounce = value; } }
    private bool _canWBounce;
    public bool CanWBounce { get { return _canWBounce; } set { _canWBounce = value; } }
    private bool reading;       //Checks if need save the height
    private bool running;       //Checks if player is WallBouncing
    private float startY;
    private float height;
    private float timeHit;
    private float k;
    private float t;


    void Awake() {
        pg = GetComponent<PlayerGround>();
        rb = GetComponent<Rigidbody2D>();
        reading = false;
        k = 1f;
    }

    void Update() {
        CheckHeight();
    }

    #region Checkers

    #region Height
    public float CheckHeight() {
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
        // Detect if Player is falling from enough heigh
        if (pg.Grounded) {
            if (Mathf.Abs(startY - rb.transform.position.y) != height) {
                height = Mathf.Abs(startY - rb.transform.position.y);
                if (height > MINIMUM_HEIGHT) CanBounce = true;
                else CanBounce = false;
            }
        }
        return CanBounce;
    }
    #endregion

    #region Check Wall Bounce
    public bool CheckWallBounce() {
        // Detect if Player is falling from enough heigh
        if (DistGround() > 2f && ((GetComponent<PlayerGround>().LeftHit) || (GetComponent<PlayerGround>().RightHit))) {
            CanWBounce = true;
            if (Time.time - timeHit > 1f) timeHit = Time.time;
        }
        else CanWBounce = false;
        return CanWBounce;
    }
    #endregion

    #region Dist to Ground
    public float DistGround() {
        playerPos = new Vector2(rb.transform.position.x, rb.transform.position.y);
        downHit = Physics2D.Raycast(playerPos - playerHeight / 2, Vector2.down, Mathf.Infinity, mask);            //Ray under the player
        return downHit.distance;            //Distance to the ground
    }
    #endregion

    #endregion

    #region Bounces

    #region VerticalBounce
    public IEnumerator NormalBounce() {
        if (CanBounce) {
            CanBounce = false;
            yield return new WaitUntil(() => pg.Grounded);
            if (InputManager.ButtonDownA() && PlayerState.State != PlayerState.MyState.Bouncing && PlayerState.LastState != PlayerState.MyState.Bouncing) {
                if (rb.velocity.y <= 0) {
                    t = Time.fixedTime - t;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(Vector2.up * k * height / t, ForceMode2D.Impulse);
                    Debug.Log(height / t);
                }
            }
            else if (InputManager.ButtonDownA()) {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * k * 0.6f * height, ForceMode2D.Impulse);
            }
            else {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * k * 0.4f * height, ForceMode2D.Impulse);
            }
        }
    }
    #endregion

    #region LateralBounce
    public IEnumerator WalledBounce() {
        if (CanWBounce && !running) {
            yield return new WaitUntil(() => (pg.LeftHit && (InputManager.MainHorizontal() > 0 || Time.time - timeHit > 0.15f))
            || (pg.RightHit && (InputManager.MainHorizontal() < 0 || Time.time - timeHit > 0.15f)));
            StartCoroutine(GetComponent<PlayerState>().Stopping(0.5f));
            if (pg.LeftHit && InputManager.ButtonDownA()) {
                rb.velocity = new Vector2(0, 0);
                if (PlayerState.State == PlayerState.MyState.Dashing) {
                    rb.AddForce(Vector2.up * wallDashForce.y * Time.deltaTime, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.right * wallDashForce.x * Time.deltaTime, ForceMode2D.Impulse);
                }
                else {
                    rb.AddForce(Vector2.up * wallForce.y * Time.deltaTime, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.right * wallForce.x * Time.deltaTime, ForceMode2D.Impulse);
                }
                running = false;
            }
            else if (pg.RightHit && InputManager.ButtonDownA()) {
                rb.velocity = new Vector2(0, 0);
                if (PlayerState.State == PlayerState.MyState.Dashing) {
                    rb.AddForce(Vector2.up * wallDashForce.y * Time.deltaTime, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.left * wallDashForce.x * Time.deltaTime, ForceMode2D.Impulse);
                }
                else {
                    rb.AddForce(Vector2.up * wallForce.y * Time.deltaTime, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.left * wallForce.x * Time.deltaTime, ForceMode2D.Impulse);
                }
                running = false;
            }
        }
    }
    #endregion

    #endregion

}