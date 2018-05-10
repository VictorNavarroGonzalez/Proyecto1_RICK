using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour{
    
    #region JOYSTICK INPUT
    //Gets input from joysticks and normalizes it
    public static float MainHorizontal() {
        float r = 0.0f;
        r += Input.GetAxis("J_Horizontal");
        r += Input.GetAxis("K_Horizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float MainVertical() {
        float r = 0.0f;
        r += Input.GetAxis("J_Vertical");
        r += Input.GetAxis("K_Vertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static Vector2 MainJoystick() {
        return new Vector2(MainHorizontal(), MainVertical());
    }
    #endregion

    #region JUMP INPUTS
    //Gets Button A
    public static bool ButtonA;
  
    public static bool ButtonDownA() {
        return Input.GetButton("ButtonA");
    }
    #endregion

    #region CHANGE INPUT
    //Gets button Y
    public static bool ButtonY;
    public static bool ButtonB;
    #endregion

    #region DASH INPUTS
    //Gets button X
    public static bool ButtonX;
    
    public static bool ButtonDownX() {
        return Input.GetButton("ButtonX");
    }
    #endregion

    #region TIMER
    private static float _timePressed;
    public static float TimePressed {
        get { return _timePressed; }
        set { _timePressed = value; }
    }
    #endregion

    public static bool ButtonStart;

    //Constantly updates waiting for input
    private void Update()
    {
        //Reads when ButtonA is pressed to know how much time the button is pressed
        if (!ButtonDownA()) TimePressed = Time.time;        
        if (!ButtonA) ButtonA = Input.GetButtonDown("ButtonA"); 
        if (!ButtonX) ButtonX = Input.GetButtonDown("ButtonX");
        if (!ButtonY) ButtonY = Input.GetButtonDown("ButtonY");
        if (!ButtonB) ButtonB = Input.GetButtonDown("ButtonB");
        if (!ButtonStart) ButtonStart = Input.GetButtonDown("ButtonStart");
    }
}
