using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    private GameObject player;
    private float t = 0.0f;
    private bool stop;

    public enum MyState { Jumping, DoubleJumping, Dashing, Bouncing, Grounding, Falling };
    public static MyState _state;
    public static MyState State {
        get { return _state; }
        set { _state = value; }
    }

    public static MyState _lastState;
    public static MyState LastState {
        get { return _lastState; }
        set { _lastState = value; }
    }

    public enum MyCharacter { SQUARE, CIRCLE }
    public static MyCharacter _character;
    public static MyCharacter Character {
        get { return _character; }
        set { _character = value; }
    }

    void Awake() {
        State = MyState.Falling;
        LastState = State;
        stop = false;
        // Initialize RICK into a Circle
        Character = MyCharacter.CIRCLE;
        GetComponent<PlayerChange>().Actualize();
    }

    void FixedUpdate() {
        // RICK HORIZONTAL MOVEMENT
        if (InputManager.MainHorizontal() > 0.0f && !stop) {
            GetComponent<PlayerMovement>().MoveRight();
        }
        else if (InputManager.MainHorizontal() == 0.0f && !stop) {
            GetComponent<PlayerMovement>().Stop();
        }
        else if (InputManager.MainHorizontal() < 0.0f && !stop) {
            GetComponent<PlayerMovement>().MoveLeft();
        }

        //if (GetComponent<Rigidbody2D>().velocity.y < 0)
        //    State = MyState.Falling;

        //StartCoroutine(GetComponent<PlayerGround>().CheckGround());
        // CIRCLE RICK
        if (InputManager.ButtonA())
        {

            if (State != MyState.Grounding && GetComponent<PlayerBounce>().CheckBounce())
            {
                StartCoroutine(GetComponent<PlayerBounce>().Bounce());
                LastState = State;
                State = MyState.Bouncing;
                Debug.Log(State);
            }
            else
            {
                switch (State)
                {
                    case MyState.Grounding:
                        GetComponent<PlayerJump>().Jump();
                        LastState = State;
                        State = MyState.Jumping;
                        break;

                    case MyState.Jumping:
                        GetComponent<PlayerJump>().DoubleJump();
                        LastState = State;
                        State = MyState.DoubleJumping;
                        break;
                }
            }

            if (GetComponent<PlayerGround>().LeftHit)
            {
                StartCoroutine(GetComponent<PlayerBounce>().LeftBounce());
            }
            else if (GetComponent<PlayerGround>().RightHit)
            {
                StartCoroutine(GetComponent<PlayerBounce>().RightBounce());
            }
        }
        //else if (LastState == MyState.Bouncing && State == MyState.Grounding)
        //{
        //    LastState = MyState.Grounding;
        //    float value = 300f;
        //    StartCoroutine(GetComponent<PlayerBounce>().Atenuation(value));
        //}
            
            

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

        //Debug.Log(State);
    }

    public IEnumerator Stopping(float value) {
        stop = true;
        yield return new WaitForSeconds(value);
        stop = false;
    }
}