using System.Collections;
using System.Collections.Generic;
using UdemyProject2.Managers;
using UnityEngine;

namespace UdemyProject2.Controllers
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] int score = 1;
        [SerializeField] AudioClip scoreClip;

        public static event System.Action<AudioClip> OnScorePlaySound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null)
            {
                GameManager.Instance.IncreaseScore(score);
                OnScorePlaySound.Invoke(scoreClip);
                Destroy(this.gameObject);
            }
        }
    }
}

