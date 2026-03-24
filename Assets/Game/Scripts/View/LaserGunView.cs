using UnityEngine;
using TMPro;
using R3;
using ViewModel;
using Zenject;

namespace View
{
    public class LaserGunView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _shootCountText;
        [SerializeField] private TextMeshProUGUI _coolDownText;

        [Inject]
        public void Construct(LaserGunViewModel viewModel)
        {
            viewModel.ShootCount.Subscribe(x => _shootCountText.text = x).AddTo(this);
            viewModel.CoolDownTime.Subscribe(x => _coolDownText.text = x).AddTo(this);
        }
    }
}