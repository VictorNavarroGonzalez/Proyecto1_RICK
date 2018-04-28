using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmackPlatform : MonoBehaviour {
    
    // Internal Platform Behavior
    public int initialState;
    private int currentState;

    public float offset;
    private Vector2[] states = new Vector2[4];

    // External Platforms Behaviors
    public GameObject[] platforms;

    // Variables needed to detect the collision
	private GameObject player;
    private LayerMask mask;
    private Vector2 size;
    private float depth;  


    void Start() {
        // Declare variable to detect the collision.
        player = GameObject.Find("Player");
        mask = LayerMask.GetMask("Player");

        size = GetComponent<BoxCollider2D>().size;
        depth = 0.05f;

        // Define all the possible states. Is important to place the
        // collider in the state 0 by default.
        Vector2 current = transform.position; // For more legible code after the declaration.
        states[0] = current;
        states[1] = new Vector2(current.x, current.y + offset);
        states[2] = new Vector2(current.x, current.y + offset * 2);
        states[3] = new Vector2(current.x, current.y + offset * 3);

        // Set the current position to the specified initial state.
        transform.position = states[initialState];
        currentState = initialState;
    }

    void FixedUpdate() {
        // Overlap Box Collision
        Vector2 boxCenter = (Vector2) transform.position + Vector2.up * (size.y + depth) * 0.5f;
        Vector2 boxSize = new Vector2(size.x * 0.8f, depth);
        bool collision = Physics2D.OverlapBox(boxCenter, boxSize, 0, mask) != null;

        // Collision Reaction
        if (collision && PlayerState.State == PlayerState.MyState.Falling) {
            Decrease();
        }
    }

    public void Increase() {
        if(currentState < states.Length - 1) {
            currentState++;
            transform.position = states[currentState];
        }
    }

    public void Decrease() {
        // If the current state if above zero, the platform will decrease
        // and the other will increase.
        if (currentState > 0) {
            currentState--;
            transform.position = states[currentState];

            for (int i = 0; i < platforms.Length - 1; i++) {
                platforms[i].GetComponent<SmackPlatform>().Increase();
            }
        }    
    }




}
