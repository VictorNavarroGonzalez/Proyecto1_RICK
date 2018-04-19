using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour {
    
    public Sprite square;
    public Sprite circle;

    public void Change() {
        if (PlayerState.Character == PlayerState.MyCharacter.CIRCLE) {
            PlayerState.Character = PlayerState.MyCharacter.SQUARE;
        }
        else if (PlayerState.Character == PlayerState.MyCharacter.SQUARE) {
            PlayerState.Character = PlayerState.MyCharacter.CIRCLE;
        }

    }

    public void Actualize() {
        switch (PlayerState.Character) {
            case PlayerState.MyCharacter.SQUARE:
                // Change RICK Apparel
                GetComponent<SpriteRenderer>().sprite = square;

                // Change RICK Properties
                GetComponent<PlayerMovement>().SideForce = 50f;
                GetComponent<PlayerJump>().JumpForce = 250f;
                GetComponent<PlayerFall>().FallForce = 2000f;
                GetComponent<PlayerDash>().DashForce = 100f;

                // Change RICK Scripts
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<CircleCollider2D>().enabled = false;
                break;

            case PlayerState.MyCharacter.CIRCLE:
                // Change RICK Apparel
                GetComponent<SpriteRenderer>().sprite = circle;
                
                // Change RICK Properties
                GetComponent<PlayerMovement>().SideForce = 100f;
                GetComponent<PlayerJump>().JumpForce = 500f;
                GetComponent<PlayerBounce>().BounceForce = 100f;
                GetComponent<PlayerDash>().DashForce = 1000f;

                // Change RICK Scripts
                GetComponent<CircleCollider2D>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = false;
                break;
        }
    }

}
