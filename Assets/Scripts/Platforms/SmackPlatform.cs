using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmackPlatform : MonoBehaviour {
    
    // Internal Platform Behavior
    public int initialState;
    public int totalStates;
    private int currentState;
    private bool active;

    public float offset;
    private List<Vector2> states = new List<Vector2>();

    // External Platforms Behaviors
    public GameObject[] platforms;

    // Variables needed to detect the collision
	private GameObject player;
    private LayerMask mask;
    private Vector2 size;
    private float depth;

    private Vector2 boxCenter;
    private Vector2 boxSize;


    void Start() {
        // Declare variable to detect the collision.
        player = GameObject.Find("Player");
        mask = LayerMask.GetMask("Player");

        size = GetComponent<Transform>().localScale;
        
        // Adjust size to the local scale.
        size.x *= GetComponent<BoxCollider2D>().size.x;
        size.y *= GetComponent<BoxCollider2D>().size.y / 2;

        depth = 0.05f;
        active = false; // To avoid multiple state changes at the same time.

        // Define all the possible states. Is important to place the
        // collider in the state 0 by default.
        Vector2 current = transform.position; // For more legible code after the declaration.
        for(int i = 0; i <= totalStates; i++) {
            states.Add(new Vector2(current.x, current.y + offset * i));
        }

        // Set the current position to the specified initial state.
        transform.position = states[initialState];
        currentState = initialState;
    }

    void FixedUpdate() {
        // Overlap Box Collision
        boxCenter = (Vector2) transform.position + Vector2.up * size.y;
        boxSize = new Vector2(size.x * 0.9f, depth);
        
        bool collision = Physics2D.OverlapBox(boxCenter, boxSize, 0, mask) != null;

        // Collision Reaction
        if (collision && PlayerState.State == PlayerState.MyState.Falling) {
            if(!active) StartCoroutine(Decrease());
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(boxCenter, boxSize);
    }

    public void Increase() {
        if(currentState < totalStates) {
            currentState++;
            transform.position = states[currentState];
        }
    }

    public IEnumerator Decrease() {
        // If the current state if above zero, the platform 
        // will decrease and the other will increase.
        if (currentState > 0) {
            active = true;

            currentState--;
            transform.position = states[currentState];

            foreach (GameObject p in platforms) { 
                p.GetComponent<SmackPlatform>().Increase();
            }
        }
        
        yield return new WaitForSeconds(0.5f);
        active = false;
    }




}
