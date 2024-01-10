using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.VFX;

namespace TheColorBall.Core
{
    public class BulletsProperties : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _bulletVfxTime = 1;
        [SerializeField] private PolygonCollider2D _polygonCollider;
        public Color bulletColor;
        public VisualEffect BulletVFX;
        private bool _isBulletHit;
        private SpriteShapeRenderer bulletSpriteRenderer;
        void OnEnable()
        {
            bulletSpriteRenderer = _bullet.GetComponent<SpriteShapeRenderer>();
            //BulletVFX.SetFloat("VFX Timing", _bulletVfxTime);
            _isBulletHit = false;
            SetBulletColor();
        }
        private void SetBulletColor()
        {
            bulletColor = BallProperties.instance.PlayerSpriteRenderer.color;
            bulletSpriteRenderer.color = bulletColor;
        }
        void FixedUpdate()
        {
            if (!_isBulletHit)
                SetBulletVelocity();
            StartCoroutine(DestroyBullet());
        }
        private IEnumerator DestroyBullet()
        {
            if (_isBulletHit || this.gameObject.transform.position.y >= BallProperties.instance.playerProperties.MaxBulletDistance)
            {
                _polygonCollider.enabled = false;
                bulletSpriteRenderer.enabled = false;
                yield return new WaitForSeconds(_bulletVfxTime);
                Destroy(this.gameObject);
            }
        }
        private void SetBulletVelocity()
        {
            //rigidBody Bullet
            //this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, BallProperties.instance.playerProperties.bulletSpeed);

            //Transform Bullet
            this.gameObject.transform.position += new Vector3(0, BallProperties.instance.playerProperties.bulletSpeed, 0);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Obstracles" && other.gameObject != BallProperties.instance.playerHolder)
            {
                /* BulletVFX.SetVector4("MainColor", bulletSpriteRenderer.color);
                BulletVFX.Play(); */
                _isBulletHit = true;
            }
        }
    }
}