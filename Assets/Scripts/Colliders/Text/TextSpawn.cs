using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawn : MonoBehaviour {

    private GameObject player;
    public GameObject prompter;
    public Sprite text;

    void Start() {
        player = GameObject.Find("Player");
    }

    // When the player collides with the collider the text
    // will appear in the prompter.
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {
            prompter.GetComponent<SpriteRenderer>().sprite = text;
        }
    }
}
