using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHover : MonoBehaviour, ISelectHandler, IDeselectHandler {

    private TextMeshProUGUI text;
    private string original;

    void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        original = text.text;
    }

    public void OnSelect(BaseEventData eventData) {
        text.fontStyle = FontStyles.Bold;
        text.fontSize += 2f;
        text.SetText(text.text + '.');
    }

    public void OnDeselect(BaseEventData data) {
        text.fontStyle = FontStyles.Normal;
        text.fontSize -= 2f;
        text.SetText(original);
    }
}
