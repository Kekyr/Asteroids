using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneLoader
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}