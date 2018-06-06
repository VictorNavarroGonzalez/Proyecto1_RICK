using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCollider : MonoBehaviour {

    public AudioSource song;
    private GameObject player;
    public float fadeTime;



    void Start() {
        player = GameObject.Find("Player");
        fadeTime = 5;
    }

    // If the player collides with the collider it will be loaded to
    // the next scene, moreover, the camera will be paused for ones seconds
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            StartCoroutine(FadeOut(song, fadeTime));
            //Debug.Log("Debería bajarse");
        }
    }



    public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime) {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            Debug.Log("Debería bajarse");
            yield return null;
        }

        audioSource.Stop();
        //audioSource.volume = startVolume;
    }
}
