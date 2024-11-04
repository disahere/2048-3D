using System.Collections;
using TMPro;
using UnityEngine;

namespace CodeBase.Game
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private float scaleDuration = 0.1f;
        [SerializeField] private float targetScale = 1.5f;
        
        private Vector3 _originalScale;
        private int _score;
        
        private void Start()
        {
            _originalScale = scoreText.transform.localScale;
            UpdateScoreUI();    
        }

        private void UpdateScoreUI()
        {
            scoreText.text = _score.ToString();   
            StartCoroutine(AnimateScoreText());
        }

        private IEnumerator AnimateScoreText()
        {
            scoreText.transform.localScale = _originalScale * targetScale;

            var elapsedTime = 0f;
            while (elapsedTime < scaleDuration)
            {
                scoreText.transform.localScale = Vector3.Lerp(scoreText.transform.localScale, _originalScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        
            scoreText.transform.localScale = _originalScale;
        }

        public void AddScore(int points)
        {
            _score += points;
            UpdateScoreUI();
        }
    }
}