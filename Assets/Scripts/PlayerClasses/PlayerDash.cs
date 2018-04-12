using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    private Rigidbody2D rb;

    private float _dashForce = 1000f;
    public float DashForce {
        get { return _dashForce; }
        set { _dashForce = value;  }
    }


    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool CheckDash() {
        switch (PlayerState.State) {
            case PlayerState.MyState.Dashing:
            case PlayerState.MyState.Grounding:
                return false;

            default: return true;
        }
    }

    public void RightDash() {
        StartCoroutine(GetComponent<PlayerState>().Stopping(0.1f));
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(Vector2.right * DashForce * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void LeftDash() {
        StartCoroutine(GetComponent<PlayerState>().Stopping(0.1f));
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(Vector2.left * DashForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
