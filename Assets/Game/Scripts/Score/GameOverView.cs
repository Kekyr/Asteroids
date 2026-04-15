using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewModel;
using Zenject;

namespace View
{
    
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _restartButton;

        private GameOverViewModel _viewModel;
        
        public Button.ButtonClickedEvent RestartButtonClicked => _restartButton.onClick;

        private void Start()
        {
            _viewModel.Score.Subscribe(x => _scoreText.text = x).AddTo(this);
        }
        
        [Inject]
        public void Construct(GameOverViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}