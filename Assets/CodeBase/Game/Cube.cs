using System;
using CodeBase.Handlers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Game
{
    public class Cube : MonoBehaviour
    {
        [Header("Game Rule")] [SerializeField] 
        private int maxCubeValue = 64;

        [Header("VFX Effects")] [SerializeField]
        private GameObject mergeVFX;

        [SerializeField] private GameObject spawnVFX;
        [SerializeField] private GameObject endGameVFX;

        [Header("Animation")] [SerializeField] 
        private AnimationClip mergeAnimation;

        [Header("Audio Clips")] [SerializeField]
        private AudioClip spawnSound;

        [SerializeField] private AudioClip mergeSound;

        private int Value { get; set; } = Multiplier;
        private const float MergeImpulseThreshold = 0.1f;
        private const int Multiplier = 2;

        private CubeMaterialHandler _materialHandler;
        private CubeValueHandler _valueHandler;
        private ScoreManager _scoreManager;
        private UIController _uiController;
        private AudioSource _audioSource;
        private Animation _animation;
        private Rigidbody _rb;
        private bool _isLaunched;

        public event Action OnCubeLaunched;

        [Obsolete("Obsolete")]
        private void Awake()
        {
            _scoreManager = FindObjectOfType<ScoreManager>();
            _uiController = FindObjectOfType<UIController>();

            _materialHandler = GetComponent<CubeMaterialHandler>();
            _valueHandler = GetComponent<CubeValueHandler>();
            _audioSource = GetComponent<AudioSource>();
            _animation = GetComponent<Animation>();
            _rb = GetComponent<Rigidbody>();

            GenerateInitialValue();
            PlaySpawnEffect();
        }

        private void Start() =>
            UpdateCube();

        private void GenerateInitialValue()
        {
            var randomValue = Random.value;
            Value = randomValue < 0.75f ? Multiplier : 4;
        }

        private void UpdateCube()
        {
            _materialHandler.SetMaterial(Value);
            _valueHandler.SetCubeValue(Value);
            _animation.Play(mergeAnimation.name);

            if (Value >= maxCubeValue)
                EndGame();
        }

        private void EndGame()
        {
            Instantiate(endGameVFX, transform.position, Quaternion.identity);
            _uiController.OnWin();
        }

        private void SetValue(int newValue)
        {
            Value = newValue;
            UpdateCube();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var otherCube = collision.gameObject.GetComponent<Cube>();

            if (otherCube == null || otherCube.Value != this.Value) return;
            var impulse = collision.relativeVelocity.magnitude * _rb.mass;

            if (impulse >= MergeImpulseThreshold)
                MergeCubes(otherCube);
        }

        private void MergeCubes(Cube otherCube)
        {
            SetValue(Value * Multiplier);
            _scoreManager?.AddScore(Value);
            PlayMergeEffect();
            Destroy(otherCube.gameObject);
        }

        private void PlaySpawnEffect()
        {
            if (spawnVFX != null)
                Instantiate(spawnVFX, transform.position, Quaternion.identity);
            if (spawnSound != null)
                _audioSource.PlayOneShot(spawnSound);
        }

        private void PlayMergeEffect()
        {
            if (mergeVFX != null)
                Instantiate(mergeVFX, transform.position, Quaternion.identity);
            if (mergeSound != null)
                _audioSource.PlayOneShot(mergeSound);
        }

        public void Launch()
        {
            if (_isLaunched) return;
            _isLaunched = true;
            OnCubeLaunched?.Invoke();
        }
    }
}