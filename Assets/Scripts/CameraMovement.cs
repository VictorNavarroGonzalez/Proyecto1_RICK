using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject player;

    public Transform end;
    public Vector3 offset;

    public float duration;
    private float t;

    void Awake() {
        end = player.GetComponent<Transform>();
    }

    void FixedUpdate() {
        // Normal Horizontal CameraMovement
        transform.position = new Vector3(end.position.x, transform.position.y, end.position.z) + offset;

        // Jumping CameraMovement
        if (PlayerState.State == PlayerState.MyState.Jumping) {
            t = Easing.Elastic.Out(Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, end.position + offset, t);
        }
        else {
            t = Easing.Exponential.Out(Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, end.position + offset, t);
        }
    }
}
