using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDestroy : MonoBehaviour {

    private GameObject player;
    public GameObject prompter;

    void Start() {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {

            prompter.SetActive(false);

        }
    }

}
