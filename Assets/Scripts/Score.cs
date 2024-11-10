using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    
    public event Action OnScoreChanged;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        OnScoreChanged?.Invoke();
    }

    public void Reset()
    {
        score = 0;
        OnScoreChanged?.Invoke();
    }
}