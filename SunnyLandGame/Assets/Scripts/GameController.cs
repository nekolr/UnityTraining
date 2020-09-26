
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static int _totalScore;

    public Text scoreText;

    public static GameController Instance;

    private void Start()
    {
        Instance = this;
    }

    public void AddScore(int score)
    {
        _totalScore += score;
        scoreText.text = _totalScore.ToString();
    }
}