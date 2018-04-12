using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // JOYSTICK INPUT
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

    // JUMP INPUTS
    public static bool ButtonA(){
        return Input.GetButtonDown("ButtonA");
    }
    public static bool ButtonDownA() {
        return Input.GetButton("ButtonA");
    }

    // CHANGE INPUT
    public static bool ButtonY() {
        return Input.GetButtonDown("ButtonY");
    }


    // DASH INPUTS
    public static bool ButtonRT() {
        if (Input.GetButtonDown("KeyRT")) return true;
        return Input.GetAxis("ButtonRT") < 0;
    }
    public static bool ButtonLT() {
        if (Input.GetButtonDown("KeyLT")) return true;
        return Input.GetAxis("ButtonLT") > 0;
    }
}
