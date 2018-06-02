using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultValues;

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
                GetComponent<PlayerMovement>().SideForce = Square.SideForce;
                GetComponent<PlayerJump>().JumpForce = Square.JumpForce;
                GetComponent<PlayerFall>().FallForce = Square.FallForce;
                GetComponent<PlayerDash>().DashForce = Square.DashForce;

                // Change RICK Scripts
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<CircleCollider2D>().enabled = false;

                GetComponent<Animator>().SetBool("Square", true);
                GetComponent<Animator>().SetBool("Circle", false);
                break;

            case PlayerState.MyCharacter.CIRCLE:  
                // Change RICK Properties
                GetComponent<PlayerMovement>().SideForce = Circle.SideForce;
                GetComponent<PlayerJump>().JumpForce = Circle.JumpForce;
                GetComponent<PlayerDash>().DashForce = Circle.DashForce;

                // Change RICK Scripts
                GetComponent<CircleCollider2D>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = false;

                GetComponent<Animator>().SetBool("Square", false);
                GetComponent<Animator>().SetBool("Circle", true);
                break;
        }
    }

}
