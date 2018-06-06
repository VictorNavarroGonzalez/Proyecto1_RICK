using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCollider : MonoBehaviour {

    public enum Scene { CIRCLE, SQUARE, BOTH };
    public Scene scene;

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

            switch (scene) {
                case Scene.CIRCLE:
                    SceneManager.LoadScene(1, LoadSceneMode.Single);
                    DefaultValues.Levels.Current = DefaultValues.Levels.C01;
                    break;

                case Scene.SQUARE:
                    SceneManager.LoadScene(2, LoadSceneMode.Single);
                    DefaultValues.Levels.Current = DefaultValues.Levels.S01;
                    break;

                case Scene.BOTH:
                    SceneManager.LoadScene(3, LoadSceneMode.Single);
                    DefaultValues.Levels.Current = DefaultValues.Levels.B01;
                    break;
            }
        }
    }

}
