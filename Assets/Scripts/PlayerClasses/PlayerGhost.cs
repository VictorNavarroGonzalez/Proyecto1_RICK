using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour {

    private bool hasGhost = false;
    new private GameObject camera;
    public GameObject prefab;
    private GameObject ghost;

    void Start() {
        camera = GameObject.Find("Camera");
    }

    // Kills the ghost if is not visible in camera.
    void FixedUpdate() {

        // Avoid accesing at an uninstantiated object.
        if (ghost != null) {

            // Check if the ghost renderer is visible on camera.
            SpriteRenderer renderer = ghost.GetComponent<SpriteRenderer>();
            if (!renderer.isVisible) {
                hasGhost = false;
                Destroy(ghost);
            }

        }
        
    }

    // Returns true if the player has a ghost to be telported. 
    public bool CheckGhost() {
        return hasGhost;
    }

    // Returns true if the player can teleport to the ghost, i.e,
    // the ghost is not being overlaped by a platform.
    public bool CheckTeleport() {
        return !ghost.GetComponent<GhostCollider>().Overlaped;
    }

    // The player and the camera are teleported to the ghost.
    // The ghost is destroyed after the teleport.
    // This function doesn't check if the teleport can be done.
    public void Teleport() {
        transform.position = ghost.transform.position;

        camera.GetComponent<CameraMovement>().Pause();
        Vector3 offset = new Vector3(0, 0, 2);
        camera.transform.position = ghost.transform.position - offset;

        hasGhost = false;
        Destroy(ghost);
    }

    // Instatiates the ghost prefab to be teleported on in the future.
    public void Create() {
        ghost = Instantiate(prefab, transform.position, transform.rotation);
        hasGhost = true;
    }

}
