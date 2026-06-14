namespace Game.Analytics
{
    public interface IAnalytics
    {
        public void Initialize();
        public void LogGameStart();
        public void LogGameEnd(int gunShotCount, int laserShotCount, int enemiesDestroyedCount);
        public void LogLaserShot();
    }
}