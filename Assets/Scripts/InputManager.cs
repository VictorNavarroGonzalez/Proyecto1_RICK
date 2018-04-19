using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour{
    
    #region JOYSTICK INPUT
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
    public static bool ButtonA(){
        return Input.GetButtonDown("ButtonA");
    }
    public static bool ButtonDownA() {
        return Input.GetButton("ButtonA");
    }
    #endregion

    #region CHANGE INPUT
    public static bool ButtonY() {
        return Input.GetButtonDown("ButtonY");
    }
    #endregion

    #region DASH INPUTS
    public static bool ButtonX()
    {
        return Input.GetButtonDown("ButtonX");
    }
    public static bool ButtonDownX()
    {
        return Input.GetButton("ButtonX");
    }
    #endregion

}
