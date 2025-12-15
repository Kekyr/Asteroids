using UnityEngine;

namespace Game
{
    public class Helper : MonoBehaviour
    {
        [SerializeField] private Vector2 _minPosition;
        [SerializeField] private Vector2 _maxPosition;

        public Vector2 CheckPosition(Vector2 position)
        {
            Vector2 checkedPosition = position;

            checkedPosition.x = CheckValue(checkedPosition.x, _minPosition.x, _maxPosition.x);
            checkedPosition.y = CheckValue(checkedPosition.y, _minPosition.y, _maxPosition.y);

            return checkedPosition;
        }

        private float CheckValue(float value, float minValue, float maxValue)
        {
            if (value > maxValue)
            {
                return minValue;
            }

            if (value < minValue)
            {
                return maxValue;
            }

            return value;
        }
    }
}