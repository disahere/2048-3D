using UnityEngine;

namespace CodeBase.Services.Unlocker
{
    public class FPSUnlocker : MonoBehaviour
    {
        private void Awake() =>
            DontDestroyOnLoad(gameObject);

        private void Start()
        {
#if UNITY_ANDROID
            Application.targetFrameRate = 120;
#elif UNITY_EDITOR
            QualitySettings.vSyncCount = 0;
#endif
        }
    }
}