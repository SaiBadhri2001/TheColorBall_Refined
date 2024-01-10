using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheColorBall.Core
{
    public class BallStateManager
    {
        public static BallState currentState = BallState.Idle;
        public static void SetBallState(BallState ballState)
        {
            switch (ballState)
            {
                case BallState.Idle:
                    {
                        currentState = BallState.Idle;
                        BallProperties.instance.ballAnimator.Play("BallTouchReleased");
                        BallProperties.instance._isReadyForDrag = false;
                        BallProperties.instance.controllerCollider.radius = 0.5f;
                        BallProperties.instance.spawnBullet = false;
                        break;
                    }
                case BallState.OnControl:
                    {
                        currentState = BallState.OnControl;
                        BallProperties.instance.ballAnimator.Play("BallTouchStarted");
                        BallProperties.instance._isReadyForDrag = true;
                        BallProperties.instance.controllerCollider.radius = 0.3f;
                        BallProperties.instance.spawnBullet = true;
                        break;
                    }
                case BallState.OnCollided:
                    {
                        StaticDatas.isPlayerAlive = false;
                        break;
                    }
            }
        }
    }
    public enum BallState
    {
        OnControl,
        Idle,
        OnCollided,
        OnCollectedDiamond,
        OnDrag
    }
}