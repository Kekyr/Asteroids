using Game;
using Player;
using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace View
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _restartButton;

        private Score _score;
        private Ship _ship;
        private SceneLoader _sceneLoader;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnClick);
        }

        public void Construct(Score score, Ship shipPresenter, SceneLoader sceneLoader)
        {
            _score = score;
            _ship = shipPresenter;
            _sceneLoader = sceneLoader;
            
            _ship.OnCollisionEnter2DAsObservable().Subscribe(OnExploded).AddTo(_ship);
        }

        private void OnExploded(Collision2D collision)
        {
            _scoreText.text = $"Score: {_score.NumberOfPoints}";
            gameObject.SetActive(true);
        }

        private void OnClick()
        {
            _sceneLoader.ReloadScene();
        }
    }
}