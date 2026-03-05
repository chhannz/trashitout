using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public UnityEvent<int> OnScoreChanged;

    [SerializeField] private float gameDuration = 60f;
    private float timeRemaining;
    private bool isGameOver = false;
    
    public UnityEvent<int> OnTimeChanged;
    public UnityEvent OnGameOver;

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
        score++;
        OnScoreChanged?.Invoke(score);
        Debug.Log($"Score: {score}");
    }

    private void EndGame()
    {
        isGameOver = true;
        timeRemaining = 0;
        OnTimeChanged?.Invoke(0);
        Debug.Log($"Waktu habis ! Skor akhir : {score}");
        OnGameOver?.Invoke();
    }
}
