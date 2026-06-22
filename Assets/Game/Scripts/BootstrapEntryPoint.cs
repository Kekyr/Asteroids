using AssetLoader;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class BootstrapEntryPoint : IInitializable
    {
        private LoadedAssets _loadedAssets;
        
        public BootstrapEntryPoint(LoadedAssets loadedAssets)
        {
            _loadedAssets = loadedAssets;
        }
        
        public void Initialize()
        {
            _loadedAssets.LoadAssets();
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}