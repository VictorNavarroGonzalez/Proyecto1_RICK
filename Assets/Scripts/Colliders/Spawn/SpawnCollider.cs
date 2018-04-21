using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnCollider : MonoBehaviour {

    public GameObject spawn;
    private GameObject player;
    new private GameObject camera;

    private bool isActive;

    public bool discrete;
    public bool delayed;
    public float time;

    void Start() {
        player = GameObject.Find("Player");
        camera = GameObject.Find("Camera");
        isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player && isActive) {
            isActive = false;
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

        isActive = true;
    }

    private IEnumerator Teleport() {
        yield return new WaitForSeconds(time);
        float distY = player.GetComponent<PlayerBounce>().DistGround();
        Debug.Log(player.GetComponent<PlayerBounce>().DistGround());
        player.transform.position = spawn.transform.position + new Vector3(0,Mathf.Abs(distY) + 0.6436337f , 0);
        camera.GetComponent<CameraMovement>().Pause();

        Vector3 offset = camera.GetComponent<CameraMovement>().offset;
        camera.transform.position = player.transform.position + offset;

        isActive = true;
    }
}
