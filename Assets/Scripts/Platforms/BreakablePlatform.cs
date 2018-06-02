using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour {

    // Adjustment Variables
    public float minimunVelocity;

    // Dynamic list of all the pieces inside the plaftorm.
    private List<GameObject> pieces = new List<GameObject>();

    // Boolean needed to stop accesing to an empty list in the update method.
    private bool broke = false;

    void Start() {

        // Get all the pieces into a List to modify them later.
        foreach (Transform child in transform) {
            pieces.Add(child.gameObject);
        }

    }

    // Set the current animator according to RICK's state.
    void Update() {

        if(!broke) {
            foreach (GameObject p in pieces) {

                Animator animator = p.GetComponent<Animator>();

                // According to the player character the sprite will change.
                if (PlayerState.Character == PlayerState.MyCharacter.CIRCLE) {
                    animator.SetBool("Circle", true);
                    animator.SetBool("Square", false);
                }
                else {
                    animator.SetBool("Circle", false);
                    animator.SetBool("Square", true);
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        // Check if the player collides with the platform.
        if(collision.gameObject.tag == "Player") {

            // Get the player rigidbody to make the check easier to read.
            Rigidbody2D player = collision.gameObject.GetComponent<Rigidbody2D>();

            // Compare the player velocity and
            if(player.velocity.y > minimunVelocity || player.velocity.y < -minimunVelocity) {

                // Make all the components dynamic to get pushed by the player
                // and destroy them after an specified time.
                foreach (GameObject p in pieces) {
                    p.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    p.GetComponent<Rigidbody2D>().mass = 1f;
                    StartCoroutine(DestroyPieces());
                }
            }
        }

    }

    // This coroutine destroys all the pieces and plays the breaking animation.
    private IEnumerator DestroyPieces() {

        foreach (GameObject p in pieces) {
            Destroy(p, 1f);

            Animator animator = p.GetComponent<Animator>();
            animator.SetBool("Broke", true);

            if (PlayerState.Character == PlayerState.MyCharacter.CIRCLE) {
                animator.SetBool("Circle", true);
                animator.SetBool("Square", false);
            }
            else {
                animator.SetBool("Circle", false);
                animator.SetBool("Square", true);
            }
        }

        // Needed to stop accesing an empty list on the update method.
        broke = true;

        yield return null;
    }
}
