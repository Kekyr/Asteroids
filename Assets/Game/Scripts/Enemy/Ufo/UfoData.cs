using Game;

namespace Enemy
{
    public class UfoData
    {
        public Transformable Transform { get; private set; }
        public float Speed { get; private set; }
        
        public UfoData(float speed)
        {
            Speed = speed;
            Transform = new Transformable();
        }
    }
}