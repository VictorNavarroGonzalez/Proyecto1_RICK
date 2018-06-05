using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextHover : MonoBehaviour {

    private TextMeshProUGUI text;

    void Start() {

        // Get text component to edit.
        text = GetComponent<TextMeshProUGUI>();

    }

	void Update () {

        // Update type color according to RICK character.
        if (PlayerState.Character == PlayerState.MyCharacter.CIRCLE) {
            text.color = new Color32(0, 0, 0, 255);
        }
        else if (PlayerState.Character == PlayerState.MyCharacter.SQUARE) {
            text.color = new Color32(255, 255, 255, 255);
        }

    }
}
