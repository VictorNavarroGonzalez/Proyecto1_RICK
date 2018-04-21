using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCollider : MonoBehaviour {

    public Object next;
    public GameObject player;
    new public Camera camera;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {
            Debug.Log("Next Scene: " + next.name);
            camera.GetComponent<CameraMovement>().Pause();
            SceneManager.LoadScene(next.name, LoadSceneMode.Single);
        }
    }

}
