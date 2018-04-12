using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool canDash;

    private float _dashForce = 1000f;
    public float DashForce {
        get { return _dashForce; }
        set { _dashForce = value;  }
    }


    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (canDash == false && PlayerState.State == PlayerState.MyState.Grounding) canDash = true;
    }

    public bool CheckDash() {
        if (PlayerState.State != PlayerState.MyState.Grounding && canDash) return true;
        return false; ;
    }

    public void RightDash() {
        canDash = false;
        StartCoroutine(GetComponent<PlayerState>().Stopping(0.1f));
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(Vector2.right * DashForce * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void LeftDash() {
        canDash = false;
        StartCoroutine(GetComponent<PlayerState>().Stopping(0.1f));
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(Vector2.left * DashForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
