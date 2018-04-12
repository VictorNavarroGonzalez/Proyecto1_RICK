using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    private GameObject player;
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
        State = MyState.Jumping;
        LastState = State;
        stop = false;
        // Initialize RICK into a Circle
        Character = MyCharacter.CIRCLE;
        GetComponent<PlayerChange>().Actualize();
    }

    void FixedUpdate() {
        Debug.Log(State);

        #region RICK HORIZONTAL MOVEMENT
        if (!stop) {
            if (InputManager.MainHorizontal() > 0.0f) {
                GetComponent<PlayerMovement>().MoveRight();
            }
            else if (InputManager.MainHorizontal() == 0.0f) {
                GetComponent<PlayerMovement>().Stop();
            }
            else if (InputManager.MainHorizontal() < 0.0f) {
                GetComponent<PlayerMovement>().MoveLeft();
            }
        }
        #endregion

        if (State != MyState.Grounding && GetComponent<PlayerGround>().CheckGround()) {
            StartCoroutine(ActiveGrounding());
        }

        if(GetComponent<PlayerBounce>().CheckBounce()) {
            StartCoroutine(GetComponent<PlayerBounce>().Bounce());
            LastState = State;
            State = MyState.Bouncing;
        }

        #region RICK DASH 
        if (InputManager.ButtonRT()) {
            if (GetComponent<PlayerGround>().CheckGround()) State = MyState.Grounding;

            if (GetComponent<PlayerDash>().CheckDash()) {
                GetComponent<PlayerDash>().RightDash();
                LastState = State;
                State = MyState.Dashing;
            }
        }

        if (InputManager.ButtonLT()) {
            if (GetComponent<PlayerGround>().CheckGround()) State = MyState.Grounding;

            if (GetComponent<PlayerDash>().CheckDash()) {
                GetComponent<PlayerDash>().LeftDash();
                LastState = State;
                State = MyState.Dashing;
            }
        }
        #endregion

        #region RICK CHANGE CHARACTER
        if (InputManager.ButtonY()) {
            GetComponent<PlayerChange>().Change();
            GetComponent<PlayerChange>().Actualize();
        }
        #endregion

        #region CIRCLE BEHAVIOR
        if (Character == MyCharacter.CIRCLE) { 
            
            if (InputManager.ButtonA()) {

                #region Jumping & Bouncing
                if (GetComponent<PlayerGround>().CheckGround()) State = MyState.Grounding;

                if (State != MyState.Grounding && GetComponent<PlayerBounce>().CheckBounce()) {
                    StartCoroutine(GetComponent<PlayerBounce>().Bounce());
                    LastState = State;
                    State = MyState.Bouncing;
                }
                else {
                    switch (State) {
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
                #endregion

                #region Wall Bouncing
                if (GetComponent<PlayerGround>().LeftHit && GetComponent<PlayerBounce>().DistGround() > 0.4f) {
                    StartCoroutine(GetComponent<PlayerBounce>().LeftBounce());
                }
                else if (GetComponent<PlayerGround>().RightHit && GetComponent<PlayerBounce>().DistGround() > 0.4f) {
                    StartCoroutine(GetComponent<PlayerBounce>().RightBounce());
                }
                #endregion

            }

        }
        #endregion

        #region SQUARE BEHAVIOR
        else if (Character == MyCharacter.SQUARE) {

            #region Jumping & Falling
            if (InputManager.ButtonA()) {
                switch (State) {
                    case MyState.Grounding:
                        GetComponent<PlayerJump>().Jump();
                        LastState = State;
                        State = MyState.Jumping;
                        break;

                    case MyState.Jumping:
                    case MyState.DoubleJumping:
                    case MyState.Bouncing:
                        GetComponent<PlayerFall>().Fall();
                        LastState = State;
                        State = MyState.Falling;
                        break;
                }
            }
            #endregion

            #region Wall Climbing
            if (GetComponent<PlayerGround>().LeftHit || GetComponent<PlayerGround>().RightHit) {
                if (InputManager.MainVertical() < 0.0f && !stop) {
                    GetComponent<PlayerMovement>().MoveUp();
                }
                else if (InputManager.MainVertical() == 0.0f && !stop) {
                    GetComponent<PlayerMovement>().StopY();
                }
                else if (InputManager.MainVertical() > 0.0f && !stop) {
                    GetComponent<PlayerMovement>().MoveDown();
                }
            }
            #endregion


        }
        #endregion

    }

    // Stop certain action on a specified time
    public IEnumerator Stopping(float time) {
        stop = true;
        yield return new WaitForSeconds(time);
        stop = false;
    }

    public IEnumerator ActiveGrounding() {
        yield return new WaitForSeconds(0.01f);
        if (GetComponent<PlayerGround>().CheckGround())
        {
            LastState = State;
            State = MyState.Grounding;
        }
    }
}