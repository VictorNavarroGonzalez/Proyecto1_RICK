using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    private GameObject player;

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
        _state = MyState.Falling;

        _character = MyCharacter.CIRCLE;
        GetComponent<PlayerChange>().Actualize();
    }

    void FixedUpdate () {
        Debug.Log(_state);
        

        // CIRCLE RICK
        if (InputManager.ButtonA()) {
            StartCoroutine(GetComponent<PlayerGround>().CheckGround());

            if (GetComponent<PlayerBounce>().CheckBounce()) {
                GetComponent<PlayerBounce>().Bounce();
            }
            else {
                switch (_state) {
                    case MyState.Grounding:
                        GetComponent<PlayerJump>().Jump();
                        break;

                    case MyState.Jumping:
                        GetComponent<PlayerJump>().DoubleJump();
                        break;
                }
            }
        }



        // RICK CHANGE CHARACTER
        if (InputManager.ButtonY()) {
            // RICK Character State
            GetComponent<PlayerChange>().Change();

            // RICK Properties
            GetComponent<PlayerChange>().Actualize();

            Debug.Log(_character);
        }


    }
}
