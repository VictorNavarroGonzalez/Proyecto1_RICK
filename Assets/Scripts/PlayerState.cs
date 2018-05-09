using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    private GameObject player;
    private bool stop;
    //Audio
    public AudioClip genericAudio;
    public AudioClip dashSound;
    public AudioSource source;
    //Player State
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
    //Player phase
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
        Character = GetComponent<PlayerChange>().initial;
        GetComponent<PlayerChange>().Actualize();
        temp = MyState.Bouncing;
    }

    void FixedUpdate() {

        // LogState();

        #region GENERAL BEHAVIOUR

        #region RICK HORIZONTAL MOVEMENT
        if (!stop) {
            if (InputManager.MainHorizontal() > 0.0f && !GetComponent<PlayerGround>().RightHit) {
                GetComponent<PlayerMovement>().MoveRight();
            }
            else if (InputManager.MainHorizontal() == 0.0f) {
                GetComponent<PlayerMovement>().Stop();
            }
            else if (InputManager.MainHorizontal() < 0.0f & !GetComponent<PlayerGround>().LeftHit) {
                GetComponent<PlayerMovement>().MoveLeft();
            }
        }
        #endregion

        #region RICK GROUNDING
        if (State != MyState.Grounding && GetComponent<PlayerGround>().CheckGround()) {
            StartCoroutine(ActiveGrounding());                                                  //Controls if Rick is on the ground
        }
        #endregion

        #region RICK DASH  
        if (InputManager.ButtonX) {
            if (GetComponent<PlayerDash>().enabled) {

                InputManager.ButtonX = false;

                if (GetComponent<PlayerDash>().CheckDash()) {
                    GetComponent<PlayerDash>().Dash();                                           //Initializes Rick's dash
                    source.PlayOneShot(dashSound, 1f);
                    LastState = State;
                    State = MyState.Dashing;
                }

            }
        }
        #endregion

        #region RICK CHANGE CHARACTER
        if (InputManager.ButtonY) {
            PlayerChange playerChange = GetComponent<PlayerChange>();

            if (playerChange.enabled) {
                InputManager.ButtonY = false;                                                   //Allows Rick to change between Square and Circle

                playerChange.Change();
                playerChange.Actualize();
            }
        }
        #endregion

        #region RICK GHOST
        if (InputManager.ButtonB) {
            
            PlayerGhost playerGhost = GetComponent<PlayerGhost>();

            if (playerGhost.enabled) {
                InputManager.ButtonB = false;

                if (playerGhost.CheckGhost()) {
                    if (playerGhost.CheckTeleport()) {
                        playerGhost.Teleport();
                        source.PlayOneShot(genericAudio, 0.5f);

                        // Change character to the ghost one and actualize the properties.
                        Character = playerGhost.GhostChar;
                        GetComponent<PlayerChange>().Actualize();
                    }
                }
                else {
                    playerGhost.Create();
                    source.PlayOneShot(genericAudio, 0.5f);

                }
            }
        }
        #endregion

        #endregion

        #region CIRCLE BEHAVIOR
        //Circle properties and habilities
        if (Character == MyCharacter.CIRCLE) {

            #region Bouncing
            //Checks if the player can bounce on the floor
            if (GetComponent<PlayerBounce>().CheckBounce()) {
                StartCoroutine(GetComponent<PlayerBounce>().Bounce());
                source.PlayOneShot(genericAudio, 0.5f);
                LastState = State;
                StartCoroutine(ActiveBouncing());
            }    
            #endregion

            #region Wall Bouncing
            //Checks if the player can bounce in a wall in both sides
            if (GetComponent<PlayerGround>().LeftHit && GetComponent<PlayerBounce>().DistGround() > 0.4f && GetComponent<PlayerBounce>().canWBounce) {
                StartCoroutine(GetComponent<PlayerBounce>().LeftBounce());
            }
            else if (GetComponent<PlayerGround>().RightHit && GetComponent<PlayerBounce>().DistGround() > 0.4f && GetComponent<PlayerBounce>().canWBounce) {
                StartCoroutine(GetComponent<PlayerBounce>().RightBounce());
            }
            #endregion

            #region Jumping
            if (InputManager.ButtonA) {
                if (GetComponent<PlayerJump>().enabled) {

                    InputManager.ButtonA = false;

                    //Checks the current player state in order to distinguish between a jump and a double jump
                    switch (State) {
                       
                        case MyState.Grounding:
                            StartCoroutine(GetComponent<PlayerBounce>().CheckWallBounce());
                            GetComponent<PlayerJump>().Jump();
                            source.PlayOneShot(genericAudio, 0.5f);
                            LastState = State;
                            State = MyState.Jumping;
                            break;

                        case MyState.Jumping:
                            GetComponent<PlayerJump>().DoubleJump();
                            source.PlayOneShot(genericAudio, 0.5f);
                            LastState = State;
                            State = MyState.DoubleJumping;
                            break;

                    }
                }
            }
            #endregion

        }
        #endregion

        #region SQUARE BEHAVIOR
        //Square properties and habilities
        else if (Character == MyCharacter.SQUARE) {

            #region Jumping & Falling
            if (InputManager.ButtonA) {
                InputManager.ButtonA = false;

                //Checks the player state in order to Smack or Jump, as both habilities are triggered by the same button
                switch (State) {
                    case MyState.Grounding:
                        if (GetComponent<PlayerGround>().Grounded) {
                            GetComponent<PlayerJump>().Jump();
                            source.PlayOneShot(genericAudio, 0.5f);
                            LastState = State;
                            State = MyState.Jumping;
                        }
                        else if (GetComponent<PlayerBounce>().DistGround() > 7f) {
                            GetComponent<PlayerFall>().Fall();
                            LastState = State;
                            State = MyState.Falling;
                        }                        
                        break;

                    case MyState.Jumping:
                    case MyState.DoubleJumping:
                    case MyState.Bouncing:
                        if(GetComponent<PlayerBounce>().DistGround() > 2.5f) {            //Minimum height to smack
                            if(!GetComponent<PlayerGround>().LeftHit && !GetComponent<PlayerGround>().RightHit) {
                                GetComponent<PlayerFall>().Fall();
                                LastState = State;
                                State = MyState.Falling;
                            }
                        }
                        break;
                }
            }
            #endregion

            #region Wall Climbing
            if(!stop) GetComponent<PlayerClimb>().Climb();
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

    public void LogState() {
        if (temp != State) {
            Debug.Log(State);
            temp = State;
        }
    }
}