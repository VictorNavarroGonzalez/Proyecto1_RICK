using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnCollider : MonoBehaviour {

    public GameObject player;
    public GameObject spawn;
    new public Camera camera;

    public bool discrete;
    public bool delayed;
    public float time;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {

            if (!delayed) time = 0f;

            if (discrete) StartCoroutine(Teleport());
            else StartCoroutine(Respawn());

        }
    }

    private IEnumerator Respawn() {
        yield return new WaitForSeconds(time);
        player.transform.position = spawn.transform.position;

        camera.GetComponent<CameraMovement>().Pause();
        Vector3 offset = new Vector3(0, 0, 2);
        camera.transform.position = player.transform.position - offset;
    }

    private IEnumerator Teleport() {
        yield return new WaitForSeconds(time);
        Vector3 pos = (player.transform.position - transform.position);
        player.transform.position = spawn.transform.position + pos;
        camera.GetComponent<CameraMovement>().Pause();

        Vector3 offset = camera.GetComponent<CameraMovement>().offset;
        camera.transform.position = player.transform.position - offset;
    }
}
