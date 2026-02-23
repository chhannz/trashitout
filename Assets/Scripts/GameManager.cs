using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentScore = 0;

    public UnityEvent<int> OnScoreChanged;
    public void AddScore()
    {
        currentScore++;
        OnScoreChanged?.Invoke(currentScore);
        //Debug.Log($"[GameManager] Score Updated: {currentScore}");
        Debug.Log("Score = "+ currentScore);
    }
}
