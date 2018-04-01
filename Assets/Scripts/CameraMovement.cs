using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject player;

    public Transform end;
    public Vector3 offset;

    public float duration;

    void Awake() {
        end = player.GetComponent<Transform>();
    }

    void FixedUpdate() {
        // Normal Horizontal CameraMovement
        transform.position = new Vector3(end.position.x, transform.position.y, end.position.z) + offset;

        // Jumping CameraMovement
        switch (PlayerState.State) {
            case PlayerState.MyState.Jumping:
                float d = end.position.y - transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, end.position + offset, d*d * Time.deltaTime);
                break;

            case PlayerState.MyState.DoubleJumping:
                //t = Easing.Elastic.Out(Time.deltaTime);
                //transform.position = Vector3.Lerp(transform.position, end.position + offset, t);
                break;

            case PlayerState.MyState.Bouncing:
                //t += Time.deltaTime;
                //transform.position = Vector3.Lerp(transform.position, end.position + offset, t);
                break;

        }
    }
}
