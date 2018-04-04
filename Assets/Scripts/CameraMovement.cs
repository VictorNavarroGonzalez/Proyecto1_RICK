<<<<<<< Updated upstream
﻿using System.Collections;
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
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform end;
    public Vector3 offset;


    void FixedUpdate() {
        // Normal Horizontal CameraMovement
        transform.position = new Vector3(end.position.x, transform.position.y, end.position.z) + offset;

        // Camera Movement
>>>>>>> Stashed changes
        switch (PlayerState.State) {
            case PlayerState.MyState.Jumping:
            case PlayerState.MyState.DoubleJumping:
            case PlayerState.MyState.Bouncing:
                float d = Mathf.Abs(end.position.y - transform.position.y);
                transform.position = Vector3.MoveTowards(transform.position, end.position + offset, d*d * Time.deltaTime);
                break;

            case PlayerState.MyState.Falling:
                d = Mathf.Abs(end.position.y - transform.position.y);
                transform.position = Vector3.MoveTowards(transform.position, end.position + offset, d*d * Time.deltaTime);
                break;

<<<<<<< Updated upstream
        }
    }
}
=======
            //case PlayerState.MyState.Smashing:
            //    d = Mathf.Abs(end.position.y - transform.position.y);
            //    transform.position = Vector3.MoveTowards(transform.position, end.position + offset, d*d*d * Time.deltaTime);
            //    break;
        }
    }
}
>>>>>>> Stashed changes
