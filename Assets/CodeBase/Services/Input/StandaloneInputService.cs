using System;
using CodeBase.Game;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public class StandaloneInputService : MonoBehaviour, IInputHandler
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

            if (!UnityEngine.Input.GetMouseButtonDown(0) || _isLaunched)
            {
                if (UnityEngine.Input.GetMouseButton(0) && _isDragging)
                    OnTouchMove(UnityEngine.Input.mousePosition);
                else if (UnityEngine.Input.GetMouseButtonUp(0) && _isDragging)
                {
                    _isDragging = false;
                    OnTouchEnd(UnityEngine.Input.mousePosition);
                }
            }
            else
            {
                _isDragging = true;
                _isTouchStarted = true;
                OnTouchBegin(UnityEngine.Input.mousePosition);
            }

            if (!_isLaunched && _isTouchStarted) 
                _cube.transform.position = Vector3.Lerp(_cube.transform.position, _targetPosition, Time.deltaTime * MoveSpeed);
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
