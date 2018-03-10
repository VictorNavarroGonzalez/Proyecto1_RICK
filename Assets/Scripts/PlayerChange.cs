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
        if (Input.GetKeyDown("t")) {
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
            playerMovement.SideForce = 150f;
            playerJump.JumpForce = 200f;
        }
        else if (currentSprite.sprite == circle) {
            playerMovement.SideForce = 300f;
            playerJump.JumpForce = 400f;
        }
    }

}
