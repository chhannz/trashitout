using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentScore = 0;

    public UnityEvent<int> OnScoreChanged;

    [SerializeField] private float gameDuration = 60f;
    private float timeRemaining;
    private bool isGameOver = false;
    [SerializeField]private UnityEvent<int> OnTimeChanged;
    [SerializeField]private UnityEvent OnGameOver;

    private void Start()
    {
        timeRemaining = gameDuration;
        OnTimeChanged?.Invoke(Mathf.CeilToInt(timeRemaining));
    }

    private void Update()
    {
        if (isGameOver) return;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            OnTimeChanged?.Invoke(Mathf.CeilToInt(timeRemaining));
        }
        else
        {
            EndGame();
        }
    }
    
    public void AddScore()
    {
        currentScore++;
        OnScoreChanged?.Invoke(currentScore);
        //Debug.Log($"[GameManager] Score Updated: {currentScore}");
        Debug.Log("Score = "+ currentScore);
    }

    private void EndGame()
    {
        isGameOver = true;
        timeRemaining = 0;
        OnTimeChanged?.Invoke(0);
        Debug.Log("Game Over");
        OnGameOver?.Invoke();
    }
}
