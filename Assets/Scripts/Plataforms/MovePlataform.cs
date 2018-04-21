using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovePlataform : MonoBehaviour {

    public float distX;
    public float timeX;
    public float distY;
    public float timeY;

    private float moveX;
    private float moveY;
    private float velX;
    private float velY;
    private bool up;
    private bool down;
    private bool left;
    private bool right;
    private Rigidbody2D rb;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        moveX = this.transform.position.x + distX;
        moveY = this.transform.position.y + distY;
        velX = distX / timeX;
        velY = distY / timeY;
        left = false;
        right = true;
        up = true;
        down = false;
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckDirection();
        if (right) this.rb.velocity = new Vector2(velX, this.rb.velocity.y);
        else if (left) this.rb.velocity = new Vector2(-velX, this.rb.velocity.y);
        if (up) this.rb.velocity = new Vector2(this.rb.velocity.x, velY);
        else if (down) this.rb.velocity = new Vector2(this.rb.velocity.x, -velY);
    }

    void CheckDirection()
    {
        if (this.transform.position.x < moveX && !left) right = true;
        else right = false;

        if (this.transform.position.x < moveX - distX) left = false;
        else if (!right) left = true;

        if (this.transform.position.y < moveY && !down) up = true;
        else up = false;

        if (this.transform.position.y < moveY - distY) down = false;
        else if (!up) down = true;
    }
}
