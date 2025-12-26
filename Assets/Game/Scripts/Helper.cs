using UnityEngine;

namespace Game
{
    public class Helper
    {
        public Vector2 MinPosition { get; private set; } = new Vector2(-1, 0);
        public Vector2 MaxPosition { get; private set; } = new Vector2(11, 12);

        public Vector2 ClampPosition(Vector2 position)
        {
            Vector2 clampedPosition = position;

            clampedPosition.x = ClampValue(clampedPosition.x, MinPosition.x, MaxPosition.x);
            clampedPosition.y = ClampValue(clampedPosition.y, MinPosition.y, MaxPosition.y);

            return clampedPosition;
        }

        private float ClampValue(float value, float minValue, float maxValue)
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

        public bool CheckPosition(Vector2 position)
        {
            bool positionXValidation = CheckValue(position.x, MinPosition.x, MaxPosition.x);
            bool positionYValidation = CheckValue(position.y, MinPosition.y, MaxPosition.y);

            return positionXValidation && positionYValidation;
        }

        private bool CheckValue(float value, float minValue, float maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                return false;
            }

            return true;
        }
    }
}