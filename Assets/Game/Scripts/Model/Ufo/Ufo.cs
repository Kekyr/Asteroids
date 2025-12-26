namespace Model
{
    public class Ufo
    {
        public Transformable Transform { get; private set; }
        public float Speed { get; private set; }

        public Ufo(float speed)
        {
            Speed = speed;
            Transform = new Transformable();
        }
    }
}