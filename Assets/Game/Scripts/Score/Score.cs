using R3;

namespace Game
{
    public class Score
    {
        public ReactiveProperty<uint> HighScore { get; } = new ReactiveProperty<uint>();
        public ReactiveProperty<uint> CurrentScore { get; } = new ReactiveProperty<uint>();

        public void Initialize(uint highScore)
        {
            CurrentScore.Value = 0;
            HighScore.Value = highScore;
        }

        public void Add(uint points)
        {
            CurrentScore.Value += points;
        }
    }
}