using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Model;
using Presenter;

namespace View
{
    public class GameOverView:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _restartButton;

        private Score _score;
        private ShipPresenter _shipPresenter;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnClick);
            _shipPresenter.Exploded -= OnExploded;
        }

        public void Init(Score score, ShipPresenter shipPresenter)
        {
            _score = score;
            _shipPresenter = shipPresenter;
        
            _shipPresenter.Exploded += OnExploded;
        }

        private void OnExploded()
        {
            _scoreText.text = $"Score: {_score.NumberOfPoints}";
            gameObject.SetActive(true);
        }

        private void OnClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}