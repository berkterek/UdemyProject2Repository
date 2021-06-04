using System.Collections;
using System.Collections.Generic;
using UdemyProject2.Combats;
using UdemyProject2.Controllers;
using UdemyProject2.Managers;
using UnityEngine;

namespace UdemyProject2.Uis
{
    public class GameOverPanel : MonoBehaviour
    {
        public void YesButtonClick()
        {
            GameManager.Instance.LoadLevel1Scene();
        }

        public void NoButtonClick()
        {
            GameManager.Instance.LoadMenuAndUi(2f);
        }
    }
}

