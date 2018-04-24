using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    private GameObject player;
    private bool stop;

    public enum MyState { Jumping, DoubleJumping, Dashing, Bouncing, Grounding, Falling };
    public static MyState _state;
    public static MyState State
    {
        get { return _state; }
        set { _state = value; }
    }
    private static MyState temp;
    public static MyState _lastState;
    public static MyState LastState
    {
        get { return _lastState; }
        set { _lastState = value; }
    }

    public enum MyCharacter { SQUARE, CIRCLE }
    public static MyCharacter _character;
    public static MyCharacter Character
    {
        get { return _character; }
        set { _character = value; }
    }

    void Awake()
    {
        State = MyState.Jumping;
        LastState = State;
        stop = false;

        // Initialize RICK into a Circle
        Character = MyCharacter.CIRCLE;
        GetComponent<PlayerChange>().Actualize();
        temp = MyState.Bouncing;
    }

    void FixedUpdate()
    {
        if (temp != State)
        {
            Debug.Log(State);
            temp = State;
        }

        #region RICK HORIZONTAL MOVEMENT
        if (!stop)
        {
            if (InputManager.MainHorizontal() > 0.0f && !GetComponent<PlayerGround>().RightHit)
            {
                GetComponent<PlayerMovement>().MoveRight();
            }
            else if (InputManager.MainHorizontal() == 0.0f)
            {
                GetComponent<PlayerMovement>().Stop();
            }
            else if (InputManager.MainHorizontal() < 0.0f & !GetComponent<PlayerGround>().LeftHit)
            {
                GetComponent<PlayerMovement>().MoveLeft();
            }
        }
        #endregion

        #region RICK GROUNDING
        if (State != MyState.Grounding && GetComponent<PlayerGround>().CheckGround())
        {
            StartCoroutine(ActiveGrounding());
        }
        #endregion

        #region RICK DASH 
        if (InputManager.ButtonX)
        {
            InputManager.ButtonX = false;

            if (GetComponent<PlayerDash>().CheckDash())
            {
                GetComponent<PlayerDash>().Dash();
                LastState = State;
                State = MyState.Dashing;
            }
        }

       #endregion

        #region RICK CHANGE CHARACTER
        if (InputManager.ButtonY)
        {
            InputManager.ButtonY = false;

            GetComponent<PlayerChange>().Change();
            GetComponent<PlayerChange>().Actualize();
        }
        #endregion

        #region RICK GHOST
        if (InputManager.ButtonB) {

            InputManager.ButtonB = false;
            if(GetComponent<PlayerGhost>().CheckGhost())
                GetComponent<PlayerGhost>().Teleport();
            else
                GetComponent<PlayerGhost>().Create();

        }
        #endregion

        #region CIRCLE BEHAVIOR
        if (Character == MyCharacter.CIRCLE)
        {

            #region Bouncing
            if (GetComponent<PlayerBounce>().CheckBounce())
            {
                StartCoroutine(GetComponent<PlayerBounce>().Bounce());
                LastState = State;
                StartCoroutine(ActiveBouncing());
            }
            #endregion

            #region Wall Bouncing
            if (GetComponent<PlayerGround>().LeftHit && GetComponent<PlayerBounce>().DistGround() > 0.4f && GetComponent<PlayerBounce>().canWBounce)
            {
                StartCoroutine(GetComponent<PlayerBounce>().LeftBounce());
            }
            else if (GetComponent<PlayerGround>().RightHit && GetComponent<PlayerBounce>().DistGround() > 0.4f && GetComponent<PlayerBounce>().canWBounce)
            {
                StartCoroutine(GetComponent<PlayerBounce>().RightBounce());
            }
            #endregion

            #region Jumping
            if (InputManager.ButtonA)
            {
                InputManager.ButtonA = false;


                switch (State)
                {
                    case MyState.Grounding:
                        StartCoroutine(GetComponent<PlayerBounce>().CheckWallBounce());
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

        }
        #endregion

        #region SQUARE BEHAVIOR
        else if (Character == MyCharacter.SQUARE)
        {

            #region Jumping & Falling
            if (InputManager.ButtonA)
            {
                InputManager.ButtonA = false;

                switch (State)
                {
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
            if (GetComponent<PlayerGround>().LeftHit || GetComponent<PlayerGround>().RightHit)
            {
                if (InputManager.MainVertical() < 0.0f && !stop)
                {
                    GetComponent<PlayerMovement>().MoveUp();
                }
                else if (InputManager.MainVertical() == 0.0f && !stop)
                {
                    GetComponent<PlayerMovement>().StopY();
                }
                else if (InputManager.MainVertical() > 0.0f && !stop)
                {
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

    //Change the state to Grounding with some delay (0.05 seconds) to have time to do the checks
    public IEnumerator ActiveGrounding() {
        if (Character == MyCharacter.CIRCLE) yield return new WaitForSeconds(0.05f);
        else if (Character == MyCharacter.SQUARE) yield return new WaitForSeconds(0.05f);
        if (GetComponent<PlayerGround>().CheckGround()) {
            LastState = State;
            State = MyState.Grounding;
        }
    }

    //Change the state to Bouncing with some delay (0.3 seconds) to have time to do the checks
    public IEnumerator ActiveBouncing() {         
        yield return new WaitForSeconds(0.3f);
        State = MyState.Bouncing;
    }
}