using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour {

    public GameObject player;
    public GameObject spawn;
    new public Camera camera;

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.gameObject == player) {
            float d = Vector2.Distance(player.transform.position, camera.transform.position);
            camera.transform.position = spawn.transform.position;
            player.transform.position = camera.transform.position - new Vector3(0, d, 0);
        }
    }

}
