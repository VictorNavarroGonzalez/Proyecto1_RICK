﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxialPlatform : MonoBehaviour {

    public bool Vertical;
    public bool Horizontal;

    private Rigidbody2D rb;
    private GameObject target;      // Target to react (Player)

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        //Block the platform while not smacking
        rb.freezeRotation = true;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" && PlayerState.Character == PlayerState.MyCharacter.SQUARE)
            // Enable rotation
            if (PlayerState.State == PlayerState.MyState.Falling && Vertical) {
                rb.freezeRotation = false;
                // Block the platform at 90 degrees
                StartCoroutine(StopVertical());     
            }
            else if (PlayerState.State == PlayerState.MyState.Dashing && Horizontal) {
                rb.freezeRotation = false;
                rb.AddForce(InputManager.MainJoystick().normalized * 4400, ForceMode2D.Impulse);
                // Block the platform at 0 or 180 degrees (depending on player position)
                StartCoroutine(StopHorizontal());
            }          
    }

    public IEnumerator StopVertical() {
        yield return new WaitForSeconds(3.6f);
        // Ensure that the platform stops in 90 degrees
        rb.rotation = 90;
        rb.freezeRotation = true;
    }

    public IEnumerator StopHorizontal() {
        yield return new WaitUntil(() => ((rb.rotation <= 180 && rb.rotation >= 90) || (rb.rotation >= 0 && rb.rotation <= 90)) && Mathf.Abs(rb.angularVelocity) < 200 && Mathf.Abs(rb.angularVelocity) > 10);
        // Ensure that the platform stops in 0 or 180 degrees
        if (rb.rotation > 90) rb.rotation = 180;
        else if (rb.rotation < 90) rb.rotation = 0;
        rb.freezeRotation = true;
    }
}