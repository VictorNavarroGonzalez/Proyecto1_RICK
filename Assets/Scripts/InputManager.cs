using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static bool ButtonA() {
        return Input.GetButtonDown("ButtonA");
    }


}
