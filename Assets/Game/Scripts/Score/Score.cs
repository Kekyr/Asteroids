using UnityEngine;

namespace ScoreBase
{
    public class Score : MonoBehaviour
    {
        private uint _numberOfPoints;

        public uint NumberOfPoints => _numberOfPoints;

        public void Add(uint points)
        {
            _numberOfPoints += points;
        }
    }
}
