using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BetterGround : MonoBehaviour {

    // Get both kinds of cliffs sprite.
    private Sprite black;
    private Sprite white;

    // Get the renderer of the cliff gameobject.
    new private SpriteRenderer renderer;


    void Awake() {

        // Load the assets from the asset's folder.
        switch (gameObject.tag) {
            case "Ground":
                white = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/white_ground.png", typeof(Sprite));
                black = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/black_ground.png", typeof(Sprite));
                break;

            case "Edge":
                white = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/white_edge.png", typeof(Sprite));
                black = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/black_edge.png", typeof(Sprite));
                break;

            case "Cliff":
                white = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/white_cliff.png", typeof(Sprite));
                black = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/black_cliff.png", typeof(Sprite));
                break;

            case "Platform":
                white = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/white_ground.png", typeof(Sprite));
                black = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/black_ground.png", typeof(Sprite));
                break;

            default:
                Debug.LogError("Ground type not existing." + gameObject.name);
                white = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/white_ground.png", typeof(Sprite));
                black = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/black_ground.png", typeof(Sprite));
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
