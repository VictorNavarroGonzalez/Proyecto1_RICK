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

    public bool CheckGhost() {
        if (hasGhost) {
            if (ghost.GetComponent<GhostCollider>().Overlaped)
                return false;

            return true;
        }

        return false;
    }

    
    public void Teleport() {
        transform.position = ghost.transform.position;

        camera.GetComponent<CameraMovement>().Pause();
        Vector3 offset = new Vector3(0, 0, 2);
        camera.transform.position = ghost.transform.position - offset;

        hasGhost = false;
        Destroy(ghost);
    }

    // Instatiates the ghost prefab to be teleported on
    public void Create() {
        ghost = Instantiate(prefab, transform.position, transform.rotation);
        hasGhost = true;
    }

}
