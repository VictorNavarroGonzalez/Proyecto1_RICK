using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnCollider : MonoBehaviour {

    public GameObject spawn;
    private GameObject player;
    new private GameObject camera;

    private bool isActive; // To avoid multiple teleport at the same time.

    public bool discrete;
    public bool delayed;
    public float time;

    void Start() {
        player = GameObject.Find("Player");
        camera = GameObject.Find("Camera");
        isActive = true;
    }

    // When the player collides with the collider, her and the camera will
    // be teleported according to the specified setting in the inspector.
    // If the bool delayed is false, the time will be setted to 0.
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player && isActive) {
            isActive = false;
            if (!delayed) time = 0f;

            if (discrete) StartCoroutine(Teleport());
            else StartCoroutine(Respawn());

        }
    }


    // According to the specified time the player and the camera
    // will be teleported in a 'no discrete' way to the SpawnPoint gameobject.
    private IEnumerator Respawn() {
        yield return new WaitForSeconds(time);
        player.transform.position = spawn.transform.position;

        camera.GetComponent<CameraBehaviour>().Pause();
        Vector3 offset = new Vector3(0, 0, 2);
        camera.transform.position = player.transform.position - offset;

        isActive = true;
    }


    // According to the specified time the player and the camera
    // will be teleported in a 'discrete' way to the SpawnPoint gameobject.
    private IEnumerator Teleport() {
        yield return new WaitForSeconds(time);
        float distY = player.GetComponent<PlayerBounce>().DistGround();
        player.transform.position = spawn.transform.position + new Vector3(0,Mathf.Abs(distY) + 0.6436337f , 0);
        camera.GetComponent<CameraBehaviour>().Pause();

        Vector3 offset = camera.GetComponent<CameraBehaviour>().offset;
        camera.transform.position = player.transform.position + offset;

        isActive = true;
    }
}
