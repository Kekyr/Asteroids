using UnityEngine;
using TMPro;
using R3;
using Zenject;

namespace Player
{
    public class LaserGunView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _shootCountText;
        [SerializeField] private TextMeshProUGUI _coolDownText;

        private LaserGunViewModel _viewModel;

        private void Start()
        {
            _viewModel.ShootCount.Subscribe(x => _shootCountText.text = x).AddTo(this);
            _viewModel.CoolDownTime.Subscribe(x => _coolDownText.text = x).AddTo(this);
        }

        [Inject]
        public void Construct(LaserGunViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}