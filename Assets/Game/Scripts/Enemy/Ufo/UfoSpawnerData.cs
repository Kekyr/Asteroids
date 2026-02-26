using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "new UfoSpawnerData", menuName = "UfoSpawnerData/Create new UfoSpawnerData")]
    public class UfoSpawnerData : ScriptableObject
    {
        [field: SerializeField] public int PoolCount { get; private set; }
        [field: SerializeField] public Ufo Prefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public uint Points { get; private set; }
        [field: SerializeField] public float MinPositionY { get; private set; }
        [field: SerializeField] public float MaxPositionY { get; private set; }
        [field: SerializeField] public float PositionX { get; private set; }
    }
}