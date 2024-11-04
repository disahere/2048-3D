using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputController : MonoBehaviour
    {
        private IInputHandler _inputHandler;

        private void Start()
        {
#if UNITY_EDITOR
            _inputHandler = gameObject.AddComponent<StandaloneInputService>();
#elif UNITY_ANDROID
            _inputHandler = gameObject.AddComponent<MobileInputHandler>();
#endif
        }
    }
}