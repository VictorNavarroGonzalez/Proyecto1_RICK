using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerGhost : MonoBehaviour {

    private bool hasGhost = false;
    private PlayerState.MyCharacter _ghostChar;
    public PlayerState.MyCharacter GhostChar {
        get { return _ghostChar;  }
    }

    new private GameObject camera;

    public GameObject prefab;

    private Sprite circle;
    private Sprite square;

    private GameObject ghost;

    void Start() {
        camera = GameObject.Find("Camera");

        circle = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Player/ghost_circle.png", typeof(Sprite));
        square = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Player/ghost_square.png", typeof(Sprite));
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

        camera.GetComponent<CameraBehaviour>().Pause();
        Vector3 offset = new Vector3(0, 0, 2);
        camera.transform.position = ghost.transform.position - offset;

        hasGhost = false;
        Destroy(ghost);
    }

    // Instatiates the ghost prefab to be teleported on in the future.
    public void Create() {
        if(PlayerState.Character == PlayerState.MyCharacter.CIRCLE) {
            ghost = Instantiate(prefab, transform.position, transform.rotation);
            ghost.GetComponent<SpriteRenderer>().sprite = circle;
            _ghostChar = PlayerState.MyCharacter.CIRCLE;
        }
        else if (PlayerState.Character == PlayerState.MyCharacter.SQUARE) {
            ghost = Instantiate(prefab, transform.position, transform.rotation);
            ghost.GetComponent<SpriteRenderer>().sprite = square;
            _ghostChar = PlayerState.MyCharacter.SQUARE;
        }
        
        hasGhost = true;
    }

}
