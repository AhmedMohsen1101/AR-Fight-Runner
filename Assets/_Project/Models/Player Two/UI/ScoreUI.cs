using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text HighScoreText;

    private void OnEnable()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged += UpdateHighScore;
        
        scoreText.text = "0";
        scoreText.text = ScoreManager.Instance.GetHighScore().ToString();
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged -= UpdateHighScore;
    }
    private void UpdateScore(int value)
    {
        scoreText.text = value.ToString();

        LeanTween.scale(scoreText.gameObject, Vector3.one * 1.5f, 1f).setEasePunch();
    }
    public void UpdateHighScore(int value)
    {
        HighScoreText.text = value.ToString();
    }
}
