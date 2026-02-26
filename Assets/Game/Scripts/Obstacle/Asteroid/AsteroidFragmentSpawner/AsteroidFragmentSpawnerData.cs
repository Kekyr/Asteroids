using UnityEngine;

namespace Obstacle
{
    [CreateAssetMenu(fileName = "new AsteroidSpawnerData",
        menuName = "AsteroidFragmentSpawnerData/Create new AsteroidFragmentSpawnerData")]
    public class AsteroidFragmentSpawnerData : ScriptableObject
    {
        [field: SerializeField] public int PoolCount { get; private set; }
        [field: SerializeField] public Asteroid Prefab { get; private set; }
        [field: SerializeField] public int ExplodeCount { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public uint Points { get; private set; }
        [field: SerializeField] public float PositionXOffset { get; private set; }
        [field: SerializeField] public float PositionYOffset { get; private set; }
    }
}