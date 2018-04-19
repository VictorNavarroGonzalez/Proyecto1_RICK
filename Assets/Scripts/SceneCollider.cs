using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCollider : MonoBehaviour {

    public Object next;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player) {
            Debug.Log("Next Scene: " + next.name);
            SceneManager.LoadScene(next.name, LoadSceneMode.Single);
        }
    }

}
