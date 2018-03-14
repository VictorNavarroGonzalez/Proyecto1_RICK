using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour {

    private SpriteRenderer currentSprite;
    public Sprite square;
    public Sprite circle;

    private PlayerMovement playerMovement;
    private PlayerJump playerJump;

    void Awake() {
        currentSprite = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();

        ActualizePlayer();
    }

    void Update() {
        if (InputManager.ButtonY()) {
            if (currentSprite.sprite == square) {
                currentSprite.sprite = circle;
            }
            else if (currentSprite.sprite == circle) {
                currentSprite.sprite = square;
            }

            ActualizePlayer();
        }    
    }

    void ActualizePlayer() {
        if (currentSprite.sprite == square) {
            playerMovement.SideForce = 50f;
            playerJump.JumpForce = 200f;

            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = false;
        }
        else if (currentSprite.sprite == circle) {
            playerMovement.SideForce = 100f;
            playerJump.JumpForce = 500f;

            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
