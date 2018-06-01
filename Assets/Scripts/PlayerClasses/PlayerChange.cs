using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour {
    

    // This function changes the player character to the other one.
    public void Change() {
        if (PlayerState.Character == PlayerState.MyCharacter.CIRCLE) {
            PlayerState.Character = PlayerState.MyCharacter.SQUARE;
        }
        else if (PlayerState.Character == PlayerState.MyCharacter.SQUARE) {
            PlayerState.Character = PlayerState.MyCharacter.CIRCLE;
        }

    }

    // This functions actualizes the player properties and habilities
    // according to her actual character (Square or Circle)
    public void Actualize() {
        switch (PlayerState.Character) {
            case PlayerState.MyCharacter.SQUARE:
                // Change RICK Properties
                GetComponent<PlayerMovement>().SideForce = 50f;
                GetComponent<PlayerJump>().JumpForce = 450f;
                GetComponent<PlayerFall>().FallForce = 2000f;
                GetComponent<PlayerDash>().DashForce = 100f;

                // Change RICK Scripts
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<CircleCollider2D>().enabled = false;

                GetComponent<Animator>().SetBool("Square", true);
                GetComponent<Animator>().SetBool("Circle", false);
                break;

            case PlayerState.MyCharacter.CIRCLE:  
                // Change RICK Properties
                GetComponent<PlayerMovement>().SideForce = 100f;
                GetComponent<PlayerJump>().JumpForce = 500f;
                GetComponent<PlayerDash>().DashForce = 100f;

                // Change RICK Scripts
                GetComponent<CircleCollider2D>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = false;

                GetComponent<Animator>().SetBool("Square", false);
                GetComponent<Animator>().SetBool("Circle", true);
                break;
        }
    }

}
