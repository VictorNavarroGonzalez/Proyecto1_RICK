using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float _sideForce = 1000f;
    public float SideForce {
        get {
            return _sideForce;
        }
        set {
            _sideForce = value;
        }
    }

    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        //Player Horizontal Movement
        if (InputManager.MainHorizontal() == 0) {
            //Ease Out Movement
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0, rb.velocity.y), Time.deltaTime);
        }
        else if (InputManager.MainHorizontal() != 0) {
            rb.velocity = new Vector2(InputManager.MainHorizontal() * _sideForce * Time.deltaTime, rb.velocity.y);
        }
    }

}
