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

   
}
