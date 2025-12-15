using System.Collections;
using Game;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace AsteroidBase
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _minPositionX;
        [SerializeField] private float _maxPositionX;
        [SerializeField] private float _positionY;

        private Helper _helper;
        private ObjectPool _pool;
        private WaitForSeconds _waitDelay;

        private void OnEnable()
        {
            _pool = GetComponent<ObjectPool>();
            _waitDelay = new WaitForSeconds(_delay);
        }

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        public void Init(Helper helper)
        {
            _helper = helper;
            enabled = true;
        }

        private IEnumerator Spawn()
        {
            while (isActiveAndEnabled)
            {
                Vector2 randomPosition;
                Vector2 randomDirection;

                float randomXPosition = Random.Range(_minPositionX, _maxPositionX);
                randomPosition = new Vector2(randomXPosition, _positionY);

                GameObject instance = _pool.Spawn(randomPosition);
                AsteroidMovement asteroidMovement = instance.GetComponent<AsteroidMovement>();

                float randomXDirection = Random.Range(Vector2.left.x, Vector2.right.x);
                float randomYDirection = Random.Range(Vector2.down.y, Vector2.up.y);

                randomDirection = new Vector2(randomXDirection, randomYDirection);

                asteroidMovement.Init(_helper, randomDirection);

                yield return _waitDelay;
            }
        }
    }
}