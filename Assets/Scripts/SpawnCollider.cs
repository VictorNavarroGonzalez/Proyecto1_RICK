using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnCollider : MonoBehaviour {

    public GameObject player;
    public GameObject spawn;
    new public Camera camera;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject == player) {
            //Vector3 pos = (player.transform.position - transform.position);
            //player.transform.position = spawn.transform.position + pos;

            Vector3 offset = new Vector3(0, 0, 2);
            player.transform.position = spawn.transform.position;

            camera.GetComponent<CameraMovement>().Pause();
            camera.transform.position = player.transform.position - offset;
            //camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
        }
    }

}
