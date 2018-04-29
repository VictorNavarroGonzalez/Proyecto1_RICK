using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCollider : MonoBehaviour {

    // public bool wallBounce;
    public bool bounce;
    public bool change;
    public bool ghost;
    public bool fall;

    private GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {
            player.GetComponent<PlayerBounce>().enabled = bounce;
            player.GetComponent<PlayerChange>().enabled = change;
            player.GetComponent<PlayerGhost>().enabled = ghost;
            player.GetComponent<PlayerFall>().enabled = fall;
        }
    }
}
