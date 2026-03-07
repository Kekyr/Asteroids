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
        
        public Button.ButtonClickedEvent RestartButtonClicked => _restartButton.onClick;
        
        public void Construct(GameOverViewModel viewModel, Ship ship)
        {
            viewModel.Score.Subscribe(x => _scoreText.text = x).AddTo(this);
            ship.OnCollisionEnter2DAsObservable().Subscribe(x => gameObject.SetActive(true)).AddTo(this);
        }
    }
}