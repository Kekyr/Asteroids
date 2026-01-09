using UnityEngine;

namespace Game
{
    public class Helper
    {
        private Vector2 _minPosition;
        private Vector2 _maxPosition;
        private Camera _camera;

        public Helper()
        {
            _camera = Camera.main;
            _minPosition = _camera.ScreenToWorldPoint(new Vector2(0, 0));
            _maxPosition = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        }
        
        public Vector2 ClampPosition(Vector2 position)
        {
            Vector2 clampedPosition = position;

            clampedPosition.x = ClampValue(clampedPosition.x, _minPosition.x, _maxPosition.x);
            clampedPosition.y = ClampValue(clampedPosition.y, _minPosition.y, _maxPosition.y);

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
            bool positionXValidation = CheckValue(position.x, _minPosition.x, _maxPosition.x);
            bool positionYValidation = CheckValue(position.y, _minPosition.y, _maxPosition.y);

            return positionXValidation && positionYValidation;
        }

        private bool CheckValue(float value, float minValue, float maxValue)
        {
            return value >= minValue && value <= maxValue;
        }
    }
}