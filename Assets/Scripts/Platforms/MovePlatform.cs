using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour {

    public Rigidbody2D target;
    public Transform Area;
    public bool inverted;
    private Rigidbody2D rb;

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }
	

	void FixedUpdate () {


        // Read the target velocity and move the platform with the same vel
        // Bool inverted decide if platform moves like the target or oposite direction
        if (!inverted) rb.velocity = new Vector2(target.velocity.x, 0);
        else rb.velocity = new Vector2(target.velocity.x * -1, 0);

    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject == Area) {
            if (!inverted) rb.velocity = new Vector2(target.velocity.x, 0);
            else rb.velocity = new Vector2(target.velocity.x * -1, 0);
            Debug.Log("entrando");
        }
    }
}
