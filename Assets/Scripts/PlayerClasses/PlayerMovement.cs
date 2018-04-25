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


    #region X axis

    #region Right
    public void MoveRight() {
        // If the joystick moves right set the velocity to 
        // the max velocity / joystick percentage of inclination
        if (rb.velocity.x < _maxSpeed * Mathf.Abs(InputManager.MainHorizontal())) {
            rb.AddForce(Vector2.right * _sideForce * Mathf.Abs(InputManager.MainHorizontal()), ForceMode2D.Force);     
        }
    }
    #endregion

    #region StopX
    //Drag force to stop the Player
    public void Stop() {        
        if (rb.velocity.x < 0.0f) {
            rb.AddForce(Vector2.right * _friction, ForceMode2D.Force);
        }
        else if (rb.velocity.x > 0.0f) {
            rb.AddForce(Vector2.left * _friction, ForceMode2D.Force);
        }
    }
#endregion

    #region Left
    public void MoveLeft() {
        //If the joystick moves left 
        if (rb.velocity.x > -_maxSpeed * Mathf.Abs(InputManager.MainHorizontal())) {
            rb.AddForce(Vector2.left * _sideForce * Mathf.Abs(InputManager.MainHorizontal()), ForceMode2D.Force);       // Set the velocity to the max velocity / joystick percentage of inclination
        }
    }
    #endregion

    #endregion

    #region Y axis

    #region Up
    public void MoveUp() {
        //If the joystick moves up 
        if (rb.velocity.y < _maxSpeed * Mathf.Abs(InputManager.MainVertical()))
            if(rb.velocity.y < 0f) rb.velocity = new Vector2(rb.velocity.x, 0); {
            rb.AddForce(Vector2.up * _sideForce * Mathf.Abs(InputManager.MainVertical()), ForceMode2D.Force);           // Set the velocity to the max velocity / joystick percentage of inclination
        }
    }
    #endregion

    #region Down
    public void MoveDown() {
        //If the joystick moves down 
        if (rb.velocity.y > -_maxSpeed * Mathf.Abs(InputManager.MainVertical())) {
            rb.AddForce(Vector2.down * _sideForce * Mathf.Abs(InputManager.MainVertical()), ForceMode2D.Force);         // Set the velocity to the max velocity / joystick percentage of inclination
        }
    }
    #endregion

    #region StopY
    //Drag force vertical to improve the feeling climbing
    public void StopY() {      
        if (rb.velocity.y < 0.0f) {
            rb.AddForce(Vector2.up * 35, ForceMode2D.Force);
        }
    }
    #endregion

    #endregion

}
