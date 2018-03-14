using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float _maxSpeed = 10f;
    public float MaxSpeed {
        get { return _maxSpeed; }
        set { _maxSpeed = value; }
    }

    private float _friction = 15f;
    public float Friction {
        get { return _friction; }
        set { _friction = value; }
    }

    private float _sideForce = 5f;
    public float SideForce {
        get { return _sideForce; }
        set { _sideForce = value; }
    }

    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        //Player Horizontal Movement
        if (InputManager.MainHorizontal() < 0.0f) {
            if(rb.velocity.x > -_maxSpeed) {
                rb.AddForce(Vector2.left * _sideForce * Mathf.Abs(InputManager.MainHorizontal()), ForceMode2D.Force);
            }         
        }
        else if (InputManager.MainHorizontal() > 0.0f) {
            if (rb.velocity.x < _maxSpeed) {
                rb.AddForce(Vector2.right * _sideForce * Mathf.Abs(InputManager.MainHorizontal()), ForceMode2D.Force);
            }  
        }
        else if(InputManager.MainHorizontal() == 0.0f) {
            //Checks if joystick is equal to 0, for decelerating the player
            if (rb.velocity.x < 0.0f) {
                rb.AddForce(Vector2.right * _friction, ForceMode2D.Force);
            }
            else if(rb.velocity.x > 0.0f) {
                rb.AddForce(Vector2.left * _friction, ForceMode2D.Force);
            }
        }

            
    }

}
