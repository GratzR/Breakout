using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _ballsLeft = 3;
    private int _maxPoints;

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
        _ballText.text = "Balls: " + _ballsLeft;
        _scoreText.text = "Score: " + _score;
    }

    public void BallCount(int _ball)
    {
        _ballsLeft += _ball;
        _ballText.text = "Balls: " + _ballsLeft;
    }

    public void AddScore(int _points)
    {
        _score += _points;
        _scoreText.text = "Score: " + _score;

        if (_score == _maxPoints)
        {
            // GameObject.FindWithTag("Player").GetComponent<Player>().LoseBall(_ballsLeft);
            showEndGame();
        }
    }

    public void showEndGame()
    {
        if (_score == _maxPoints)
        {
            _endGameText.text = "You won! \n You reached " + _score + " / " + _maxPoints + " points";
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            Ball ball = GameObject.FindWithTag("Ball").GetComponent<Ball>();
            
            Destroy(player);
            Destroy(ball);
        }
        else
        {
            _endGameText.text = "Game over! \n You reached " + _score + " / " + _maxPoints + " points";
        }
        Destroy(_ballText);
        Destroy(_scoreText);
    }

    public void setMaxPoints(int maxPoints)
    {
        _maxPoints = maxPoints;
    }
}
