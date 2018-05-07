using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{

    private Rigidbody2D rb;
    public RaycastHit2D downHit;
    public AudioClip bounceSound;
    public AudioSource source;

    public bool canBounce;
    public bool canWBounce;
    private bool reading;           //Control when needs to save the height
    private bool isChecking;

    Vector2 playerHeight;
    Vector2 playerPos;

    private bool _stopBounce;       //Disable Bounce
    public bool StopBounce {
        get { return _stopBounce; }
        set { _stopBounce = value; }
    }

    private bool _stopWallBounce;       //Disable Walled bounce
    public bool StopWallBounce {
        get { return _stopWallBounce; }
        set { _stopWallBounce = value; }
    }

    private float tempY;            //Save the maximum height of a fall
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
        isChecking = false;
        _stopBounce = false;
        _stopWallBounce = false;
    }

    void Update() {
        //When player starts to fall, save his height if there isn't any saved height yet
        if (rb.velocity.y < 0 && !reading) {
            reading = true;
            tempY = rb.transform.position.y;
        }
    }

    void FixedUpdate()
    {
        CheckBounce();      // Detect if Player is falling from enough height
    }

    #region Checkers

    #region Dist to Ground
    public float DistGround() {
        playerPos = new Vector2(rb.transform.position.x, rb.transform.position.y);
        downHit = Physics2D.Raycast(playerPos - playerHeight / 2, Vector2.down);            //Ray under the player

        return downHit.distance;            //Distance to the ground
    }
    #endregion

    #region Check Normal Bounce
    public bool CheckBounce() {
        // Detect if Player is falling from enough heigh
        if (GetComponent<PlayerGround>().Grounded && reading && !isChecking) {
            //When player arrives to the ground
            isChecking = true;

            //If height is bigger than jump height + double jump height, Player can bounce
            if (Mathf.Abs(tempY - rb.transform.position.y) > 9f) canBounce = true;
            else canBounce = false;

            //Reactive the read of the player's height 
            StartCoroutine(Reactive());
        }
        
        return canBounce;
    }
    
    public IEnumerator Reactive() {
        yield return new WaitForSeconds(1f);
        reading = false;
        isChecking = false;
    }

    #endregion

    #region Check Wall Bounce
    //Avoid Player to bounce when he is jumping near a wall
    public IEnumerator CheckWallBounce() {
        if (GetComponent<PlayerGround>().LeftHit || GetComponent<PlayerGround>().RightHit) {        //If Player is touching a wall
            canWBounce = false;
            yield return new WaitForSeconds(0.6f);      //Wait 0.6 seconds before check if he should bounce
            canWBounce = true;
        }

    }
    #endregion

    #endregion

    #region Normal Bounce
    public IEnumerator Bounce() {
        if (!StopBounce) {
            canBounce = false;
            Debug.Log("Salto");
            float multiplier;           //Part of force that depends on fall height

            if (PlayerState.State == PlayerState.MyState.Bouncing || (PlayerState.State == PlayerState.MyState.Dashing && PlayerState.LastState == PlayerState.MyState.Bouncing))       //If player has bounce before: 
                multiplier = Mathf.Abs(tempY - rb.transform.position.y) / 2;        //Set bounce force only a half of normal bounce
            else multiplier = Mathf.Abs(tempY - rb.transform.position.y);  //Set normal bounce force

            if (multiplier > 15.3f) multiplier = 15.3f;                 // Set a maximum bounce force for a fall

            yield return new WaitUntil(() => (GetComponent<PlayerGround>().Grounded));          //Wait until Player touch ground
            if (InputManager.ButtonDownA())         //If button A is pressed:
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * BounceForce * multiplier * Time.deltaTime, ForceMode2D.Impulse);           //Bounce
            }
            
        }
    }
    #endregion

    #region Left Bounce
    public IEnumerator LeftBounce()
    {
        if (!StopWallBounce) {
            yield return new WaitUntil(() => (GetComponent<PlayerGround>().LeftHit));           //If Player is hitting a left side wall
            if (InputManager.ButtonDownA())          //If button A is pressed:
            {
                StartCoroutine(GetComponent<PlayerState>().Stopping(0.5f));         //Stops the joystick input for 0.5 second to improve bounce feeling
                rb.velocity = new Vector2(0, 0);

                //If Player dash before bounce:
                if (PlayerState.State == PlayerState.MyState.Dashing) {
                    //Increase the bounce force
                    rb.AddForce(Vector2.right * BounceForce * 11f * Time.deltaTime, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.up * GetComponent<PlayerJump>().JumpForce * 1.3f * Time.deltaTime, ForceMode2D.Impulse);
                    source.PlayOneShot(bounceSound, 1f);
                }
                else {
                    //Normal bounce force
                    rb.AddForce(Vector2.right * BounceForce * 7f * Time.deltaTime, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.up * GetComponent<PlayerJump>().JumpForce * 1.3f * Time.deltaTime, ForceMode2D.Impulse);
                    source.PlayOneShot(bounceSound, 1f);
                }
            }
        }       
    }
    #endregion

    #region Right Bounce
    public IEnumerator RightBounce()
    {
        if(!StopWallBounce) {
            yield return new WaitUntil(() => (GetComponent<PlayerGround>().RightHit));      //If Player is hitting a right side wall
            if (InputManager.ButtonDownA())           //If button A is pressed:
            {
                StartCoroutine(GetComponent<PlayerState>().Stopping(0.5f));       //Stops the joystick input for 0.5 second to improve bounce feeling
                rb.velocity = new Vector2(0, 0);

                //If Player dash before bounce:
                if (PlayerState.State == PlayerState.MyState.Dashing) {
                    //Increase the bounce force
                    rb.AddForce(Vector2.left * BounceForce * 11f * Time.deltaTime, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.up * GetComponent<PlayerJump>().JumpForce * 1.3f * Time.deltaTime, ForceMode2D.Impulse);
                    source.PlayOneShot(bounceSound, 1f);
                }
                else {
                    //Normal bounce force
                    rb.AddForce(Vector2.left * BounceForce * 7f * Time.deltaTime, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.up * GetComponent<PlayerJump>().JumpForce * 1.3f * Time.deltaTime, ForceMode2D.Impulse);
                    source.PlayOneShot(bounceSound, 1f);
                }
            }
        }     
    }
    #endregion

}