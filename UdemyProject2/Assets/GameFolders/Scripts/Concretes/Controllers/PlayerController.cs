using System;
using System.Collections;
using System.Collections.Generic;
using UdemyProject2.Abstracts.Inputs;
using UdemyProject2.Animations;
using UdemyProject2.Combats;
using UdemyProject2.ExtensionMethods;
using UdemyProject2.Inputs;
using UdemyProject2.Movements;
using UdemyProject2.Uis;
using UnityEngine;

namespace UdemyProject2.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] AudioClip deadClip;

        float _horizontal;
        float _vertical;
        bool _isJump;

        IPlayerInput _input;
        Mover _mover;
        Jump _jump;
        CharacterAnimation _characterAnimation;
        Flip _flip;
        OnGround _onGround;
        Climbing _climbing;
        Health _health;
        Damage _damage;
        AudioSource _audioSource;

        public static event Action<AudioClip> OnPlayerDead;

        private void Awake()
        {
            _characterAnimation = GetComponent<CharacterAnimation>();
            _mover = GetComponent<Mover>();
            _jump = GetComponent<Jump>();
            _flip = GetComponent<Flip>();
            _onGround = GetComponent<OnGround>();
            _climbing = GetComponent<Climbing>();
            _health = GetComponent<Health>();
            _damage = GetComponent<Damage>();
            _audioSource = GetComponent<AudioSource>();
            _input = new PcInput(); //instance
        }

        private void OnEnable()
        {
            GameCanvas gameCanvas = FindObjectOfType<GameCanvas>();

            if (gameCanvas != null)
            {
                _health.OnDead += gameCanvas.ShowGameOverPanel;
                DisplayHealth displayHealth = gameCanvas.GetComponentInChildren<DisplayHealth>();
                _health.OnHealthChanged += displayHealth.WriteHealth;
            }

            _health.OnDead += () => OnPlayerDead.Invoke(deadClip);
            _health.OnHealthChanged += PlayOnHit;
        }

        private void Update()
        {
            if (_health.IsDead) return;

            _horizontal = _input.Horizontal;
            _vertical = _input.Vertical;

            if (_input.IsJumpButtonDown && _onGround.IsOnGround && !_climbing.IsClimbing)
            {
                _isJump = true;
            }

            _characterAnimation.MoveAnimation(_horizontal);
            _characterAnimation.JumpAnimation(!_onGround.IsOnGround && _jump.IsJump && !_climbing.IsClimbing);
            _characterAnimation.ClimbingAnimation(_climbing.IsClimbing);
        }

        private void FixedUpdate()
        {
            _climbing.ClimbAction(_vertical);
            _mover.HorizontalMove(_horizontal);
            _flip.FlipCharacter(_horizontal);

            if (_isJump)
            {
                _jump.JumpAction();
                _isJump = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Health health = collision.ObjectHasHealth();

            if (health != null && collision.WasHitTopSide())
            {
                health.TakeHit(_damage);
                _jump.JumpAction();
            }
        }

        private void PlayOnHit(int currentHealth, int maxHealth)
        {
            if (currentHealth == maxHealth) return;

            _audioSource.Play();
        }
    }
}

