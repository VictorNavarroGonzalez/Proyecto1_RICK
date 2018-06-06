﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraBehaviour : MonoBehaviour {

    private GameObject player;
    private Vector3 target;
    public Vector3 offset;

    Sequence grounding;
    Sequence jumping;
    Sequence bouncing;
    Sequence falling;

    void Awake() {

        DOTween.Init();

        grounding = DOTween.Sequence();
        jumping = DOTween.Sequence();
        bouncing = DOTween.Sequence();
        falling = DOTween.Sequence();

        GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;

    }

    void Start() {
        player = GameObject.Find("Player");
    }

    void FixedUpdate() {

        target = player.GetComponent<Transform>().position;

        #region CAMERA MOVEMENT
        switch (PlayerState.State) {

            case PlayerState.MyState.Jumping:
            case PlayerState.MyState.DoubleJumping:
                #region Jumping Tween
                jumping.Insert(1, transform.DOMoveX(target.x, 0f));
                jumping.Insert(1, transform.DOMoveY(target.y, 2f));
                jumping.SetUpdate(UpdateType.Fixed, false);
                #endregion
                break;

            case PlayerState.MyState.Bouncing:
                #region Bouncing Tween
                bouncing.Insert(2, transform.DOMoveX(target.x, 0f));
                bouncing.Insert(2, transform.DOMoveY(target.y, 2f));
                bouncing.SetUpdate(UpdateType.Fixed, false);
                #endregion
                break;

            case PlayerState.MyState.Falling:
                StartCoroutine(SmackingTween());
                break;

            case PlayerState.MyState.Grounding:
                #region Grounding Tween
                grounding.Insert(4, transform.DOMoveX(target.x, 0f));
                grounding.Insert(4, transform.DOMoveY(target.y, 2f)).SetEase(Ease.OutCubic);
                grounding.Insert(4, transform.DOMoveZ(target.z + offset.z, 3f));
                //grounding.Insert(4, transform.DORotate(player.rotation.eulerAngles, 1f));
                grounding.SetUpdate(UpdateType.Fixed, false);
                #endregion
                break;

            default:
                #region Grounding Tween
                grounding.Insert(4, transform.DOMoveX(target.x, 0f));
                grounding.Insert(4, transform.DOMoveY(target.y, 2f)).SetEase(Ease.OutCubic);
                grounding.SetUpdate(UpdateType.Fixed, false);
                #endregion
                break;

        }
        #endregion

        if (PlayerState.Character == PlayerState.MyCharacter.CIRCLE)
            GetComponent<Camera>().backgroundColor = Color.HSVToRGB(0f, 0f, 0.9f);
        else
            GetComponent<Camera>().backgroundColor = Color.HSVToRGB(0.54f, 0.24f, 0.23f);
    }

    public IEnumerator SmackingTween() {
        yield return new WaitUntil(() => player.GetComponent<PlayerBounce>().DistGround() < 2f);

        #region Falling Tween
        falling.Insert(3, transform.DOMoveX(target.x, 0f));
        falling.Insert(3, transform.DOMoveY(target.y, 1f));
        falling.Insert(3, transform.DOShakePosition(0.35f));
        //falling.Insert(3, transform.DOShakeRotation(1.5f, new Vector3(0, 0, 90), 1, 90, true));
        falling.SetUpdate(UpdateType.Fixed, false);
        #endregion
    }

    public void Pause() {
        DOTween.PauseAll();
    }
}
