using System.Collections;
using System.Collections.Generic;
using UdemyProject2.Managers;
using UnityEngine;

namespace UdemyProject2.Uis
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] MenuPanel menuPanel;

        private void OnEnable()
        {
            GameManager.Instance.OnSceneChanged += HandleSceneChanged;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnSceneChanged -= HandleSceneChanged;
        }

        private void HandleSceneChanged(bool isActive)
        {
            if (isActive == menuPanel.gameObject.activeSelf) return;

            menuPanel.gameObject.SetActive(isActive);
        }
    }
}

