using Analytics;
using Zenject;

namespace Game
{
    public class GameplayEntryPoint : IInitializable
    {
        private IAnalytics _analytics;

        public GameplayEntryPoint(IAnalytics analytics)
        {
            _analytics = analytics;
        }

        public void Initialize()
        {
            _analytics.LogGameStart();
        }
    }
}