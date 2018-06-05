using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterGround : MonoBehaviour {

    // Get both kinds of cliffs sprite.
    private Sprite black;
    private Sprite white;

    // Get the renderer of the cliff gameobject.
    new private SpriteRenderer renderer;


    void Awake() {

        // Load the assets from the asset's folder.
        switch (gameObject.tag) {
            case "Platform":
            case "Ground":
                white = Resources.Load<Sprite>("Sprites/Map/white_ground");
                black = Resources.Load<Sprite>("Sprites/Map/black_ground");
                break;

            case "Edge":
                white = Resources.Load<Sprite>("Sprites/Map/white_edge");
                black = Resources.Load<Sprite>("Sprites/Map/black_edge");
                break;

            case "Cliff":
                white = Resources.Load<Sprite>("Sprites/Map/white_cliff");
                black = Resources.Load<Sprite>("Sprites/Map/black_cliff");
                break;

            default:
                Debug.LogError("Ground type not existing." + gameObject.name);
                white = Resources.Load<Sprite>("Sprites/Map/white_ground");
                black = Resources.Load<Sprite>("Sprites/Map/black_ground");
                break;
        }

        // Get the renderer component.
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {

        // According to the player character the sprite will change.
        if (PlayerState.Character == PlayerState.MyCharacter.CIRCLE) {
            renderer.sprite = white;
        }
        else {
            renderer.sprite = black;
        }

    }
}
