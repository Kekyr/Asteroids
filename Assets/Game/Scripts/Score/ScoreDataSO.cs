using UnityEngine;

namespace ScoreBase
{
    [CreateAssetMenu(fileName = "new ScoreDataSO", menuName = "ScoreDataSO/Create new ScoreDataSO")]
    public class ScoreDataSO : ScriptableObject
    {
        [SerializeField] private uint _points;

        public uint Points => _points;
    }
}