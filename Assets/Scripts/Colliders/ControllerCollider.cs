using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControllerCollider : MonoBehaviour {

    [System.Serializable]
    public class Options {
        public bool jump;
        public bool bounce;
        public bool walljump;
        public bool change;
        public bool ghost;
        public bool fall;
        public bool climb;
        public bool dash;

        public float elasticConstant;
        public float jumpForce;

        public PlayerState.MyCharacter character;
    }

    public Options enter;
    public Options exit;

    private GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {

            #region Update Player Controls
            player.GetComponent<PlayerState>().StopBounce = !enter.bounce;
            player.GetComponent<PlayerState>().StopWallBounce = !enter.walljump;
            player.GetComponent<PlayerChange>().enabled = enter.change;
            player.GetComponent<PlayerGhost>().enabled = enter.ghost;
            player.GetComponent<PlayerFall>().enabled = enter.fall;
            player.GetComponent<PlayerClimb>().enabled = enter.climb;
            player.GetComponent<PlayerDash>().enabled = enter.dash;
            player.GetComponent<PlayerJump>().enabled = enter.jump;
            #endregion

            player.GetComponent<PlayerBounce>().K = enter.elasticConstant;
            player.GetComponent<PlayerJump>().JumpForce = enter.jumpForce;

            PlayerState.Character = enter.character;
            player.GetComponent<PlayerChange>().Actualize();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject == player) {

            #region Update Player Controls
            player.GetComponent<PlayerState>().StopBounce = !exit.bounce;
            player.GetComponent<PlayerState>().StopWallBounce = !exit.walljump;
            player.GetComponent<PlayerChange>().enabled = exit.change;
            player.GetComponent<PlayerGhost>().enabled = exit.ghost;
            player.GetComponent<PlayerFall>().enabled = exit.fall;
            player.GetComponent<PlayerClimb>().enabled = exit.climb;
            player.GetComponent<PlayerDash>().enabled = exit.dash;
            player.GetComponent<PlayerJump>().enabled = exit.jump;
            #endregion

            player.GetComponent<PlayerBounce>().K = exit.elasticConstant;
            player.GetComponent<PlayerJump>().JumpForce = exit.jumpForce;

            PlayerState.Character = exit.character;
            player.GetComponent<PlayerChange>().Actualize();
        }
    }
}
