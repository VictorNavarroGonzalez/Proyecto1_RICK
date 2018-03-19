using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Rigidbody2D rb;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;


    void FixedUpdate () {

        if (rb.velocity.y > 0.1) {
            // Delayed CameraFollow on VerticalAxis
            // Save current position and increase the current offset by the difference that the player.y has changed
            // When that player.y(offset) reaches a limit it reduces to 0 before player.velocity.y = 0

        }
        else {
            // Normal CameraFollow on HorizontalAxis
            offset = new Vector3(0, 0, -10);
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
            transform.position = smoothedPos;
        }
        
	}
}
