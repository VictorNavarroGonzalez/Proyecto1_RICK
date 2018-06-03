using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCollider : MonoBehaviour {

    public Object nextScene;
    private GameObject player;
    new private GameObject camera;

    // Get the player and the camera
    void Start() {
        player = GameObject.Find("Player");
        camera = GameObject.Find("Camera");
    }

    // If the player collides with the collider it will be loaded to
    // the next scene, moreover, the camera will be paused for ones seconds
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            camera.GetComponent<CameraBehaviour>().Pause();
            SceneManager.LoadScene(nextScene.name, LoadSceneMode.Single);
        }
    }

}
