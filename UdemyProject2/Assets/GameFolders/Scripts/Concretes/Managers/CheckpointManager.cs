using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UdemyProject2.Combats;
using UdemyProject2.Controllers;
using UnityEngine;

namespace UdemyProject2.Managers
{
    public class CheckpointManager : MonoBehaviour
    {
        CheckpointController[] _checkpointControllers;
        Health _health;

        private void Awake()
        {
            _checkpointControllers = GetComponentsInChildren<CheckpointController>();
            _health = FindObjectOfType<PlayerController>().GetComponent<Health>();
        }

        private void Start()
        {
            _health.OnHealthChanged += HandleHealthChanged;
        }

        private void HandleHealthChanged(int currentHealth, int maxHealth)
        {
            _health.transform.position = _checkpointControllers.LastOrDefault(x => x.IsPassed).transform.position;
        }
    }
}

