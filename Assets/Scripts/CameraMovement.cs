using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public Vector3 offset;

    void Awake() {
        DOTween.Init();
    }

    void FixedUpdate() {

        Tween flying = transform.DOLocalMoveY(player.position.y, 1f, false).SetEase(Ease.OutExpo);

        // Normal Horizontal CameraMovement
        transform.DOMoveX(player.position.x, 0f, false);

        // Camera Movement
        switch (PlayerState.State) {

            case PlayerState.MyState.Jumping:
            case PlayerState.MyState.DoubleJumping:
            case PlayerState.MyState.Bouncing:
                if(flying.IsComplete()) flying.Play();
                break;

            case PlayerState.MyState.Falling:
                transform.DOMoveY(player.position.y, 2f, false);
                break; 

            case PlayerState.MyState.Grounding:
                transform.DOMoveY(player.position.y, 2f, false);
                break;

        }
    }
}