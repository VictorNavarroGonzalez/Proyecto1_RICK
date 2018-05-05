using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {

    public Transform player;
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

    }

    void FixedUpdate() {

        #region CAMERA MOVEMENT
        switch (PlayerState.State) {

            case PlayerState.MyState.Jumping:
            case PlayerState.MyState.DoubleJumping:
                #region Jumping Tween
                jumping.Insert(1, transform.DOMoveX(player.position.x, 0f));
                jumping.Insert(1, transform.DOMoveY(player.position.y, 2f));
                jumping.SetUpdate(UpdateType.Fixed, false);
                #endregion
                break;

            case PlayerState.MyState.Bouncing:
                #region Bouncing Tween
                bouncing.Insert(2, transform.DOMoveX(player.position.x, 0f));
                bouncing.Insert(2, transform.DOMoveY(player.position.y, 2f));
                bouncing.SetUpdate(UpdateType.Fixed, false);
                #endregion
                break;

            case PlayerState.MyState.Falling:
                #region Falling Tween
                falling.Insert(3, transform.DOMoveX(player.position.x, 0f));
                falling.Insert(3, transform.DOMoveY(player.position.y, 1f));
                falling.Insert(3, transform.DOShakePosition(1.5f));
                //falling.Insert(3, transform.DOShakeRotation(1.5f, new Vector3(0, 0, 90), 1, 90, true));
                falling.SetUpdate(UpdateType.Fixed, false);
                #endregion
                break; 

            case PlayerState.MyState.Grounding:
                #region Grounding Tween
                grounding.Insert(4, transform.DOMoveX(player.position.x, 0f));
                grounding.Insert(4, transform.DOMoveY(player.position.y, 2f)).SetEase(Ease.OutCubic);
                grounding.Insert(4, transform.DOMoveZ(player.position.z + offset.z, 3f));
                //grounding.Insert(4, transform.DORotate(player.rotation.eulerAngles, 1f));
                grounding.SetUpdate(UpdateType.Fixed, false);
                #endregion
                break;

            default:
                #region Grounding Tween
                grounding.Insert(4, transform.DOMoveX(player.position.x, 0f));
                grounding.Insert(4, transform.DOMoveY(player.position.y, 2f)).SetEase(Ease.OutCubic);
                grounding.SetUpdate(UpdateType.Fixed, false);
                #endregion
                break;

        }
        #endregion

    }

    public void Pause() {
        DOTween.PauseAll();
    }
}