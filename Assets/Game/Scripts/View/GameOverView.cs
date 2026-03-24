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
        
        public Button.ButtonClickedEvent RestartButtonClicked => _restartButton.onClick;
        
        [Inject]
        public void Construct(GameOverViewModel viewModel)
        {
            viewModel.Score.Subscribe(x => _scoreText.text = x).AddTo(this);
        }
    }
}