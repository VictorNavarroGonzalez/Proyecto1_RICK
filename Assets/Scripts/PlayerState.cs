using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    private GameObject player;
    private float t = 0.0f;

    public enum MyState { Jumping, DoubleJumping, Dashing, Bouncing, Grounding, Falling };
    public static MyState _state;
    public static MyState State {
        get { return _state; }
        set { _state = value; }
    }

    public enum MyCharacter { SQUARE, CIRCLE }
    public static MyCharacter _character;
    public static MyCharacter Character {
        get { return _character; }
        set { _character = value; }
    }

    void Awake() {
        State = MyState.Falling;

        // Initialize RICK into a Circle
        Character = MyCharacter.CIRCLE;
        GetComponent<PlayerChange>().Actualize();
    }

    void FixedUpdate() {
        // RICK HORIZONTAL MOVEMENT
        if (InputManager.MainHorizontal() > 0.0f) {
            GetComponent<PlayerMovement>().MoveRight();
        }
        else if (InputManager.MainHorizontal() == 0.0f) {
            GetComponent<PlayerMovement>().Stop();
        }
        else if (InputManager.MainHorizontal() < 0.0f) {
            GetComponent<PlayerMovement>().MoveLeft();
        }

        if (GetComponent<Rigidbody2D>().velocity.y < 0)
            State = MyState.Falling;

        // CIRCLE RICK
        if (InputManager.ButtonA()) {
            StartCoroutine(GetComponent<PlayerGround>().CheckGround());

            if (GetComponent<PlayerBounce>().CheckBounce()) {
                State = MyState.Bouncing;
                StartCoroutine(GetComponent<PlayerBounce>().Bounce());       
            }
            else {
                switch (State) {
                    case MyState.Grounding:
                        GetComponent<PlayerJump>().Jump();
                        State = MyState.Jumping;
                        break;

                    case MyState.Jumping:
                        GetComponent<PlayerJump>().DoubleJump();
                        State = MyState.DoubleJumping;
                        break;
                }
            }
        }

        // RICK DASH
        if (GetComponent<PlayerDash>().CheckDash()) {
            if (InputManager.ButtonRT()) GetComponent<PlayerDash>().RightDash();
            if (InputManager.ButtonLT()) GetComponent<PlayerDash>().LeftDash();
        }


        // RICK CHANGE CHARACTER
        if (InputManager.ButtonY()) {
            GetComponent<PlayerChange>().Change();
            GetComponent<PlayerChange>().Actualize();
        }
    }
}