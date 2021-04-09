using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _ballsLeft = 3;

    [SerializeField] 
    private Text _ballText;

    private int _score;
    
    [SerializeField]
    private Text _scoreText;

    [SerializeField] 
    private Text _endGameText;
    // Start is called before the first frame update
    void Start()
    {
        _ballText.text = "Balls: " + _ballsLeft + "/3";
        _scoreText.text = "Score: " + _score;
    }

    public void BallCount(int _ball)
    {
        _ballsLeft += _ball;
        _ballText.text = "Balls: " + _ballsLeft + "/3";
    }

    public void AddScore(int _points)
    {
        _score += _points;
        _scoreText.text = "Score: " + _score;
    }

    public void showEndGame(int maxScore)
    {
        Destroy(_ballText);
        Destroy(_scoreText);
        _endGameText.text = "Game over! \n You reached " + _score + " / " + maxScore + " points";
    }
}
