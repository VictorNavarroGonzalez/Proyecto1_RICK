using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BetterGround : MonoBehaviour {

    // Get both kinds of ground sprite.
    private Sprite black;
    private Sprite white;

    // Get the renderer of the ground gameobject.
    new private SpriteRenderer renderer;

    
	void Awake () {
        // Load the assets from the asset's folder.
        white = (Sprite) AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/white_ground.png", typeof(Sprite));
        black = (Sprite) AssetDatabase.LoadAssetAtPath("Assets/Sprites/Map/black_ground.png", typeof(Sprite));

        // Get the renderer component.
        renderer = GetComponent<SpriteRenderer>();
    }
	
	void Update () {

        // According to the player character the sprite will change.
        if (PlayerState.Character == PlayerState.MyCharacter.CIRCLE) {
            renderer.sprite = white;
        }
        else {
            renderer.sprite = black;
        }
            
    }
}
