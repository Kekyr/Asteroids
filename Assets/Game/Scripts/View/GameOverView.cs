using Game;
using Player;
using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewModel;

namespace View
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _restartButton;
        
        private SceneLoader _sceneLoader;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnClick);
        }

        public void Construct(GameOverViewModel viewModel, Ship ship, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            viewModel.Score.Subscribe(x => _scoreText.text = x).AddTo(this);
            ship.OnCollisionEnter2DAsObservable().Subscribe(x => gameObject.SetActive(true)).AddTo(ship);
        }

        private void OnClick()
        {
            _sceneLoader.ReloadScene();
        }
    }
}