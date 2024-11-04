using System;
using CodeBase.Game;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public class MobileInputHandler : MonoBehaviour, IInputHandler
    {
        private const float MoveSpeed = 5f;
        private const float LaunchForce = 800f;

        private Cube _cube;
        private CubeSpawner _spawner;
        private Rigidbody _cubeRb;
        private Vector3 _targetPosition;
        private Vector2 _initialTouchPosition;

        private bool _isDragging;
        private bool _isLaunched;
        private bool _isTouchStarted;

        [Obsolete("Obsolete")]
        private void Start()
        {
            _spawner = FindObjectOfType<CubeSpawner>();

            if (_spawner == null) return;
            _spawner.OnCubeSpawned += SetCurrentCube;

            var initialCube = _spawner.GetCurrentCube();
            if (initialCube != null)
                SetCurrentCube(initialCube);
        }

        private void Update()
        {
            if (_cube == null) return;

            if (UnityEngine.Input.touchCount > 0)
            {
                var touch = UnityEngine.Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && !_isLaunched)
                {
                    _isDragging = true;
                    _isTouchStarted = true;
                    OnTouchBegin(touch.position);
                }
                else if (touch.phase == TouchPhase.Moved && _isDragging)
                {
                    OnTouchMove(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended && _isDragging)
                {
                    _isDragging = false;
                    OnTouchEnd(touch.position);
                }
            }

            if (!_isLaunched && _isTouchStarted)
            {
                _cube.transform.position = Vector3.Lerp(_cube.transform.position, _targetPosition, Time.deltaTime * MoveSpeed);
            }
        }

        private void SetCurrentCube(Cube cube)
        {
            _cube = cube;
            _cubeRb = _cube.GetComponent<Rigidbody>();
            _targetPosition = _cube.transform.position;
            _isLaunched = false;
            _isTouchStarted = false;
        }

        public void OnTouchBegin(Vector2 position)
        {
            _initialTouchPosition = position;
            _cubeRb.useGravity = false;
            _cubeRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }

        public void OnTouchMove(Vector2 position)
        {
            if (!_isTouchStarted || _cube == null) return;

            float deltaX = (position.x - _initialTouchPosition.x) * 0.01f;
            _targetPosition = _cube.transform.position + new Vector3(deltaX, 0, 0);
            _targetPosition.x = Mathf.Clamp(_targetPosition.x, -2f, 2f);
        }

        public void OnTouchEnd(Vector2 position)
        {
            if (_cube == null) return;

            _cube.Launch();
            _cubeRb.useGravity = true;
            _cubeRb.constraints = RigidbodyConstraints.None;
            _cubeRb.AddForce(Vector3.forward * LaunchForce);
            _isLaunched = true;
            _isTouchStarted = false;
        }
    }
}
