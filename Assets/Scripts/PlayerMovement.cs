using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float MAX_VEL = 10f;

    private float _sideForce = 5f;
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
        if (InputManager.MainHorizontal() < 0.0f) {
            if(rb.velocity.x > -10f)
            {
                rb.AddForce(Vector2.left * _sideForce, ForceMode2D.Force);
            }
                
        }
        else if (InputManager.MainHorizontal() > 0.0f) {
            if (rb.velocity.x < 10f)
                rb.AddForce(Vector2.right * _sideForce, ForceMode2D.Force);
        }

            
    }

}
