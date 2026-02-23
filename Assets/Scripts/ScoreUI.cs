using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore(int newScore)
    {
        _textMesh.text = $"Score : {newScore}";
    }
}
