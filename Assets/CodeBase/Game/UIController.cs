using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Game
{
    public class UIController : MonoBehaviour
    {
        private const float Deley = 4f;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        private static IEnumerator AfterGameState()
        {
            yield return new WaitForSeconds(Deley);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnWin()
        {
            winPanel.SetActive(true);
            StartCoroutine(AfterGameState());
        }

        public void OnLose()
        {
            losePanel.SetActive(true);
            StartCoroutine(AfterGameState());
        }
    }
}