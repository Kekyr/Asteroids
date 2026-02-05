using Game;
using Player;
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

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnClick);
            _ship.Exploded -= OnExploded;
        }

        public void Init(Score score, Ship shipPresenter)
        {
            _score = score;
            _ship = shipPresenter;

            _ship.Exploded += OnExploded;
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