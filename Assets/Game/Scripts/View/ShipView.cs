using UnityEngine;
using TMPro;
using R3;
using ViewModel;

namespace View
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _positionTextMesh;
        [SerializeField] private TextMeshProUGUI _angleTextMesh;
        [SerializeField] private TextMeshProUGUI _velocityTextMesh;

        public void Construct(ShipViewModel viewModel)
        {
            viewModel.Position.Subscribe(x => _positionTextMesh.text = x).AddTo(this);
            viewModel.Rotation.Subscribe(x => _angleTextMesh.text = x).AddTo(this);
            viewModel.Velocity.Subscribe(x => _velocityTextMesh.text = x).AddTo(this);
        }
    }
}