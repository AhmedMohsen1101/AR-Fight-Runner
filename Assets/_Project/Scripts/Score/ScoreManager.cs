using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private const string highScoreKey = "HighScore";
    public int score;
    public int HighScore;

    public UnityAction<int> OnScoreChanged;
    public UnityAction<int> OnHighScoreChanged;
    private void Awake()
    {
         Instance = this;
    }
    private void OnEnable()
    { 
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            AddHighScore();
        }
    }
    private void OnDisable()
    {
        if (score > HighScore)
            PlayerPrefs.SetInt(highScoreKey, score);
    }
    public void AddScore(int score)
    {
        this.score += score;
        OnScoreChanged?.Invoke(this.score);
    }

    public void AddHighScore()
    {
        HighScore = PlayerPrefs.GetInt(highScoreKey);
        OnHighScoreChanged?.Invoke(HighScore);
    }

    public int GetHighScore()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            return PlayerPrefs.GetInt(highScoreKey);
        }
        return 0;
    }
    public int GetScore()
    {
        return score;
    }
}
