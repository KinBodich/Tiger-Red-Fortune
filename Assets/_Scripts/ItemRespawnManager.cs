using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Managers
{
    public class ItemRespawnManager : BaseSingleton<ItemRespawnManager>
    {
        [SerializeField] private List<GameObject> _items;

        private GameManager _gameManager;
        private int _itemsRandCount;
        private int _itemsMaxRandCount = 3;
        private Vector2 _randomSpawnPosition;
        [SerializeField] private float _gravityScale = .01f;
        private float _respawnRate = 2f;

        public float CameraTopY { get; private set; }
        public float CameraRight { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _gameManager = GameManager.Instance;
        }

        private void Start()
        {
            Camera mainCamera = Camera.main;

            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            CameraTopY = transform.position.y + cameraHeight / 2f;
            CameraRight = (transform.position.x + cameraWidth / 2f) - .5f;

            StartCoroutine(ItemRespawm());
            StartCoroutine(IncreaseCount());
            StartCoroutine(IncreaseSpeed());
        }

        private IEnumerator ItemRespawm()
        {
            while (_gameManager.GameState == GameState.Playing)
            {
                _itemsRandCount = Random.Range(1, _itemsMaxRandCount);

                for (int i = 0; i < _itemsRandCount; i++)
                {
                    _randomSpawnPosition = new Vector2(Random.Range(-CameraRight, CameraRight), CameraTopY);
                    var spawnedObject = Instantiate(_items[Random.Range(0, _items.Count)], _randomSpawnPosition, Quaternion.identity);
                    var spawnedObjectRb = spawnedObject.GetComponent<Rigidbody2D>();
                    spawnedObjectRb.gravityScale = _gravityScale;
                }

                yield return new WaitForSeconds(_respawnRate);
            }
        }

        private IEnumerator IncreaseCount()
        {
            while (_gameManager.GameState == GameState.Playing)
            {
                yield return new WaitForSeconds(10);

                //_itemsMaxRandCount += 1;
                if (_respawnRate > .8f)
                    _respawnRate -= .1f;
            }
        }

        private IEnumerator IncreaseSpeed()
        {
            while (_gameManager.GameState == GameState.Playing)
            {
                yield return new WaitForSeconds(10);

                _gravityScale += .01f;

            }
        }
    }
}