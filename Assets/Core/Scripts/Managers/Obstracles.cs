using UnityEngine;
using MoreMountains.Feedbacks;
using System;

namespace TheColorBall.Core
{
    public class Obstracles : MonoBehaviour
    {
        public ObstracleType obstracleType;
        public float _vfxWaitTime = 1;
        [SerializeField] int _health;
        [SerializeField] MMF_Player _obstracleDestroyFeedBack;
        [SerializeField] ObstracleDestroyParticleSystem _obstracleDestroyParticleSystem;
        SpriteRenderer _obstracleSpriteRenderer;
        private void Start()
        {
            _obstracleSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            _health = ObstracleManager.Instance.ObstracleHealthSetter(obstracleType);
        }
        bool doOnce = false;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Bullet" &&
                            BallProperties.instance.PlayerSpriteRenderer.color ==
                                        _obstracleSpriteRenderer.color)
            {
                _health -= GameManager.instance.gameProperties.IntialBulletDamage;

                ObstracleManager.Instance.ObstracleFeedback.GetFeedbackOfType<MMF_SquashAndStretch>().SquashAndStretchTarget = this.transform.parent;
                ObstracleManager.Instance.ObstracleFeedback.PlayFeedbacks();
            }
        }
        private void Update()
        {
            if (_health == 0)
            {
                if (!_obstracleDestroyFeedBack.IsPlaying)
                {
                    _obstracleDestroyParticleSystem.SetColor(GetComponent<SpriteRenderer>().color);

                    _obstracleDestroyFeedBack.PlayFeedbacks();
                    Destroy(this.gameObject);
                }
            }
        }
    }
    [Serializable]
    public class ObstracleDestroyParticleSystem
    {
        public ParticleSystem PrimaryParticleSystem;
        public ParticleSystem SecondaryParticleSystem;
        public void SetColor(Color color)
        {
            if (PrimaryParticleSystem)
            {
                ParticleSystem.MainModule _primaryParticleSystem = PrimaryParticleSystem.main;
                _primaryParticleSystem.startColor = color;
            }
            if (SecondaryParticleSystem)
            {
                ParticleSystem.MainModule _secondaryParticleSystem = SecondaryParticleSystem.main;
                _secondaryParticleSystem.startColor = color;
            }
        }
        public void PlayParticles()
        {
            if (PrimaryParticleSystem)
            {
                PrimaryParticleSystem.Play();
            }
            if (SecondaryParticleSystem)
            {
                SecondaryParticleSystem.Play();
            }
        }
    }
}