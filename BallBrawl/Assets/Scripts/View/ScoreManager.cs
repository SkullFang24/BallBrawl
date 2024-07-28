using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreTextMesh;
    public TextMeshProUGUI scoreTextMesh;

    private int _score;
    private int _highestScore;

    private void Start()
    {
        _highestScore = PlayerPrefs.GetInt("sc", 0);
        UpdateHighScoreText();
        _score = 0;
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        _score += points;
        UpdateScoreText();
        UpdateHighScore(_score); 
    }

    private void UpdateScoreText()
    {
        scoreTextMesh.text = " " + _score.ToString();
    }

    public void UpdateHighScore(int newScore)
    {
        if (newScore > _highestScore)
        {
            _highestScore = newScore;
            SaveHighestScore();
            UpdateHighScoreText();
        }
    }

    private void UpdateHighScoreText()
    {
        highScoreTextMesh.text = "HighScore: " + _highestScore.ToString();
    }

    private void SaveHighestScore()
    {
        PlayerPrefs.SetInt("sc", _highestScore);
    }
}
