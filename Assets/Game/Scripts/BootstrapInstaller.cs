using Game.Save;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            Container.Bind<ISaveLoader>().To<SaveLoader>().AsSingle();
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}