using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Horizontal");
        r += Input.GetAxis("K_Horizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Vertical");
        r += Input.GetAxis("K_Vertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector2 MainJoystick()
    {
        return new Vector2(MainHorizontal(), MainVertical());
    }

    public static bool ButtonA()
    {
        return Input.GetButtonDown("ButtonA");
    }

    public static bool PressedButtonA()
    {
        return Input.GetButton("ButtonA");
    }

    public static bool ButtonY()
    {
        return Input.GetButtonDown("ButtonY");
    }

    public static bool ButtonRT()
    {
        return Input.GetButtonDown("ButtonRT");
    }
    public static bool ButtonLT()
    {
        return Input.GetButtonDown("ButtonLT");
    }
}
//    public static bool ButtonRT(){
//        Debug.Log("pillao l");
//        return Input.GetAxis("ButtonRT")>0;
//    }

//    public static bool ButtonLT(){
//        Debug.Log("pillao k");
//        return Input.GetAxis("ButtonLT")>0;
//    }
//}
