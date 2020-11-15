using System.Collections;
using System.Collections.Generic;
using UdemyProject2.Abstracts.Inputs;
using UnityEngine;

namespace UdemyProject2.Inputs
{
    public class MobileInput:IPlayerInput
    {
        public float Horizontal => Input.GetAxis("Horizontal");
        public float Vertical => Input.GetAxis("Vertical");
        public bool IsJumpButtonDown => Input.GetButtonDown("Jump");
    }
}

