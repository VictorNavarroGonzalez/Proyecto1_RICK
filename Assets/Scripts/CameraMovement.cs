using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public new Transform camera;

    public AnimationCurve curve;

    public Rigidbody2D rb;

    void Awake() {
        transform.position = player.position;
    }

    void FixedUpdate() {
        transform.DOLocalMoveX(player.position.x, 0f);
        


        if (rb.velocity.y < 0f)
        {
            transform.DOLocalMoveY(player.position.y, 1f).SetEase(curve);
            //StartCoroutine(CameraJump());
            
        }
    }

    IEnumerator CameraJump()
    {
        
        
        yield return null;
    }

}
