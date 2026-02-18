using UnityEngine;

namespace Obstacle
{
    [CreateAssetMenu(fileName = "new AsteroidSpawnerData",
        menuName = "AsteroidFragmentSpawnerData/Create new AsteroidFragmentSpawnerData")]
    public class AsteroidFragmentSpawnerData : ScriptableObject
    {
        [SerializeField] private int _poolCount;
        [SerializeField] private Asteroid _prefab;
        [SerializeField] private int _explodeCount;
        [SerializeField] private float _speed;
        [SerializeField] private uint _points;
        [SerializeField] private float _positionXOffset;
        [SerializeField] private float _positionYOffset;
        
        public int PoolCount => _poolCount;
        public Asteroid Prefab => _prefab;
        public int ExplodeCount => _explodeCount;
        public float Speed => _speed;
        public uint Points => _points;
        public float PositionXOffset => _positionXOffset;
        public float PositionYOffset => _positionYOffset;
    }
}