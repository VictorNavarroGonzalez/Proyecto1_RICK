using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCollider : MonoBehaviour {

    public bool bounce;
    public bool walljump;
    public bool change;
    public bool ghost;
    public bool fall;
    public bool climb;
    public bool dash;

    private GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {

            #region Update Player Controls
            player.GetComponent<PlayerBounce>().enabled = bounce;
            player.GetComponent<PlayerBounce>().canWBounce = walljump;
            player.GetComponent<PlayerChange>().enabled = change;
            player.GetComponent<PlayerGhost>().enabled = ghost;
            player.GetComponent<PlayerFall>().enabled = fall;
            player.GetComponent<PlayerClimb>().enabled = climb;
            player.GetComponent<PlayerDash>().enabled = dash;
            #endregion

            // Destroy the objects after the collision.
            gameObject.SetActive(false);
        }
    }
}
