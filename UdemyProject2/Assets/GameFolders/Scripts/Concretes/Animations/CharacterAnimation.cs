using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdemyProject2.Animations
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void MoveAnimation(float horizontal)
        {
            float mathfValue = Mathf.Abs(horizontal);

            if (_animator.GetFloat("moveSpeed") == mathfValue) return;

            _animator.SetFloat("moveSpeed", mathfValue);
        }

        public void DyingAnimation()
        {
            _animator.SetTrigger("dying");
        }
        
        public void JumpAnimation(bool isJump)
        {
            if (isJump == _animator.GetBool("isJump")) return;

            _animator.SetBool("isJump", isJump);
        }

        public void ClimbingAnimation(bool isClimbing)
        {
            if (_animator.GetBool("isClimb") == isClimbing) return;

            _animator.SetBool("isClimb", isClimbing);
        }
    }
}

