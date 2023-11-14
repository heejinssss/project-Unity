using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ScoreCounter5 : MonoBehaviour
{
    public static ScoreCounter5 Instance { get; private set; }

    private int _score;

    public int Score
    {
        get => _score;

        set
        {
            if (_score == value) return;

            _score = value;

            scoreText.SetText($"{_score}");
        }
    }

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake() => Instance = this;
}
