using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {

    private Rigidbody2D rb;
    private bool canDash;
    private Vector2 temp;
    private float _dashForce = 1000f;
    public float DashForce {
        get { return _dashForce; }
        set { _dashForce = value;  }
    }

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    //Checks and updates if the player is on the ground, in order to reset the dash
    private void FixedUpdate() {
        if (canDash == false && (PlayerState.State == PlayerState.MyState.Grounding || PlayerState.State == PlayerState.MyState.Climbing)) canDash = true;
    }

    //Checks whether the player can dash or not
    public bool CheckDash() {
        if (PlayerState.State != PlayerState.MyState.Grounding && canDash) return true;
        return false; 
    }

    //Adds an impulse to the player 
    public void Dash() {
        canDash = false;
        temp = rb.velocity;
        StartCoroutine(GetComponent<PlayerState>().Stopping(0.1f));
        StartCoroutine(Counterdash());
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(rb.velocity * DashForce* Time.deltaTime , ForceMode2D.Impulse);
    }
    
    //Slows down the player gradually
    public IEnumerator Counterdash()
    {
        for (int i = 120; i > 1 || !GetComponent<PlayerBounce>().canWBounce; i-=10)
        {
            rb.AddForce(-rb.velocity * DashForce / i * Time.deltaTime, ForceMode2D.Impulse);
            yield return new WaitForEndOfFrame();
        }
        //Debug.Log("STOP");
        PlayerState.State = PlayerState._lastState;
        PlayerState._lastState = PlayerState.State;
    }
}
