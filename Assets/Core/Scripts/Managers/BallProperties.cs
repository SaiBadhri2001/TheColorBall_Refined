using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using MoreMountains.Feedbacks;

namespace TheColorBall.Core
{
    public class BallProperties : MonoBehaviour
    {
        //public
        public GameObject playerHolder;
        public GameObject player;
        public CircleCollider2D controllerCollider;
        public PlayerProperties playerProperties;
        public Animator ballAnimator;
        public Transform bulletSpawnTansform;
        public MMF_Player BallDieFeedbacks;
        public static BallProperties instance;
        [HideInInspector]
        public bool _isReadyForDrag = false;
        [HideInInspector]
        public bool spawnBullet = false;
        [HideInInspector]
        public SpriteRenderer PlayerSpriteRenderer;
        //private
        Vector3 dragPos = new Vector3();
        public bool isBallImmune = false;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        void Start()
        {
            PlayerSpriteRenderer = player.GetComponent<SpriteRenderer>();
            _isReadyForDrag = false;
            StaticDatas.isPlayerAlive = true;
            controllerCollider.radius = 0.5f;
            BallStateManager.SetBallState(BallState.Idle);
            playerProperties.startColor = PlayerSpriteRenderer.color;
        }
        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                RaycastHit2D hit2D = GetRayCastTarget(touch);

                GetTouch(touch, hit2D, StaticDatas.isPlayerAlive);
            }
        }
        RaycastHit2D GetRayCastTarget(Touch touch)
        {
            Vector3 touchToWorldpos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchToWorldpos2D = new Vector2(touchToWorldpos.x, touchToWorldpos.y);

            RaycastHit2D hit2D = Physics2D.Raycast(touchToWorldpos2D, Camera.main.transform.forward);
            return hit2D;
        }
        #region GetTouch
        public void GetTouch(Touch touch, RaycastHit2D hit2D, bool isPlayerAlive)
        {
            if (isPlayerAlive)
            {
                if (hit2D == controllerCollider)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (!StaticDatas.isPlayStarted)
                            StaticDatas.isPlayStarted = true;

                        BallStateManager.SetBallState(BallState.OnControl);
                        StartCoroutine(SpawnBullet());
                    }
                }
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (_isReadyForDrag)
                    {
                        dragPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                        playerHolder.transform.position = Vector3.Lerp(playerHolder.transform.position, dragPos, playerProperties.movementBuffer);
                        BallStateManager.SetBallState(BallState.OnDrag);
                    }
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    BallStateManager.SetBallState(BallState.Idle);
                    StopCoroutine(SpawnBullet());
                }
            }
        }
        #endregion
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isBallImmune && other.gameObject.tag == "Obstracles")
            {
                BallStateManager.SetBallState(BallState.OnCollided);
                BallDieFeedbacks.PlayFeedbacks();

                StartCoroutine(WaitForBallDie());
            }
        }
        IEnumerator WaitForBallDie()
        {
            yield return new WaitForSeconds(BallDieFeedbacks.GetFeedbackOfType<MMF_Particles>().BoundParticleSystem.main.duration);
            UIManager.Instance.DisplayRestartUI();

            StaticDatas.totalCrosses = 0;
        }
        IEnumerator SpawnBullet()
        {
            while (spawnBullet && StaticDatas.isPlayerAlive)
            {
                yield return new WaitForSeconds(playerProperties.BulletIntravel);
                Instantiate(playerProperties.bulletPrefab, bulletSpawnTansform.position, Quaternion.identity);
            }
        }
    }
}