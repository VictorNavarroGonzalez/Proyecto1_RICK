using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour {

    public Button button;

    // Select the specified button on start for
    // avoiding no having a selection to start with.
    void Start() {
        if (button != null) {
            button.Select();
        }
    }

    // This function selects the specified button on
    // the current menu panel.
    public void SelectButton() {
        if (button != null) {
            button.Select();
        }
    }
}
