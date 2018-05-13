using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDestroy : MonoBehaviour {

    private GameObject player;
    public GameObject[] prompters;

    void Start() {
        player = GameObject.Find("Player");
    }

    // Sets the prompters objects to inactive for optimizing memmory
    // and to avoid repetition of the same instruction.
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {

            foreach (GameObject p in prompters) {
                p.SetActive(false);
            }

        }
    }

}
