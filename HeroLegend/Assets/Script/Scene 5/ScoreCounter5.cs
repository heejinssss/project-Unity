using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ScoreCounter5 : MonoBehaviour
{
    public static ScoreCounter5 Instance { get; private set; }

    private int _score;

    private int itemNumber;

    public int Score
    {
        get => _score;

        set
        {
            if (_score == value) return;

            _score = value;

            scoreText.SetText($"{_score}");

            /* 새로운 3 match Item [S] */
            //if (_score >= 100)
            //{
            //    itemNumber++;
            //    ItemDatabase5.LoadItems($"Items {itemNumber+1}/");
            //    _score = 0;
            //    Board5.Instance.UpdateAllTiles();

            //    // PlayerAction5의 메서드 호출
            //    PlayerAction5.Instance.EnableSpecialAbilityFor10Seconds();
            //}
            /* 새로운 3 match Item [E] */
        }
    }

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake() => Instance = this;
}
