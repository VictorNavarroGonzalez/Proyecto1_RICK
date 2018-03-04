using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float sideSpeed = 5.0f;

    private Rigidbody2D rb;

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate () {
        rb.velocity = new Vector2(InputManager.MainHorizontal() * sideSpeed * Time.deltaTime, rb.velocity.y);
    }
}
