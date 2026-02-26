using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "new GunData", menuName = "GunData/Create new GunData")]
    public class GunData : ScriptableObject
    {
        [field:SerializeField] public Bullet Prefab { get; private set; }
        [field:SerializeField] public int PoolCount{ get; private set; }
        [field:SerializeField] public float BulletSpeed { get; private set; }
    }
}