using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHover : MonoBehaviour, ISelectHandler, IDeselectHandler {

    // Get the text of the button and his original value.
    private TextMeshProUGUI text;
    private string original;

    void Awake() {

        // Get the childen of the button which is his child.
        text = GetComponentInChildren<TextMeshProUGUI>();
        original = text.text;

    }

    void Update() {

        // Update type color according to RICK character.
        if(PlayerState.Character == PlayerState.MyCharacter.CIRCLE) {
            text.color = new Color32(0, 0, 0, 255);
        }
        else if (PlayerState.Character == PlayerState.MyCharacter.SQUARE) {
            text.color = new Color32(255, 255, 255, 255);
        }

    }

    // Properties to consider when the button is selected.
    public void OnSelect(BaseEventData eventData) {
        text.SetText(text.text + '.');
        text.fontStyle = FontStyles.Bold;
        text.fontSize += 2f;
        text.UpdateFontAsset();
    }

    // Properties to consider when the button is deselected.
    public void OnDeselect(BaseEventData data) {
        text.SetText(original);
        text.fontStyle = FontStyles.Normal;
        text.fontSize -= 2f;    
    }
}
