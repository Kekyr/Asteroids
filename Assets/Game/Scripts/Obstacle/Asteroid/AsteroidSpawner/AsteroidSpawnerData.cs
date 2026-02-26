using UnityEngine;

namespace Obstacle
{
    [CreateAssetMenu(fileName = "new AsteroidSpawnerData", menuName = "AsteroidSpawnerData/Create new AsteroidSpawnerData")]
    public class AsteroidSpawnerData : ScriptableObject
    {
        [field: SerializeField] public int PoolCount { get; private set; }
        [field: SerializeField] public Asteroid Prefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public uint Points { get; private set; }
        [field: SerializeField] public float MinPositionY { get; private set; }
        [field: SerializeField] public float MaxPositionY { get; private set; }
        [field: SerializeField] public float MinPositionX { get; private set; }
        [field: SerializeField] public float MaxPositionX { get; private set; }
    }
}