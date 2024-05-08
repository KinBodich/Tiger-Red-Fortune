using Common;
using Common.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mechanics
{
    public class GameInput : MonoBehaviour
    {
        [SerializeField] private float _gravityStrength = 2f;
        [SerializeField] private float _gravityRadius = 5f;
        [SerializeField] private float _gravityDuration = 2f;
        [SerializeField] private float _repelForce = 10f;
        [SerializeField] private float _gravityCooldownDuration = 5f;
        [SerializeField] private float _lineCooldownDuration = 3f;

        [Space(10)]
        [SerializeField] private GameObject _singularityPrefab;
        [SerializeField] private GameObject _linePrefab;

        private Vector2 _touchStartPosition;
        private bool _isDrawingLine = false;
        private float _gravityCooldownTimer = 0f;
        private float _lineCooldownTimer = 0f;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (_gameManager.GameState != GameState.Playing) return;
            UpdateCooldowns();
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _touchStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _isDrawingLine = false;
            }

            if (Input.GetMouseButton(0) && !_isDrawingLine)
            {
                Vector2 currentTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (currentTouchPosition != _touchStartPosition)
                {
                    _isDrawingLine = true;
                }
            }

            if (!_isDrawingLine && Input.GetMouseButtonUp(0) && _gravityCooldownTimer <= 0f)
            {
                _isDrawingLine = false;
                CreateGravityZone(_touchStartPosition);
                _gravityCooldownTimer = _gravityCooldownDuration;
            }

            if (_isDrawingLine && Input.GetMouseButtonUp(0) && _lineCooldownTimer <= 0f)
            {
                Vector2 dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CreateRepelLine(_touchStartPosition ,dragPosition);
                _lineCooldownTimer = _lineCooldownDuration;
            }
        }

        private void UpdateCooldowns()
        {
            if (_gravityCooldownTimer > 0f)
            {
                _gravityCooldownTimer -= Time.deltaTime;
            }

            if (_lineCooldownTimer > 0f)
            {
                _lineCooldownTimer -= Time.deltaTime;
            }
        }

        private void CreateGravityZone(Vector2 position)
        {
            StartCoroutine(ApplyGravity(_gravityDuration, position));
        }

        private void CreateRepelLine(Vector2 startPosition, Vector2 endPosition)
        {
            Vector2 direction = (endPosition - startPosition).normalized;
            StartCoroutine(ApplyLine(_gravityDuration, direction, startPosition, endPosition));
        }

        private IEnumerator ApplyGravity(float duration, Vector2 gravityPosition)
        {
            var spawnedPrefab = Instantiate(_singularityPrefab, gravityPosition, Quaternion.identity);

            spawnedPrefab.TryGetComponent(out Singularity singularity);
            StartCoroutine(singularity.ScalePrefab(1, .5f));

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                var colliders = Physics2D.OverlapCircleAll(gravityPosition, _gravityRadius);

                foreach (Collider2D collider in colliders)
                {
                    Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 gravityDirection = (gravityPosition - rb.position).normalized;
                        rb.AddForce(gravityDirection * _gravityStrength, ForceMode2D.Force);
                    }
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return StartCoroutine(singularity.ScalePrefab(0, .5f));

            Destroy(spawnedPrefab);
        }

        private IEnumerator ApplyLine(float duration, Vector2 direction, Vector2 startPosition, Vector2 endPosition)
        {
            float elapsedTime = 0f;

            var line = Instantiate(_linePrefab, Vector2.zero, Quaternion.identity);
            var lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);

            while (elapsedTime < duration)
            {
                Collider2D[] colliders = Physics2D.OverlapAreaAll(startPosition, endPosition);

                

                foreach (Collider2D collider in colliders)
                {
                    Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.AddForce(direction * _repelForce, ForceMode2D.Impulse);
                    }
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Destroy(line);
        }
    }
}