using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    
    public UnityEvent<int> OnScoreChanged;

    public void AddScore()
    {
        score++;
        OnScoreChanged?.Invoke(score);
        Debug.Log($"Score: {score}");
    }
}
