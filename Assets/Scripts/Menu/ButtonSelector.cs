using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour {

    public Button button;


    void Start() {
        if (button != null) {
            button.Select();
        }
    }

    public void SelectButton() {
        if (button != null) {
            button.Select();
        }
    }
}
