using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        public GameObject curtain;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show() =>
            gameObject.SetActive(this);

        public void Hide() =>
            StartCoroutine(HideIn());

        private IEnumerator HideIn()
        {
            if (curtain != null)
                yield return new WaitForSeconds(0.1f);

            gameObject.SetActive(false);
        }
    }
}