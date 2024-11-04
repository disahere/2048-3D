using System.Collections;
using UnityEngine;

namespace CodeBase.Game
{
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] private UIController uiController;

        private const string TargetTag = "Cube";
        private const float Deley = 2f;

        private Coroutine _destroyCoroutine;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(TargetTag)) return;
            _destroyCoroutine = StartCoroutine(CheckBeforeDestroy(other));
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag(TargetTag)) return;
            if (_destroyCoroutine == null) return;
            StopCoroutine(_destroyCoroutine);
            _destroyCoroutine = null;
        }

        private IEnumerator CheckBeforeDestroy(Collider other)
        {
            yield return new WaitForSeconds(Deley);
            if (!other || !other.gameObject.CompareTag(TargetTag)) yield break;
            Destroy(other.gameObject);
            uiController.OnLose();
        }
    }
}