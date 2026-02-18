using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "new UfoSpawnerData", menuName = "UfoSpawnerData/Create new UfoSpawnerData")]
    public class UfoSpawnerData:ScriptableObject
    {
        [SerializeField] private int _poolCount;
        [SerializeField] private Ufo _prefab;
        [SerializeField] private float _speed;
        [SerializeField] private float _delay;
        [SerializeField] private uint _points;
        [SerializeField] private float _minPositionY;
        [SerializeField] private float _maxPositionY;
        [SerializeField] private float _positionX;
        
        public int PoolCount => _poolCount;
        public Ufo Prefab => _prefab;
        public float Speed => _speed;
        public float Delay => _delay;
        public uint Points => _points;
        public float MinPositionY => _minPositionY;
        public float MaxPositionY => _maxPositionY;
        public float PositionX => _positionX;
    }
}