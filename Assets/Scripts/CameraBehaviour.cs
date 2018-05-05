using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public float speed;
    public Vector3 offset;

    private GameObject player;
    private Vector3 target;
    
    void Start() {
        // Get player transform component
        player = GameObject.Find("Player");
    }

    void FixedUpdate() {
        float step = speed * Time.deltaTime;
        target = player.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, target + offset, step);
    }
}
