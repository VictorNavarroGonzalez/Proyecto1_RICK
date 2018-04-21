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
    private bool startUp;
    private bool goY;
    private bool backY;
    private bool backX;
    private bool goX;
    private bool startRight;
    private Rigidbody2D rb;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        moveX = this.transform.position.x + distX;
        moveY = this.transform.position.y + distY;
        velX = distX / timeX;
        velY = distY / timeY;
        backX = false;
        backY = false;
        goX = true;
        goY = true;
        if (distX > 0) startRight = true;
        else startRight = false;
        if (distY > 0) startUp = true;
        else startUp = false;


    }
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckDirection();
        if (goX) this.rb.velocity = new Vector2(velX, this.rb.velocity.y);
        else if (backX) this.rb.velocity = new Vector2(-velX, this.rb.velocity.y);
        if (goY) this.rb.velocity = new Vector2(this.rb.velocity.x, velY);
        else if (backY) this.rb.velocity = new Vector2(this.rb.velocity.x, -velY);
    }

    void CheckDirection()
    {
        if (startRight)
        {
            if (this.transform.position.x < moveX && !backX) goX = true;
            else goX = false;

            if (this.transform.position.x < moveX - distX) backX = false;
            else if (!goX) backX = true;
        }
        else
        {
            if (this.transform.position.x > moveX && !backX) goX = true;
            else goX = false;

            if (this.transform.position.x > moveX - distX) backX = false;
            else if (!goX) backX = true;
        }

        if(startUp)
        {
            if (this.transform.position.y < moveY && !backY) goY = true;
            else goY = false;

            if (this.transform.position.y < moveY - distY) backY = false;
            else if (!goY) backY = true;
        }
        else
        {
            if (this.transform.position.y > moveY && !backY) goY = true;
            else goY = false;

            if (this.transform.position.y > moveY - distY) backY = false;
            else if (!goY) backY = true;
        }
    }
}
