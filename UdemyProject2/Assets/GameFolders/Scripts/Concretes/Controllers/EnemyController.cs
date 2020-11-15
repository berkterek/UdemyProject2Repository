using System.Collections;
using System.Collections.Generic;
using UdemyProject2.Animations;
using UdemyProject2.Combats;
using UdemyProject2.ExtensionMethods;
using UdemyProject2.Movements;
using UnityEngine;

namespace UdemyProject2.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] AudioClip deadClip;

        Mover _mover;
        CharacterAnimation _characterAnimation;
        Health _health;
        Flip _flip;
        OnReachedEdge _onReachedEdge;
        Damage _damage;

        bool _isOnEdge;
        float _direction;

        public static event System.Action<AudioClip> OnEnemyDead;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _characterAnimation = GetComponent<CharacterAnimation>();
            _health = GetComponent<Health>();
            _flip = GetComponent<Flip>();
            _onReachedEdge = GetComponent<OnReachedEdge>();
            _damage = GetComponent<Damage>();
            _direction = 1f;
        }

        private void OnEnable()
        {
            _health.OnDead += DeadAction;
            _health.OnDead += () => OnEnemyDead.Invoke(deadClip);
        }

        private void FixedUpdate()
        {
            if (_health.IsDead) return;

            _mover.HorizontalMove(_direction);
            _characterAnimation.MoveAnimation(_direction);
        }

        private void LateUpdate()
        {
            if (_health.IsDead) return;

            if (_onReachedEdge.ReachedEdge()) 
            {
                _direction *= -1;
                _flip.FlipCharacter(_direction);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Health health = collision.ObjectHasHealth();

            if (health != null && collision.WasHitLeftOrRightSide())
            {
                health.TakeHit(_damage);
            }
        }

        private void DeadAction()
        {
            StartCoroutine(DeadActionAsync());
        }

        private IEnumerator DeadActionAsync()
        {
            _characterAnimation.DyingAnimation();
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
        }
    }
}

