using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Game
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject cubePrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float spawnDelay = 0.5f;

        private Cube _currentCube;
        public event Action<Cube> OnCubeSpawned;

        private void Start() =>
            SpawnNewCube();

        private void SpawnNewCube() =>
            StartCoroutine(SpawnCubeWithDelay());

        private IEnumerator SpawnCubeWithDelay()
        {
            yield return new WaitForSeconds(spawnDelay);

            var cubeObject = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);
            _currentCube = cubeObject.GetComponent<Cube>();

            if (_currentCube == null) yield break;
            _currentCube.OnCubeLaunched += HandleCubeLaunched;
            OnCubeSpawned?.Invoke(_currentCube);
        }

        private void HandleCubeLaunched()
        {
            _currentCube.OnCubeLaunched -= HandleCubeLaunched;
            SpawnNewCube();
        }

        public Cube GetCurrentCube() =>
            _currentCube;
    }
}