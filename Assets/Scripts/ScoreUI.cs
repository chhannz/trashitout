using System;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI TextScore;

    private void Awake()
    {
        TextScore = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore(int score)
    {
        TextScore.text = $"SCORE {score}";
    }
}
