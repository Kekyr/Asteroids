using System;
using UnityEngine;

namespace PlayerBase
{
    public class Ship: MonoBehaviour
    {
        public event Action Exploded;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            gameObject.SetActive(false);
            Exploded?.Invoke();
        }
    }
}
