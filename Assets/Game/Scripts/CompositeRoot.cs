using System;
using PlayerBase;
using UnityEngine;

namespace Game
{
    public class CompositeRoot : MonoBehaviour
    {
        [SerializeField] private Movement _movement;
        
        private PlayerInput _input;
        
        private void Awake()
        {
            Validate();
            
            _input = new PlayerInput();
            _input.Enable();
            
            _movement.Init(_input);
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Validate()
        {
            if (_movement == null)
            {
                throw new ArgumentNullException(nameof(_movement));
            }
        }
    }
}