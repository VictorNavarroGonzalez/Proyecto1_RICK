using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDestroy : MonoBehaviour {

    private GameObject player;
    public GameObject prompter;

    void Start() {
        player = GameObject.Find("Player");
    }

    // Sets the prompter object to inactive for optimizing memmory
    // and to avoid repetition of the same instruction.
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {

            prompter.SetActive(false);

        }
    }

}
