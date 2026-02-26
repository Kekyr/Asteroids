using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "new LaserGunData", menuName = "LaserGunData/Create new LaserGunData")]
    public class LaserGunData : ScriptableObject
    {
        [field: SerializeField] public int MaxShootCount { get; private set; }
        [field: SerializeField] public float CoolDownDuration { get; private set; }
        [field: SerializeField] public float ShootDuration { get; private set; }
        [field: SerializeField] public float RestoreDuration { get; private set; }
    }
}