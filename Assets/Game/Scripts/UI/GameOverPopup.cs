using System;
using PlayerBase;
using ScoreBase;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
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

    public void Init(Score score, Ship ship)
    {
        _score = score;
        _ship = ship;
        
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