using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxialPlatform : MonoBehaviour {

    private Rigidbody2D rb;
    private GameObject target;      //Target to react (Player)

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        rb.freezeRotation = true;           //Block the platform while not smacking
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" && PlayerState.State == PlayerState.MyState.Falling) {
            rb.freezeRotation = false;          //Enable rotation
            StartCoroutine(Active());           //Disable rotation when platform stops
        }
    }

    public IEnumerator Active() {
        yield return new WaitForSeconds(2f);
        while(rb.rotation == 90) rb.freezeRotation = true;          //ensure that the platform stops in 90 degrees
    }
}
