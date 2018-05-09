using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour {

    private bool active;
    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        active = false;
    }

    public void Climb() {
        // Move up
        if (InputManager.MainVertical() < 0.0f) {
            if (!active) {
                StartCoroutine(Top());          //Check if player is at the top of the wall
            }
            GetComponent<PlayerMovement>().MoveUp();
        }
        //Drag Force to reduce the fall and improve the feeling
        else if (InputManager.MainVertical() == 0.0f) {
            GetComponent<PlayerMovement>().StopY();         
        }
        //Move down
        else if (InputManager.MainVertical() > 0.0f) {
            GetComponent<PlayerMovement>().MoveDown();
        }
     
    }


    public IEnumerator Top() {
        // Desable coroutine to prevent overlap
        active = true;
        // Wait until the player reach the top
        yield return new WaitUntil(() => (!GetComponent<PlayerGround>().LeftHit && !GetComponent<PlayerGround>().RightHit));   
        // Impulse to reach the top 
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * 300, ForceMode2D.Force);       
        active = false;
    }
}
