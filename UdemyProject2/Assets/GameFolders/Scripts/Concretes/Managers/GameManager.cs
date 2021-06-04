using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UdemyProject2.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] float delayLevelTime = 1f;
        [SerializeField] int score;

        public static GameManager Instance { get; private set; }

        public event System.Action<bool> OnSceneChanged;
        public event System.Action<int> OnScoreChanged;

        private void Awake()
        {
            SigletonThisGameObject();
        }

        private void SigletonThisGameObject()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void LoadScene(int levelIndex = 0)
        {
            StartCoroutine(LoadSceneAsync(levelIndex));
        }

        private IEnumerator LoadSceneAsync(int levelIndex)
        {
            yield return new WaitForSeconds(delayLevelTime);

            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            yield return SceneManager.UnloadSceneAsync(buildIndex);
            SceneManager.LoadSceneAsync(buildIndex + levelIndex, LoadSceneMode.Additive).completed +=
                (AsyncOperation obj) =>
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndex + levelIndex));
                };

            OnSceneChanged?.Invoke(false);
        }

        public void ExitGame()
        {
            Debug.Log("Exit Game Method Triggered");
            Application.Quit();
        }

        public void LoadMenuAndUi(float delayLoadingTime)
        {
            StartCoroutine(LoadMenuAndUiAsync(delayLoadingTime));
        }

        public void LoadLevel1()
        {
            StartCoroutine(LoadLevel1Async());
        }

        private IEnumerator LoadLevel1Async()
        {
            yield return new WaitForSeconds(delayLevelTime);

            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            yield return SceneManager.UnloadSceneAsync(buildIndex);
            SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive).completed +=
                (AsyncOperation obj) => { SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndex)); };

            OnSceneChanged?.Invoke(false);
        }

        private IEnumerator LoadMenuAndUiAsync(float delayLoadingTime)
        {
            yield return new WaitForSeconds(delayLoadingTime);
            yield return SceneManager.LoadSceneAsync("Menu");
            yield return SceneManager.LoadSceneAsync("Ui", LoadSceneMode.Additive);

            OnSceneChanged?.Invoke(true);
        }

        public void LoadLevel1Scene()
        {
            StartCoroutine(LoadLevel1SceneAsync());
        }

        private IEnumerator LoadLevel1SceneAsync()
        {
            yield return new WaitForSeconds(delayLevelTime);

            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            yield return SceneManager.UnloadSceneAsync(buildIndex);
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive).completed +=
                (AsyncOperation obj) => { SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2)); };

            OnSceneChanged?.Invoke(false);
        }

        public void IncreaseScore(int score = 0)
        {
            this.score += score;
            OnScoreChanged?.Invoke(this.score);
        }
    }
}