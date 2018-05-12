using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCollider : MonoBehaviour {

    public Object next;
    public GameObject player;
    new public Camera camera;

    // If the player collides with the collider it will be loaded to
    // the next scene, moreover, the camera will be paused for ones seconds
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {
            Debug.Log("Next Scene: " + next.name);
            camera.GetComponent<CameraBehaviour>().Pause();
            SceneManager.LoadScene(next.name, LoadSceneMode.Single);
        }
    }

}
